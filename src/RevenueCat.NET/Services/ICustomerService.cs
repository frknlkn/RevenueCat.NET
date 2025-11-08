using RevenueCat.NET.Models;

namespace RevenueCat.NET.Services;

public interface ICustomerService
{
    Task<ListResponse<Customer>> ListAsync(
        string projectId,
        int? limit = null,
        string? startingAfter = null,
        string? search = null,
        CancellationToken cancellationToken = default);

    Task<Customer> GetAsync(
        string projectId,
        string customerId,
        string[]? expand = null,
        CancellationToken cancellationToken = default);

    Task<Customer> CreateAsync(
        string projectId,
        CreateCustomerRequest request,
        CancellationToken cancellationToken = default);

    Task<DeletedObject> DeleteAsync(
        string projectId,
        string customerId,
        CancellationToken cancellationToken = default);

    Task<TransferResponse> TransferAsync(
        string projectId,
        string customerId,
        TransferCustomerRequest request,
        CancellationToken cancellationToken = default);
}

public sealed record CreateCustomerRequest(
    string Id,
    IReadOnlyList<CustomerAttribute>? Attributes = null
);

public sealed record TransferCustomerRequest(
    string TargetCustomerId,
    IReadOnlyList<string>? AppIds = null
);

public sealed record TransferResponse(
    string Object,
    string SourceCustomerId,
    string TargetCustomerId,
    long TransferredAt
);
