using RevenueCat.NET.Models;

namespace RevenueCat.NET.Services;

internal sealed class AppService(IHttpRequestExecutor executor) : IAppService
{
    public Task<ListResponse<App>> ListAsync(
        string projectId,
        int? limit = null,
        string? startingAfter = null,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(projectId);
        
        var query = QueryStringBuilder.Build(limit, startingAfter);
        return executor.ExecuteAsync<ListResponse<App>>(
            HttpMethod.Get,
            $"/projects/{projectId}/apps{query}",
            cancellationToken: cancellationToken);
    }

    public Task<App> GetAsync(
        string projectId,
        string appId,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(projectId);
        ArgumentException.ThrowIfNullOrWhiteSpace(appId);
        
        return executor.ExecuteAsync<App>(
            HttpMethod.Get,
            $"/projects/{projectId}/apps/{appId}",
            cancellationToken: cancellationToken);
    }

    public Task<App> CreateAsync(
        string projectId,
        CreateAppRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(projectId);
        ArgumentNullException.ThrowIfNull(request);
        
        return executor.ExecuteAsync<App>(
            HttpMethod.Post,
            $"/projects/{projectId}/apps",
            request,
            cancellationToken);
    }

    public Task<App> UpdateAsync(
        string projectId,
        string appId,
        UpdateAppRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(projectId);
        ArgumentException.ThrowIfNullOrWhiteSpace(appId);
        ArgumentNullException.ThrowIfNull(request);
        
        return executor.ExecuteAsync<App>(
            HttpMethod.Post,
            $"/projects/{projectId}/apps/{appId}",
            request,
            cancellationToken);
    }

    public Task<DeletedObject> DeleteAsync(
        string projectId,
        string appId,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(projectId);
        ArgumentException.ThrowIfNullOrWhiteSpace(appId);
        
        return executor.ExecuteAsync<DeletedObject>(
            HttpMethod.Delete,
            $"/projects/{projectId}/apps/{appId}",
            cancellationToken: cancellationToken);
    }
}
