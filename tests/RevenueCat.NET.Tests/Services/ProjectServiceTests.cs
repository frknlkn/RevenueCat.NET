using Moq;
using RevenueCat.NET.Models.Common;
using RevenueCat.NET.Models.Projects;
using RevenueCat.NET.Services;

namespace RevenueCat.NET.Tests.Services;

public class ProjectServiceTests
{
    private readonly Mock<IHttpRequestExecutor> _mockExecutor;
    private readonly ProjectService _service;

    public ProjectServiceTests()
    {
        _mockExecutor = new Mock<IHttpRequestExecutor>();
        _service = new ProjectService(_mockExecutor.Object);
    }

    [Fact]
    public async Task ListAsync_BuildsCorrectUrl()
    {
        // Arrange
        var expectedResponse = new ListResponse<Project>();
        
        _mockExecutor
            .Setup(x => x.ExecuteAsync<ListResponse<Project>>(
                HttpMethod.Get,
                "/projects",
                null,
                default,
                null))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _service.ListAsync();

        // Assert
        Assert.Same(expectedResponse, result);
        _mockExecutor.Verify();
    }

    [Fact]
    public async Task ListAsync_WithPagination_BuildsCorrectUrl()
    {
        // Arrange
        var expectedResponse = new ListResponse<Project>();
        
        _mockExecutor
            .Setup(x => x.ExecuteAsync<ListResponse<Project>>(
                HttpMethod.Get,
                "/projects?limit=10&starting_after=proj_789",
                null,
                default,
                null))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _service.ListAsync(limit: 10, startingAfter: "proj_789");

        // Assert
        Assert.Same(expectedResponse, result);
        _mockExecutor.Verify();
    }

    [Fact]
    public async Task ListAsync_WithLimit_BuildsCorrectUrl()
    {
        // Arrange
        var expectedResponse = new ListResponse<Project>();
        
        _mockExecutor
            .Setup(x => x.ExecuteAsync<ListResponse<Project>>(
                HttpMethod.Get,
                "/projects?limit=5",
                null,
                default,
                null))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _service.ListAsync(limit: 5);

        // Assert
        Assert.Same(expectedResponse, result);
        _mockExecutor.Verify();
    }

    [Fact]
    public async Task ListAsync_WithStartingAfter_BuildsCorrectUrl()
    {
        // Arrange
        var expectedResponse = new ListResponse<Project>();
        
        _mockExecutor
            .Setup(x => x.ExecuteAsync<ListResponse<Project>>(
                HttpMethod.Get,
                "/projects?starting_after=proj_123",
                null,
                default,
                null))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _service.ListAsync(startingAfter: "proj_123");

        // Assert
        Assert.Same(expectedResponse, result);
        _mockExecutor.Verify();
    }

    [Fact]
    public async Task GetAsync_BuildsCorrectUrl()
    {
        // Arrange
        var projectId = "proj_123";
        var expectedResponse = new Project();
        
        _mockExecutor
            .Setup(x => x.ExecuteAsync<Project>(
                HttpMethod.Get,
                "/projects/proj_123",
                null,
                default,
                null))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _service.GetAsync(projectId);

        // Assert
        Assert.Same(expectedResponse, result);
        _mockExecutor.Verify();
    }

    [Fact]
    public async Task GetAsync_ThrowsArgumentException_WhenProjectIdIsNull()
    {
        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => 
            _service.GetAsync(null!));
    }

    [Fact]
    public async Task GetAsync_ThrowsArgumentException_WhenProjectIdIsEmpty()
    {
        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => 
            _service.GetAsync(string.Empty));
    }

    [Fact]
    public async Task GetAsync_ThrowsArgumentException_WhenProjectIdIsWhitespace()
    {
        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => 
            _service.GetAsync("   "));
    }
}
