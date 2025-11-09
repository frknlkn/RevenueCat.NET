using RevenueCat.NET.Models.Paywalls;

namespace RevenueCat.NET.Services;

internal sealed class PaywallService(IHttpRequestExecutor executor) : IPaywallService
{
    public Task<Paywall> CreateAsync(
        string projectId,
        CreatePaywallRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(projectId);
        ArgumentNullException.ThrowIfNull(request);
        
        return executor.ExecuteAsync<Paywall>(
            HttpMethod.Post,
            $"/projects/{projectId}/paywalls",
            request,
            cancellationToken);
    }
}
