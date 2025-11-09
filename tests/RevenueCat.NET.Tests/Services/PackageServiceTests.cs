using Moq;
using RevenueCat.NET.Models;
using RevenueCat.NET.Models.Common;
using RevenueCat.NET.Models.Enums;
using RevenueCat.NET.Models.Packages;
using RevenueCat.NET.Models.Products;
using RevenueCat.NET.Services;

namespace RevenueCat.NET.Tests.Services;

public class PackageServiceTests
{
    private readonly Mock<IHttpRequestExecutor> _mockExecutor;
    private readonly PackageService _service;

    public PackageServiceTests()
    {
        _mockExecutor = new Mock<IHttpRequestExecutor>();
        _service = new PackageService(_mockExecutor.Object);
    }

    [Fact]
    public async Task ListAsync_BuildsCorrectUrl()
    {
        // Arrange
        var projectId = "proj_123";
        var offeringId = "off_456";
        var expectedResponse = new ListResponse<Package>();
        
        _mockExecutor
            .Setup(x => x.ExecuteAsync<ListResponse<Package>>(
                HttpMethod.Get,
                "/projects/proj_123/offerings/off_456/packages",
                null,
                default,
                null))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _service.ListAsync(projectId, offeringId);

        // Assert
        Assert.Same(expectedResponse, result);
        _mockExecutor.Verify();
    }

    [Fact]
    public async Task ListAsync_WithPagination_BuildsCorrectUrl()
    {
        // Arrange
        var projectId = "proj_123";
        var offeringId = "off_456";
        var expectedResponse = new ListResponse<Package>();
        
        _mockExecutor
            .Setup(x => x.ExecuteAsync<ListResponse<Package>>(
                HttpMethod.Get,
                "/projects/proj_123/offerings/off_456/packages?limit=10&starting_after=pkg_789",
                null,
                default,
                null))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _service.ListAsync(projectId, offeringId, limit: 10, startingAfter: "pkg_789");

        // Assert
        Assert.Same(expectedResponse, result);
        _mockExecutor.Verify();
    }

    [Fact]
    public async Task ListAsync_WithExpand_BuildsCorrectUrl()
    {
        // Arrange
        var projectId = "proj_123";
        var offeringId = "off_456";
        var expand = new[] { "products" };
        var expectedResponse = new ListResponse<Package>();
        
        _mockExecutor
            .Setup(x => x.ExecuteAsync<ListResponse<Package>>(
                HttpMethod.Get,
                It.Is<string>(s => s.StartsWith("/projects/proj_123/offerings/off_456/packages?expand=")),
                null,
                default,
                null))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _service.ListAsync(projectId, offeringId, expand: expand);

        // Assert
        Assert.Same(expectedResponse, result);
        _mockExecutor.Verify();
    }

    [Fact]
    public async Task ListAsync_WithAllParameters_BuildsCorrectUrl()
    {
        // Arrange
        var projectId = "proj_123";
        var offeringId = "off_456";
        var expand = new[] { "products" };
        var expectedResponse = new ListResponse<Package>();
        
        _mockExecutor
            .Setup(x => x.ExecuteAsync<ListResponse<Package>>(
                HttpMethod.Get,
                It.Is<string>(s => 
                    s.Contains("/projects/proj_123/offerings/off_456/packages?") &&
                    s.Contains("limit=5") &&
                    s.Contains("starting_after=pkg_999") &&
                    s.Contains("expand=")),
                null,
                default,
                null))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _service.ListAsync(projectId, offeringId, limit: 5, startingAfter: "pkg_999", expand: expand);

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
        var packageId = "pkg_789";
        var expectedResponse = new Package();
        
        _mockExecutor
            .Setup(x => x.ExecuteAsync<Package>(
                HttpMethod.Get,
                "/projects/proj_123/offerings/off_456/packages/pkg_789",
                null,
                default,
                null))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _service.GetAsync(projectId, offeringId, packageId);

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
        var packageId = "pkg_789";
        var expand = new[] { "products" };
        var expectedResponse = new Package();
        
        _mockExecutor
            .Setup(x => x.ExecuteAsync<Package>(
                HttpMethod.Get,
                It.Is<string>(s => s.StartsWith("/projects/proj_123/offerings/off_456/packages/pkg_789?expand=")),
                null,
                default,
                null))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _service.GetAsync(projectId, offeringId, packageId, expand);

        // Assert
        Assert.Same(expectedResponse, result);
        _mockExecutor.Verify();
    }

    [Fact]
    public async Task CreateAsync_BuildsCorrectUrl()
    {
        // Arrange
        var projectId = "proj_123";
        var offeringId = "off_456";
        var request = new CreatePackageRequest(
            "monthly",
            "Monthly Package",
            "prod_789",
            1
        );
        var expectedResponse = new Package();
        
        _mockExecutor
            .Setup(x => x.ExecuteAsync<Package>(
                HttpMethod.Post,
                "/projects/proj_123/offerings/off_456/packages",
                request,
                default,
                null))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _service.CreateAsync(projectId, offeringId, request);

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
        var packageId = "pkg_789";
        var request = new UpdatePackageRequest(
            DisplayName: "Updated Package"
        );
        var expectedResponse = new Package();
        
        _mockExecutor
            .Setup(x => x.ExecuteAsync<Package>(
                HttpMethod.Post,
                "/projects/proj_123/offerings/off_456/packages/pkg_789",
                request,
                default,
                null))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _service.UpdateAsync(projectId, offeringId, packageId, request);

        // Assert
        Assert.Same(expectedResponse, result);
        _mockExecutor.Verify();
    }

