using Refit;
using RevenueCat.NET.Models;
using RevenueCat.NET.Models.Common;
using RevenueCat.NET.Models.Entitlements;
using RevenueCat.NET.Models.Products;

namespace RevenueCat.NET.Services;

/// <summary>
/// Service for managing entitlements in RevenueCat.
/// </summary>
/// <remarks>
/// API Documentation: <see href="https://www.revenuecat.com/docs/api-v2#tag/Entitlements"/>
/// </remarks>
public interface IEntitlementService
{
    /// <summary>
    /// Lists all entitlements for a project.
    /// </summary>
    [Get("/v2/projects/{projectId}/entitlements")]
    Task<ListResponse<Entitlement>> ListAsync(
        string projectId,
        [Query] int? limit = null,
        [AliasAs("starting_after")] [Query] string? startingAfter = null,
        [Query(CollectionFormat.Multi)] string[]? expand = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a specific entitlement by ID.
    /// </summary>
    [Get("/v2/projects/{projectId}/entitlements/{entitlementId}")]
    Task<Entitlement> GetAsync(
        string projectId,
        string entitlementId,
        [Query(CollectionFormat.Multi)] string[]? expand = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a new entitlement.
    /// </summary>
    [Post("/v2/projects/{projectId}/entitlements")]
    Task<Entitlement> CreateAsync(
        string projectId,
        [Body] CreateEntitlementRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing entitlement.
    /// </summary>
    [Post("/v2/projects/{projectId}/entitlements/{entitlementId}")]
    Task<Entitlement> UpdateAsync(
        string projectId,
        string entitlementId,
        [Body] UpdateEntitlementRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes an entitlement permanently.
    /// </summary>
    [Delete("/v2/projects/{projectId}/entitlements/{entitlementId}")]
    Task<DeletedObject> DeleteAsync(
        string projectId,
        string entitlementId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Attaches products to an entitlement.
    /// </summary>
    [Post("/v2/projects/{projectId}/entitlements/{entitlementId}/actions/attach_products")]
    Task<Entitlement> AttachProductsAsync(
        string projectId,
        string entitlementId,
        [Body] AttachProductsRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Detaches products from an entitlement.
    /// </summary>
    [Post("/v2/projects/{projectId}/entitlements/{entitlementId}/actions/detach_products")]
    Task<Entitlement> DetachProductsAsync(
        string projectId,
        string entitlementId,
        [Body] DetachProductsRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets all products attached to an entitlement.
    /// </summary>
    [Get("/v2/projects/{projectId}/entitlements/{entitlementId}/products")]
    Task<ListResponse<Product>> GetProductsAsync(
        string projectId,
        string entitlementId,
        [Query] int? limit = null,
        [AliasAs("starting_after")] [Query] string? startingAfter = null,
        CancellationToken cancellationToken = default);
}

/// <summary>
/// Request to create a new entitlement.
/// </summary>
/// <param name="LookupKey">The unique lookup key for the entitlement.</param>
/// <param name="DisplayName">The display name for the entitlement.</param>
public sealed record CreateEntitlementRequest(
    string LookupKey,
    string DisplayName
);

/// <summary>
/// Request to update an entitlement.
/// </summary>
/// <param name="DisplayName">The new display name for the entitlement.</param>
public sealed record UpdateEntitlementRequest(
    string DisplayName
);

/// <summary>
/// Request to attach products to an entitlement.
/// </summary>
/// <param name="ProductIds">The list of product IDs to attach.</param>
public sealed record AttachProductsRequest(
    IReadOnlyList<string> ProductIds
);

/// <summary>
/// Request to detach products from an entitlement.
/// </summary>
/// <param name="ProductIds">The list of product IDs to detach.</param>
public sealed record DetachProductsRequest(
    IReadOnlyList<string> ProductIds
);
