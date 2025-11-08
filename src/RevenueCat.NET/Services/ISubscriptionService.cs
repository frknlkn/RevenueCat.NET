using RevenueCat.NET.Models;

namespace RevenueCat.NET.Services;

public interface ISubscriptionService
{
    Task<ListResponse<Subscription>> ListAsync(
        string projectId,
        string customerId,
        int? limit = null,
        string? startingAfter = null,
        CancellationToken cancellationToken = default);

    Task<Subscription> GetAsync(
        string projectId,
        string customerId,
        string subscriptionId,
        CancellationToken cancellationToken = default);

    Task<Subscription> CancelAsync(
        string projectId,
        string customerId,
        string subscriptionId,
        CancellationToken cancellationToken = default);

    Task<Subscription> RefundAsync(
        string projectId,
        string customerId,
        string subscriptionId,
        CancellationToken cancellationToken = default);
}
