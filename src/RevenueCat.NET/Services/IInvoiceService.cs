using Refit;
using RevenueCat.NET.Models.Common;
using RevenueCat.NET.Models.Invoices;

namespace RevenueCat.NET.Services;

public interface IInvoiceService
{
    [Get("/v2/projects/{projectId}/customers/{customerId}/invoices")]
    Task<ListResponse<Invoice>> ListAsync(
        string projectId,
        string customerId,
        [Query] int? limit = null,
        [AliasAs("starting_after")] [Query] string? startingAfter = null,
        CancellationToken cancellationToken = default);

    [Get("/v2/projects/{projectId}/customers/{customerId}/invoices/{invoiceId}")]
    Task<Invoice> GetAsync(
        string projectId,
        string customerId,
        string invoiceId,
        CancellationToken cancellationToken = default);

    [Get("/v2/projects/{projectId}/customers/{customerId}/invoices/{invoiceId}/file")]
    Task<string> GetInvoiceFileAsync(
        string projectId,
        string customerId,
        string invoiceId,
        CancellationToken cancellationToken = default);
}
