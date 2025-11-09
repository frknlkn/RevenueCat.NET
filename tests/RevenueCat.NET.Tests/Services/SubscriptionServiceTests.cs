using Moq;
using RevenueCat.NET.Models.Common;
using RevenueCat.NET.Models.Entitlements;
using RevenueCat.NET.Models.Subscriptions;
using RevenueCat.NET.Services;

namespace RevenueCat.NET.Tests.Services;

public class SubscriptionServiceTests
{
    private readonly Mock<IHttpRequestExecutor> _mockExecutor;
    private readonly SubscriptionService _service;

    public SubscriptionServiceTests()
    {
        _mockExecutor = new Mock<IHttpRequestExecutor>();
        _service = new SubscriptionService(_mockExecutor.Object);
    }

    [Fact]
    public async Task ListSubscriptionsAsync_BuildsCorrectUrl()
    {
        // Arrange
        var projectId = "proj_123";
        var customerId = "cust_456";
        var expectedResponse = new ListResponse<Subscription>();
        
        _mockExecutor
            .Setup(x => x.ExecuteAsync<ListResponse<Subscription>>(
                HttpMethod.Get,
                "/projects/proj_123/customers/cust_456/subscriptions",
                null,
                default,
                null))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _service.ListSubscriptionsAsync(projectId, customerId);

        // Assert
        Assert.Same(expectedResponse, result);
        _mockExecutor.Verify();
    }

    [Fact]
    public async Task ListSubscriptionsAsync_WithEnvironmentFilter_BuildsCorrectUrl()
    {
        // Arrange
        var projectId = "proj_123";
        var customerId = "cust_456";
        var expectedResponse = new ListResponse<Subscription>();
        
        _mockExecutor
            .Setup(x => x.ExecuteAsync<ListResponse<Subscription>>(
                HttpMethod.Get,
                "/projects/proj_123/customers/cust_456/subscriptions?environment=production",
                null,
                default,
                null))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _service.ListSubscriptionsAsync(projectId, customerId, environment: "production");

        // Assert
        Assert.Same(expectedResponse, result);
        _mockExecutor.Verify();
    }

    [Fact]
    public async Task ListSubscriptionsAsync_WithPagination_BuildsCorrectUrl()
    {
        // Arrange
        var projectId = "proj_123";
        var customerId = "cust_456";
        var expectedResponse = new ListResponse<Subscription>();
        
        _mockExecutor
            .Setup(x => x.ExecuteAsync<ListResponse<Subscription>>(
                HttpMethod.Get,
                "/projects/proj_123/customers/cust_456/subscriptions?limit=10&starting_after=sub_789",
                null,
                default,
                null))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _service.ListSubscriptionsAsync(projectId, customerId, limit: 10, startingAfter: "sub_789");

        // Assert
        Assert.Same(expectedResponse, result);
        _mockExecutor.Verify();
    }

    [Fact]
    public async Task GetSubscriptionAsync_BuildsCorrectUrl()
    {
        // Arrange
        var projectId = "proj_123";
        var subscriptionId = "sub_456";
        var expectedResponse = new Subscription();
        
        _mockExecutor
            .Setup(x => x.ExecuteAsync<Subscription>(
                HttpMethod.Get,
                "/projects/proj_123/subscriptions/sub_456",
                null,
                default,
                null))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _service.GetSubscriptionAsync(projectId, subscriptionId);

        // Assert
        Assert.Same(expectedResponse, result);
        _mockExecutor.Verify();
    }

    [Fact]
    public async Task SearchSubscriptionsAsync_BuildsCorrectUrl()
    {
        // Arrange
        var projectId = "proj_123";
        var storeIdentifier = "store_sub_123";
        var expectedResponse = new ListResponse<Subscription>();
        
        _mockExecutor
            .Setup(x => x.ExecuteAsync<ListResponse<Subscription>>(
                HttpMethod.Get,
                It.Is<string>(s => s.StartsWith("/projects/proj_123/subscriptions?store_subscription_identifier=")),
                null,
                default,
                null))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _service.SearchSubscriptionsAsync(projectId, storeIdentifier);

        // Assert
        Assert.Same(expectedResponse, result);
        _mockExecutor.Verify();
    }

    [Fact]
    public async Task CancelSubscriptionAsync_BuildsCorrectUrl()
    {
        // Arrange
        var projectId = "proj_123";
        var subscriptionId = "sub_456";
        var expectedResponse = new Subscription();
        
        _mockExecutor
            .Setup(x => x.ExecuteAsync<Subscription>(
                HttpMethod.Post,
                "/projects/proj_123/subscriptions/sub_456/actions/cancel",
                null,
                default,
                null))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _service.CancelSubscriptionAsync(projectId, subscriptionId);

        // Assert
        Assert.Same(expectedResponse, result);
        _mockExecutor.Verify();
    }

