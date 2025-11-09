using System.Text.Json;
using RevenueCat.NET.Models.Enums;
using Environment = RevenueCat.NET.Models.Enums.Environment;

namespace RevenueCat.NET.Tests.Models;

public class EnumSerializationTests
{
    private readonly JsonSerializerOptions _jsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
    };

    [Theory]
    [InlineData("\"production\"", Environment.Production)]
    [InlineData("\"sandbox\"", Environment.Sandbox)]
    public void Environment_Deserialize_Success(string json, Environment expected)
    {
        // Act
        var result = JsonSerializer.Deserialize<Environment>(json, _jsonOptions);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(Environment.Production, "production")]
    [InlineData(Environment.Sandbox, "sandbox")]
    public void Environment_Serialize_Success(Environment value, string expected)
    {
        // Act
        var json = JsonSerializer.Serialize(value, _jsonOptions);

        // Assert
        Assert.Contains(expected, json);
    }

    [Theory]
    [InlineData("\"subscription\"", ProductType.Subscription)]
    [InlineData("\"one_time\"", ProductType.OneTime)]
    [InlineData("\"consumable\"", ProductType.Consumable)]
    [InlineData("\"non_consumable\"", ProductType.NonConsumable)]
    [InlineData("\"non_renewing_subscription\"", ProductType.NonRenewingSubscription)]
    public void ProductType_Deserialize_Success(string json, ProductType expected)
    {
        // Act
        var result = JsonSerializer.Deserialize<ProductType>(json, _jsonOptions);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(ProductType.Subscription, "subscription")]
    [InlineData(ProductType.OneTime, "one_time")]
    [InlineData(ProductType.Consumable, "consumable")]
    [InlineData(ProductType.NonConsumable, "non_consumable")]
    [InlineData(ProductType.NonRenewingSubscription, "non_renewing_subscription")]
    public void ProductType_Serialize_Success(ProductType value, string expected)
    {
        // Act
        var json = JsonSerializer.Serialize(value, _jsonOptions);

        // Assert
        Assert.Contains(expected, json);
    }

    [Theory]
    [InlineData("\"amazon\"", Store.Amazon)]
    [InlineData("\"app_store\"", Store.AppStore)]
    [InlineData("\"mac_app_store\"", Store.MacAppStore)]
    [InlineData("\"play_store\"", Store.PlayStore)]
    [InlineData("\"promotional\"", Store.Promotional)]
    [InlineData("\"stripe\"", Store.Stripe)]
    [InlineData("\"rc_billing\"", Store.RCBilling)]
    [InlineData("\"roku\"", Store.Roku)]
    [InlineData("\"paddle\"", Store.Paddle)]
    public void Store_Deserialize_Success(string json, Store expected)
    {
        // Act
        var result = JsonSerializer.Deserialize<Store>(json, _jsonOptions);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(Store.Amazon, "amazon")]
    [InlineData(Store.AppStore, "app_store")]
    [InlineData(Store.MacAppStore, "mac_app_store")]
    [InlineData(Store.PlayStore, "play_store")]
    [InlineData(Store.Promotional, "promotional")]
    [InlineData(Store.Stripe, "stripe")]
    [InlineData(Store.RCBilling, "rc_billing")]
    [InlineData(Store.Roku, "roku")]
    [InlineData(Store.Paddle, "paddle")]
    public void Store_Serialize_Success(Store value, string expected)
    {
        // Act
        var json = JsonSerializer.Serialize(value, _jsonOptions);

        // Assert
        Assert.Contains(expected, json);
    }

    [Theory]
    [InlineData("\"parameter_error\"", ErrorType.ParameterError)]
    [InlineData("\"resource_already_exists\"", ErrorType.ResourceAlreadyExists)]
    [InlineData("\"resource_missing\"", ErrorType.ResourceMissing)]
    [InlineData("\"idempotency_error\"", ErrorType.IdempotencyError)]
    [InlineData("\"rate_limit_error\"", ErrorType.RateLimitError)]
    [InlineData("\"authentication_error\"", ErrorType.AuthenticationError)]
    [InlineData("\"authorization_error\"", ErrorType.AuthorizationError)]
    [InlineData("\"store_error\"", ErrorType.StoreError)]
    [InlineData("\"server_error\"", ErrorType.ServerError)]
    [InlineData("\"resource_locked_error\"", ErrorType.ResourceLockedError)]
    [InlineData("\"unprocessable_entity_error\"", ErrorType.UnprocessableEntityError)]
    [InlineData("\"invalid_request\"", ErrorType.InvalidRequest)]
    public void ErrorType_Deserialize_Success(string json, ErrorType expected)
    {
        // Act
        var result = JsonSerializer.Deserialize<ErrorType>(json, _jsonOptions);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(ErrorType.ParameterError, "parameter_error")]
    [InlineData(ErrorType.ResourceAlreadyExists, "resource_already_exists")]
    [InlineData(ErrorType.ResourceMissing, "resource_missing")]
    [InlineData(ErrorType.IdempotencyError, "idempotency_error")]
    [InlineData(ErrorType.RateLimitError, "rate_limit_error")]
    [InlineData(ErrorType.AuthenticationError, "authentication_error")]
    [InlineData(ErrorType.AuthorizationError, "authorization_error")]
    [InlineData(ErrorType.StoreError, "store_error")]
    [InlineData(ErrorType.ServerError, "server_error")]
    [InlineData(ErrorType.ResourceLockedError, "resource_locked_error")]
    [InlineData(ErrorType.UnprocessableEntityError, "unprocessable_entity_error")]
    [InlineData(ErrorType.InvalidRequest, "invalid_request")]
    public void ErrorType_Serialize_Success(ErrorType value, string expected)
    {
        // Act
        var json = JsonSerializer.Serialize(value, _jsonOptions);

        // Assert
        Assert.Contains(expected, json);
    }

    [Fact]
    public void Enum_InObject_Deserialize_Success()
    {
        // Arrange
        var json = """
        {
            "environment": "production",
            "store": "app_store",
            "product_type": "subscription"
        }
        """;

        // Act
        var obj = JsonSerializer.Deserialize<TestObject>(json, _jsonOptions);

        // Assert
        Assert.NotNull(obj);
        Assert.Equal(Environment.Production, obj.Environment);
        Assert.Equal(Store.AppStore, obj.Store);
        Assert.Equal(ProductType.Subscription, obj.ProductType);
    }

    [Fact]
    public void Enum_InObject_Serialize_Success()
    {
        // Arrange
        var obj = new TestObject
        {
            Environment = Environment.Sandbox,
            Store = Store.PlayStore,
            ProductType = ProductType.OneTime
        };

        // Act
        var json = JsonSerializer.Serialize(obj, _jsonOptions);

        // Assert
        Assert.Contains("\"environment\":\"sandbox\"", json);
        Assert.Contains("\"store\":\"play_store\"", json);
        Assert.Contains("\"product_type\":\"one_time\"", json);
    }

    private class TestObject
    {
        public Environment Environment { get; set; }
        public Store Store { get; set; }
        public ProductType ProductType { get; set; }
    }
}
