using System.Text.Json;
using Moq;
using RevenueCat.NET.Models;
using RevenueCat.NET.Models.Common;
using RevenueCat.NET.Models.Customers;
using RevenueCat.NET.Services;

namespace RevenueCat.NET.Tests.Services;

public class CustomerServiceTests
{
    private readonly Mock<IHttpRequestExecutor> _mockExecutor;
    private readonly CustomerService _service;

    public CustomerServiceTests()
    {
        _mockExecutor = new Mock<IHttpRequestExecutor>();
        _service = new CustomerService(_mockExecutor.Object);
    }

    [Fact]
    public async Task ListAsync_BuildsCorrectUrl()
    {
        // Arrange
        var projectId = "proj_123";
        var expectedResponse = new ListResponse<Customer>();
        
        _mockExecutor
            .Setup(x => x.ExecuteAsync<ListResponse<Customer>>(
                HttpMethod.Get,
                "/projects/proj_123/customers",
                null,
                default,
                null))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _service.ListAsync(projectId);

        // Assert
        Assert.Same(expectedResponse, result);
        _mockExecutor.Verify();
    }

    [Fact]
    public async Task ListAsync_WithPagination_BuildsCorrectUrl()
    {
        // Arrange
        var projectId = "proj_123";
        var expectedResponse = new ListResponse<Customer>();
        
        _mockExecutor
            .Setup(x => x.ExecuteAsync<ListResponse<Customer>>(
                HttpMethod.Get,
                "/projects/proj_123/customers?limit=10&starting_after=cust_456",
                null,
                default,
                null))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _service.ListAsync(projectId, limit: 10, startingAfter: "cust_456");

        // Assert
        Assert.Same(expectedResponse, result);
        _mockExecutor.Verify();
    }

    [Fact]
    public async Task ListAsync_WithSearch_BuildsCorrectUrl()
    {
        // Arrange
        var projectId = "proj_123";
        var expectedResponse = new ListResponse<Customer>();
        
        _mockExecutor
            .Setup(x => x.ExecuteAsync<ListResponse<Customer>>(
                HttpMethod.Get,
                It.Is<string>(s => s.StartsWith("/projects/proj_123/customers?") && s.Contains("search=")),
                null,
                default,
                null))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _service.ListAsync(projectId, search: "test@example.com");

        // Assert
        Assert.Same(expectedResponse, result);
        _mockExecutor.Verify();
    }

    [Fact]
    public async Task GetAsync_BuildsCorrectUrl()
    {
        // Arrange
        var projectId = "proj_123";
        var customerId = "cust_456";
        var expectedResponse = new Customer();
        
        _mockExecutor
            .Setup(x => x.ExecuteAsync<Customer>(
                HttpMethod.Get,
                "/projects/proj_123/customers/cust_456",
                null,
                default,
                null))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _service.GetAsync(projectId, customerId);

        // Assert
        Assert.Same(expectedResponse, result);
        _mockExecutor.Verify();
    }

    [Fact]
    public async Task GetAsync_WithExpand_BuildsCorrectUrl()
    {
        // Arrange
        var projectId = "proj_123";
        var customerId = "cust_456";
        var expand = new[] { "attributes", "active_entitlements" };
        var expectedResponse = new Customer();
        
        _mockExecutor
            .Setup(x => x.ExecuteAsync<Customer>(
                HttpMethod.Get,
                It.Is<string>(s => s.StartsWith("/projects/proj_123/customers/cust_456?expand=")),
                null,
                default,
                null))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _service.GetAsync(projectId, customerId, expand);

        // Assert
        Assert.Same(expectedResponse, result);
        _mockExecutor.Verify();
    }

    [Fact]
    public async Task CreateAsync_BuildsCorrectUrl()
    {
        // Arrange
        var projectId = "proj_123";
        var request = new CreateCustomerRequest("cust_456");
        var expectedResponse = new Customer();
        
        _mockExecutor
            .Setup(x => x.ExecuteAsync<Customer>(
                HttpMethod.Post,
                "/projects/proj_123/customers",
                request,
                default,
                null))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _service.CreateAsync(projectId, request);

        // Assert
        Assert.Same(expectedResponse, result);
        _mockExecutor.Verify();
    }

