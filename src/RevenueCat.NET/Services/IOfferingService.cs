using RevenueCat.NET.Models;

namespace RevenueCat.NET.Services;

public interface IOfferingService
{
    Task<ListResponse<Offering>> ListAsync(
        string projectId,
        int? limit = null,
        string? startingAfter = null,
        CancellationToken cancellationToken = default);

    Task<Offering> GetAsync(
        string projectId,
        string offeringId,
        CancellationToken cancellationToken = default);

    Task<Offering> CreateAsync(
        string projectId,
        CreateOfferingRequest request,
        CancellationToken cancellationToken = default);

    Task<Offering> UpdateAsync(
        string projectId,
        string offeringId,
        UpdateOfferingRequest request,
        CancellationToken cancellationToken = default);

    Task<DeletedObject> DeleteAsync(
        string projectId,
        string offeringId,
        CancellationToken cancellationToken = default);

    Task<Offering> SetDefaultAsync(
        string projectId,
        string offeringId,
        CancellationToken cancellationToken = default);
}

public sealed record CreateOfferingRequest(
    string LookupKey,
    string DisplayName,
    bool IsDefault = false
);

public sealed record UpdateOfferingRequest(
    string DisplayName
);
