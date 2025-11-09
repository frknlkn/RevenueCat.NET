using System.Text.Json;
using RevenueCat.NET.Models;
using RevenueCat.NET.Models.Enums;

namespace RevenueCat.NET.Tests.Models;

public class RevenueCatErrorTests
{
    private readonly JsonSerializerOptions _jsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
    };

    [Fact]
    public void RevenueCatError_Deserialize_AllFields_Success()
    {
        // Arrange
        var json = """
        {
            "object": "error",
            "type": "parameter_error",
            "param": "customer_id",
            "doc_url": "https://docs.revenuecat.com/errors/parameter_error",
            "message": "Invalid customer_id format",
            "retryable": false,
            "backoff_ms": null
        }
        """;

        // Act
        var error = JsonSerializer.Deserialize<RevenueCatError>(json, _jsonOptions);

        // Assert
        Assert.NotNull(error);
        Assert.Equal("error", error.Object);
        Assert.Equal(ErrorType.ParameterError, error.Type);
        Assert.Equal("customer_id", error.Param);
        Assert.Equal("https://docs.revenuecat.com/errors/parameter_error", error.DocUrl);
        Assert.Equal("Invalid customer_id format", error.Message);
        Assert.False(error.Retryable);
        Assert.Null(error.BackoffMs);
    }

    [Fact]
    public void RevenueCatError_Deserialize_WithBackoff_Success()
    {
        // Arrange
        var json = """
        {
            "object": "error",
            "type": "rate_limit_error",
            "doc_url": "https://docs.revenuecat.com/errors/rate_limit",
            "message": "Rate limit exceeded",
            "retryable": true,
            "backoff_ms": 5000
        }
        """;

        // Act
        var error = JsonSerializer.Deserialize<RevenueCatError>(json, _jsonOptions);

        // Assert
        Assert.NotNull(error);
        Assert.Equal(ErrorType.RateLimitError, error.Type);
        Assert.Equal("Rate limit exceeded", error.Message);
        Assert.True(error.Retryable);
        Assert.Equal(5000, error.BackoffMs);
    }

    [Fact]
    public void RevenueCatError_Deserialize_ResourceMissing_Success()
    {
        // Arrange
        var json = """
        {
            "object": "error",
            "type": "resource_missing",
            "doc_url": "https://docs.revenuecat.com/errors/resource_missing",
            "message": "Customer not found",
            "retryable": false
        }
        """;

        // Act
        var error = JsonSerializer.Deserialize<RevenueCatError>(json, _jsonOptions);

        // Assert
        Assert.NotNull(error);
        Assert.Equal(ErrorType.ResourceMissing, error.Type);
        Assert.Equal("Customer not found", error.Message);
        Assert.False(error.Retryable);
        Assert.Null(error.Param);
        Assert.Null(error.BackoffMs);
    }

    [Fact]
    public void RevenueCatError_Deserialize_AuthenticationError_Success()
    {
        // Arrange
        var json = """
        {
            "object": "error",
            "type": "authentication_error",
            "doc_url": "https://docs.revenuecat.com/errors/authentication",
            "message": "Invalid API key",
            "retryable": false
        }
        """;

        // Act
        var error = JsonSerializer.Deserialize<RevenueCatError>(json, _jsonOptions);

        // Assert
        Assert.NotNull(error);
        Assert.Equal(ErrorType.AuthenticationError, error.Type);
        Assert.Equal("Invalid API key", error.Message);
        Assert.False(error.Retryable);
    }

    [Fact]
    public void RevenueCatError_Deserialize_ServerError_Success()
    {
        // Arrange
        var json = """
        {
            "object": "error",
            "type": "server_error",
            "doc_url": "https://docs.revenuecat.com/errors/server_error",
            "message": "Internal server error",
            "retryable": true,
            "backoff_ms": 1000
        }
        """;

        // Act
        var error = JsonSerializer.Deserialize<RevenueCatError>(json, _jsonOptions);

        // Assert
        Assert.NotNull(error);
        Assert.Equal(ErrorType.ServerError, error.Type);
        Assert.Equal("Internal server error", error.Message);
        Assert.True(error.Retryable);
        Assert.Equal(1000, error.BackoffMs);
    }

    [Fact]
    public void RevenueCatError_Serialize_ProducesCorrectJson()
    {
        // Arrange
        var error = new RevenueCatError
        {
            Object = "error",
            Type = ErrorType.ParameterError,
            Param = "email",
            DocUrl = "https://docs.revenuecat.com/errors",
            Message = "Invalid email format",
            Retryable = false,
            BackoffMs = null
        };

        // Act
        var json = JsonSerializer.Serialize(error, _jsonOptions);

        // Assert
        Assert.Contains("\"object\":\"error\"", json);
        Assert.Contains("\"type\":\"parameter_error\"", json);
        Assert.Contains("\"param\":\"email\"", json);
        Assert.Contains("\"message\":\"Invalid email format\"", json);
        Assert.Contains("\"retryable\":false", json);
    }

    [Fact]
    public void RevenueCatError_DefaultValues_AreCorrect()
    {
        // Arrange & Act
        var error = new RevenueCatError();

        // Assert
        Assert.Equal("error", error.Object);
        Assert.Equal(ErrorType.ParameterError, error.Type); // Default enum value
        Assert.Null(error.Param);
        Assert.Equal(string.Empty, error.DocUrl);
        Assert.Equal(string.Empty, error.Message);
        Assert.False(error.Retryable);
        Assert.Null(error.BackoffMs);
    }

    [Theory]
    [InlineData(ErrorType.ParameterError)]
    [InlineData(ErrorType.ResourceAlreadyExists)]
    [InlineData(ErrorType.ResourceMissing)]
    [InlineData(ErrorType.IdempotencyError)]
    [InlineData(ErrorType.RateLimitError)]
    [InlineData(ErrorType.AuthenticationError)]
    [InlineData(ErrorType.AuthorizationError)]
    [InlineData(ErrorType.StoreError)]
    [InlineData(ErrorType.ServerError)]
    [InlineData(ErrorType.ResourceLockedError)]
    [InlineData(ErrorType.UnprocessableEntityError)]
    [InlineData(ErrorType.InvalidRequest)]
    public void RevenueCatError_AllErrorTypes_Deserialize_Success(ErrorType errorType)
    {
        // Arrange
        var errorTypeString = errorType.ToString().ToLower();
        // Convert PascalCase to snake_case
        errorTypeString = System.Text.RegularExpressions.Regex.Replace(errorTypeString, "([a-z])([A-Z])", "$1_$2").ToLower();
        
        var json = $$"""
        {
            "object": "error",
            "type": "{{errorTypeString}}",
            "doc_url": "https://docs.revenuecat.com/errors",
            "message": "Test error message",
            "retryable": false
        }
        """;

        // Act
        var error = JsonSerializer.Deserialize<RevenueCatError>(json, _jsonOptions);

        // Assert
        Assert.NotNull(error);
        Assert.Equal(errorType, error.Type);
        Assert.Equal("Test error message", error.Message);
    }
}