    [Fact]
    public async Task DeleteAsync_BuildsCorrectUrl()
    {
        // Arrange
        var projectId = "proj_123";
        var customerId = "cust_456";
        var expectedResponse = new DeletedObject("customer", "cust_456", 1699564800000);
        
        _mockExecutor
            .Setup(x => x.ExecuteAsync<DeletedObject>(
                HttpMethod.Delete,
                "/projects/proj_123/customers/cust_456",
                null,
                default,
                null))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _service.DeleteAsync(projectId, customerId);

        // Assert
        Assert.Same(expectedResponse, result);
        _mockExecutor.Verify();
    }

    [Fact]
    public async Task TransferAsync_BuildsCorrectUrl()
    {
        // Arrange
        var projectId = "proj_123";
        var customerId = "cust_456";
        var request = new TransferCustomerRequest("cust_789");
        var expectedResponse = new TransferResponse("transfer", "cust_456", "cust_789", 1699564800000);
        
        _mockExecutor
            .Setup(x => x.ExecuteAsync<TransferResponse>(
                HttpMethod.Post,
                "/projects/proj_123/customers/cust_456/actions/transfer",
                request,
                default,
                null))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _service.TransferAsync(projectId, customerId, request);

        // Assert
        Assert.Same(expectedResponse, result);
        _mockExecutor.Verify();
    }

    [Fact]
    public async Task ListAliasesAsync_BuildsCorrectUrl()
    {
        // Arrange
        var projectId = "proj_123";
        var customerId = "cust_456";
        var expectedResponse = new ListResponse<CustomerAlias>();
        
        _mockExecutor
            .Setup(x => x.ExecuteAsync<ListResponse<CustomerAlias>>(
                HttpMethod.Get,
                "/projects/proj_123/customers/cust_456/aliases",
                null,
                default,
                null))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _service.ListAliasesAsync(projectId, customerId);

        // Assert
        Assert.Same(expectedResponse, result);
        _mockExecutor.Verify();
    }

    [Fact]
    public async Task ListAttributesAsync_BuildsCorrectUrl()
    {
        // Arrange
        var projectId = "proj_123";
        var customerId = "cust_456";
        var expectedResponse = new ListResponse<CustomerAttribute>();
        
        _mockExecutor
            .Setup(x => x.ExecuteAsync<ListResponse<CustomerAttribute>>(
                HttpMethod.Get,
                "/projects/proj_123/customers/cust_456/attributes",
                null,
                default,
                null))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _service.ListAttributesAsync(projectId, customerId);

        // Assert
        Assert.Same(expectedResponse, result);
        _mockExecutor.Verify();
    }

    [Fact]
    public async Task SetAttributesAsync_BuildsCorrectUrl()
    {
        // Arrange
        var projectId = "proj_123";
        var customerId = "cust_456";
        var request = new SetCustomerAttributesRequest(new List<CustomerAttribute>());
        var expectedResponse = new ListResponse<CustomerAttribute>();
        
        _mockExecutor
            .Setup(x => x.ExecuteAsync<ListResponse<CustomerAttribute>>(
                HttpMethod.Post,
                "/projects/proj_123/customers/cust_456/attributes",
                request,
                default,
                null))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _service.SetAttributesAsync(projectId, customerId, request);

        // Assert
        Assert.Same(expectedResponse, result);
        _mockExecutor.Verify();
    }

    [Fact]
    public async Task ListActiveEntitlementsAsync_BuildsCorrectUrl()
    {
        // Arrange
        var projectId = "proj_123";
        var customerId = "cust_456";
        var expectedResponse = new ListResponse<CustomerEntitlement>();
        
        _mockExecutor
            .Setup(x => x.ExecuteAsync<ListResponse<CustomerEntitlement>>(
                HttpMethod.Get,
                "/projects/proj_123/customers/cust_456/active_entitlements",
                null,
                default,
                null))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _service.ListActiveEntitlementsAsync(projectId, customerId);

        // Assert
        Assert.Same(expectedResponse, result);
        _mockExecutor.Verify();
    }

    [Fact]
    public async Task ListVirtualCurrencyBalancesAsync_BuildsCorrectUrl()
    {
        // Arrange
        var projectId = "proj_123";
        var customerId = "cust_456";
        var expectedResponse = new ListResponse<VirtualCurrencyBalance>();
        
        _mockExecutor
            .Setup(x => x.ExecuteAsync<ListResponse<VirtualCurrencyBalance>>(
                HttpMethod.Get,
                "/projects/proj_123/customers/cust_456/virtual_currency_balances",
                null,
                default,
                null))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _service.ListVirtualCurrencyBalancesAsync(projectId, customerId);

        // Assert
        Assert.Same(expectedResponse, result);
        _mockExecutor.Verify();
    }

