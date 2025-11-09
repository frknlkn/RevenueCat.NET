using RevenueCat.NET.Models.Common;
using RevenueCat.NET.Models.Entitlements;
using RevenueCat.NET.Models.Purchases;

namespace RevenueCat.NET.Services;

public interface IPurchaseService
{
    /// <summary>
    /// Lists all purchases for a customer.
    /// </summary>
    /// <param name="projectId">The project ID.</param>
    /// <param name="customerId">The customer ID.</param>
    /// <param name="environment">Optional environment filter (production or sandbox).</param>
    /// <param name="limit">Maximum number of items to return.</param>
    /// <param name="startingAfter">Cursor for pagination.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A list of purchases.</returns>
    Task<ListResponse<Purchase>> ListPurchasesAsync(
        string projectId,
        string customerId,
        string? environment = null,
        int? limit = null,
        string? startingAfter = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a specific purchase by ID.
    /// </summary>
    /// <param name="projectId">The project ID.</param>
    /// <param name="customerId">The customer ID.</param>
    /// <param name="purchaseId">The purchase ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The purchase.</returns>
    Task<Purchase> GetPurchaseAsync(
        string projectId,
        string customerId,
        string purchaseId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Searches for purchases by store purchase identifier.
    /// </summary>
    /// <param name="projectId">The project ID.</param>
    /// <param name="storePurchaseIdentifier">The store-specific purchase identifier.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A list of matching purchases.</returns>
    Task<ListResponse<Purchase>> SearchPurchasesAsync(
        string projectId,
        string storePurchaseIdentifier,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Refunds a Web Billing purchase.
    /// </summary>
    /// <param name="projectId">The project ID.</param>
    /// <param name="customerId">The customer ID.</param>
    /// <param name="purchaseId">The purchase ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The refunded purchase.</returns>
    Task<Purchase> RefundPurchaseAsync(
        string projectId,
        string customerId,
        string purchaseId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Lists entitlements associated with a purchase.
    /// </summary>
    /// <param name="projectId">The project ID.</param>
    /// <param name="customerId">The customer ID.</param>
    /// <param name="purchaseId">The purchase ID.</param>
    /// <param name="limit">Maximum number of items to return.</param>
    /// <param name="startingAfter">Cursor for pagination.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A list of entitlements.</returns>
    Task<ListResponse<Entitlement>> ListPurchaseEntitlementsAsync(
        string projectId,
        string customerId,
        string purchaseId,
        int? limit = null,
        string? startingAfter = null,
        CancellationToken cancellationToken = default);
}
