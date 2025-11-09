using Moq;
using RevenueCat.NET.Models.Apps;
using RevenueCat.NET.Models.Common;
using RevenueCat.NET.Models.Enums;
using RevenueCat.NET.Services;

namespace RevenueCat.NET.Tests.Services;

public class AppServiceTests
{
    private readonly Mock<IHttpRequestExecutor> _mockExecutor;
    private readonly AppService _service;

    public AppServiceTests()
    {
        _mockExecutor = new Mock<IHttpRequestExecutor>();
        _service = new AppService(_mockExecutor.Object);
    }

    [Fact]
    public async Task ListAsync_BuildsCorrectUrl()
    {
        // Arrange
        var projectId = "proj_123";
        var expectedResponse = new ListResponse<App>();
        
        _mockExecutor
            .Setup(x => x.ExecuteAsync<ListResponse<App>>(
                HttpMethod.Get,
                "/projects/proj_123/apps",
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
        var expectedResponse = new ListResponse<App>();
        
        _mockExecutor
            .Setup(x => x.ExecuteAsync<ListResponse<App>>(
                HttpMethod.Get,
                "/projects/proj_123/apps?limit=10&starting_after=app_789",
                null,
                default,
                null))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _service.ListAsync(projectId, limit: 10, startingAfter: "app_789");

        // Assert
        Assert.Same(expectedResponse, result);
        _mockExecutor.Verify();
    }

    [Fact]
    public async Task GetAsync_BuildsCorrectUrl()
    {
        // Arrange
        var projectId = "proj_123";
        var appId = "app_456";
        var expectedResponse = new App();
        
        _mockExecutor
            .Setup(x => x.ExecuteAsync<App>(
                HttpMethod.Get,
                "/projects/proj_123/apps/app_456",
                null,
                default,
                null))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _service.GetAsync(projectId, appId);

        // Assert
        Assert.Same(expectedResponse, result);
        _mockExecutor.Verify();
    }

    [Fact]
    public async Task CreateAsync_WithAppStoreConfig_BuildsCorrectUrl()
    {
        // Arrange
        var projectId = "proj_123";
        var request = new CreateAppRequest
        {
            Name = "My iOS App",
            Type = AppType.AppStore,
            AppStore = new AppStoreDetails
            {
                BundleId = "com.example.myapp",
                SharedSecret = "secret_123"
            }
        };
        var expectedResponse = new App();
        
        _mockExecutor
            .Setup(x => x.ExecuteAsync<App>(
                HttpMethod.Post,
                "/projects/proj_123/apps",
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
    public async Task CreateAsync_WithPlayStoreConfig_BuildsCorrectUrl()
    {
        // Arrange
        var projectId = "proj_123";
        var request = new CreateAppRequest
        {
            Name = "My Android App",
            Type = AppType.PlayStore,
            PlayStore = new PlayStoreDetails
            {
                PackageName = "com.example.myapp"
            }
        };
        var expectedResponse = new App();
        
        _mockExecutor
            .Setup(x => x.ExecuteAsync<App>(
                HttpMethod.Post,
                "/projects/proj_123/apps",
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
        var appId = "app_456";
        var request = new UpdateAppRequest
        {
            Name = "Updated App Name"
        };
        var expectedResponse = new App();
        
        _mockExecutor
            .Setup(x => x.ExecuteAsync<App>(
                HttpMethod.Post,
                "/projects/proj_123/apps/app_456",
                request,
                default,
                null))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _service.UpdateAsync(projectId, appId, request);

        // Assert
        Assert.Same(expectedResponse, result);
        _mockExecutor.Verify();
    }

    [Fact]
    public async Task UpdateAsync_WithStoreConfig_BuildsCorrectUrl()
    {
        // Arrange
        var projectId = "proj_123";
        var appId = "app_456";
        var request = new UpdateAppRequest
        {
            AppStore = new AppStoreDetails
            {
                BundleId = "com.example.updated",
                SharedSecret = "new_secret"
            }
        };
        var expectedResponse = new App();
        
        _mockExecutor
            .Setup(x => x.ExecuteAsync<App>(
                HttpMethod.Post,
                "/projects/proj_123/apps/app_456",
                request,
                default,
                null))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _service.UpdateAsync(projectId, appId, request);

        // Assert
        Assert.Same(expectedResponse, result);
        _mockExecutor.Verify();
    }

    [Fact]
    public async Task DeleteAsync_BuildsCorrectUrl()
    {
        // Arrange
        var projectId = "proj_123";
        var appId = "app_456";
        var expectedResponse = new DeletedObject("app", "app_456", 1699564800000);
        
        _mockExecutor
            .Setup(x => x.ExecuteAsync<DeletedObject>(
                HttpMethod.Delete,
                "/projects/proj_123/apps/app_456",
                null,
                default,
                null))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _service.DeleteAsync(projectId, appId);

        // Assert
        Assert.Same(expectedResponse, result);
        _mockExecutor.Verify();
    }

    [Fact]
    public async Task ListPublicApiKeysAsync_BuildsCorrectUrl()
    {
        // Arrange
        var projectId = "proj_123";
        var appId = "app_456";
        var expectedResponse = new ListResponse<PublicApiKey>();
        
        _mockExecutor
            .Setup(x => x.ExecuteAsync<ListResponse<PublicApiKey>>(
                HttpMethod.Get,
                "/projects/proj_123/apps/app_456/public_api_keys",
                null,
                default,
                null))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _service.ListPublicApiKeysAsync(projectId, appId);

        // Assert
        Assert.Same(expectedResponse, result);
        _mockExecutor.Verify();
    }

    [Fact]
    public async Task ListPublicApiKeysAsync_WithPagination_BuildsCorrectUrl()
    {
        // Arrange
        var projectId = "proj_123";
        var appId = "app_456";
        var expectedResponse = new ListResponse<PublicApiKey>();
        
        _mockExecutor
            .Setup(x => x.ExecuteAsync<ListResponse<PublicApiKey>>(
                HttpMethod.Get,
                "/projects/proj_123/apps/app_456/public_api_keys?limit=5&starting_after=key_789",
                null,
                default,
                null))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _service.ListPublicApiKeysAsync(projectId, appId, limit: 5, startingAfter: "key_789");

        // Assert
        Assert.Same(expectedResponse, result);
        _mockExecutor.Verify();
    }

    [Fact]
    public async Task GetStoreKitConfigAsync_BuildsCorrectUrl()
    {
        // Arrange
        var projectId = "proj_123";
        var appId = "app_456";
        var expectedResponse = new StoreKitConfigFile();
        
        _mockExecutor
            .Setup(x => x.ExecuteAsync<StoreKitConfigFile>(
                HttpMethod.Get,
                "/projects/proj_123/apps/app_456/storekit_config",
                null,
                default,
                null))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _service.GetStoreKitConfigAsync(projectId, appId);

        // Assert
        Assert.Same(expectedResponse, result);
        _mockExecutor.Verify();
    }

    [Fact]
    public async Task ListAsync_ThrowsArgumentException_WhenProjectIdIsNull()
    {
        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => 
            _service.ListAsync(null!));
    }

    [Fact]
    public async Task GetAsync_ThrowsArgumentException_WhenProjectIdIsNull()
    {
        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => 
            _service.GetAsync(null!, "app_123"));
    }

    [Fact]
    public async Task GetAsync_ThrowsArgumentException_WhenAppIdIsNull()
    {
        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => 
            _service.GetAsync("proj_123", null!));
    }

    [Fact]
    public async Task CreateAsync_ThrowsArgumentNullException_WhenRequestIsNull()
    {
        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => 
            _service.CreateAsync("proj_123", null!));
    }
}
