using RevenueCat.NET.Models;
using RevenueCat.NET.Models.Common;
using RevenueCat.NET.Models.Enums;
using RevenueCat.NET.Models.Packages;
using RevenueCat.NET.Models.Products;

namespace RevenueCat.NET.Services;

public interface IPackageService
{
    Task<ListResponse<Package>> ListAsync(
        string projectId,
        string offeringId,
        int? limit = null,
        string? startingAfter = null,
        string[]? expand = null,
        CancellationToken cancellationToken = default);

    Task<Package> GetAsync(
        string projectId,
        string offeringId,
        string packageId,
        string[]? expand = null,
        CancellationToken cancellationToken = default);

    Task<Package> CreateAsync(
        string projectId,
        string offeringId,
        CreatePackageRequest request,
        CancellationToken cancellationToken = default);

    Task<Package> UpdateAsync(
        string projectId,
        string offeringId,
        string packageId,
        UpdatePackageRequest request,
        CancellationToken cancellationToken = default);

    Task<DeletedObject> DeleteAsync(
        string projectId,
        string offeringId,
        string packageId,
        CancellationToken cancellationToken = default);

    Task<ListResponse<Product>> GetProductsFromPackageAsync(
        string projectId,
        string offeringId,
        string packageId,
        int? limit = null,
        string? startingAfter = null,
        CancellationToken cancellationToken = default);

    Task<Package> AttachProductsToPackageAsync(
        string projectId,
        string offeringId,
        string packageId,
        AttachProductsToPackageRequest request,
        CancellationToken cancellationToken = default);

    Task<Package> DetachProductsFromPackageAsync(
        string projectId,
        string offeringId,
        string packageId,
        DetachProductsFromPackageRequest request,
        CancellationToken cancellationToken = default);
}

public sealed record CreatePackageRequest(
    string LookupKey,
    string DisplayName,
    string ProductId,
    int Position
);

public sealed record UpdatePackageRequest(
    string? DisplayName = null,
    int? Position = null
);

public sealed record AttachProductsToPackageRequest(
    IReadOnlyList<ProductAttachment> Products
);

public sealed record ProductAttachment(
    string ProductId,
    EligibilityCriteria EligibilityCriteria
);

public sealed record DetachProductsFromPackageRequest(
    IReadOnlyList<string> ProductIds
);
