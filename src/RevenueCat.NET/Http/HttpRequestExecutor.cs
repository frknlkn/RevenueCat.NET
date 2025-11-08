using System.Net;
using System.Text;
using System.Text.Json;
using RevenueCat.NET.Configuration;
using RevenueCat.NET.Exceptions;
using RevenueCat.NET.Models;

namespace RevenueCat.NET;

internal sealed class HttpRequestExecutor(HttpClient httpClient, RevenueCatOptions options) : IHttpRequestExecutor
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
        DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull,
        WriteIndented = false
    };

    public async Task<TResponse> ExecuteAsync<TResponse>(
        HttpMethod method,
        string endpoint,
        object? body = null,
        CancellationToken cancellationToken = default)
    {
        var attempt = 0;
        Exception? lastException = null;

        while (attempt < options.MaxRetryAttempts)
        {
            try
            {
                using var request = CreateRequest(method, endpoint, body);
                using var response = await httpClient.SendAsync(request, cancellationToken).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
                    return JsonSerializer.Deserialize<TResponse>(content, JsonOptions)!;
                }

                if (response.StatusCode == HttpStatusCode.TooManyRequests && options.EnableRetryOnRateLimit)
                {
                    var retryAfter = GetRetryAfterDelay(response);
                    await Task.Delay(retryAfter, cancellationToken).ConfigureAwait(false);
                    attempt++;
                    continue;
                }

                await ThrowRevenueCatExceptionAsync(response, cancellationToken).ConfigureAwait(false);
            }
            catch (Exception ex) when (ex is not RevenueCatException && IsRetryable(ex))
            {
                lastException = ex;
                attempt++;
                
                if (attempt < options.MaxRetryAttempts)
                {
                    await Task.Delay(options.RetryDelay * attempt, cancellationToken).ConfigureAwait(false);
                    continue;
                }
            }
        }

        throw new RevenueCatException("Max retry attempts reached", lastException);
    }

    public async Task ExecuteAsync(
        HttpMethod method,
        string endpoint,
        object? body = null,
        CancellationToken cancellationToken = default)
    {
        await ExecuteAsync<object>(method, endpoint, body, cancellationToken).ConfigureAwait(false);
    }

    private static HttpRequestMessage CreateRequest(HttpMethod method, string endpoint, object? body)
    {
        var request = new HttpRequestMessage(method, endpoint);

        if (body is not null)
        {
            var json = JsonSerializer.Serialize(body, JsonOptions);
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");
        }

        return request;
    }

    private static TimeSpan GetRetryAfterDelay(HttpResponseMessage response)
    {
        if (response.Headers.RetryAfter?.Delta is { } delta)
        {
            return delta;
        }

        if (response.Headers.TryGetValues("Retry-After", out var values) 
            && int.TryParse(values.FirstOrDefault(), out var seconds))
        {
            return TimeSpan.FromSeconds(seconds);
        }

        return TimeSpan.FromSeconds(1);
    }

    private static async Task ThrowRevenueCatExceptionAsync(HttpResponseMessage response, CancellationToken cancellationToken)
    {
        var content = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
        
        ErrorResponse? error = null;
        try
        {
            error = JsonSerializer.Deserialize<ErrorResponse>(content, JsonOptions);
        }
        catch
        {
            // Ignore deserialization errors
        }

        throw response.StatusCode switch
        {
            HttpStatusCode.BadRequest => new BadRequestException(error?.Message ?? "Bad request", error),
            HttpStatusCode.Unauthorized => new UnauthorizedException(error?.Message ?? "Unauthorized", error),
            HttpStatusCode.Forbidden => new ForbiddenException(error?.Message ?? "Forbidden", error),
            HttpStatusCode.NotFound => new NotFoundException(error?.Message ?? "Resource not found", error),
            HttpStatusCode.Conflict => new ConflictException(error?.Message ?? "Conflict", error),
            (HttpStatusCode)422 => new UnprocessableEntityException(error?.Message ?? "Unprocessable entity", error),
            (HttpStatusCode)423 => new LockedException(error?.Message ?? "Resource locked", error),
            HttpStatusCode.TooManyRequests => new RateLimitException(error?.Message ?? "Rate limit exceeded", error),
            _ => new RevenueCatException($"Request failed with status {response.StatusCode}", error)
        };
    }

    private static bool IsRetryable(Exception exception) =>
        exception is HttpRequestException or TaskCanceledException;
}
