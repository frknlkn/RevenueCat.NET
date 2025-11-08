using RevenueCat.NET.Models;

namespace RevenueCat.NET.Services;

internal sealed class CustomerService(IHttpRequestExecutor executor) : ICustomerService
{
    public Task<ListResponse<Customer>> ListAsync(
        string projectId,
        int? limit = null,
        string? startingAfter = null,
        string? search = null,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(projectId);
        
        var query = QueryStringBuilder.Build(limit, startingAfter, search);
        return executor.ExecuteAsync<ListResponse<Customer>>(
            HttpMethod.Get,
            $"/projects/{projectId}/customers{query}",
            cancellationToken: cancellationToken);
    }

    public Task<Customer> GetAsync(
        string projectId,
        string customerId,
        string[]? expand = null,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(projectId);
        ArgumentException.ThrowIfNullOrWhiteSpace(customerId);
        
        var query = QueryStringBuilder.BuildExpand(expand);
        return executor.ExecuteAsync<Customer>(
            HttpMethod.Get,
            $"/projects/{projectId}/customers/{Uri.EscapeDataString(customerId)}{query}",
            cancellationToken: cancellationToken);
    }

    public Task<Customer> CreateAsync(
        string projectId,
        CreateCustomerRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(projectId);
        ArgumentNullException.ThrowIfNull(request);
        
        return executor.ExecuteAsync<Customer>(
            HttpMethod.Post,
            $"/projects/{projectId}/customers",
            request,
            cancellationToken);
    }

    public Task<DeletedObject> DeleteAsync(
        string projectId,
        string customerId,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(projectId);
        ArgumentException.ThrowIfNullOrWhiteSpace(customerId);
        
        return executor.ExecuteAsync<DeletedObject>(
            HttpMethod.Delete,
            $"/projects/{projectId}/customers/{Uri.EscapeDataString(customerId)}",
            cancellationToken: cancellationToken);
    }

    public Task<TransferResponse> TransferAsync(
        string projectId,
        string customerId,
        TransferCustomerRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(projectId);
        ArgumentException.ThrowIfNullOrWhiteSpace(customerId);
        ArgumentNullException.ThrowIfNull(request);
        
        return executor.ExecuteAsync<TransferResponse>(
            HttpMethod.Post,
            $"/projects/{projectId}/customers/{Uri.EscapeDataString(customerId)}/actions/transfer",
            request,
            cancellationToken);
    }
}
