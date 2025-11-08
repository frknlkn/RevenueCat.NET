using RevenueCat.NET.Models;

namespace RevenueCat.NET.Services;

public interface IInvoiceService
{
    Task<ListResponse<Invoice>> ListAsync(
        string projectId,
        string customerId,
        int? limit = null,
        string? startingAfter = null,
        CancellationToken cancellationToken = default);

    Task<Invoice> GetAsync(
        string projectId,
        string customerId,
        string invoiceId,
        CancellationToken cancellationToken = default);
}
