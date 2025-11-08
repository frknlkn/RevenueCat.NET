using RevenueCat.NET.Models;

namespace RevenueCat.NET.Services;

public interface IEntitlementService
{
    Task<ListResponse<Entitlement>> ListAsync(
        string projectId,
        int? limit = null,
        string? startingAfter = null,
        string[]? expand = null,
        CancellationToken cancellationToken = default);

    Task<Entitlement> GetAsync(
        string projectId,
        string entitlementId,
        string[]? expand = null,
        CancellationToken cancellationToken = default);

    Task<Entitlement> CreateAsync(
        string projectId,
        CreateEntitlementRequest request,
        CancellationToken cancellationToken = default);

    Task<Entitlement> UpdateAsync(
        string projectId,
        string entitlementId,
        UpdateEntitlementRequest request,
        CancellationToken cancellationToken = default);

    Task<DeletedObject> DeleteAsync(
        string projectId,
        string entitlementId,
        CancellationToken cancellationToken = default);

    Task<Entitlement> AttachProductsAsync(
        string projectId,
        string entitlementId,
        AttachProductsRequest request,
        CancellationToken cancellationToken = default);

    Task<ListResponse<Product>> GetProductsAsync(
        string projectId,
        string entitlementId,
        int? limit = null,
        string? startingAfter = null,
        CancellationToken cancellationToken = default);
}

public sealed record CreateEntitlementRequest(
    string LookupKey,
    string DisplayName
);

public sealed record UpdateEntitlementRequest(
    string DisplayName
);

public sealed record AttachProductsRequest(
    IReadOnlyList<string> ProductIds
);
