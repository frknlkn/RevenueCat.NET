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
    /// <param name="projectId">The project ID.</param>
    /// <param name="limit">Maximum number of items to return (default: 20, max: 100).</param>
    /// <param name="startingAfter">Cursor for pagination.</param>
    /// <param name="expand">Optional fields to expand (e.g., "items.products").</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A paginated list of entitlements.</returns>
    /// <exception cref="ArgumentException">Thrown when projectId is null or whitespace.</exception>
    /// <exception cref="RevenueCat.NET.Exceptions.RevenueCatException">Thrown when the API returns an error.</exception>
    Task<ListResponse<Entitlement>> ListAsync(
        string projectId,
        int? limit = null,
        string? startingAfter = null,
        string[]? expand = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a specific entitlement by ID.
    /// </summary>
    /// <param name="projectId">The project ID.</param>
    /// <param name="entitlementId">The entitlement ID.</param>
    /// <param name="expand">Optional fields to expand (e.g., "products").</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The entitlement details.</returns>
    /// <exception cref="ArgumentException">Thrown when projectId or entitlementId is null or whitespace.</exception>
    /// <exception cref="RevenueCat.NET.Exceptions.RevenueCatResourceNotFoundException">Thrown when the entitlement is not found.</exception>
    /// <exception cref="RevenueCat.NET.Exceptions.RevenueCatException">Thrown when the API returns an error.</exception>
    Task<Entitlement> GetAsync(
        string projectId,
        string entitlementId,
        string[]? expand = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a new entitlement.
    /// </summary>
    /// <param name="projectId">The project ID.</param>
    /// <param name="request">The entitlement creation request.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The created entitlement.</returns>
    /// <exception cref="ArgumentException">Thrown when projectId is null or whitespace.</exception>
    /// <exception cref="ArgumentNullException">Thrown when request is null.</exception>
    /// <exception cref="RevenueCat.NET.Exceptions.RevenueCatException">Thrown when the API returns an error.</exception>
    Task<Entitlement> CreateAsync(
        string projectId,
        CreateEntitlementRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing entitlement.
    /// </summary>
    /// <param name="projectId">The project ID.</param>
    /// <param name="entitlementId">The entitlement ID to update.</param>
    /// <param name="request">The entitlement update request.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The updated entitlement.</returns>
    /// <exception cref="ArgumentException">Thrown when projectId or entitlementId is null or whitespace.</exception>
    /// <exception cref="ArgumentNullException">Thrown when request is null.</exception>
    /// <exception cref="RevenueCat.NET.Exceptions.RevenueCatException">Thrown when the API returns an error.</exception>
    Task<Entitlement> UpdateAsync(
        string projectId,
        string entitlementId,
        UpdateEntitlementRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes an entitlement permanently.
    /// </summary>
    /// <param name="projectId">The project ID.</param>
    /// <param name="entitlementId">The entitlement ID to delete.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A deleted object confirmation.</returns>
    /// <exception cref="ArgumentException">Thrown when projectId or entitlementId is null or whitespace.</exception>
    /// <exception cref="RevenueCat.NET.Exceptions.RevenueCatResourceNotFoundException">Thrown when the entitlement is not found.</exception>
    /// <exception cref="RevenueCat.NET.Exceptions.RevenueCatException">Thrown when the API returns an error.</exception>
    Task<DeletedObject> DeleteAsync(
        string projectId,
        string entitlementId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Attaches products to an entitlement.
    /// </summary>
    /// <param name="projectId">The project ID.</param>
    /// <param name="entitlementId">The entitlement ID.</param>
    /// <param name="request">The attach products request containing product IDs.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The updated entitlement.</returns>
    /// <exception cref="ArgumentException">Thrown when projectId or entitlementId is null or whitespace.</exception>
    /// <exception cref="ArgumentNullException">Thrown when request is null.</exception>
    /// <exception cref="RevenueCat.NET.Exceptions.RevenueCatException">Thrown when the API returns an error.</exception>
    Task<Entitlement> AttachProductsAsync(
        string projectId,
        string entitlementId,
        AttachProductsRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Detaches products from an entitlement.
    /// </summary>
    /// <param name="projectId">The project ID.</param>
    /// <param name="entitlementId">The entitlement ID.</param>
    /// <param name="request">The detach products request containing product IDs.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The updated entitlement.</returns>
    /// <exception cref="ArgumentException">Thrown when projectId or entitlementId is null or whitespace.</exception>
    /// <exception cref="ArgumentNullException">Thrown when request is null.</exception>
    /// <exception cref="RevenueCat.NET.Exceptions.RevenueCatException">Thrown when the API returns an error.</exception>
    Task<Entitlement> DetachProductsAsync(
        string projectId,
        string entitlementId,
        DetachProductsRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets all products attached to an entitlement.
    /// </summary>
    /// <param name="projectId">The project ID.</param>
    /// <param name="entitlementId">The entitlement ID.</param>
    /// <param name="limit">Maximum number of items to return (default: 20, max: 100).</param>
    /// <param name="startingAfter">Cursor for pagination.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A paginated list of products.</returns>
    /// <exception cref="ArgumentException">Thrown when projectId or entitlementId is null or whitespace.</exception>
    /// <exception cref="RevenueCat.NET.Exceptions.RevenueCatException">Thrown when the API returns an error.</exception>
    Task<ListResponse<Product>> GetProductsAsync(
        string projectId,
        string entitlementId,
        int? limit = null,
        string? startingAfter = null,
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
