using System.Text.Json;
using RevenueCat.NET.Models.Apps;
using RevenueCat.NET.Models.Enums;

namespace RevenueCat.NET.Tests.Models;

public class AppTests
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
    };

    [Fact]
    public void App_Deserialize_WithAppStoreDetails_Success()
    {
        // Arrange
        var json = """
        {
            "object": "app",
            "id": "app_123",
            "created_at": 1699564800000,
            "name": "My iOS App",
            "type": "app_store",
            "project_id": "proj_456",
            "app_store": {
                "bundle_id": "com.example.myapp",
                "shared_secret": "secret_789"
            }
        }
        """;

        // Act
        var app = JsonSerializer.Deserialize<App>(json, JsonOptions);

        // Assert
        Assert.NotNull(app);
        Assert.Equal("app", app.Object);
        Assert.Equal("app_123", app.Id);
        Assert.Equal(1699564800000, app.CreatedAt);
        Assert.Equal("My iOS App", app.Name);
        Assert.Equal(AppType.AppStore, app.Type);
        Assert.Equal("proj_456", app.ProjectId);
        
        Assert.NotNull(app.AppStore);
        Assert.Equal("com.example.myapp", app.AppStore.BundleId);
        Assert.Equal("secret_789", app.AppStore.SharedSecret);
        
        Assert.Null(app.PlayStore);
        Assert.Null(app.Stripe);
    }

    [Fact]
    public void App_Deserialize_WithPlayStoreDetails_Success()
    {
        // Arrange
        var json = """
        {
            "object": "app",
            "id": "app_123",
            "created_at": 1699564800000,
            "name": "My Android App",
            "type": "play_store",
            "project_id": "proj_456",
            "play_store": {
                "package_name": "com.example.myapp"
            }
        }
        """;

        // Act
        var app = JsonSerializer.Deserialize<App>(json, JsonOptions);

        // Assert
        Assert.NotNull(app);
        Assert.Equal("My Android App", app.Name);
        Assert.Equal(AppType.PlayStore, app.Type);
        
        Assert.NotNull(app.PlayStore);
        Assert.Equal("com.example.myapp", app.PlayStore.PackageName);
        
        Assert.Null(app.AppStore);
    }

    [Fact]
    public void App_Deserialize_WithStripeDetails_Success()
    {
        // Arrange
        var json = """
        {
            "object": "app",
            "id": "app_123",
            "created_at": 1699564800000,
            "name": "My Web App",
            "type": "stripe",
            "project_id": "proj_456",
            "stripe": {
                "stripe_account_id": "acct_123"
            }
        }
        """;

        // Act
        var app = JsonSerializer.Deserialize<App>(json, JsonOptions);

        // Assert
        Assert.NotNull(app);
        Assert.Equal(AppType.Stripe, app.Type);
        
        Assert.NotNull(app.Stripe);
        Assert.Equal("acct_123", app.Stripe.StripeAccountId);
    }

    [Fact]
    public void App_Deserialize_WithRCBillingDetails_Success()
    {
        // Arrange
        var json = """
        {
            "object": "app",
            "id": "app_123",
            "created_at": 1699564800000,
            "name": "My RC Billing App",
            "type": "rc_billing",
            "project_id": "proj_456",
            "rc_billing": {
                "stripe_account_id": "acct_123",
                "app_name": "My App",
                "support_email": "support@example.com",
                "default_currency": "USD"
            }
        }
        """;

        // Act
        var app = JsonSerializer.Deserialize<App>(json, JsonOptions);

        // Assert
        Assert.NotNull(app);
        Assert.Equal(AppType.RCBilling, app.Type);
        
        Assert.NotNull(app.RCBilling);
        Assert.Equal("acct_123", app.RCBilling.StripeAccountId);
        Assert.Equal("My App", app.RCBilling.AppName);
        Assert.Equal("support@example.com", app.RCBilling.SupportEmail);
        Assert.Equal("USD", app.RCBilling.DefaultCurrency);
    }

    [Fact]
    public void App_Deserialize_WithAmazonDetails_Success()
    {
        // Arrange
        var json = """
        {
            "object": "app",
            "id": "app_123",
            "created_at": 1699564800000,
            "name": "My Amazon App",
            "type": "amazon",
            "project_id": "proj_456",
            "amazon": {
                "package_name": "com.example.myapp",
                "shared_secret": "secret_789"
            }
        }
        """;

        // Act
        var app = JsonSerializer.Deserialize<App>(json, JsonOptions);

        // Assert
        Assert.NotNull(app);
        Assert.Equal(AppType.Amazon, app.Type);
        
        Assert.NotNull(app.Amazon);
        Assert.Equal("com.example.myapp", app.Amazon.PackageName);
        Assert.Equal("secret_789", app.Amazon.SharedSecret);
    }

    [Fact]
    public void App_Deserialize_WithRokuDetails_Success()
    {
        // Arrange
        var json = """
        {
            "object": "app",
            "id": "app_123",
            "created_at": 1699564800000,
            "name": "My Roku Channel",
            "type": "roku",
            "project_id": "proj_456",
            "roku": {
                "roku_api_key": "key_123",
                "roku_channel_id": "channel_456",
                "roku_channel_name": "My Channel"
            }
        }
        """;

        // Act
        var app = JsonSerializer.Deserialize<App>(json, JsonOptions);

        // Assert
        Assert.NotNull(app);
        Assert.Equal(AppType.Roku, app.Type);
        
        Assert.NotNull(app.Roku);
        Assert.Equal("key_123", app.Roku.RokuApiKey);
        Assert.Equal("channel_456", app.Roku.RokuChannelId);
        Assert.Equal("My Channel", app.Roku.RokuChannelName);
    }

    [Fact]
    public void App_Deserialize_WithPaddleDetails_Success()
    {
        // Arrange
        var json = """
        {
            "object": "app",
            "id": "app_123",
            "created_at": 1699564800000,
            "name": "My Paddle App",
            "type": "paddle",
            "project_id": "proj_456",
            "paddle": {
                "paddle_api_key": "key_123",
                "paddle_is_sandbox": true
            }
        }
        """;

        // Act
        var app = JsonSerializer.Deserialize<App>(json, JsonOptions);

        // Assert
        Assert.NotNull(app);
        Assert.Equal(AppType.Paddle, app.Type);
        
        Assert.NotNull(app.Paddle);
        Assert.Equal("key_123", app.Paddle.PaddleApiKey);
        Assert.True(app.Paddle.PaddleIsSandbox);
    }

    [Fact]
    public void App_Deserialize_WithMacAppStoreDetails_Success()
    {
        // Arrange
        var json = """
        {
            "object": "app",
            "id": "app_123",
            "created_at": 1699564800000,
            "name": "My Mac App",
            "type": "mac_app_store",
            "project_id": "proj_456",
            "mac_app_store": {
                "bundle_id": "com.example.macapp",
                "shared_secret": "secret_789"
            }
        }
        """;

        // Act
        var app = JsonSerializer.Deserialize<App>(json, JsonOptions);

        // Assert
        Assert.NotNull(app);
        Assert.Equal(AppType.MacAppStore, app.Type);
        
        Assert.NotNull(app.MacAppStore);
        Assert.Equal("com.example.macapp", app.MacAppStore.BundleId);
        Assert.Equal("secret_789", app.MacAppStore.SharedSecret);
    }

    [Fact]
    public void PublicApiKey_Deserialize_Success()
    {
        // Arrange
        var json = """
        {
            "object": "public_api_key",
            "id": "key_123",
            "created_at": 1699564800000,
            "key": "pk_abc123",
            "app_id": "app_456"
        }
        """;

        // Act
        var apiKey = JsonSerializer.Deserialize<PublicApiKey>(json, JsonOptions);

        // Assert
        Assert.NotNull(apiKey);
        Assert.Equal("public_api_key", apiKey.Object);
        Assert.Equal("key_123", apiKey.Id);
        Assert.Equal(1699564800000, apiKey.CreatedAt);
        Assert.Equal("pk_abc123", apiKey.Key);
        Assert.Equal("app_456", apiKey.AppId);
    }

    [Fact]
    public void StoreKitConfigFile_Deserialize_Success()
    {
        // Arrange
        var json = """
        {
            "object": "storekit_config_file",
            "content": "{\n  \"version\": \"1.0\"\n}"
        }
        """;

        // Act
        var config = JsonSerializer.Deserialize<StoreKitConfigFile>(json, JsonOptions);

        // Assert
        Assert.NotNull(config);
        Assert.Equal("storekit_config_file", config.Object);
        Assert.Equal("{\n  \"version\": \"1.0\"\n}", config.Content);
    }
}
