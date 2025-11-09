using RevenueCat.NET.Models.Common;
using RevenueCat.NET.Models.Entitlements;
using RevenueCat.NET.Models.Subscriptions;

namespace RevenueCat.NET.Services;

internal sealed class SubscriptionService(IHttpRequestExecutor executor) : ISubscriptionService
{
    public Task<ListResponse<Subscription>> ListSubscriptionsAsync(
        string projectId,
        string customerId,
        string? environment = null,
        int? limit = null,
        string? startingAfter = null,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(projectId);
        ArgumentException.ThrowIfNullOrWhiteSpace(customerId);

        var query = QueryStringBuilder.Build(limit, startingAfter, environment: environment);
        return executor.ExecuteAsync<ListResponse<Subscription>>(
            HttpMethod.Get,
            $"/projects/{projectId}/customers/{Uri.EscapeDataString(customerId)}/subscriptions{query}",
            cancellationToken: cancellationToken);
    }

    public Task<Subscription> GetSubscriptionAsync(
        string projectId,
        string subscriptionId,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(projectId);
        ArgumentException.ThrowIfNullOrWhiteSpace(subscriptionId);

        return executor.ExecuteAsync<Subscription>(
            HttpMethod.Get,
            $"/projects/{projectId}/subscriptions/{subscriptionId}",
            cancellationToken: cancellationToken);
    }

    public Task<ListResponse<Subscription>> SearchSubscriptionsAsync(
        string projectId,
        string storeSubscriptionIdentifier,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(projectId);
        ArgumentException.ThrowIfNullOrWhiteSpace(storeSubscriptionIdentifier);

        var query = $"?store_subscription_identifier={Uri.EscapeDataString(storeSubscriptionIdentifier)}";
        return executor.ExecuteAsync<ListResponse<Subscription>>(
            HttpMethod.Get,
            $"/projects/{projectId}/subscriptions{query}",
            cancellationToken: cancellationToken);
    }

    public Task<Subscription> CancelSubscriptionAsync(
        string projectId,
        string subscriptionId,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(projectId);
        ArgumentException.ThrowIfNullOrWhiteSpace(subscriptionId);

        return executor.ExecuteAsync<Subscription>(
            HttpMethod.Post,
            $"/projects/{projectId}/subscriptions/{subscriptionId}/actions/cancel",
            cancellationToken: cancellationToken);
    }

    public Task<Subscription> RefundSubscriptionAsync(
        string projectId,
        string subscriptionId,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(projectId);
        ArgumentException.ThrowIfNullOrWhiteSpace(subscriptionId);

        return executor.ExecuteAsync<Subscription>(
            HttpMethod.Post,
            $"/projects/{projectId}/subscriptions/{subscriptionId}/actions/refund",
            cancellationToken: cancellationToken);
    }

    public Task<AuthenticatedManagementUrl> GetAuthenticatedManagementUrlAsync(
        string projectId,
        string subscriptionId,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(projectId);
        ArgumentException.ThrowIfNullOrWhiteSpace(subscriptionId);

        return executor.ExecuteAsync<AuthenticatedManagementUrl>(
            HttpMethod.Get,
            $"/projects/{projectId}/subscriptions/{subscriptionId}/authenticated_management_url",
            cancellationToken: cancellationToken);
    }

    public Task<ListResponse<SubscriptionTransaction>> GetSubscriptionTransactionsAsync(
        string projectId,
        string subscriptionId,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(projectId);
        ArgumentException.ThrowIfNullOrWhiteSpace(subscriptionId);

        return executor.ExecuteAsync<ListResponse<SubscriptionTransaction>>(
            HttpMethod.Get,
            $"/projects/{projectId}/subscriptions/{subscriptionId}/transactions",
            cancellationToken: cancellationToken);
    }

    public Task<SubscriptionTransaction> RefundSubscriptionTransactionAsync(
        string projectId,
        string subscriptionId,
        string transactionId,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(projectId);
        ArgumentException.ThrowIfNullOrWhiteSpace(subscriptionId);
        ArgumentException.ThrowIfNullOrWhiteSpace(transactionId);

        return executor.ExecuteAsync<SubscriptionTransaction>(
            HttpMethod.Post,
            $"/projects/{projectId}/subscriptions/{subscriptionId}/transactions/{transactionId}/actions/refund",
            cancellationToken: cancellationToken);
    }

    public Task<ListResponse<Entitlement>> ListSubscriptionEntitlementsAsync(
        string projectId,
        string subscriptionId,
        int? limit = null,
        string? startingAfter = null,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(projectId);
        ArgumentException.ThrowIfNullOrWhiteSpace(subscriptionId);

        var query = QueryStringBuilder.Build(limit, startingAfter);
        return executor.ExecuteAsync<ListResponse<Entitlement>>(
            HttpMethod.Get,
            $"/projects/{projectId}/subscriptions/{subscriptionId}/entitlements{query}",
            cancellationToken: cancellationToken);
    }
}