    [Fact]
    public async Task UpdateAsync_WithPosition_BuildsCorrectUrl()
    {
        // Arrange
        var projectId = "proj_123";
        var offeringId = "off_456";
        var packageId = "pkg_789";
        var request = new UpdatePackageRequest(
            DisplayName: "Updated Package",
            Position: 2
        );
        var expectedResponse = new Package();
        
        _mockExecutor
            .Setup(x => x.ExecuteAsync<Package>(
                HttpMethod.Post,
                "/projects/proj_123/offerings/off_456/packages/pkg_789",
                request,
                default,
                null))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _service.UpdateAsync(projectId, offeringId, packageId, request);

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
        var packageId = "pkg_789";
        var expectedResponse = new DeletedObject("package", "pkg_789", 1699564800000);
        
        _mockExecutor
            .Setup(x => x.ExecuteAsync<DeletedObject>(
                HttpMethod.Delete,
                "/projects/proj_123/offerings/off_456/packages/pkg_789",
                null,
                default,
                null))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _service.DeleteAsync(projectId, offeringId, packageId);

        // Assert
        Assert.Same(expectedResponse, result);
        _mockExecutor.Verify();
    }

    [Fact]
    public async Task GetProductsFromPackageAsync_BuildsCorrectUrl()
    {
        // Arrange
        var projectId = "proj_123";
        var offeringId = "off_456";
        var packageId = "pkg_789";
        var expectedResponse = new ListResponse<Product>();
        
        _mockExecutor
            .Setup(x => x.ExecuteAsync<ListResponse<Product>>(
                HttpMethod.Get,
                "/projects/proj_123/offerings/off_456/packages/pkg_789/products",
                null,
                default,
                null))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _service.GetProductsFromPackageAsync(projectId, offeringId, packageId);

        // Assert
        Assert.Same(expectedResponse, result);
        _mockExecutor.Verify();
    }

    [Fact]
    public async Task GetProductsFromPackageAsync_WithPagination_BuildsCorrectUrl()
    {
        // Arrange
        var projectId = "proj_123";
        var offeringId = "off_456";
        var packageId = "pkg_789";
        var expectedResponse = new ListResponse<Product>();
        
        _mockExecutor
            .Setup(x => x.ExecuteAsync<ListResponse<Product>>(
                HttpMethod.Get,
                "/projects/proj_123/offerings/off_456/packages/pkg_789/products?limit=10&starting_after=prod_999",
                null,
                default,
                null))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _service.GetProductsFromPackageAsync(projectId, offeringId, packageId, limit: 10, startingAfter: "prod_999");

        // Assert
        Assert.Same(expectedResponse, result);
        _mockExecutor.Verify();
    }

    [Fact]
    public async Task AttachProductsToPackageAsync_BuildsCorrectUrl()
    {
        // Arrange
        var projectId = "proj_123";
        var offeringId = "off_456";
        var packageId = "pkg_789";
        var request = new AttachProductsToPackageRequest(
            new List<ProductAttachment>
            {
                new("prod_001", EligibilityCriteria.All),
                new("prod_002", EligibilityCriteria.GoogleSdkLessThan6)
            }
        );
        var expectedResponse = new Package();
        
        _mockExecutor
            .Setup(x => x.ExecuteAsync<Package>(
                HttpMethod.Post,
                "/projects/proj_123/offerings/off_456/packages/pkg_789/products/attach",
                request,
                default,
                null))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _service.AttachProductsToPackageAsync(projectId, offeringId, packageId, request);

        // Assert
        Assert.Same(expectedResponse, result);
        _mockExecutor.Verify();
    }

    [Fact]
    public async Task AttachProductsToPackageAsync_WithDifferentEligibilityCriteria_BuildsCorrectUrl()
    {
        // Arrange
        var projectId = "proj_123";
        var offeringId = "off_456";
        var packageId = "pkg_789";
        var request = new AttachProductsToPackageRequest(
            new List<ProductAttachment>
            {
                new("prod_001", EligibilityCriteria.GoogleSdkGreaterOrEqual6)
            }
        );
        var expectedResponse = new Package();
        
        _mockExecutor
            .Setup(x => x.ExecuteAsync<Package>(
                HttpMethod.Post,
                "/projects/proj_123/offerings/off_456/packages/pkg_789/products/attach",
                request,
                default,
                null))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _service.AttachProductsToPackageAsync(projectId, offeringId, packageId, request);

        // Assert
        Assert.Same(expectedResponse, result);
        _mockExecutor.Verify();
    }

    [Fact]
    public async Task DetachProductsFromPackageAsync_BuildsCorrectUrl()
    {
        // Arrange
        var projectId = "proj_123";
        var offeringId = "off_456";
        var packageId = "pkg_789";
        var request = new DetachProductsFromPackageRequest(
            new List<string> { "prod_001", "prod_002" }
        );
        var expectedResponse = new Package();
        
        _mockExecutor
            .Setup(x => x.ExecuteAsync<Package>(
                HttpMethod.Post,
                "/projects/proj_123/offerings/off_456/packages/pkg_789/products/detach",
                request,
                default,
                null))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _service.DetachProductsFromPackageAsync(projectId, offeringId, packageId, request);

        // Assert
        Assert.Same(expectedResponse, result);
        _mockExecutor.Verify();
    }
}