    [Fact]
    public async Task ListVirtualCurrencyBalancesAsync_WithIncludeEmpty_BuildsCorrectUrl()
    {
        // Arrange
        var projectId = "proj_123";
        var customerId = "cust_456";
        var expectedResponse = new ListResponse<VirtualCurrencyBalance>();
        
        _mockExecutor
            .Setup(x => x.ExecuteAsync<ListResponse<VirtualCurrencyBalance>>(
                HttpMethod.Get,
                "/projects/proj_123/customers/cust_456/virtual_currency_balances?include_empty_balances=true",
                null,
                default,
                null))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _service.ListVirtualCurrencyBalancesAsync(projectId, customerId, includeEmptyBalances: true);

        // Assert
        Assert.Same(expectedResponse, result);
        _mockExecutor.Verify();
    }

    [Fact]
    public async Task CreateVirtualCurrencyTransactionAsync_BuildsCorrectUrl()
    {
        // Arrange
        var projectId = "proj_123";
        var customerId = "cust_456";
        var request = new CreateVirtualCurrencyTransactionRequest("gold", 100);
        var expectedResponse = new VirtualCurrencyBalance();
        
        _mockExecutor
            .Setup(x => x.ExecuteAsync<VirtualCurrencyBalance>(
                HttpMethod.Post,
                "/projects/proj_123/customers/cust_456/virtual_currency_transactions",
                request,
                default,
                null))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _service.CreateVirtualCurrencyTransactionAsync(projectId, customerId, request);

        // Assert
        Assert.Same(expectedResponse, result);
        _mockExecutor.Verify();
    }

    [Fact]
    public async Task CreateVirtualCurrencyTransactionAsync_WithIdempotencyKey_PassesKey()
    {
        // Arrange
        var projectId = "proj_123";
        var customerId = "cust_456";
        var request = new CreateVirtualCurrencyTransactionRequest("gold", 100);
        var idempotencyKey = "key_123";
        var expectedResponse = new VirtualCurrencyBalance();
        
        _mockExecutor
            .Setup(x => x.ExecuteAsync<VirtualCurrencyBalance>(
                HttpMethod.Post,
                "/projects/proj_123/customers/cust_456/virtual_currency_transactions",
                request,
                default,
                idempotencyKey))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _service.CreateVirtualCurrencyTransactionAsync(projectId, customerId, request, idempotencyKey);

        // Assert
        Assert.Same(expectedResponse, result);
        _mockExecutor.Verify();
    }

    [Fact]
    public async Task UpdateVirtualCurrencyBalanceAsync_BuildsCorrectUrl()
    {
        // Arrange
        var projectId = "proj_123";
        var customerId = "cust_456";
        var currencyCode = "gold";
        var request = new UpdateVirtualCurrencyBalanceRequest(500);
        var expectedResponse = new VirtualCurrencyBalance();
        
        _mockExecutor
            .Setup(x => x.ExecuteAsync<VirtualCurrencyBalance>(
                HttpMethod.Put,
                "/projects/proj_123/customers/cust_456/virtual_currency_balances/gold",
                request,
                default,
                null))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _service.UpdateVirtualCurrencyBalanceAsync(projectId, customerId, currencyCode, request);

        // Assert
        Assert.Same(expectedResponse, result);
        _mockExecutor.Verify();
    }

    [Fact]
    public async Task UpdateVirtualCurrencyBalanceAsync_WithIdempotencyKey_PassesKey()
    {
        // Arrange
        var projectId = "proj_123";
        var customerId = "cust_456";
        var currencyCode = "gold";
        var request = new UpdateVirtualCurrencyBalanceRequest(500);
        var idempotencyKey = "key_456";
        var expectedResponse = new VirtualCurrencyBalance();
        
        _mockExecutor
            .Setup(x => x.ExecuteAsync<VirtualCurrencyBalance>(
                HttpMethod.Put,
                "/projects/proj_123/customers/cust_456/virtual_currency_balances/gold",
                request,
                default,
                idempotencyKey))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _service.UpdateVirtualCurrencyBalanceAsync(projectId, customerId, currencyCode, request, idempotencyKey);

        // Assert
        Assert.Same(expectedResponse, result);
        _mockExecutor.Verify();
    }
}
