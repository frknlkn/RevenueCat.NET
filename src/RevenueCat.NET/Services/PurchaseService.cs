using RevenueCat.NET.Models;

namespace RevenueCat.NET.Services;

internal sealed class PurchaseService(IHttpRequestExecutor executor) : IPurchaseService
{
    public Task<ListResponse<Purchase>> ListAsync(
        string projectId,
        string customerId,
        int? limit = null,
        string? startingAfter = null,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(projectId);
        ArgumentException.ThrowIfNullOrWhiteSpace(customerId);
        
        var query = QueryStringBuilder.Build(limit, startingAfter);
        return executor.ExecuteAsync<ListResponse<Purchase>>(
            HttpMethod.Get,
            $"/projects/{projectId}/customers/{Uri.EscapeDataString(customerId)}/purchases{query}",
            cancellationToken: cancellationToken);
    }

    public Task<Purchase> GetAsync(
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

    public Task<Purchase> RefundAsync(
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
}
