using Moq;
using RevenueCat.NET.Models;
using RevenueCat.NET.Models.Common;
using RevenueCat.NET.Models.Enums;
using RevenueCat.NET.Models.Products;
using RevenueCat.NET.Services;

namespace RevenueCat.NET.Tests.Services;

public class ProductServiceTests
{
    private readonly Mock<IHttpRequestExecutor> _mockExecutor;
    private readonly ProductService _service;

    public ProductServiceTests()
    {
        _mockExecutor = new Mock<IHttpRequestExecutor>();
        _service = new ProductService(_mockExecutor.Object);
    }

    [Fact]
    public async Task ListAsync_BuildsCorrectUrl()
    {
        // Arrange
        var projectId = "proj_123";
        var expectedResponse = new ListResponse<Product>();
        
        _mockExecutor
            .Setup(x => x.ExecuteAsync<ListResponse<Product>>(
                HttpMethod.Get,
                "/projects/proj_123/products",
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
    public async Task ListAsync_WithAppIdFilter_BuildsCorrectUrl()
    {
        // Arrange
        var projectId = "proj_123";
        var appId = "app_456";
        var expectedResponse = new ListResponse<Product>();
        
        _mockExecutor
            .Setup(x => x.ExecuteAsync<ListResponse<Product>>(
                HttpMethod.Get,
                "/projects/proj_123/products?app_id=app_456",
                null,
                default,
                null))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _service.ListAsync(projectId, appId: appId);

        // Assert
        Assert.Same(expectedResponse, result);
        _mockExecutor.Verify();
    }

    [Fact]
    public async Task ListAsync_WithPagination_BuildsCorrectUrl()
    {
        // Arrange
        var projectId = "proj_123";
        var expectedResponse = new ListResponse<Product>();
        
        _mockExecutor
            .Setup(x => x.ExecuteAsync<ListResponse<Product>>(
                HttpMethod.Get,
                "/projects/proj_123/products?limit=10&starting_after=prod_789",
                null,
                default,
                null))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _service.ListAsync(projectId, limit: 10, startingAfter: "prod_789");

        // Assert
        Assert.Same(expectedResponse, result);
        _mockExecutor.Verify();
    }

    [Fact]
    public async Task ListAsync_WithExpand_BuildsCorrectUrl()
    {
        // Arrange
        var projectId = "proj_123";
        var expand = new[] { "app" };
        var expectedResponse = new ListResponse<Product>();
        
        _mockExecutor
            .Setup(x => x.ExecuteAsync<ListResponse<Product>>(
                HttpMethod.Get,
                It.Is<string>(s => s.StartsWith("/projects/proj_123/products?expand=")),
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
    public async Task ListAsync_WithAllParameters_BuildsCorrectUrl()
    {
        // Arrange
        var projectId = "proj_123";
        var appId = "app_456";
        var expand = new[] { "app" };
        var expectedResponse = new ListResponse<Product>();
        
        _mockExecutor
            .Setup(x => x.ExecuteAsync<ListResponse<Product>>(
                HttpMethod.Get,
                It.Is<string>(s => 
                    s.Contains("/projects/proj_123/products?") &&
                    s.Contains("app_id=app_456") &&
                    s.Contains("limit=5") &&
                    s.Contains("starting_after=prod_999") &&
                    s.Contains("expand=")),
                null,
                default,
                null))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _service.ListAsync(projectId, appId: appId, limit: 5, startingAfter: "prod_999", expand: expand);

        // Assert
        Assert.Same(expectedResponse, result);
        _mockExecutor.Verify();
    }

    [Fact]
    public async Task GetAsync_BuildsCorrectUrl()
    {
        // Arrange
        var projectId = "proj_123";
        var productId = "prod_456";
        var expectedResponse = new Product();
        
        _mockExecutor
            .Setup(x => x.ExecuteAsync<Product>(
                HttpMethod.Get,
                "/projects/proj_123/products/prod_456",
                null,
                default,
                null))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _service.GetAsync(projectId, productId);

        // Assert
        Assert.Same(expectedResponse, result);
        _mockExecutor.Verify();
    }

    [Fact]
    public async Task GetAsync_WithExpand_BuildsCorrectUrl()
    {
        // Arrange
        var projectId = "proj_123";
        var productId = "prod_456";
        var expand = new[] { "app" };
        var expectedResponse = new Product();
        
        _mockExecutor
            .Setup(x => x.ExecuteAsync<Product>(
                HttpMethod.Get,
                It.Is<string>(s => s.StartsWith("/projects/proj_123/products/prod_456?expand=")),
                null,
                default,
                null))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _service.GetAsync(projectId, productId, expand);

        // Assert
        Assert.Same(expectedResponse, result);
        _mockExecutor.Verify();
    }

    [Fact]
    public async Task CreateAsync_BuildsCorrectUrl()
    {
        // Arrange
        var projectId = "proj_123";
        var request = new CreateProductRequest(
            "com.example.monthly",
            "app_456",
            ProductType.Subscription,
            "Monthly Subscription"
        );
        var expectedResponse = new Product();
        
        _mockExecutor
            .Setup(x => x.ExecuteAsync<Product>(
                HttpMethod.Post,
                "/projects/proj_123/products",
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
        var productId = "prod_456";
        var expectedResponse = new DeletedObject("product", "prod_456", 1699564800000);
        
        _mockExecutor
            .Setup(x => x.ExecuteAsync<DeletedObject>(
                HttpMethod.Delete,
                "/projects/proj_123/products/prod_456",
                null,
                default,
                null))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _service.DeleteAsync(projectId, productId);

        // Assert
        Assert.Same(expectedResponse, result);
        _mockExecutor.Verify();
    }

    [Fact]
    public async Task CreateProductInStoreAsync_WithSubscription_BuildsCorrectUrl()
    {
        // Arrange
        var projectId = "proj_123";
        var productId = "prod_456";
        var input = new CreateAppStoreConnectSubscriptionInput
        {
            ProductId = "com.example.monthly",
            ReferenceName = "Monthly Subscription",
            SubscriptionGroupId = "group_123",
            AvailableInAllTerritories = true
        };
        var expectedResponse = new StoreProduct();
        
        _mockExecutor
            .Setup(x => x.ExecuteAsync<StoreProduct>(
                HttpMethod.Post,
                "/projects/proj_123/products/prod_456/actions/create_product_in_store",
                input,
                default,
                null))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _service.CreateProductInStoreAsync(projectId, productId, input);

        // Assert
        Assert.Same(expectedResponse, result);
        _mockExecutor.Verify();
    }

    [Fact]
    public async Task CreateProductInStoreAsync_WithInAppPurchase_BuildsCorrectUrl()
    {
        // Arrange
        var projectId = "proj_123";
        var productId = "prod_456";
        var input = new CreateAppStoreConnectInAppPurchaseInput
        {
            ProductId = "com.example.coins",
            ReferenceName = "100 Coins",
            InAppPurchaseType = "consumable",
            AvailableInAllTerritories = true
        };
        var expectedResponse = new StoreProduct();
        
        _mockExecutor
            .Setup(x => x.ExecuteAsync<StoreProduct>(
                HttpMethod.Post,
                "/projects/proj_123/products/prod_456/actions/create_product_in_store",
                input,
                default,
                null))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _service.CreateProductInStoreAsync(projectId, productId, input);

        // Assert
        Assert.Same(expectedResponse, result);
        _mockExecutor.Verify();
    }
}
