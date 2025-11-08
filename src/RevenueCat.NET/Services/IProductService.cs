using RevenueCat.NET.Models;

namespace RevenueCat.NET.Services;

public interface IProductService
{
    Task<ListResponse<Product>> ListAsync(
        string projectId,
        string? appId = null,
        int? limit = null,
        string? startingAfter = null,
        string[]? expand = null,
        CancellationToken cancellationToken = default);

    Task<Product> GetAsync(
        string projectId,
        string productId,
        string[]? expand = null,
        CancellationToken cancellationToken = default);

    Task<Product> CreateAsync(
        string projectId,
        CreateProductRequest request,
        CancellationToken cancellationToken = default);

    Task<DeletedObject> DeleteAsync(
        string projectId,
        string productId,
        CancellationToken cancellationToken = default);
}

public sealed record CreateProductRequest(
    string StoreIdentifier,
    string AppId,
    ProductType Type,
    string? DisplayName = null
);
