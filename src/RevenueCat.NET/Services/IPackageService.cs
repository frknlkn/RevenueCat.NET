using Refit;
using RevenueCat.NET.Models;
using RevenueCat.NET.Models.Common;
using RevenueCat.NET.Models.Enums;
using RevenueCat.NET.Models.Packages;
using RevenueCat.NET.Models.Products;

namespace RevenueCat.NET.Services;

public interface IPackageService
{
    [Get("/v2/projects/{projectId}/offerings/{offeringId}/packages")]
    Task<ListResponse<Package>> ListAsync(
        string projectId,
        string offeringId,
        [Query] int? limit = null,
        [AliasAs("starting_after")] [Query] string? startingAfter = null,
        [Query(CollectionFormat.Multi)] string[]? expand = null,
        CancellationToken cancellationToken = default);

    [Get("/v2/projects/{projectId}/offerings/{offeringId}/packages/{packageId}")]
    Task<Package> GetAsync(
        string projectId,
        string offeringId,
        string packageId,
        [Query(CollectionFormat.Multi)] string[]? expand = null,
        CancellationToken cancellationToken = default);

    [Post("/v2/projects/{projectId}/offerings/{offeringId}/packages")]
    Task<Package> CreateAsync(
        string projectId,
        string offeringId,
        [Body] CreatePackageRequest request,
        CancellationToken cancellationToken = default);

    [Post("/v2/projects/{projectId}/offerings/{offeringId}/packages/{packageId}")]
    Task<Package> UpdateAsync(
        string projectId,
        string offeringId,
        string packageId,
        [Body] UpdatePackageRequest request,
        CancellationToken cancellationToken = default);

    [Delete("/v2/projects/{projectId}/offerings/{offeringId}/packages/{packageId}")]
    Task<DeletedObject> DeleteAsync(
        string projectId,
        string offeringId,
        string packageId,
        CancellationToken cancellationToken = default);

    [Get("/v2/projects/{projectId}/offerings/{offeringId}/packages/{packageId}/products")]
    Task<ListResponse<Product>> GetProductsFromPackageAsync(
        string projectId,
        string offeringId,
        string packageId,
        [Query] int? limit = null,
        [AliasAs("starting_after")] [Query] string? startingAfter = null,
        CancellationToken cancellationToken = default);

    [Post("/v2/projects/{projectId}/offerings/{offeringId}/packages/{packageId}/actions/attach_products")]
    Task<Package> AttachProductsToPackageAsync(
        string projectId,
        string offeringId,
        string packageId,
        [Body] AttachProductsToPackageRequest request,
        CancellationToken cancellationToken = default);

    [Post("/v2/projects/{projectId}/offerings/{offeringId}/packages/{packageId}/actions/detach_products")]
    Task<Package> DetachProductsFromPackageAsync(
        string projectId,
        string offeringId,
        string packageId,
        [Body] DetachProductsFromPackageRequest request,
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
