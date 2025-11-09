using Moq;
using RevenueCat.NET.Models;
using RevenueCat.NET.Models.Common;
using RevenueCat.NET.Models.Offerings;
using RevenueCat.NET.Services;

namespace RevenueCat.NET.Tests.Services;

public class OfferingServiceTests
{
    private readonly Mock<IHttpRequestExecutor> _mockExecutor;
    private readonly OfferingService _service;

    public OfferingServiceTests()
    {
        _mockExecutor = new Mock<IHttpRequestExecutor>();
        _service = new OfferingService(_mockExecutor.Object);
    }

    [Fact]
    public async Task ListAsync_BuildsCorrectUrl()
    {
        // Arrange
        var projectId = "proj_123";
        var expectedResponse = new ListResponse<Offering>();
        
        _mockExecutor
            .Setup(x => x.ExecuteAsync<ListResponse<Offering>>(
                HttpMethod.Get,
                "/projects/proj_123/offerings",
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
        var expectedResponse = new ListResponse<Offering>();
        
        _mockExecutor
            .Setup(x => x.ExecuteAsync<ListResponse<Offering>>(
                HttpMethod.Get,
                "/projects/proj_123/offerings?limit=10&starting_after=off_789",
                null,
                default,
                null))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _service.ListAsync(projectId, limit: 10, startingAfter: "off_789");

        // Assert
        Assert.Same(expectedResponse, result);
        _mockExecutor.Verify();
    }

    [Fact]
    public async Task ListAsync_WithExpand_BuildsCorrectUrl()
    {
        // Arrange
        var projectId = "proj_123";
        var expand = new[] { "packages", "packages.products" };
        var expectedResponse = new ListResponse<Offering>();
        
        _mockExecutor
            .Setup(x => x.ExecuteAsync<ListResponse<Offering>>(
                HttpMethod.Get,
                It.Is<string>(s => s.StartsWith("/projects/proj_123/offerings?expand=")),
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
        var expand = new[] { "packages" };
        var expectedResponse = new ListResponse<Offering>();
        
        _mockExecutor
            .Setup(x => x.ExecuteAsync<ListResponse<Offering>>(
                HttpMethod.Get,
                It.Is<string>(s => 
                    s.Contains("/projects/proj_123/offerings?") &&
                    s.Contains("limit=5") &&
                    s.Contains("starting_after=off_456") &&
                    s.Contains("expand=")),
                null,
                default,
                null))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _service.ListAsync(projectId, limit: 5, startingAfter: "off_456", expand: expand);

        // Assert
        Assert.Same(expectedResponse, result);
        _mockExecutor.Verify();
    }

    [Fact]
    public async Task GetAsync_BuildsCorrectUrl()
    {
        // Arrange
        var projectId = "proj_123";
        var offeringId = "off_456";
        var expectedResponse = new Offering();
        
        _mockExecutor
            .Setup(x => x.ExecuteAsync<Offering>(
                HttpMethod.Get,
                "/projects/proj_123/offerings/off_456",
                null,
                default,
                null))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _service.GetAsync(projectId, offeringId);

        // Assert
        Assert.Same(expectedResponse, result);
        _mockExecutor.Verify();
    }

    [Fact]
    public async Task GetAsync_WithExpand_BuildsCorrectUrl()
    {
        // Arrange
        var projectId = "proj_123";
        var offeringId = "off_456";
        var expand = new[] { "packages", "packages.products" };
        var expectedResponse = new Offering();
        
        _mockExecutor
            .Setup(x => x.ExecuteAsync<Offering>(
                HttpMethod.Get,
                It.Is<string>(s => s.StartsWith("/projects/proj_123/offerings/off_456?expand=")),
                null,
                default,
                null))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _service.GetAsync(projectId, offeringId, expand);

        // Assert
        Assert.Same(expectedResponse, result);
        _mockExecutor.Verify();
    }

    [Fact]
    public async Task CreateAsync_BuildsCorrectUrl()
    {
        // Arrange
        var projectId = "proj_123";
        var request = new CreateOfferingRequest(
            "default",
            "Default Offering"
        );
        var expectedResponse = new Offering();
        
        _mockExecutor
            .Setup(x => x.ExecuteAsync<Offering>(
                HttpMethod.Post,
                "/projects/proj_123/offerings",
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
    public async Task CreateAsync_WithMetadata_BuildsCorrectUrl()
    {
        // Arrange
        var projectId = "proj_123";
        var metadata = new Dictionary<string, object>
        {
            { "campaign", "summer_sale" },
            { "discount", 20 }
        };
        var request = new CreateOfferingRequest(
            "default",
            "Default Offering",
            IsCurrent: true,
            Metadata: metadata
        );
        var expectedResponse = new Offering();
        
        _mockExecutor
            .Setup(x => x.ExecuteAsync<Offering>(
                HttpMethod.Post,
                "/projects/proj_123/offerings",
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
        var offeringId = "off_456";
        var request = new UpdateOfferingRequest(
            DisplayName: "Updated Offering"
        );
        var expectedResponse = new Offering();
        
        _mockExecutor
            .Setup(x => x.ExecuteAsync<Offering>(
                HttpMethod.Post,
                "/projects/proj_123/offerings/off_456",
                request,
                default,
                null))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _service.UpdateAsync(projectId, offeringId, request);

        // Assert
        Assert.Same(expectedResponse, result);
        _mockExecutor.Verify();
    }

    [Fact]
    public async Task UpdateAsync_WithIsCurrent_BuildsCorrectUrl()
    {
        // Arrange
        var projectId = "proj_123";
        var offeringId = "off_456";
        var request = new UpdateOfferingRequest(
            DisplayName: "Updated Offering",
            IsCurrent: true
        );
        var expectedResponse = new Offering();
        
        _mockExecutor
            .Setup(x => x.ExecuteAsync<Offering>(
                HttpMethod.Post,
                "/projects/proj_123/offerings/off_456",
                request,
                default,
                null))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _service.UpdateAsync(projectId, offeringId, request);

        // Assert
        Assert.Same(expectedResponse, result);
        _mockExecutor.Verify();
    }

    [Fact]
    public async Task UpdateAsync_WithMetadata_BuildsCorrectUrl()
    {
        // Arrange
        var projectId = "proj_123";
        var offeringId = "off_456";
        var metadata = new Dictionary<string, object>
        {
            { "updated", true },
            { "version", 2 }
        };
        var request = new UpdateOfferingRequest(
            DisplayName: "Updated Offering",
            Metadata: metadata
        );
        var expectedResponse = new Offering();
        
        _mockExecutor
            .Setup(x => x.ExecuteAsync<Offering>(
                HttpMethod.Post,
                "/projects/proj_123/offerings/off_456",
                request,
                default,
                null))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _service.UpdateAsync(projectId, offeringId, request);

        // Assert
        Assert.Same(expectedResponse, result);
        _mockExecutor.Verify();
    }

    [Fact]
    public async Task DeleteAsync_BuildsCorrectUrl()
    {
        // Arrange
        var projectId = "proj_123";
        var offeringId = "off_456";
        var expectedResponse = new DeletedObject("offering", "off_456", 1699564800000);
        
        _mockExecutor
            .Setup(x => x.ExecuteAsync<DeletedObject>(
                HttpMethod.Delete,
                "/projects/proj_123/offerings/off_456",
                null,
                default,
                null))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _service.DeleteAsync(projectId, offeringId);

        // Assert
        Assert.Same(expectedResponse, result);
        _mockExecutor.Verify();
    }

    [Fact]
    public async Task SetDefaultAsync_BuildsCorrectUrl()
    {
        // Arrange
        var projectId = "proj_123";
        var offeringId = "off_456";
        var expectedResponse = new Offering();
        
        _mockExecutor
            .Setup(x => x.ExecuteAsync<Offering>(
                HttpMethod.Post,
                "/projects/proj_123/offerings/off_456/actions/set_default",
                null,
                default,
                null))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _service.SetDefaultAsync(projectId, offeringId);

        // Assert
        Assert.Same(expectedResponse, result);
        _mockExecutor.Verify();
    }
}
