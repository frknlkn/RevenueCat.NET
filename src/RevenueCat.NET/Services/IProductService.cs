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
    /// <param name="projectId">The project ID.</param>
    /// <param name="appId">Optional app ID filter.</param>
    /// <param name="limit">Maximum number of items to return (default: 20, max: 100).</param>
    /// <param name="startingAfter">Cursor for pagination.</param>
    /// <param name="expand">Optional fields to expand (e.g., "items.app").</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A paginated list of products.</returns>
    /// <exception cref="ArgumentException">Thrown when projectId is null or whitespace.</exception>
    /// <exception cref="RevenueCat.NET.Exceptions.RevenueCatException">Thrown when the API returns an error.</exception>
    Task<ListResponse<Product>> ListAsync(
        string projectId,
        string? appId = null,
        int? limit = null,
        string? startingAfter = null,
        string[]? expand = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a specific product by ID.
    /// </summary>
    /// <param name="projectId">The project ID.</param>
    /// <param name="productId">The product ID.</param>
    /// <param name="expand">Optional fields to expand (e.g., "app").</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The product details.</returns>
    /// <exception cref="ArgumentException">Thrown when projectId or productId is null or whitespace.</exception>
    /// <exception cref="RevenueCat.NET.Exceptions.RevenueCatResourceNotFoundException">Thrown when the product is not found.</exception>
    /// <exception cref="RevenueCat.NET.Exceptions.RevenueCatException">Thrown when the API returns an error.</exception>
    Task<Product> GetAsync(
        string projectId,
        string productId,
        string[]? expand = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a new product.
    /// </summary>
    /// <param name="projectId">The project ID.</param>
    /// <param name="request">The product creation request.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The created product.</returns>
    /// <exception cref="ArgumentException">Thrown when projectId is null or whitespace.</exception>
    /// <exception cref="ArgumentNullException">Thrown when request is null.</exception>
    /// <exception cref="RevenueCat.NET.Exceptions.RevenueCatException">Thrown when the API returns an error.</exception>
    Task<Product> CreateAsync(
        string projectId,
        CreateProductRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a product permanently.
    /// </summary>
    /// <param name="projectId">The project ID.</param>
    /// <param name="productId">The product ID to delete.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A deleted object confirmation.</returns>
    /// <exception cref="ArgumentException">Thrown when projectId or productId is null or whitespace.</exception>
    /// <exception cref="RevenueCat.NET.Exceptions.RevenueCatResourceNotFoundException">Thrown when the product is not found.</exception>
    /// <exception cref="RevenueCat.NET.Exceptions.RevenueCatException">Thrown when the API returns an error.</exception>
    Task<DeletedObject> DeleteAsync(
        string projectId,
        string productId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a product in the store (e.g., App Store Connect).
    /// </summary>
    /// <param name="projectId">The project ID.</param>
    /// <param name="productId">The product ID.</param>
    /// <param name="createInput">The store-specific creation input.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The created store product.</returns>
    /// <exception cref="ArgumentException">Thrown when projectId or productId is null or whitespace.</exception>
    /// <exception cref="ArgumentNullException">Thrown when createInput is null.</exception>
    /// <exception cref="RevenueCat.NET.Exceptions.RevenueCatException">Thrown when the API returns an error.</exception>
    Task<StoreProduct> CreateProductInStoreAsync(
        string projectId,
        string productId,
        object createInput,
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
