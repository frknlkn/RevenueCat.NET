using Moq;
using RevenueCat.NET.Models.Common;
using RevenueCat.NET.Models.Entitlements;
using RevenueCat.NET.Models.Purchases;
using RevenueCat.NET.Services;

namespace RevenueCat.NET.Tests.Services;

public class PurchaseServiceTests
{
    private readonly Mock<IHttpRequestExecutor> _mockExecutor;
    private readonly PurchaseService _service;

    public PurchaseServiceTests()
    {
        _mockExecutor = new Mock<IHttpRequestExecutor>();
        _service = new PurchaseService(_mockExecutor.Object);
    }

    [Fact]
    public async Task ListPurchasesAsync_BuildsCorrectUrl()
    {
        // Arrange
        var projectId = "proj_123";
        var customerId = "cust_456";
        var expectedResponse = new ListResponse<Purchase>();
        
        _mockExecutor
            .Setup(x => x.ExecuteAsync<ListResponse<Purchase>>(
                HttpMethod.Get,
                "/projects/proj_123/customers/cust_456/purchases",
                null,
                default,
                null))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _service.ListPurchasesAsync(projectId, customerId);

        // Assert
        Assert.Same(expectedResponse, result);
        _mockExecutor.Verify();
    }

    [Fact]
    public async Task ListPurchasesAsync_WithEnvironmentFilter_BuildsCorrectUrl()
    {
        // Arrange
        var projectId = "proj_123";
        var customerId = "cust_456";
        var expectedResponse = new ListResponse<Purchase>();
        
        _mockExecutor
            .Setup(x => x.ExecuteAsync<ListResponse<Purchase>>(
                HttpMethod.Get,
                "/projects/proj_123/customers/cust_456/purchases?environment=production",
                null,
                default,
                null))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _service.ListPurchasesAsync(projectId, customerId, environment: "production");

        // Assert
        Assert.Same(expectedResponse, result);
        _mockExecutor.Verify();
    }

    [Fact]
    public async Task ListPurchasesAsync_WithPagination_BuildsCorrectUrl()
    {
        // Arrange
        var projectId = "proj_123";
        var customerId = "cust_456";
        var expectedResponse = new ListResponse<Purchase>();
        
        _mockExecutor
            .Setup(x => x.ExecuteAsync<ListResponse<Purchase>>(
                HttpMethod.Get,
                "/projects/proj_123/customers/cust_456/purchases?limit=10&starting_after=pur_789",
                null,
                default,
                null))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _service.ListPurchasesAsync(projectId, customerId, limit: 10, startingAfter: "pur_789");

        // Assert
        Assert.Same(expectedResponse, result);
        _mockExecutor.Verify();
    }

    [Fact]
    public async Task GetPurchaseAsync_BuildsCorrectUrl()
    {
        // Arrange
        var projectId = "proj_123";
        var customerId = "cust_456";
        var purchaseId = "pur_789";
        var expectedResponse = new Purchase();
        
        _mockExecutor
            .Setup(x => x.ExecuteAsync<Purchase>(
                HttpMethod.Get,
                "/projects/proj_123/customers/cust_456/purchases/pur_789",
                null,
                default,
                null))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _service.GetPurchaseAsync(projectId, customerId, purchaseId);

        // Assert
        Assert.Same(expectedResponse, result);
        _mockExecutor.Verify();
    }

    [Fact]
    public async Task SearchPurchasesAsync_BuildsCorrectUrl()
    {
        // Arrange
        var projectId = "proj_123";
        var storeIdentifier = "store_pur_123";
        var expectedResponse = new ListResponse<Purchase>();
        
        _mockExecutor
            .Setup(x => x.ExecuteAsync<ListResponse<Purchase>>(
                HttpMethod.Get,
                It.Is<string>(s => s.StartsWith("/projects/proj_123/purchases?store_purchase_identifier=")),
                null,
                default,
                null))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _service.SearchPurchasesAsync(projectId, storeIdentifier);

        // Assert
        Assert.Same(expectedResponse, result);
        _mockExecutor.Verify();
    }

    [Fact]
    public async Task RefundPurchaseAsync_BuildsCorrectUrl()
    {
        // Arrange
        var projectId = "proj_123";
        var customerId = "cust_456";
        var purchaseId = "pur_789";
        var expectedResponse = new Purchase();
        
        _mockExecutor
            .Setup(x => x.ExecuteAsync<Purchase>(
                HttpMethod.Post,
                "/projects/proj_123/customers/cust_456/purchases/pur_789/actions/refund",
                null,
                default,
                null))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _service.RefundPurchaseAsync(projectId, customerId, purchaseId);

        // Assert
        Assert.Same(expectedResponse, result);
        _mockExecutor.Verify();
    }

    [Fact]
    public async Task ListPurchaseEntitlementsAsync_BuildsCorrectUrl()
    {
        // Arrange
        var projectId = "proj_123";
        var customerId = "cust_456";
        var purchaseId = "pur_789";
        var expectedResponse = new ListResponse<Entitlement>();
        
        _mockExecutor
            .Setup(x => x.ExecuteAsync<ListResponse<Entitlement>>(
                HttpMethod.Get,
                "/projects/proj_123/customers/cust_456/purchases/pur_789/entitlements",
                null,
                default,
                null))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _service.ListPurchaseEntitlementsAsync(projectId, customerId, purchaseId);

        // Assert
        Assert.Same(expectedResponse, result);
        _mockExecutor.Verify();
    }

    [Fact]
    public async Task ListPurchaseEntitlementsAsync_WithPagination_BuildsCorrectUrl()
    {
        // Arrange
        var projectId = "proj_123";
        var customerId = "cust_456";
        var purchaseId = "pur_789";
        var expectedResponse = new ListResponse<Entitlement>();
        
        _mockExecutor
            .Setup(x => x.ExecuteAsync<ListResponse<Entitlement>>(
                HttpMethod.Get,
                "/projects/proj_123/customers/cust_456/purchases/pur_789/entitlements?limit=5&starting_after=ent_123",
                null,
                default,
                null))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _service.ListPurchaseEntitlementsAsync(projectId, customerId, purchaseId, limit: 5, startingAfter: "ent_123");

        // Assert
        Assert.Same(expectedResponse, result);
        _mockExecutor.Verify();
    }
}
