using RevenueCat.NET.Models;
using RevenueCat.NET.Models.Common;
using RevenueCat.NET.Models.Packages;
using RevenueCat.NET.Models.Products;

namespace RevenueCat.NET.Services;

internal sealed class PackageService(IHttpRequestExecutor executor) : IPackageService
{
    public Task<ListResponse<Package>> ListAsync(
        string projectId,
        string offeringId,
        int? limit = null,
        string? startingAfter = null,
        string[]? expand = null,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(projectId);
        ArgumentException.ThrowIfNullOrWhiteSpace(offeringId);

        var parameters = new List<string>();
        if (limit.HasValue)
        {
            parameters.Add($"limit={limit.Value}");
        }
        if (!string.IsNullOrWhiteSpace(startingAfter))
        {
            parameters.Add($"starting_after={Uri.EscapeDataString(startingAfter)}");
        }
        if (expand is { Length: > 0 })
        {
            parameters.Add($"expand={string.Join(",", expand.Select(Uri.EscapeDataString))}");
        }

        var query = parameters.Count > 0 ? $"?{string.Join("&", parameters)}" : string.Empty;

        return executor.ExecuteAsync<ListResponse<Package>>(
            HttpMethod.Get,
            $"/projects/{projectId}/offerings/{offeringId}/packages{query}",
            cancellationToken: cancellationToken);
    }

    public Task<Package> GetAsync(
        string projectId,
        string offeringId,
        string packageId,
        string[]? expand = null,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(projectId);
        ArgumentException.ThrowIfNullOrWhiteSpace(offeringId);
        ArgumentException.ThrowIfNullOrWhiteSpace(packageId);

        var query = QueryStringBuilder.BuildExpand(expand);
        return executor.ExecuteAsync<Package>(
            HttpMethod.Get,
            $"/projects/{projectId}/offerings/{offeringId}/packages/{packageId}{query}",
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

    public Task<ListResponse<Product>> GetProductsFromPackageAsync(
        string projectId,
        string offeringId,
        string packageId,
        int? limit = null,
        string? startingAfter = null,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(projectId);
        ArgumentException.ThrowIfNullOrWhiteSpace(offeringId);
        ArgumentException.ThrowIfNullOrWhiteSpace(packageId);

        var query = QueryStringBuilder.Build(limit, startingAfter);
        return executor.ExecuteAsync<ListResponse<Product>>(
            HttpMethod.Get,
            $"/projects/{projectId}/offerings/{offeringId}/packages/{packageId}/products{query}",
            cancellationToken: cancellationToken);
    }

    public Task<Package> AttachProductsToPackageAsync(
        string projectId,
        string offeringId,
        string packageId,
        AttachProductsToPackageRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(projectId);
        ArgumentException.ThrowIfNullOrWhiteSpace(offeringId);
        ArgumentException.ThrowIfNullOrWhiteSpace(packageId);
        ArgumentNullException.ThrowIfNull(request);

        return executor.ExecuteAsync<Package>(
            HttpMethod.Post,
            $"/projects/{projectId}/offerings/{offeringId}/packages/{packageId}/products/attach",
            request,
            cancellationToken);
    }

    public Task<Package> DetachProductsFromPackageAsync(
        string projectId,
        string offeringId,
        string packageId,
        DetachProductsFromPackageRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(projectId);
        ArgumentException.ThrowIfNullOrWhiteSpace(offeringId);
        ArgumentException.ThrowIfNullOrWhiteSpace(packageId);
        ArgumentNullException.ThrowIfNull(request);

        return executor.ExecuteAsync<Package>(
            HttpMethod.Post,
            $"/projects/{projectId}/offerings/{offeringId}/packages/{packageId}/products/detach",
            request,
            cancellationToken);
    }
}
