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
    /// <param name="projectId">The project ID.</param>
    /// <param name="limit">Maximum number of items to return (default: 20, max: 100).</param>
    /// <param name="startingAfter">Cursor for pagination.</param>
    /// <param name="search">Search customers by email address.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A paginated list of customers.</returns>
    /// <exception cref="ArgumentException">Thrown when projectId is null or whitespace.</exception>
    /// <exception cref="RevenueCat.NET.Exceptions.RevenueCatException">Thrown when the API returns an error.</exception>
    Task<ListResponse<Customer>> ListAsync(
        string projectId,
        int? limit = null,
        string? startingAfter = null,
        string? search = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a specific customer by ID.
    /// </summary>
    /// <param name="projectId">The project ID.</param>
    /// <param name="customerId">The customer ID.</param>
    /// <param name="expand">Optional fields to expand (e.g., "attributes", "active_entitlements").</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The customer details.</returns>
    /// <exception cref="ArgumentException">Thrown when projectId or customerId is null or whitespace.</exception>
    /// <exception cref="RevenueCat.NET.Exceptions.RevenueCatResourceNotFoundException">Thrown when the customer is not found.</exception>
    /// <exception cref="RevenueCat.NET.Exceptions.RevenueCatException">Thrown when the API returns an error.</exception>
    Task<Customer> GetAsync(
        string projectId,
        string customerId,
        string[]? expand = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a new customer.
    /// </summary>
    /// <param name="projectId">The project ID.</param>
    /// <param name="request">The customer creation request.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The created customer.</returns>
    /// <exception cref="ArgumentException">Thrown when projectId is null or whitespace.</exception>
    /// <exception cref="ArgumentNullException">Thrown when request is null.</exception>
    /// <exception cref="RevenueCat.NET.Exceptions.RevenueCatConflictException">Thrown when a customer with the same ID already exists.</exception>
    /// <exception cref="RevenueCat.NET.Exceptions.RevenueCatException">Thrown when the API returns an error.</exception>
    Task<Customer> CreateAsync(
        string projectId,
        CreateCustomerRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a customer permanently.
    /// </summary>
    /// <param name="projectId">The project ID.</param>
    /// <param name="customerId">The customer ID to delete.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A deleted object confirmation.</returns>
    /// <exception cref="ArgumentException">Thrown when projectId or customerId is null or whitespace.</exception>
    /// <exception cref="RevenueCat.NET.Exceptions.RevenueCatResourceNotFoundException">Thrown when the customer is not found.</exception>
    /// <exception cref="RevenueCat.NET.Exceptions.RevenueCatException">Thrown when the API returns an error.</exception>
    Task<DeletedObject> DeleteAsync(
        string projectId,
        string customerId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Transfers customer data from one customer to another.
    /// </summary>
    /// <param name="projectId">The project ID.</param>
    /// <param name="customerId">The source customer ID.</param>
    /// <param name="request">The transfer request containing the target customer ID and optional app IDs filter.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The transfer response with details about the operation.</returns>
    /// <exception cref="ArgumentException">Thrown when projectId or customerId is null or whitespace.</exception>
    /// <exception cref="ArgumentNullException">Thrown when request is null.</exception>
    /// <exception cref="RevenueCat.NET.Exceptions.RevenueCatException">Thrown when the API returns an error.</exception>
    Task<TransferResponse> TransferAsync(
        string projectId,
        string customerId,
        TransferCustomerRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Lists all aliases for a customer.
    /// </summary>
    /// <param name="projectId">The project ID.</param>
    /// <param name="customerId">The customer ID.</param>
    /// <param name="limit">Maximum number of items to return (default: 20, max: 100).</param>
    /// <param name="startingAfter">Cursor for pagination.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A paginated list of customer aliases.</returns>
    /// <exception cref="ArgumentException">Thrown when projectId or customerId is null or whitespace.</exception>
    /// <exception cref="RevenueCat.NET.Exceptions.RevenueCatException">Thrown when the API returns an error.</exception>
    Task<ListResponse<CustomerAlias>> ListAliasesAsync(
        string projectId,
        string customerId,
        int? limit = null,
        string? startingAfter = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Lists all attributes for a customer.
    /// </summary>
    /// <param name="projectId">The project ID.</param>
    /// <param name="customerId">The customer ID.</param>
    /// <param name="limit">Maximum number of items to return (default: 20, max: 100).</param>
    /// <param name="startingAfter">Cursor for pagination.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A paginated list of customer attributes.</returns>
    /// <exception cref="ArgumentException">Thrown when projectId or customerId is null or whitespace.</exception>
    /// <exception cref="RevenueCat.NET.Exceptions.RevenueCatException">Thrown when the API returns an error.</exception>
    Task<ListResponse<CustomerAttribute>> ListAttributesAsync(
        string projectId,
        string customerId,
        int? limit = null,
        string? startingAfter = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Sets or updates customer attributes in bulk.
    /// </summary>
    /// <param name="projectId">The project ID.</param>
    /// <param name="customerId">The customer ID.</param>
    /// <param name="request">The attributes to set or update.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The updated list of customer attributes.</returns>
    /// <exception cref="ArgumentException">Thrown when projectId or customerId is null or whitespace.</exception>
    /// <exception cref="ArgumentNullException">Thrown when request is null.</exception>
    /// <exception cref="RevenueCat.NET.Exceptions.RevenueCatException">Thrown when the API returns an error.</exception>
    Task<ListResponse<CustomerAttribute>> SetAttributesAsync(
        string projectId,
        string customerId,
        SetCustomerAttributesRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Lists all active entitlements for a customer.
    /// </summary>
    /// <param name="projectId">The project ID.</param>
    /// <param name="customerId">The customer ID.</param>
    /// <param name="limit">Maximum number of items to return (default: 20, max: 100).</param>
    /// <param name="startingAfter">Cursor for pagination.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A paginated list of active entitlements.</returns>
    /// <exception cref="ArgumentException">Thrown when projectId or customerId is null or whitespace.</exception>
    /// <exception cref="RevenueCat.NET.Exceptions.RevenueCatException">Thrown when the API returns an error.</exception>
    Task<ListResponse<CustomerEntitlement>> ListActiveEntitlementsAsync(
        string projectId,
        string customerId,
        int? limit = null,
        string? startingAfter = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Lists virtual currency balances for a customer.
    /// </summary>
    /// <param name="projectId">The project ID.</param>
    /// <param name="customerId">The customer ID.</param>
    /// <param name="includeEmptyBalances">Whether to include currencies with zero balance.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A list of virtual currency balances.</returns>
    /// <exception cref="ArgumentException">Thrown when projectId or customerId is null or whitespace.</exception>
    /// <exception cref="RevenueCat.NET.Exceptions.RevenueCatException">Thrown when the API returns an error.</exception>
    Task<ListResponse<VirtualCurrencyBalance>> ListVirtualCurrencyBalancesAsync(
        string projectId,
        string customerId,
        bool? includeEmptyBalances = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a virtual currency transaction for a customer.
    /// </summary>
    /// <param name="projectId">The project ID.</param>
    /// <param name="customerId">The customer ID.</param>
    /// <param name="request">The transaction request containing currency code and amount.</param>
    /// <param name="idempotencyKey">Optional idempotency key to prevent duplicate transactions.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The updated virtual currency balance.</returns>
    /// <exception cref="ArgumentException">Thrown when projectId or customerId is null or whitespace.</exception>
    /// <exception cref="ArgumentNullException">Thrown when request is null.</exception>
    /// <exception cref="RevenueCat.NET.Exceptions.RevenueCatException">Thrown when the API returns an error.</exception>
    Task<VirtualCurrencyBalance> CreateVirtualCurrencyTransactionAsync(
        string projectId,
        string customerId,
        CreateVirtualCurrencyTransactionRequest request,
        string? idempotencyKey = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates a virtual currency balance for a customer.
    /// </summary>
    /// <param name="projectId">The project ID.</param>
    /// <param name="customerId">The customer ID.</param>
    /// <param name="currencyCode">The currency code to update.</param>
    /// <param name="request">The balance update request.</param>
    /// <param name="idempotencyKey">Optional idempotency key to prevent duplicate updates.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The updated virtual currency balance.</returns>
    /// <exception cref="ArgumentException">Thrown when projectId, customerId, or currencyCode is null or whitespace.</exception>
    /// <exception cref="ArgumentNullException">Thrown when request is null.</exception>
    /// <exception cref="RevenueCat.NET.Exceptions.RevenueCatException">Thrown when the API returns an error.</exception>
    Task<VirtualCurrencyBalance> UpdateVirtualCurrencyBalanceAsync(
        string projectId,
        string customerId,
        string currencyCode,
        UpdateVirtualCurrencyBalanceRequest request,
        string? idempotencyKey = null,
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
