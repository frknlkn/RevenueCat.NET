using RevenueCat.NET.Models;

namespace RevenueCat.NET.Services;

public interface IPaywallService
{
    Task<Paywall> CreateAsync(
        string projectId,
        CreatePaywallRequest request,
        CancellationToken cancellationToken = default);
}

public sealed record CreatePaywallRequest(
    string OfferingId
);
