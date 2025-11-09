using Moq;
using RevenueCat.NET.Models.Common;
using RevenueCat.NET.Models.Invoices;
using RevenueCat.NET.Services;

namespace RevenueCat.NET.Tests.Services;

public class InvoiceServiceTests
{
    private readonly Mock<IHttpRequestExecutor> _mockExecutor;
    private readonly InvoiceService _service;

    public InvoiceServiceTests()
    {
        _mockExecutor = new Mock<IHttpRequestExecutor>();
        _service = new InvoiceService(_mockExecutor.Object);
    }

    [Fact]
    public async Task ListAsync_BuildsCorrectUrl()
    {
        // Arrange
        var projectId = "proj_123";
        var customerId = "cust_456";
        var expectedResponse = new ListResponse<Invoice>();
        
        _mockExecutor
            .Setup(x => x.ExecuteAsync<ListResponse<Invoice>>(
                HttpMethod.Get,
                "/projects/proj_123/customers/cust_456/invoices",
                null,
                default,
                null))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _service.ListAsync(projectId, customerId);

        // Assert
        Assert.Same(expectedResponse, result);
        _mockExecutor.Verify();
    }

    [Fact]
    public async Task ListAsync_WithPagination_BuildsCorrectUrl()
    {
        // Arrange
        var projectId = "proj_123";
        var customerId = "cust_456";
        var expectedResponse = new ListResponse<Invoice>();
        
        _mockExecutor
            .Setup(x => x.ExecuteAsync<ListResponse<Invoice>>(
                HttpMethod.Get,
                "/projects/proj_123/customers/cust_456/invoices?limit=10&starting_after=inv_789",
                null,
                default,
                null))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _service.ListAsync(projectId, customerId, limit: 10, startingAfter: "inv_789");

        // Assert
        Assert.Same(expectedResponse, result);
        _mockExecutor.Verify();
    }

    [Fact]
    public async Task GetAsync_BuildsCorrectUrl()
    {
        // Arrange
        var projectId = "proj_123";
        var customerId = "cust_456";
        var invoiceId = "inv_789";
        var expectedResponse = new Invoice();
        
        _mockExecutor
            .Setup(x => x.ExecuteAsync<Invoice>(
                HttpMethod.Get,
                "/projects/proj_123/customers/cust_456/invoices/inv_789",
                null,
                default,
                null))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _service.GetAsync(projectId, customerId, invoiceId);

        // Assert
        Assert.Same(expectedResponse, result);
        _mockExecutor.Verify();
    }

    [Fact]
    public async Task GetInvoiceFileAsync_BuildsCorrectUrl()
    {
        // Arrange
        var projectId = "proj_123";
        var customerId = "cust_456";
        var invoiceId = "inv_789";
        var expectedUrl = "https://example.com/invoice.pdf";
        var expectedResponse = new InvoiceFileResponse(expectedUrl);
        
        _mockExecutor
            .Setup(x => x.ExecuteAsync<InvoiceFileResponse>(
                HttpMethod.Get,
                "/projects/proj_123/customers/cust_456/invoices/inv_789/file",
                null,
                default,
                null))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _service.GetInvoiceFileAsync(projectId, customerId, invoiceId);

        // Assert
        Assert.Equal(expectedUrl, result);
        _mockExecutor.Verify();
    }

    [Fact]
    public async Task ListAsync_EscapesCustomerId()
    {
        // Arrange
        var projectId = "proj_123";
        var customerId = "user@example.com";
        var expectedResponse = new ListResponse<Invoice>();
        
        _mockExecutor
            .Setup(x => x.ExecuteAsync<ListResponse<Invoice>>(
                HttpMethod.Get,
                It.Is<string>(s => s.Contains("user%40example.com")),
                null,
                default,
                null))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _service.ListAsync(projectId, customerId);

        // Assert
        Assert.Same(expectedResponse, result);
        _mockExecutor.Verify();
    }

    [Fact]
    public async Task GetAsync_EscapesCustomerId()
    {
        // Arrange
        var projectId = "proj_123";
        var customerId = "user@example.com";
        var invoiceId = "inv_789";
        var expectedResponse = new Invoice();
        
        _mockExecutor
            .Setup(x => x.ExecuteAsync<Invoice>(
                HttpMethod.Get,
                It.Is<string>(s => s.Contains("user%40example.com")),
                null,
                default,
                null))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _service.GetAsync(projectId, customerId, invoiceId);

        // Assert
        Assert.Same(expectedResponse, result);
        _mockExecutor.Verify();
    }

    [Fact]
    public async Task GetInvoiceFileAsync_EscapesCustomerId()
    {
        // Arrange
        var projectId = "proj_123";
        var customerId = "user@example.com";
        var invoiceId = "inv_789";
        var expectedUrl = "https://example.com/invoice.pdf";
        var expectedResponse = new InvoiceFileResponse(expectedUrl);
        
        _mockExecutor
            .Setup(x => x.ExecuteAsync<InvoiceFileResponse>(
                HttpMethod.Get,
                It.Is<string>(s => s.Contains("user%40example.com")),
                null,
                default,
                null))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _service.GetInvoiceFileAsync(projectId, customerId, invoiceId);

        // Assert
        Assert.Equal(expectedUrl, result);
        _mockExecutor.Verify();
    }
}
