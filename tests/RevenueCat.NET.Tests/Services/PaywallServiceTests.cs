using Moq;
using RevenueCat.NET.Models.Paywalls;
using RevenueCat.NET.Services;

namespace RevenueCat.NET.Tests.Services;

public class PaywallServiceTests
{
    private readonly Mock<IHttpRequestExecutor> _mockExecutor;
    private readonly PaywallService _service;

    public PaywallServiceTests()
    {
        _mockExecutor = new Mock<IHttpRequestExecutor>();
        _service = new PaywallService(_mockExecutor.Object);
    }

    [Fact]
    public async Task CreateAsync_BuildsCorrectUrl()
    {
        // Arrange
        var projectId = "proj_123";
        var request = new CreatePaywallRequest("off_456");
        var expectedResponse = new Paywall
        {
            Id = "pw_789",
            OfferingId = "off_456"
        };
        
        _mockExecutor
            .Setup(x => x.ExecuteAsync<Paywall>(
                HttpMethod.Post,
                "/projects/proj_123/paywalls",
                request,
                default,
                null))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _service.CreateAsync(projectId, request);

        // Assert
        Assert.Same(expectedResponse, result);
        Assert.Equal("off_456", result.OfferingId);
        _mockExecutor.Verify();
    }

    [Fact]
    public async Task CreateAsync_WithOfferingAssociation_Success()
    {
        // Arrange
        var projectId = "proj_123";
        var offeringId = "off_456";
        var request = new CreatePaywallRequest(offeringId);
        var expectedResponse = new Paywall
        {
            Id = "pw_789",
            OfferingId = offeringId,
            Name = "Test Paywall",
            CreatedAt = 1699564800000
        };
        
        _mockExecutor
            .Setup(x => x.ExecuteAsync<Paywall>(
                HttpMethod.Post,
                "/projects/proj_123/paywalls",
                request,
                default,
                null))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _service.CreateAsync(projectId, request);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("pw_789", result.Id);
        Assert.Equal(offeringId, result.OfferingId);
        Assert.Equal("Test Paywall", result.Name);
        Assert.Equal(1699564800000, result.CreatedAt);
        _mockExecutor.Verify(x => x.ExecuteAsync<Paywall>(
            HttpMethod.Post,
            "/projects/proj_123/paywalls",
            request,
            default,
            null), Times.Once);
    }

    [Fact]
    public async Task CreateAsync_ThrowsArgumentException_WhenProjectIdIsNull()
    {
        // Arrange
        var request = new CreatePaywallRequest("off_456");

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => 
            _service.CreateAsync(null!, request));
    }

    [Fact]
    public async Task CreateAsync_ThrowsArgumentException_WhenProjectIdIsEmpty()
    {
        // Arrange
        var request = new CreatePaywallRequest("off_456");

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => 
            _service.CreateAsync(string.Empty, request));
    }

    [Fact]
    public async Task CreateAsync_ThrowsArgumentNullException_WhenRequestIsNull()
    {
        // Arrange
        var projectId = "proj_123";

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => 
            _service.CreateAsync(projectId, null!));
    }

    [Fact]
    public async Task CreateAsync_PassesCancellationToken()
    {
        // Arrange
        var projectId = "proj_123";
        var request = new CreatePaywallRequest("off_456");
        var cancellationToken = new CancellationToken();
        var expectedResponse = new Paywall();
        
        _mockExecutor
            .Setup(x => x.ExecuteAsync<Paywall>(
                HttpMethod.Post,
                "/projects/proj_123/paywalls",
                request,
                cancellationToken,
                null))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _service.CreateAsync(projectId, request, cancellationToken);

        // Assert
        Assert.Same(expectedResponse, result);
        _mockExecutor.Verify(x => x.ExecuteAsync<Paywall>(
            HttpMethod.Post,
            "/projects/proj_123/paywalls",
            request,
            cancellationToken,
            null), Times.Once);
    }
}
