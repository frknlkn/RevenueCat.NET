using RevenueCat.NET.Models.Common;
using RevenueCat.NET.Models.Entitlements;
using RevenueCat.NET.Models.Purchases;

namespace RevenueCat.NET.Services;

internal sealed class PurchaseService(IHttpRequestExecutor executor) : IPurchaseService
{
    public Task<ListResponse<Purchase>> ListPurchasesAsync(
        string projectId,
        string customerId,
        string? environment = null,
        int? limit = null,
        string? startingAfter = null,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(projectId);
        ArgumentException.ThrowIfNullOrWhiteSpace(customerId);

        var queryParams = new Dictionary<string, string?>();
        if (!string.IsNullOrWhiteSpace(environment))
        {
            queryParams["environment"] = environment;
        }
        if (limit.HasValue)
        {
            queryParams["limit"] = limit.Value.ToString();
        }
        if (!string.IsNullOrWhiteSpace(startingAfter))
        {
            queryParams["starting_after"] = startingAfter;
        }

        var query = QueryStringBuilder.Build(queryParams);
        return executor.ExecuteAsync<ListResponse<Purchase>>(
            HttpMethod.Get,
            $"/projects/{projectId}/customers/{Uri.EscapeDataString(customerId)}/purchases{query}",
            cancellationToken: cancellationToken);
    }

    public Task<Purchase> GetPurchaseAsync(
        string projectId,
        string customerId,
        string purchaseId,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(projectId);
        ArgumentException.ThrowIfNullOrWhiteSpace(customerId);
        ArgumentException.ThrowIfNullOrWhiteSpace(purchaseId);

        return executor.ExecuteAsync<Purchase>(
            HttpMethod.Get,
            $"/projects/{projectId}/customers/{Uri.EscapeDataString(customerId)}/purchases/{purchaseId}",
            cancellationToken: cancellationToken);
    }

    public Task<ListResponse<Purchase>> SearchPurchasesAsync(
        string projectId,
        string storePurchaseIdentifier,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(projectId);
        ArgumentException.ThrowIfNullOrWhiteSpace(storePurchaseIdentifier);

        var queryParams = new Dictionary<string, string?>
        {
            ["store_purchase_identifier"] = storePurchaseIdentifier
        };

        var query = QueryStringBuilder.Build(queryParams);
        return executor.ExecuteAsync<ListResponse<Purchase>>(
            HttpMethod.Get,
            $"/projects/{projectId}/purchases{query}",
            cancellationToken: cancellationToken);
    }

    public Task<Purchase> RefundPurchaseAsync(
        string projectId,
        string customerId,
        string purchaseId,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(projectId);
        ArgumentException.ThrowIfNullOrWhiteSpace(customerId);
        ArgumentException.ThrowIfNullOrWhiteSpace(purchaseId);

        return executor.ExecuteAsync<Purchase>(
            HttpMethod.Post,
            $"/projects/{projectId}/customers/{Uri.EscapeDataString(customerId)}/purchases/{purchaseId}/actions/refund",
            cancellationToken: cancellationToken);
    }

    public Task<ListResponse<Entitlement>> ListPurchaseEntitlementsAsync(
        string projectId,
        string customerId,
        string purchaseId,
        int? limit = null,
        string? startingAfter = null,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(projectId);
        ArgumentException.ThrowIfNullOrWhiteSpace(customerId);
        ArgumentException.ThrowIfNullOrWhiteSpace(purchaseId);

        var query = QueryStringBuilder.Build(limit, startingAfter);
        return executor.ExecuteAsync<ListResponse<Entitlement>>(
            HttpMethod.Get,
            $"/projects/{projectId}/customers/{Uri.EscapeDataString(customerId)}/purchases/{purchaseId}/entitlements{query}",
            cancellationToken: cancellationToken);
    }
}
