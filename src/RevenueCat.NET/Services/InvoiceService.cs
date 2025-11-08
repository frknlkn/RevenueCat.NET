using RevenueCat.NET.Models;

namespace RevenueCat.NET.Services;

internal sealed class InvoiceService(IHttpRequestExecutor executor) : IInvoiceService
{
    public Task<ListResponse<Invoice>> ListAsync(
        string projectId,
        string customerId,
        int? limit = null,
        string? startingAfter = null,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(projectId);
        ArgumentException.ThrowIfNullOrWhiteSpace(customerId);
        
        var query = QueryStringBuilder.Build(limit, startingAfter);
        return executor.ExecuteAsync<ListResponse<Invoice>>(
            HttpMethod.Get,
            $"/projects/{projectId}/customers/{Uri.EscapeDataString(customerId)}/invoices{query}",
            cancellationToken: cancellationToken);
    }

    public Task<Invoice> GetAsync(
        string projectId,
        string customerId,
        string invoiceId,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(projectId);
        ArgumentException.ThrowIfNullOrWhiteSpace(customerId);
        ArgumentException.ThrowIfNullOrWhiteSpace(invoiceId);
        
        return executor.ExecuteAsync<Invoice>(
            HttpMethod.Get,
            $"/projects/{projectId}/customers/{Uri.EscapeDataString(customerId)}/invoices/{invoiceId}",
            cancellationToken: cancellationToken);
    }
}
