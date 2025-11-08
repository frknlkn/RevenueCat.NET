using RevenueCat.NET.Models;

namespace RevenueCat.NET.Services;

internal sealed class SubscriptionService(IHttpRequestExecutor executor) : ISubscriptionService
{
    public Task<ListResponse<Subscription>> ListAsync(
        string projectId,
        string customerId,
        int? limit = null,
        string? startingAfter = null,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(projectId);
        ArgumentException.ThrowIfNullOrWhiteSpace(customerId);
        
        var query = QueryStringBuilder.Build(limit, startingAfter);
        return executor.ExecuteAsync<ListResponse<Subscription>>(
            HttpMethod.Get,
            $"/projects/{projectId}/customers/{Uri.EscapeDataString(customerId)}/subscriptions{query}",
            cancellationToken: cancellationToken);
    }

    public Task<Subscription> GetAsync(
        string projectId,
        string customerId,
        string subscriptionId,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(projectId);
        ArgumentException.ThrowIfNullOrWhiteSpace(customerId);
        ArgumentException.ThrowIfNullOrWhiteSpace(subscriptionId);
        
        return executor.ExecuteAsync<Subscription>(
            HttpMethod.Get,
            $"/projects/{projectId}/customers/{Uri.EscapeDataString(customerId)}/subscriptions/{subscriptionId}",
            cancellationToken: cancellationToken);
    }

    public Task<Subscription> CancelAsync(
        string projectId,
        string customerId,
        string subscriptionId,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(projectId);
        ArgumentException.ThrowIfNullOrWhiteSpace(customerId);
        ArgumentException.ThrowIfNullOrWhiteSpace(subscriptionId);
        
        return executor.ExecuteAsync<Subscription>(
            HttpMethod.Post,
            $"/projects/{projectId}/customers/{Uri.EscapeDataString(customerId)}/subscriptions/{subscriptionId}/actions/cancel",
            cancellationToken: cancellationToken);
    }

    public Task<Subscription> RefundAsync(
        string projectId,
        string customerId,
        string subscriptionId,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(projectId);
        ArgumentException.ThrowIfNullOrWhiteSpace(customerId);
        ArgumentException.ThrowIfNullOrWhiteSpace(subscriptionId);
        
        return executor.ExecuteAsync<Subscription>(
            HttpMethod.Post,
            $"/projects/{projectId}/customers/{Uri.EscapeDataString(customerId)}/subscriptions/{subscriptionId}/actions/refund",
            cancellationToken: cancellationToken);
    }
}
