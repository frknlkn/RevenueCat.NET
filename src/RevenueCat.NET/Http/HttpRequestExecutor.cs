using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using RevenueCat.NET.Configuration;
using RevenueCat.NET.Exceptions;
using RevenueCat.NET.Models;
using RevenueCat.NET.Models.Enums;

namespace RevenueCat.NET;

internal sealed class HttpRequestExecutor(HttpClient httpClient, RevenueCatOptions options) : IHttpRequestExecutor
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        PropertyNameCaseInsensitive = true,
        WriteIndented = false,
        Converters =
        {
            new JsonStringEnumConverter(JsonNamingPolicy.SnakeCaseLower, allowIntegerValues: false)
        }
    };

    public async Task<TResponse> ExecuteAsync<TResponse>(
        HttpMethod method,
        string endpoint,
        object? body = null,
        CancellationToken cancellationToken = default,
        string? idempotencyKey = null)
    {
        var attempt = 0;
        Exception? lastException = null;

        while (attempt < options.MaxRetryAttempts)
        {
            try
            {
                using var request = CreateRequest(method, endpoint, body, idempotencyKey);
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
        CancellationToken cancellationToken = default,
        string? idempotencyKey = null)
    {
        await ExecuteAsync<object>(method, endpoint, body, cancellationToken, idempotencyKey).ConfigureAwait(false);
    }

    private static HttpRequestMessage CreateRequest(HttpMethod method, string endpoint, object? body, string? idempotencyKey = null)
    {
        var request = new HttpRequestMessage(method, endpoint);

        if (body is not null)
        {
            var json = JsonSerializer.Serialize(body, JsonOptions);
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");
        }

        if (!string.IsNullOrWhiteSpace(idempotencyKey))
        {
            request.Headers.Add("Idempotency-Key", idempotencyKey);
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
        var statusCode = (int)response.StatusCode;
        
        // Try to deserialize new error format
        RevenueCatError? structuredError = null;
        ErrorResponse? legacyError = null;
        
        try
        {
            structuredError = JsonSerializer.Deserialize<RevenueCatError>(content, JsonOptions);
        }
        catch
        {
            // Try legacy format
            try
            {
                legacyError = JsonSerializer.Deserialize<ErrorResponse>(content, JsonOptions);
            }
            catch
            {
                // Ignore deserialization errors
            }
        }

        // If we have structured error, throw typed exception based on error type
        if (structuredError != null)
        {
            throw structuredError.Type switch
            {
                ErrorType.ParameterError => new RevenueCatParameterException(structuredError, statusCode),
                ErrorType.ResourceMissing => new RevenueCatResourceNotFoundException(structuredError, statusCode),
                ErrorType.ResourceAlreadyExists => new RevenueCatConflictException(structuredError, statusCode),
                ErrorType.IdempotencyError => new RevenueCatConflictException(structuredError, statusCode),
                ErrorType.RateLimitError => new RevenueCatRateLimitException(structuredError, statusCode),
                ErrorType.AuthenticationError => new RevenueCatAuthenticationException(structuredError, statusCode),
                ErrorType.AuthorizationError => new RevenueCatAuthorizationException(structuredError, statusCode),
                _ => new RevenueCatException(structuredError, statusCode)
            };
        }

        // Fall back to legacy exception handling
        throw response.StatusCode switch
        {
            HttpStatusCode.BadRequest => new BadRequestException(legacyError?.Message ?? "Bad request", legacyError),
            HttpStatusCode.Unauthorized => new UnauthorizedException(legacyError?.Message ?? "Unauthorized", legacyError),
            HttpStatusCode.Forbidden => new ForbiddenException(legacyError?.Message ?? "Forbidden", legacyError),
            HttpStatusCode.NotFound => new NotFoundException(legacyError?.Message ?? "Resource not found", legacyError),
            HttpStatusCode.Conflict => new ConflictException(legacyError?.Message ?? "Conflict", legacyError),
            (HttpStatusCode)422 => new UnprocessableEntityException(legacyError?.Message ?? "Unprocessable entity", legacyError),
            (HttpStatusCode)423 => new LockedException(legacyError?.Message ?? "Resource locked", legacyError),
            HttpStatusCode.TooManyRequests => new RateLimitException(legacyError?.Message ?? "Rate limit exceeded", legacyError),
            _ => new RevenueCatException($"Request failed with status {response.StatusCode}", legacyError)
        };
    }

    private static bool IsRetryable(Exception exception) =>
        exception is HttpRequestException or TaskCanceledException;
}
