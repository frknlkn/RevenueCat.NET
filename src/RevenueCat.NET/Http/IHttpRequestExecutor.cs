namespace RevenueCat.NET;

internal interface IHttpRequestExecutor
{
    Task<TResponse> ExecuteAsync<TResponse>(
        HttpMethod method,
        string endpoint,
        object? body = null,
        CancellationToken cancellationToken = default);

    Task ExecuteAsync(
        HttpMethod method,
        string endpoint,
        object? body = null,
        CancellationToken cancellationToken = default);
}
