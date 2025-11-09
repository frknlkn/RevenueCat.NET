using RevenueCat.NET.Models;
using RevenueCat.NET.Models.Common;
using RevenueCat.NET.Models.Entitlements;
using RevenueCat.NET.Models.Products;

namespace RevenueCat.NET.Services;

internal sealed class EntitlementService(IHttpRequestExecutor executor) : IEntitlementService
{
    public Task<ListResponse<Entitlement>> ListAsync(
        string projectId,
        int? limit = null,
        string? startingAfter = null,
        string[]? expand = null,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(projectId);

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

        return executor.ExecuteAsync<ListResponse<Entitlement>>(
            HttpMethod.Get,
            $"/projects/{projectId}/entitlements{query}",
            cancellationToken: cancellationToken);
    }

    public Task<Entitlement> GetAsync(
        string projectId,
        string entitlementId,
        string[]? expand = null,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(projectId);
        ArgumentException.ThrowIfNullOrWhiteSpace(entitlementId);

        var query = QueryStringBuilder.BuildExpand(expand);
        return executor.ExecuteAsync<Entitlement>(
            HttpMethod.Get,
            $"/projects/{projectId}/entitlements/{entitlementId}{query}",
            cancellationToken: cancellationToken);
    }

    public Task<Entitlement> CreateAsync(
        string projectId,
        CreateEntitlementRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(projectId);
        ArgumentNullException.ThrowIfNull(request);

        return executor.ExecuteAsync<Entitlement>(
            HttpMethod.Post,
            $"/projects/{projectId}/entitlements",
            request,
            cancellationToken);
    }

    public Task<Entitlement> UpdateAsync(
        string projectId,
        string entitlementId,
        UpdateEntitlementRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(projectId);
        ArgumentException.ThrowIfNullOrWhiteSpace(entitlementId);
        ArgumentNullException.ThrowIfNull(request);

        return executor.ExecuteAsync<Entitlement>(
            HttpMethod.Post,
            $"/projects/{projectId}/entitlements/{entitlementId}",
            request,
            cancellationToken);
    }

    public Task<DeletedObject> DeleteAsync(
        string projectId,
        string entitlementId,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(projectId);
        ArgumentException.ThrowIfNullOrWhiteSpace(entitlementId);

        return executor.ExecuteAsync<DeletedObject>(
            HttpMethod.Delete,
            $"/projects/{projectId}/entitlements/{entitlementId}",
            cancellationToken: cancellationToken);
    }

    public Task<Entitlement> AttachProductsAsync(
        string projectId,
        string entitlementId,
        AttachProductsRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(projectId);
        ArgumentException.ThrowIfNullOrWhiteSpace(entitlementId);
        ArgumentNullException.ThrowIfNull(request);

        return executor.ExecuteAsync<Entitlement>(
            HttpMethod.Post,
            $"/projects/{projectId}/entitlements/{entitlementId}/actions/attach_products",
            request,
            cancellationToken);
    }

    public Task<Entitlement> DetachProductsAsync(
        string projectId,
        string entitlementId,
        DetachProductsRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(projectId);
        ArgumentException.ThrowIfNullOrWhiteSpace(entitlementId);
        ArgumentNullException.ThrowIfNull(request);

        return executor.ExecuteAsync<Entitlement>(
            HttpMethod.Post,
            $"/projects/{projectId}/entitlements/{entitlementId}/actions/detach_products",
            request,
            cancellationToken);
    }

    public Task<ListResponse<Product>> GetProductsAsync(
        string projectId,
        string entitlementId,
        int? limit = null,
        string? startingAfter = null,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(projectId);
        ArgumentException.ThrowIfNullOrWhiteSpace(entitlementId);

        var query = QueryStringBuilder.Build(limit, startingAfter);
        return executor.ExecuteAsync<ListResponse<Product>>(
            HttpMethod.Get,
            $"/projects/{projectId}/entitlements/{entitlementId}/products{query}",
            cancellationToken: cancellationToken);
    }
}
