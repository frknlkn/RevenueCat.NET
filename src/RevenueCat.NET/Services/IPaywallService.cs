using Refit;
using RevenueCat.NET.Models.Paywalls;

namespace RevenueCat.NET.Services;

public interface IPaywallService
{
    [Post("/v2/projects/{projectId}/paywalls")]
    Task<Paywall> CreateAsync(
        string projectId,
        [Body] CreatePaywallRequest request,
        CancellationToken cancellationToken = default);
}

public sealed record CreatePaywallRequest(
    string OfferingId
);
