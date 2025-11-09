using System.Net;
using System.Text;
using System.Text.Json;
using Moq;
using Moq.Protected;
using RevenueCat.NET.Configuration;
using RevenueCat.NET.Exceptions;
using RevenueCat.NET.Models;
using RevenueCat.NET.Models.Enums;

namespace RevenueCat.NET.Tests.Http;

public class HttpRequestExecutorTests
{
    private readonly JsonSerializerOptions _jsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
    };

    [Fact]
    public async Task ExecuteAsync_ParameterError_ThrowsRevenueCatParameterException()
    {
        // Arrange
        var error = new RevenueCatError
        {
            Type = ErrorType.ParameterError,
            Message = "Invalid parameter",
            Param = "customer_id",
            DocUrl = "https://docs.revenuecat.com",
            Retryable = false
        };
        var executor = CreateExecutor(HttpStatusCode.BadRequest, error);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<RevenueCatParameterException>(
            () => executor.ExecuteAsync<object>(HttpMethod.Get, "/test"));

        Assert.Equal("Invalid parameter", exception.Message);
        Assert.NotNull(exception.Error);
        Assert.Equal(ErrorType.ParameterError, exception.Error.Type);
        Assert.Equal(400, exception.StatusCode);
    }

    [Fact]
    public async Task ExecuteAsync_ResourceMissing_ThrowsRevenueCatResourceNotFoundException()
    {
        // Arrange
        var error = new RevenueCatError
        {
            Type = ErrorType.ResourceMissing,
            Message = "Customer not found",
            DocUrl = "https://docs.revenuecat.com",
            Retryable = false
        };
        var executor = CreateExecutor(HttpStatusCode.NotFound, error);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<RevenueCatResourceNotFoundException>(
            () => executor.ExecuteAsync<object>(HttpMethod.Get, "/test"));

        Assert.Equal("Customer not found", exception.Message);
        Assert.NotNull(exception.Error);
        Assert.Equal(ErrorType.ResourceMissing, exception.Error.Type);
        Assert.Equal(404, exception.StatusCode);
    }

    [Fact]
    public async Task ExecuteAsync_ResourceAlreadyExists_ThrowsRevenueCatConflictException()
    {
        // Arrange
        var error = new RevenueCatError
        {
            Type = ErrorType.ResourceAlreadyExists,
            Message = "Resource already exists",
            DocUrl = "https://docs.revenuecat.com",
            Retryable = false
        };
        var executor = CreateExecutor(HttpStatusCode.Conflict, error);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<RevenueCatConflictException>(
            () => executor.ExecuteAsync<object>(HttpMethod.Post, "/test"));

        Assert.Equal("Resource already exists", exception.Message);
        Assert.NotNull(exception.Error);
        Assert.Equal(ErrorType.ResourceAlreadyExists, exception.Error.Type);
    }

    [Fact]
    public async Task ExecuteAsync_IdempotencyError_ThrowsRevenueCatConflictException()
    {
        // Arrange
        var error = new RevenueCatError
        {
            Type = ErrorType.IdempotencyError,
            Message = "Idempotency key conflict",
            DocUrl = "https://docs.revenuecat.com",
            Retryable = false
        };
        var executor = CreateExecutor(HttpStatusCode.Conflict, error);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<RevenueCatConflictException>(
            () => executor.ExecuteAsync<object>(HttpMethod.Post, "/test"));

        Assert.Equal("Idempotency key conflict", exception.Message);
        Assert.Equal(ErrorType.IdempotencyError, exception.Error!.Type);
    }

    [Fact]
    public async Task ExecuteAsync_RateLimitError_ThrowsRevenueCatRateLimitException()
    {
        // Arrange
        var error = new RevenueCatError
        {
            Type = ErrorType.RateLimitError,
            Message = "Rate limit exceeded",
            DocUrl = "https://docs.revenuecat.com",
            Retryable = true,
            BackoffMs = 5000
        };
        var executor = CreateExecutor(HttpStatusCode.TooManyRequests, error);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<RevenueCatRateLimitException>(
            () => executor.ExecuteAsync<object>(HttpMethod.Get, "/test"));

        Assert.Equal("Rate limit exceeded", exception.Message);
        Assert.NotNull(exception.Error);
        Assert.Equal(ErrorType.RateLimitError, exception.Error.Type);
        Assert.True(exception.Error.Retryable);
        Assert.Equal(5000, exception.Error.BackoffMs);
    }

    [Fact]
    public async Task ExecuteAsync_AuthenticationError_ThrowsRevenueCatAuthenticationException()
    {
        // Arrange
        var error = new RevenueCatError
        {
            Type = ErrorType.AuthenticationError,
            Message = "Invalid API key",
            DocUrl = "https://docs.revenuecat.com",
            Retryable = false
        };
        var executor = CreateExecutor(HttpStatusCode.Unauthorized, error);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<RevenueCatAuthenticationException>(
            () => executor.ExecuteAsync<object>(HttpMethod.Get, "/test"));

        Assert.Equal("Invalid API key", exception.Message);
        Assert.Equal(ErrorType.AuthenticationError, exception.Error!.Type);
    }

    [Fact]
    public async Task ExecuteAsync_AuthorizationError_ThrowsRevenueCatAuthorizationException()
    {
        // Arrange
        var error = new RevenueCatError
        {
            Type = ErrorType.AuthorizationError,
            Message = "Insufficient permissions",
            DocUrl = "https://docs.revenuecat.com",
            Retryable = false
        };
        var executor = CreateExecutor(HttpStatusCode.Forbidden, error);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<RevenueCatAuthorizationException>(
            () => executor.ExecuteAsync<object>(HttpMethod.Get, "/test"));

        Assert.Equal("Insufficient permissions", exception.Message);
        Assert.Equal(ErrorType.AuthorizationError, exception.Error!.Type);
    }

    [Fact]
    public async Task ExecuteAsync_ServerError_ThrowsRevenueCatException()
    {
        // Arrange
        var error = new RevenueCatError
        {
            Type = ErrorType.ServerError,
            Message = "Internal server error",
            DocUrl = "https://docs.revenuecat.com",
            Retryable = true,
            BackoffMs = 1000
        };
        var executor = CreateExecutor(HttpStatusCode.InternalServerError, error);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<RevenueCatException>(
            () => executor.ExecuteAsync<object>(HttpMethod.Get, "/test"));

        Assert.Equal("Internal server error", exception.Message);
        Assert.NotNull(exception.Error);
        Assert.Equal(ErrorType.ServerError, exception.Error.Type);
    }

    [Fact]
    public async Task ExecuteAsync_StoreError_ThrowsRevenueCatException()
    {
        // Arrange
        var error = new RevenueCatError
        {
            Type = ErrorType.StoreError,
            Message = "Store communication failed",
            DocUrl = "https://docs.revenuecat.com",
            Retryable = false
        };
        var executor = CreateExecutor(HttpStatusCode.BadGateway, error);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<RevenueCatException>(
            () => executor.ExecuteAsync<object>(HttpMethod.Get, "/test"));

        Assert.Equal("Store communication failed", exception.Message);
        Assert.Equal(ErrorType.StoreError, exception.Error!.Type);
    }

    [Fact]
    public async Task ExecuteAsync_SuccessResponse_ReturnsDeserializedObject()
    {
        // Arrange
        var responseData = new { id = "test_123", name = "Test Object" };
        var executor = CreateExecutor(HttpStatusCode.OK, responseData);

        // Act
        var result = await executor.ExecuteAsync<TestResponse>(HttpMethod.Get, "/test");

        // Assert
        Assert.NotNull(result);
        Assert.Equal("test_123", result.Id);
        Assert.Equal("Test Object", result.Name);
    }

    [Fact]
    public async Task ExecuteAsync_LegacyErrorFormat_ThrowsAppropriateException()
    {
        // Arrange - test backward compatibility with legacy error format
        var legacyError = new ErrorResponse(
            Type: "bad_request",
            Message: "Bad request error",
            Retryable: false
        );
        var executor = CreateExecutor(HttpStatusCode.BadRequest, legacyError);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<BadRequestException>(
            () => executor.ExecuteAsync<object>(HttpMethod.Get, "/test"));

        Assert.Equal("Bad request error", exception.Message);
    }

    private HttpRequestExecutor CreateExecutor(HttpStatusCode statusCode, object responseContent)
    {
        var json = JsonSerializer.Serialize(responseContent, _jsonOptions);
        var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

        var mockHandler = new Mock<HttpMessageHandler>();
        mockHandler.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = statusCode,
                Content = httpContent
            });

        var httpClient = new HttpClient(mockHandler.Object)
        {
            BaseAddress = new Uri("https://api.revenuecat.com/v2/")
        };

        var options = new RevenueCatOptions
        {
            ApiKey = "test_api_key",
            MaxRetryAttempts = 1,
            EnableRetryOnRateLimit = false
        };

        return new HttpRequestExecutor(httpClient, options);
    }

    private class TestResponse
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
    }
}
