using RevenueCat.NET.Models;
using RevenueCat.NET.Models.Common;
using RevenueCat.NET.Models.Customers;

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

    public Task<ListResponse<CustomerAlias>> ListAliasesAsync(
        string projectId,
        string customerId,
        int? limit = null,
        string? startingAfter = null,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(projectId);
        ArgumentException.ThrowIfNullOrWhiteSpace(customerId);

        var query = QueryStringBuilder.Build(limit, startingAfter);
        return executor.ExecuteAsync<ListResponse<CustomerAlias>>(
            HttpMethod.Get,
            $"/projects/{projectId}/customers/{Uri.EscapeDataString(customerId)}/aliases{query}",
            cancellationToken: cancellationToken);
    }

    public Task<ListResponse<CustomerAttribute>> ListAttributesAsync(
        string projectId,
        string customerId,
        int? limit = null,
        string? startingAfter = null,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(projectId);
        ArgumentException.ThrowIfNullOrWhiteSpace(customerId);

        var query = QueryStringBuilder.Build(limit, startingAfter);
        return executor.ExecuteAsync<ListResponse<CustomerAttribute>>(
            HttpMethod.Get,
            $"/projects/{projectId}/customers/{Uri.EscapeDataString(customerId)}/attributes{query}",
            cancellationToken: cancellationToken);
    }

    public Task<ListResponse<CustomerAttribute>> SetAttributesAsync(
        string projectId,
        string customerId,
        SetCustomerAttributesRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(projectId);
        ArgumentException.ThrowIfNullOrWhiteSpace(customerId);
        ArgumentNullException.ThrowIfNull(request);

        return executor.ExecuteAsync<ListResponse<CustomerAttribute>>(
            HttpMethod.Post,
            $"/projects/{projectId}/customers/{Uri.EscapeDataString(customerId)}/attributes",
            request,
            cancellationToken);
    }

    public Task<ListResponse<CustomerEntitlement>> ListActiveEntitlementsAsync(
        string projectId,
        string customerId,
        int? limit = null,
        string? startingAfter = null,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(projectId);
        ArgumentException.ThrowIfNullOrWhiteSpace(customerId);

        var query = QueryStringBuilder.Build(limit, startingAfter);
        return executor.ExecuteAsync<ListResponse<CustomerEntitlement>>(
            HttpMethod.Get,
            $"/projects/{projectId}/customers/{Uri.EscapeDataString(customerId)}/active_entitlements{query}",
            cancellationToken: cancellationToken);
    }

    public Task<ListResponse<VirtualCurrencyBalance>> ListVirtualCurrencyBalancesAsync(
        string projectId,
        string customerId,
        bool? includeEmptyBalances = null,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(projectId);
        ArgumentException.ThrowIfNullOrWhiteSpace(customerId);

        var query = QueryStringBuilder.BuildWithBoolParam("include_empty_balances", includeEmptyBalances);
        return executor.ExecuteAsync<ListResponse<VirtualCurrencyBalance>>(
            HttpMethod.Get,
            $"/projects/{projectId}/customers/{Uri.EscapeDataString(customerId)}/virtual_currency_balances{query}",
            cancellationToken: cancellationToken);
    }

    public Task<VirtualCurrencyBalance> CreateVirtualCurrencyTransactionAsync(
        string projectId,
        string customerId,
        CreateVirtualCurrencyTransactionRequest request,
        string? idempotencyKey = null,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(projectId);
        ArgumentException.ThrowIfNullOrWhiteSpace(customerId);
        ArgumentNullException.ThrowIfNull(request);

        return executor.ExecuteAsync<VirtualCurrencyBalance>(
            HttpMethod.Post,
            $"/projects/{projectId}/customers/{Uri.EscapeDataString(customerId)}/virtual_currency_transactions",
            request,
            cancellationToken,
            idempotencyKey);
    }

    public Task<VirtualCurrencyBalance> UpdateVirtualCurrencyBalanceAsync(
        string projectId,
        string customerId,
        string currencyCode,
        UpdateVirtualCurrencyBalanceRequest request,
        string? idempotencyKey = null,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(projectId);
        ArgumentException.ThrowIfNullOrWhiteSpace(customerId);
        ArgumentException.ThrowIfNullOrWhiteSpace(currencyCode);
        ArgumentNullException.ThrowIfNull(request);

        return executor.ExecuteAsync<VirtualCurrencyBalance>(
            HttpMethod.Put,
            $"/projects/{projectId}/customers/{Uri.EscapeDataString(customerId)}/virtual_currency_balances/{Uri.EscapeDataString(currencyCode)}",
            request,
            cancellationToken,
            idempotencyKey);
    }
}
