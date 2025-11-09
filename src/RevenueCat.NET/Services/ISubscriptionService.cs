using RevenueCat.NET.Models.Common;
using RevenueCat.NET.Models.Entitlements;
using RevenueCat.NET.Models.Subscriptions;

namespace RevenueCat.NET.Services;

public interface ISubscriptionService
{
    /// <summary>
    /// Lists all subscriptions for a customer.
    /// </summary>
    /// <param name="projectId">The project ID.</param>
    /// <param name="customerId">The customer ID.</param>
    /// <param name="environment">Optional environment filter (production or sandbox).</param>
    /// <param name="limit">Maximum number of items to return.</param>
    /// <param name="startingAfter">Cursor for pagination.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A paginated list of subscriptions.</returns>
    Task<ListResponse<Subscription>> ListSubscriptionsAsync(
        string projectId,
        string customerId,
        string? environment = null,
        int? limit = null,
        string? startingAfter = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a specific subscription by ID.
    /// </summary>
    /// <param name="projectId">The project ID.</param>
    /// <param name="subscriptionId">The subscription ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The subscription details.</returns>
    Task<Subscription> GetSubscriptionAsync(
        string projectId,
        string subscriptionId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Searches for subscriptions by store subscription identifier.
    /// </summary>
    /// <param name="projectId">The project ID.</param>
    /// <param name="storeSubscriptionIdentifier">The store-specific subscription identifier.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A list of matching subscriptions.</returns>
    Task<ListResponse<Subscription>> SearchSubscriptionsAsync(
        string projectId,
        string storeSubscriptionIdentifier,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Cancels a Web Billing subscription.
    /// </summary>
    /// <param name="projectId">The project ID.</param>
    /// <param name="subscriptionId">The subscription ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The updated subscription.</returns>
    Task<Subscription> CancelSubscriptionAsync(
        string projectId,
        string subscriptionId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Refunds a Web Billing subscription.
    /// </summary>
    /// <param name="projectId">The project ID.</param>
    /// <param name="subscriptionId">The subscription ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The updated subscription.</returns>
    Task<Subscription> RefundSubscriptionAsync(
        string projectId,
        string subscriptionId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets an authenticated management URL for a subscription.
    /// </summary>
    /// <param name="projectId">The project ID.</param>
    /// <param name="subscriptionId">The subscription ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The authenticated management URL.</returns>
    Task<AuthenticatedManagementUrl> GetAuthenticatedManagementUrlAsync(
        string projectId,
        string subscriptionId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets transactions for a Play Store subscription.
    /// </summary>
    /// <param name="projectId">The project ID.</param>
    /// <param name="subscriptionId">The subscription ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A list of subscription transactions.</returns>
    Task<ListResponse<SubscriptionTransaction>> GetSubscriptionTransactionsAsync(
        string projectId,
        string subscriptionId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Refunds a Play Store subscription transaction.
    /// </summary>
    /// <param name="projectId">The project ID.</param>
    /// <param name="subscriptionId">The subscription ID.</param>
    /// <param name="transactionId">The transaction ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The refunded transaction.</returns>
    Task<SubscriptionTransaction> RefundSubscriptionTransactionAsync(
        string projectId,
        string subscriptionId,
        string transactionId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Lists entitlements for a subscription.
    /// </summary>
    /// <param name="projectId">The project ID.</param>
    /// <param name="subscriptionId">The subscription ID.</param>
    /// <param name="limit">Maximum number of items to return.</param>
    /// <param name="startingAfter">Cursor for pagination.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A paginated list of entitlements.</returns>
    Task<ListResponse<Entitlement>> ListSubscriptionEntitlementsAsync(
        string projectId,
        string subscriptionId,
        int? limit = null,
        string? startingAfter = null,
        CancellationToken cancellationToken = default);
}
