using Refit;
using RevenueCat.NET.Models;
using RevenueCat.NET.Models.Common;
using RevenueCat.NET.Models.Customers;

namespace RevenueCat.NET.Services;

/// <summary>
/// Service for managing customers in RevenueCat.
/// </summary>
/// <remarks>
/// API Documentation: <see href="https://www.revenuecat.com/docs/api-v2#tag/Customers"/>
/// </remarks>
public interface ICustomerService
{
    /// <summary>
    /// Lists all customers for a project.
    /// </summary>
    [Get("/v2/projects/{projectId}/customers")]
    Task<ListResponse<Customer>> ListAsync(
        string projectId,
        [Query] int? limit = null,
        [AliasAs("starting_after")] [Query] string? startingAfter = null,
        [Query] string? search = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a specific customer by ID.
    /// </summary>
    [Get("/v2/projects/{projectId}/customers/{customerId}")]
    Task<Customer> GetAsync(
        string projectId,
        string customerId,
        [Query(CollectionFormat.Multi)] string[]? expand = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a new customer.
    /// </summary>
    [Post("/v2/projects/{projectId}/customers")]
    Task<Customer> CreateAsync(
        string projectId,
        [Body] CreateCustomerRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a customer permanently.
    /// </summary>
    [Delete("/v2/projects/{projectId}/customers/{customerId}")]
    Task<DeletedObject> DeleteAsync(
        string projectId,
        string customerId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Transfers customer data from one customer to another.
    /// </summary>
    [Post("/v2/projects/{projectId}/customers/{customerId}/actions/transfer")]
    Task<TransferResponse> TransferAsync(
        string projectId,
        string customerId,
        [Body] TransferCustomerRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Lists all aliases for a customer.
    /// </summary>
    [Get("/v2/projects/{projectId}/customers/{customerId}/aliases")]
    Task<ListResponse<CustomerAlias>> ListAliasesAsync(
        string projectId,
        string customerId,
        [Query] int? limit = null,
        [AliasAs("starting_after")] [Query] string? startingAfter = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Lists all attributes for a customer.
    /// </summary>
    [Get("/v2/projects/{projectId}/customers/{customerId}/attributes")]
    Task<ListResponse<CustomerAttribute>> ListAttributesAsync(
        string projectId,
        string customerId,
        [Query] int? limit = null,
        [AliasAs("starting_after")] [Query] string? startingAfter = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Sets or updates customer attributes in bulk.
    /// </summary>
    [Post("/v2/projects/{projectId}/customers/{customerId}/attributes")]
    Task<ListResponse<CustomerAttribute>> SetAttributesAsync(
        string projectId,
        string customerId,
        [Body] SetCustomerAttributesRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Lists all active entitlements for a customer.
    /// </summary>
    [Get("/v2/projects/{projectId}/customers/{customerId}/active_entitlements")]
    Task<ListResponse<CustomerEntitlement>> ListActiveEntitlementsAsync(
        string projectId,
        string customerId,
        [Query] int? limit = null,
        [AliasAs("starting_after")] [Query] string? startingAfter = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Lists virtual currency balances for a customer.
    /// </summary>
    [Get("/v2/projects/{projectId}/customers/{customerId}/virtual_currency_balances")]
    Task<ListResponse<VirtualCurrencyBalance>> ListVirtualCurrencyBalancesAsync(
        string projectId,
        string customerId,
        [AliasAs("include_empty_balances")] [Query] bool? includeEmptyBalances = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a virtual currency transaction for a customer.
    /// </summary>
    [Post("/v2/projects/{projectId}/customers/{customerId}/virtual_currency_transactions")]
    Task<VirtualCurrencyBalance> CreateVirtualCurrencyTransactionAsync(
        string projectId,
        string customerId,
        [Body] CreateVirtualCurrencyTransactionRequest request,
        [Header("Idempotency-Key")] string? idempotencyKey = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates a virtual currency balance for a customer.
    /// </summary>
    [Put("/v2/projects/{projectId}/customers/{customerId}/virtual_currency_balances/{currencyCode}")]
    Task<VirtualCurrencyBalance> UpdateVirtualCurrencyBalanceAsync(
        string projectId,
        string customerId,
        string currencyCode,
        [Body] UpdateVirtualCurrencyBalanceRequest request,
        [Header("Idempotency-Key")] string? idempotencyKey = null,
        CancellationToken cancellationToken = default);
}

/// <summary>
/// Request to create a new customer.
/// </summary>
/// <param name="Id">The customer ID.</param>
/// <param name="Attributes">Optional customer attributes.</param>
public sealed record CreateCustomerRequest(
    string Id,
    IReadOnlyList<CustomerAttribute>? Attributes = null
);

/// <summary>
/// Request to transfer customer data from one customer to another.
/// </summary>
/// <param name="TargetCustomerId">The target customer ID to transfer data to.</param>
/// <param name="AppIds">Optional list of app IDs to filter the transfer.</param>
public sealed record TransferCustomerRequest(
    string TargetCustomerId,
    IReadOnlyList<string>? AppIds = null
);

/// <summary>
/// Response from a customer transfer operation.
/// </summary>
/// <param name="Object">The object type (always "transfer").</param>
/// <param name="SourceCustomerId">The source customer ID.</param>
/// <param name="TargetCustomerId">The target customer ID.</param>
/// <param name="TransferredAt">The timestamp when the transfer occurred (milliseconds since epoch).</param>
public sealed record TransferResponse(
    string Object,
    string SourceCustomerId,
    string TargetCustomerId,
    long TransferredAt
);

/// <summary>
/// Request to set or update customer attributes.
/// </summary>
/// <param name="Attributes">The list of attributes to set or update.</param>
public sealed record SetCustomerAttributesRequest(
    IReadOnlyList<CustomerAttribute> Attributes
);

/// <summary>
/// Request to create a virtual currency transaction.
/// </summary>
/// <param name="CurrencyCode">The currency code.</param>
/// <param name="Amount">The transaction amount (can be positive or negative).</param>
public sealed record CreateVirtualCurrencyTransactionRequest(
    string CurrencyCode,
    int Amount
);

/// <summary>
/// Request to update a virtual currency balance.
/// </summary>
/// <param name="Balance">The new balance value.</param>
public sealed record UpdateVirtualCurrencyBalanceRequest(
    int Balance
);
