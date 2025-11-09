using Moq;
using RevenueCat.NET.Models.Charts;
using RevenueCat.NET.Services;

namespace RevenueCat.NET.Tests.Services;

public class ChartsServiceTests
{
    private readonly Mock<IHttpRequestExecutor> _mockExecutor;
    private readonly ChartsService _service;

    public ChartsServiceTests()
    {
        _mockExecutor = new Mock<IHttpRequestExecutor>();
        _service = new ChartsService(_mockExecutor.Object);
    }

    [Fact]
    public async Task GetMetricsAsync_BuildsCorrectUrl()
    {
        // Arrange
        var projectId = "proj_123";
        var expectedResponse = new OverviewMetrics();
        
        _mockExecutor
            .Setup(x => x.ExecuteAsync<OverviewMetrics>(
                HttpMethod.Get,
                "/projects/proj_123/overview_metrics",
                null,
                default,
                null))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _service.GetMetricsAsync(projectId);

        // Assert
        Assert.Same(expectedResponse, result);
        _mockExecutor.Verify();
    }

    [Fact]
    public async Task GetMetricsAsync_WithCurrency_BuildsCorrectUrl()
    {
        // Arrange
        var projectId = "proj_123";
        var currency = "USD";
        var expectedResponse = new OverviewMetrics();
        
        _mockExecutor
            .Setup(x => x.ExecuteAsync<OverviewMetrics>(
                HttpMethod.Get,
                "/projects/proj_123/overview_metrics?currency=USD",
                null,
                default,
                null))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _service.GetMetricsAsync(projectId, currency);

        // Assert
        Assert.Same(expectedResponse, result);
        _mockExecutor.Verify();
    }

    [Fact]
    public async Task GetMetricsAsync_WithEuroCurrency_BuildsCorrectUrl()
    {
        // Arrange
        var projectId = "proj_456";
        var currency = "EUR";
        var expectedResponse = new OverviewMetrics();
        
        _mockExecutor
            .Setup(x => x.ExecuteAsync<OverviewMetrics>(
                HttpMethod.Get,
                "/projects/proj_456/overview_metrics?currency=EUR",
                null,
                default,
                null))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _service.GetMetricsAsync(projectId, currency);

        // Assert
        Assert.Same(expectedResponse, result);
        _mockExecutor.Verify();
    }

    [Fact]
    public async Task GetMetricsAsync_WithNullCurrency_BuildsCorrectUrl()
    {
        // Arrange
        var projectId = "proj_789";
        var expectedResponse = new OverviewMetrics();
        
        _mockExecutor
            .Setup(x => x.ExecuteAsync<OverviewMetrics>(
                HttpMethod.Get,
                "/projects/proj_789/overview_metrics",
                null,
                default,
                null))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _service.GetMetricsAsync(projectId, currency: null);

        // Assert
        Assert.Same(expectedResponse, result);
        _mockExecutor.Verify();
    }

    [Fact]
    public async Task GetMetricsAsync_WithEmptyCurrency_BuildsCorrectUrl()
    {
        // Arrange
        var projectId = "proj_789";
        var expectedResponse = new OverviewMetrics();
        
        _mockExecutor
            .Setup(x => x.ExecuteAsync<OverviewMetrics>(
                HttpMethod.Get,
                "/projects/proj_789/overview_metrics",
                null,
                default,
                null))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _service.GetMetricsAsync(projectId, currency: string.Empty);

        // Assert
        Assert.Same(expectedResponse, result);
        _mockExecutor.Verify();
    }

    [Fact]
    public async Task GetMetricsAsync_WithWhitespaceCurrency_BuildsCorrectUrl()
    {
        // Arrange
        var projectId = "proj_789";
        var expectedResponse = new OverviewMetrics();
        
        _mockExecutor
            .Setup(x => x.ExecuteAsync<OverviewMetrics>(
                HttpMethod.Get,
                "/projects/proj_789/overview_metrics",
                null,
                default,
                null))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _service.GetMetricsAsync(projectId, currency: "   ");

        // Assert
        Assert.Same(expectedResponse, result);
        _mockExecutor.Verify();
    }

    [Fact]
    public async Task GetMetricsAsync_ThrowsArgumentException_WhenProjectIdIsNull()
    {
        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => 
            _service.GetMetricsAsync(null!));
    }

    [Fact]
    public async Task GetMetricsAsync_ThrowsArgumentException_WhenProjectIdIsEmpty()
    {
        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => 
            _service.GetMetricsAsync(string.Empty));
    }

    [Fact]
    public async Task GetMetricsAsync_ThrowsArgumentException_WhenProjectIdIsWhitespace()
    {
        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => 
            _service.GetMetricsAsync("   "));
    }

    [Fact]
    public async Task GetMetricsAsync_WithSpecialCharactersInCurrency_EscapesCorrectly()
    {
        // Arrange
        var projectId = "proj_123";
        var currency = "USD+EUR";
        var expectedResponse = new OverviewMetrics();
        
        _mockExecutor
            .Setup(x => x.ExecuteAsync<OverviewMetrics>(
                HttpMethod.Get,
                "/projects/proj_123/overview_metrics?currency=USD%2BEUR",
                null,
                default,
                null))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _service.GetMetricsAsync(projectId, currency);

        // Assert
        Assert.Same(expectedResponse, result);
        _mockExecutor.Verify();
    }
}
