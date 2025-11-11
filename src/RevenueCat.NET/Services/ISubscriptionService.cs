using Refit;
using RevenueCat.NET.Models.Common;
using RevenueCat.NET.Models.Entitlements;
using RevenueCat.NET.Models.Subscriptions;

namespace RevenueCat.NET.Services;

public interface ISubscriptionService
{
    /// <summary>
    /// Lists all subscriptions for a customer.
    /// </summary>
    [Get("/v2/projects/{projectId}/customers/{customerId}/subscriptions")]
    Task<ListResponse<Subscription>> ListSubscriptionsAsync(
        string projectId,
        string customerId,
        [Query] string? environment = null,
        [Query] int? limit = null,
        [AliasAs("starting_after")] [Query] string? startingAfter = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a specific subscription by ID.
    /// </summary>
    [Get("/v2/projects/{projectId}/subscriptions/{subscriptionId}")]
    Task<Subscription> GetSubscriptionAsync(
        string projectId,
        string subscriptionId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Searches for subscriptions by store subscription identifier.
    /// </summary>
    [Get("/v2/projects/{projectId}/subscriptions")]
    Task<ListResponse<Subscription>> SearchSubscriptionsAsync(
        string projectId,
        [AliasAs("store_subscription_identifier")] [Query] string storeSubscriptionIdentifier,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Cancels a Web Billing subscription.
    /// </summary>
    [Post("/v2/projects/{projectId}/subscriptions/{subscriptionId}/actions/cancel")]
    Task<Subscription> CancelSubscriptionAsync(
        string projectId,
        string subscriptionId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Refunds a Web Billing subscription.
    /// </summary>
    [Post("/v2/projects/{projectId}/subscriptions/{subscriptionId}/actions/refund")]
    Task<Subscription> RefundSubscriptionAsync(
        string projectId,
        string subscriptionId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets an authenticated management URL for a subscription.
    /// </summary>
    [Get("/v2/projects/{projectId}/subscriptions/{subscriptionId}/authenticated_management_url")]
    Task<AuthenticatedManagementUrl> GetAuthenticatedManagementUrlAsync(
        string projectId,
        string subscriptionId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets transactions for a Play Store subscription.
    /// </summary>
    [Get("/v2/projects/{projectId}/subscriptions/{subscriptionId}/transactions")]
    Task<ListResponse<SubscriptionTransaction>> GetSubscriptionTransactionsAsync(
        string projectId,
        string subscriptionId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Refunds a Play Store subscription transaction.
    /// </summary>
    [Post("/v2/projects/{projectId}/subscriptions/{subscriptionId}/transactions/{transactionId}/actions/refund")]
    Task<SubscriptionTransaction> RefundSubscriptionTransactionAsync(
        string projectId,
        string subscriptionId,
        string transactionId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Lists entitlements for a subscription.
    /// </summary>
    [Get("/v2/projects/{projectId}/subscriptions/{subscriptionId}/entitlements")]
    Task<ListResponse<Entitlement>> ListSubscriptionEntitlementsAsync(
        string projectId,
        string subscriptionId,
        [Query] int? limit = null,
        [AliasAs("starting_after")] [Query] string? startingAfter = null,
        CancellationToken cancellationToken = default);
}