    [Fact]
    public async Task RefundSubscriptionAsync_BuildsCorrectUrl()
    {
        // Arrange
        var projectId = "proj_123";
        var subscriptionId = "sub_456";
        var expectedResponse = new Subscription();
        
        _mockExecutor
            .Setup(x => x.ExecuteAsync<Subscription>(
                HttpMethod.Post,
                "/projects/proj_123/subscriptions/sub_456/actions/refund",
                null,
                default,
                null))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _service.RefundSubscriptionAsync(projectId, subscriptionId);

        // Assert
        Assert.Same(expectedResponse, result);
        _mockExecutor.Verify();
    }

    [Fact]
    public async Task GetAuthenticatedManagementUrlAsync_BuildsCorrectUrl()
    {
        // Arrange
        var projectId = "proj_123";
        var subscriptionId = "sub_456";
        var expectedResponse = new AuthenticatedManagementUrl();
        
        _mockExecutor
            .Setup(x => x.ExecuteAsync<AuthenticatedManagementUrl>(
                HttpMethod.Get,
                "/projects/proj_123/subscriptions/sub_456/authenticated_management_url",
                null,
                default,
                null))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _service.GetAuthenticatedManagementUrlAsync(projectId, subscriptionId);

        // Assert
        Assert.Same(expectedResponse, result);
        _mockExecutor.Verify();
    }

    [Fact]
    public async Task GetSubscriptionTransactionsAsync_BuildsCorrectUrl()
    {
        // Arrange
        var projectId = "proj_123";
        var subscriptionId = "sub_456";
        var expectedResponse = new ListResponse<SubscriptionTransaction>();
        
        _mockExecutor
            .Setup(x => x.ExecuteAsync<ListResponse<SubscriptionTransaction>>(
                HttpMethod.Get,
                "/projects/proj_123/subscriptions/sub_456/transactions",
                null,
                default,
                null))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _service.GetSubscriptionTransactionsAsync(projectId, subscriptionId);

        // Assert
        Assert.Same(expectedResponse, result);
        _mockExecutor.Verify();
    }

    [Fact]
    public async Task RefundSubscriptionTransactionAsync_BuildsCorrectUrl()
    {
        // Arrange
        var projectId = "proj_123";
        var subscriptionId = "sub_456";
        var transactionId = "txn_789";
        var expectedResponse = new SubscriptionTransaction();
        
        _mockExecutor
            .Setup(x => x.ExecuteAsync<SubscriptionTransaction>(
                HttpMethod.Post,
                "/projects/proj_123/subscriptions/sub_456/transactions/txn_789/actions/refund",
                null,
                default,
                null))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _service.RefundSubscriptionTransactionAsync(projectId, subscriptionId, transactionId);

        // Assert
        Assert.Same(expectedResponse, result);
        _mockExecutor.Verify();
    }

    [Fact]
    public async Task ListSubscriptionEntitlementsAsync_BuildsCorrectUrl()
    {
        // Arrange
        var projectId = "proj_123";
        var subscriptionId = "sub_456";
        var expectedResponse = new ListResponse<Entitlement>();
        
        _mockExecutor
            .Setup(x => x.ExecuteAsync<ListResponse<Entitlement>>(
                HttpMethod.Get,
                "/projects/proj_123/subscriptions/sub_456/entitlements",
                null,
                default,
                null))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _service.ListSubscriptionEntitlementsAsync(projectId, subscriptionId);

        // Assert
        Assert.Same(expectedResponse, result);
        _mockExecutor.Verify();
    }

    [Fact]
    public async Task ListSubscriptionEntitlementsAsync_WithPagination_BuildsCorrectUrl()
    {
        // Arrange
        var projectId = "proj_123";
        var subscriptionId = "sub_456";
        var expectedResponse = new ListResponse<Entitlement>();
        
        _mockExecutor
            .Setup(x => x.ExecuteAsync<ListResponse<Entitlement>>(
                HttpMethod.Get,
                "/projects/proj_123/subscriptions/sub_456/entitlements?limit=5&starting_after=ent_123",
                null,
                default,
                null))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _service.ListSubscriptionEntitlementsAsync(projectId, subscriptionId, limit: 5, startingAfter: "ent_123");

        // Assert
        Assert.Same(expectedResponse, result);
        _mockExecutor.Verify();
    }
}
