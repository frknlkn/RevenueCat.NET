using RevenueCat.NET.Models;

namespace RevenueCat.NET.Services;

public interface IPackageService
{
    Task<ListResponse<Package>> ListAsync(
        string projectId,
        string offeringId,
        int? limit = null,
        string? startingAfter = null,
        CancellationToken cancellationToken = default);

    Task<Package> GetAsync(
        string projectId,
        string offeringId,
        string packageId,
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
