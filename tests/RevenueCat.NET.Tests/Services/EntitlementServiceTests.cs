using Moq;
using RevenueCat.NET.Models;
using RevenueCat.NET.Models.Common;
using RevenueCat.NET.Models.Entitlements;
using RevenueCat.NET.Models.Products;
using RevenueCat.NET.Services;

namespace RevenueCat.NET.Tests.Services;

public class EntitlementServiceTests
{
    private readonly Mock<IHttpRequestExecutor> _mockExecutor;
    private readonly EntitlementService _service;

    public EntitlementServiceTests()
    {
        _mockExecutor = new Mock<IHttpRequestExecutor>();
        _service = new EntitlementService(_mockExecutor.Object);
    }

    [Fact]
    public async Task ListAsync_BuildsCorrectUrl()
    {
        // Arrange
        var projectId = "proj_123";
        var expectedResponse = new ListResponse<Entitlement>();
        
        _mockExecutor
            .Setup(x => x.ExecuteAsync<ListResponse<Entitlement>>(
                HttpMethod.Get,
                "/projects/proj_123/entitlements",
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
        var expectedResponse = new ListResponse<Entitlement>();
        
        _mockExecutor
            .Setup(x => x.ExecuteAsync<ListResponse<Entitlement>>(
                HttpMethod.Get,
                "/projects/proj_123/entitlements?limit=10&starting_after=ent_789",
                null,
                default,
                null))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _service.ListAsync(projectId, limit: 10, startingAfter: "ent_789");

        // Assert
        Assert.Same(expectedResponse, result);
        _mockExecutor.Verify();
    }

    [Fact]
    public async Task ListAsync_WithExpand_BuildsCorrectUrl()
    {
        // Arrange
        var projectId = "proj_123";
        var expand = new[] { "products" };
        var expectedResponse = new ListResponse<Entitlement>();
        
        _mockExecutor
            .Setup(x => x.ExecuteAsync<ListResponse<Entitlement>>(
                HttpMethod.Get,
                It.Is<string>(s => s.StartsWith("/projects/proj_123/entitlements?expand=")),
                null,
                default,
                null))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _service.ListAsync(projectId, expand: expand);

        // Assert
        Assert.Same(expectedResponse, result);
        _mockExecutor.Verify();
    }

    [Fact]
    public async Task GetAsync_BuildsCorrectUrl()
    {
        // Arrange
        var projectId = "proj_123";
        var entitlementId = "ent_456";
        var expectedResponse = new Entitlement();
        
        _mockExecutor
            .Setup(x => x.ExecuteAsync<Entitlement>(
                HttpMethod.Get,
                "/projects/proj_123/entitlements/ent_456",
                null,
                default,
                null))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _service.GetAsync(projectId, entitlementId);

        // Assert
        Assert.Same(expectedResponse, result);
        _mockExecutor.Verify();
    }


    [Fact]
    public async Task GetAsync_WithExpand_BuildsCorrectUrl()
    {
        // Arrange
        var projectId = "proj_123";
        var entitlementId = "ent_456";
        var expand = new[] { "products" };
        var expectedResponse = new Entitlement();
        
        _mockExecutor
            .Setup(x => x.ExecuteAsync<Entitlement>(
                HttpMethod.Get,
                It.Is<string>(s => s.StartsWith("/projects/proj_123/entitlements/ent_456?expand=")),
                null,
                default,
                null))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _service.GetAsync(projectId, entitlementId, expand);

        // Assert
        Assert.Same(expectedResponse, result);
        _mockExecutor.Verify();
    }

    [Fact]
    public async Task CreateAsync_BuildsCorrectUrl()
    {
        // Arrange
        var projectId = "proj_123";
        var request = new CreateEntitlementRequest(
            "premium",
            "Premium Access"
        );
        var expectedResponse = new Entitlement();
        
        _mockExecutor
            .Setup(x => x.ExecuteAsync<Entitlement>(
                HttpMethod.Post,
                "/projects/proj_123/entitlements",
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
    public async Task UpdateAsync_BuildsCorrectUrl()
    {
        // Arrange
        var projectId = "proj_123";
        var entitlementId = "ent_456";
        var request = new UpdateEntitlementRequest(
            "Premium Plus Access"
        );
        var expectedResponse = new Entitlement();
        
        _mockExecutor
            .Setup(x => x.ExecuteAsync<Entitlement>(
                HttpMethod.Post,
                "/projects/proj_123/entitlements/ent_456",
                request,
                default,
                null))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _service.UpdateAsync(projectId, entitlementId, request);

        // Assert
        Assert.Same(expectedResponse, result);
        _mockExecutor.Verify();
    }

    [Fact]
    public async Task DeleteAsync_BuildsCorrectUrl()
    {
        // Arrange
        var projectId = "proj_123";
        var entitlementId = "ent_456";
        var expectedResponse = new DeletedObject("entitlement", "ent_456", 1699564800000);
        
        _mockExecutor
            .Setup(x => x.ExecuteAsync<DeletedObject>(
                HttpMethod.Delete,
                "/projects/proj_123/entitlements/ent_456",
                null,
                default,
                null))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _service.DeleteAsync(projectId, entitlementId);

        // Assert
        Assert.Same(expectedResponse, result);
        _mockExecutor.Verify();
    }

    [Fact]
    public async Task AttachProductsAsync_BuildsCorrectUrl()
    {
        // Arrange
        var projectId = "proj_123";
        var entitlementId = "ent_456";
        var request = new AttachProductsRequest(new[] { "prod_789", "prod_012" });
        var expectedResponse = new Entitlement();
        
        _mockExecutor
            .Setup(x => x.ExecuteAsync<Entitlement>(
                HttpMethod.Post,
                "/projects/proj_123/entitlements/ent_456/actions/attach_products",
                request,
                default,
                null))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _service.AttachProductsAsync(projectId, entitlementId, request);

        // Assert
        Assert.Same(expectedResponse, result);
        _mockExecutor.Verify();
    }

    [Fact]
    public async Task DetachProductsAsync_BuildsCorrectUrl()
    {
        // Arrange
        var projectId = "proj_123";
        var entitlementId = "ent_456";
        var request = new DetachProductsRequest(new[] { "prod_789", "prod_012" });
        var expectedResponse = new Entitlement();
        
        _mockExecutor
            .Setup(x => x.ExecuteAsync<Entitlement>(
                HttpMethod.Post,
                "/projects/proj_123/entitlements/ent_456/actions/detach_products",
                request,
                default,
                null))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _service.DetachProductsAsync(projectId, entitlementId, request);

        // Assert
        Assert.Same(expectedResponse, result);
        _mockExecutor.Verify();
    }

    [Fact]
    public async Task GetProductsAsync_BuildsCorrectUrl()
    {
        // Arrange
        var projectId = "proj_123";
        var entitlementId = "ent_456";
        var expectedResponse = new ListResponse<Product>();
        
        _mockExecutor
            .Setup(x => x.ExecuteAsync<ListResponse<Product>>(
                HttpMethod.Get,
                "/projects/proj_123/entitlements/ent_456/products",
                null,
                default,
                null))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _service.GetProductsAsync(projectId, entitlementId);

        // Assert
        Assert.Same(expectedResponse, result);
        _mockExecutor.Verify();
    }

    [Fact]
    public async Task GetProductsAsync_WithPagination_BuildsCorrectUrl()
    {
        // Arrange
        var projectId = "proj_123";
        var entitlementId = "ent_456";
        var expectedResponse = new ListResponse<Product>();
        
        _mockExecutor
            .Setup(x => x.ExecuteAsync<ListResponse<Product>>(
                HttpMethod.Get,
                "/projects/proj_123/entitlements/ent_456/products?limit=10&starting_after=prod_789",
                null,
                default,
                null))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _service.GetProductsAsync(projectId, entitlementId, limit: 10, startingAfter: "prod_789");

        // Assert
        Assert.Same(expectedResponse, result);
        _mockExecutor.Verify();
    }
}
