using Refit;
using RevenueCat.NET.Models.Common;
using RevenueCat.NET.Models.Entitlements;
using RevenueCat.NET.Models.Purchases;

namespace RevenueCat.NET.Services;

public interface IPurchaseService
{
    /// <summary>
    /// Lists all purchases for a customer.
    /// </summary>
    [Get("/v2/projects/{projectId}/customers/{customerId}/purchases")]
    Task<ListResponse<Purchase>> ListPurchasesAsync(
        string projectId,
        string customerId,
        [Query] string? environment = null,
        [Query] int? limit = null,
        [AliasAs("starting_after")] [Query] string? startingAfter = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a specific purchase by ID.
    /// </summary>
    [Get("/v2/projects/{projectId}/customers/{customerId}/purchases/{purchaseId}")]
    Task<Purchase> GetPurchaseAsync(
        string projectId,
        string customerId,
        string purchaseId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Searches for purchases by store purchase identifier.
    /// </summary>
    [Get("/v2/projects/{projectId}/purchases")]
    Task<ListResponse<Purchase>> SearchPurchasesAsync(
        string projectId,
        [AliasAs("store_purchase_identifier")] [Query] string storePurchaseIdentifier,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Refunds a Web Billing purchase.
    /// </summary>
    [Post("/v2/projects/{projectId}/customers/{customerId}/purchases/{purchaseId}/actions/refund")]
    Task<Purchase> RefundPurchaseAsync(
        string projectId,
        string customerId,
        string purchaseId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Lists entitlements associated with a purchase.
    /// </summary>
    [Get("/v2/projects/{projectId}/customers/{customerId}/purchases/{purchaseId}/entitlements")]
    Task<ListResponse<Entitlement>> ListPurchaseEntitlementsAsync(
        string projectId,
        string customerId,
        string purchaseId,
        [Query] int? limit = null,
        [AliasAs("starting_after")] [Query] string? startingAfter = null,
        CancellationToken cancellationToken = default);
}
