using Refit;
using RevenueCat.NET.Models;
using RevenueCat.NET.Models.Common;
using RevenueCat.NET.Models.Products;
using RevenueCat.NET.Models.Enums;

namespace RevenueCat.NET.Services;

/// <summary>
/// Service for managing products in RevenueCat.
/// </summary>
/// <remarks>
/// API Documentation: <see href="https://www.revenuecat.com/docs/api-v2#tag/Products"/>
/// </remarks>
public interface IProductService
{
    /// <summary>
    /// Lists all products for a project.
    /// </summary>
    [Get("/v2/projects/{projectId}/products")]
    Task<ListResponse<Product>> ListAsync(
        string projectId,
        [AliasAs("app_id")] [Query] string? appId = null,
        [Query] int? limit = null,
        [AliasAs("starting_after")] [Query] string? startingAfter = null,
        [Query(CollectionFormat.Multi)] string[]? expand = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a specific product by ID.
    /// </summary>
    [Get("/v2/projects/{projectId}/products/{productId}")]
    Task<Product> GetAsync(
        string projectId,
        string productId,
        [Query(CollectionFormat.Multi)] string[]? expand = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a new product.
    /// </summary>
    [Post("/v2/projects/{projectId}/products")]
    Task<Product> CreateAsync(
        string projectId,
        [Body] CreateProductRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a product permanently.
    /// </summary>
    [Delete("/v2/projects/{projectId}/products/{productId}")]
    Task<DeletedObject> DeleteAsync(
        string projectId,
        string productId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a product in the store (e.g., App Store Connect).
    /// </summary>
    [Post("/v2/projects/{projectId}/products/{productId}/actions/create_in_store")]
    Task<StoreProduct> CreateProductInStoreAsync(
        string projectId,
        string productId,
        [Body] object createInput,
        CancellationToken cancellationToken = default);
}

/// <summary>
/// Request to create a new product.
/// </summary>
/// <param name="StoreIdentifier">The store-specific product identifier.</param>
/// <param name="AppId">The app ID this product belongs to.</param>
/// <param name="Type">The product type (subscription, one_time, etc.).</param>
/// <param name="DisplayName">Optional display name for the product.</param>
public sealed record CreateProductRequest(
    string StoreIdentifier,
    string AppId,
    ProductType Type,
    string? DisplayName = null
);
