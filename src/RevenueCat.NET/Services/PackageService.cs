using RevenueCat.NET.Models;

namespace RevenueCat.NET.Services;

internal sealed class PackageService(IHttpRequestExecutor executor) : IPackageService
{
    public Task<ListResponse<Package>> ListAsync(
        string projectId,
        string offeringId,
        int? limit = null,
        string? startingAfter = null,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(projectId);
        ArgumentException.ThrowIfNullOrWhiteSpace(offeringId);
        
        var query = QueryStringBuilder.Build(limit, startingAfter);
        return executor.ExecuteAsync<ListResponse<Package>>(
            HttpMethod.Get,
            $"/projects/{projectId}/offerings/{offeringId}/packages{query}",
            cancellationToken: cancellationToken);
    }

    public Task<Package> GetAsync(
        string projectId,
        string offeringId,
        string packageId,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(projectId);
        ArgumentException.ThrowIfNullOrWhiteSpace(offeringId);
        ArgumentException.ThrowIfNullOrWhiteSpace(packageId);
        
        return executor.ExecuteAsync<Package>(
            HttpMethod.Get,
            $"/projects/{projectId}/offerings/{offeringId}/packages/{packageId}",
            cancellationToken: cancellationToken);
    }

    public Task<Package> CreateAsync(
        string projectId,
        string offeringId,
        CreatePackageRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(projectId);
        ArgumentException.ThrowIfNullOrWhiteSpace(offeringId);
        ArgumentNullException.ThrowIfNull(request);
        
        return executor.ExecuteAsync<Package>(
            HttpMethod.Post,
            $"/projects/{projectId}/offerings/{offeringId}/packages",
            request,
            cancellationToken);
    }

    public Task<Package> UpdateAsync(
        string projectId,
        string offeringId,
        string packageId,
        UpdatePackageRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(projectId);
        ArgumentException.ThrowIfNullOrWhiteSpace(offeringId);
        ArgumentException.ThrowIfNullOrWhiteSpace(packageId);
        ArgumentNullException.ThrowIfNull(request);
        
        return executor.ExecuteAsync<Package>(
            HttpMethod.Post,
            $"/projects/{projectId}/offerings/{offeringId}/packages/{packageId}",
            request,
            cancellationToken);
    }

    public Task<DeletedObject> DeleteAsync(
        string projectId,
        string offeringId,
        string packageId,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(projectId);
        ArgumentException.ThrowIfNullOrWhiteSpace(offeringId);
        ArgumentException.ThrowIfNullOrWhiteSpace(packageId);
        
        return executor.ExecuteAsync<DeletedObject>(
            HttpMethod.Delete,
            $"/projects/{projectId}/offerings/{offeringId}/packages/{packageId}",
            cancellationToken: cancellationToken);
    }
}
