using RevenueCat.NET.Models;

namespace RevenueCat.NET.Services;

public interface IPurchaseService
{
    Task<ListResponse<Purchase>> ListAsync(
        string projectId,
        string customerId,
        int? limit = null,
        string? startingAfter = null,
        CancellationToken cancellationToken = default);

    Task<Purchase> GetAsync(
        string projectId,
        string customerId,
        string purchaseId,
        CancellationToken cancellationToken = default);

    Task<Purchase> RefundAsync(
        string projectId,
        string customerId,
        string purchaseId,
        CancellationToken cancellationToken = default);
}
