using RevenueCat.NET.Models.Common;
using RevenueCat.NET.Models.Invoices;

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

    Task<string> GetInvoiceFileAsync(
        string projectId,
        string customerId,
        string invoiceId,
        CancellationToken cancellationToken = default);
}
