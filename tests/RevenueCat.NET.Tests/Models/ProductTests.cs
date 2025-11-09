using System.Text.Json;
using RevenueCat.NET.Models.Enums;
using RevenueCat.NET.Models.Products;
using RevenueCat.NET.Models.Subscriptions;

namespace RevenueCat.NET.Tests.Models;

public class ProductTests
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
    };

    [Fact]
    public void Product_Deserialize_WithSubscriptionProduct_Success()
    {
        // Arrange
        var json = """
        {
            "object": "product",
            "id": "prod_123",
            "created_at": 1699564800000,
            "store_identifier": "com.example.monthly",
            "type": "subscription",
            "subscription": {
                "duration": "P1M",
                "grace_period_duration": "P3D",
                "trial_duration": "P7D"
            },
            "app_id": "app_456",
            "display_name": "Monthly Subscription"
        }
        """;

        // Act
        var product = JsonSerializer.Deserialize<Product>(json, JsonOptions);

        // Assert
        Assert.NotNull(product);
        Assert.Equal("product", product.Object);
        Assert.Equal("prod_123", product.Id);
        Assert.Equal(1699564800000, product.CreatedAt);
        Assert.Equal("com.example.monthly", product.StoreIdentifier);
        Assert.Equal(ProductType.Subscription, product.Type);
        Assert.Equal("app_456", product.AppId);
        Assert.Equal("Monthly Subscription", product.DisplayName);
        
        Assert.NotNull(product.Subscription);
        Assert.Equal("P1M", product.Subscription.Duration);
        Assert.Equal("P3D", product.Subscription.GracePeriodDuration);
        Assert.Equal("P7D", product.Subscription.TrialDuration);
        
        Assert.Null(product.OneTime);
    }

    [Fact]
    public void Product_Deserialize_WithOneTimeProduct_Success()
    {
        // Arrange
        var json = """
        {
            "object": "product",
            "id": "prod_789",
            "created_at": 1699564800000,
            "store_identifier": "com.example.coins",
            "type": "consumable",
            "one_time": {
                "is_consumable": true
            },
            "app_id": "app_456",
            "display_name": "100 Coins"
        }
        """;

        // Act
        var product = JsonSerializer.Deserialize<Product>(json, JsonOptions);

        // Assert
        Assert.NotNull(product);
        Assert.Equal("prod_789", product.Id);
        Assert.Equal("com.example.coins", product.StoreIdentifier);
        Assert.Equal(ProductType.Consumable, product.Type);
        Assert.Equal("100 Coins", product.DisplayName);
        
        Assert.NotNull(product.OneTime);
        Assert.True(product.OneTime.IsConsumable);
        
        Assert.Null(product.Subscription);
    }

    [Fact]
    public void Product_Deserialize_WithExpandedApp_Success()
    {
        // Arrange
        var json = """
        {
            "object": "product",
            "id": "prod_123",
            "created_at": 1699564800000,
            "store_identifier": "com.example.monthly",
            "type": "subscription",
            "app_id": "app_456",
            "app": {
                "object": "app",
                "id": "app_456",
                "name": "My App"
            },
            "display_name": "Monthly Subscription"
        }
        """;

        // Act
        var product = JsonSerializer.Deserialize<Product>(json, JsonOptions);

        // Assert
        Assert.NotNull(product);
        Assert.Equal("prod_123", product.Id);
        Assert.Equal("app_456", product.AppId);
        Assert.NotNull(product.App);
    }

    [Fact]
    public void Product_Deserialize_WithMinimalProperties_Success()
    {
        // Arrange
        var json = """
        {
            "object": "product",
            "id": "prod_123",
            "created_at": 1699564800000,
            "store_identifier": "com.example.product",
            "type": "non_consumable",
            "app_id": "app_456"
        }
        """;

        // Act
        var product = JsonSerializer.Deserialize<Product>(json, JsonOptions);

        // Assert
        Assert.NotNull(product);
        Assert.Equal("prod_123", product.Id);
        Assert.Equal("com.example.product", product.StoreIdentifier);
        Assert.Equal(ProductType.NonConsumable, product.Type);
        Assert.Equal("app_456", product.AppId);
        Assert.Null(product.DisplayName);
        Assert.Null(product.Subscription);
        Assert.Null(product.OneTime);
        Assert.Null(product.App);
    }

    [Fact]
    public void OneTimeProduct_Deserialize_Success()
    {
        // Arrange
        var json = """
        {
            "is_consumable": false
        }
        """;

        // Act
        var oneTime = JsonSerializer.Deserialize<OneTimeProduct>(json, JsonOptions);

        // Assert
        Assert.NotNull(oneTime);
        Assert.False(oneTime.IsConsumable);
    }

    [Fact]
    public void StoreProduct_Deserialize_Success()
    {
        // Arrange
        var json = """
        {
            "object": "store_product",
            "store_identifier": "com.example.premium",
            "display_name": "Premium Subscription",
            "description": "Get access to all premium features"
        }
        """;

        // Act
        var storeProduct = JsonSerializer.Deserialize<StoreProduct>(json, JsonOptions);

        // Assert
        Assert.NotNull(storeProduct);
        Assert.Equal("store_product", storeProduct.Object);
        Assert.Equal("com.example.premium", storeProduct.StoreIdentifier);
        Assert.Equal("Premium Subscription", storeProduct.DisplayName);
        Assert.Equal("Get access to all premium features", storeProduct.Description);
    }

    [Fact]
    public void StoreProduct_Deserialize_WithMinimalProperties_Success()
    {
        // Arrange
        var json = """
        {
            "object": "store_product",
            "store_identifier": "com.example.basic"
        }
        """;

        // Act
        var storeProduct = JsonSerializer.Deserialize<StoreProduct>(json, JsonOptions);

        // Assert
        Assert.NotNull(storeProduct);
        Assert.Equal("com.example.basic", storeProduct.StoreIdentifier);
        Assert.Null(storeProduct.DisplayName);
        Assert.Null(storeProduct.Description);
    }
}
