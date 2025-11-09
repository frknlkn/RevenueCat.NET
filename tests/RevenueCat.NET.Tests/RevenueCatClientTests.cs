using RevenueCat.NET.Configuration;

namespace RevenueCat.NET.Tests;

public class RevenueCatClientTests
{
    [Fact]
    public void Constructor_WithValidApiKey_CreatesInstance()
    {
        var client = new RevenueCatClient("test_api_key");

        Assert.NotNull(client);
        Assert.NotNull(client.Projects);
        Assert.NotNull(client.Apps);
        Assert.NotNull(client.Customers);
        Assert.NotNull(client.Products);
        Assert.NotNull(client.Entitlements);
        Assert.NotNull(client.Offerings);
        Assert.NotNull(client.Packages);
        Assert.NotNull(client.Subscriptions);
        Assert.NotNull(client.Purchases);
        Assert.NotNull(client.Invoices);
        Assert.NotNull(client.Paywalls);
        Assert.NotNull(client.Charts);
    }

    [Fact]
    public void Constructor_WithNullApiKey_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentNullException>(() => new RevenueCatClient(null!));
    }

    [Fact]
    public void Constructor_WithEmptyApiKey_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new RevenueCatClient(string.Empty));
    }

    [Fact]
    public void Constructor_WithWhitespaceApiKey_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new RevenueCatClient("   "));
    }

    [Fact]
    public void Constructor_WithOptions_CreatesInstance()
    {
        var client = new RevenueCatClient("test_api_key", options =>
        {
            options.Timeout = TimeSpan.FromSeconds(60);
            options.MaxRetryAttempts = 5;
        });

        Assert.NotNull(client);
    }

    [Fact]
    public void Constructor_WithOptionsObject_CreatesInstance()
    {
        var options = new RevenueCatOptions
        {
            ApiKey = "test_api_key",
            BaseUrl = "https://api.revenuecat.com/v2",
            Timeout = TimeSpan.FromSeconds(45),
            MaxRetryAttempts = 4,
            RetryDelay = TimeSpan.FromSeconds(1),
            EnableRetryOnRateLimit = true,
            ThrowOnError = true
        };

        var client = new RevenueCatClient(options);

        Assert.NotNull(client);
        Assert.NotNull(client.Projects);
        Assert.NotNull(client.Apps);
        Assert.NotNull(client.Customers);
        Assert.NotNull(client.Products);
        Assert.NotNull(client.Entitlements);
        Assert.NotNull(client.Offerings);
        Assert.NotNull(client.Packages);
        Assert.NotNull(client.Subscriptions);
        Assert.NotNull(client.Purchases);
        Assert.NotNull(client.Invoices);
        Assert.NotNull(client.Paywalls);
        Assert.NotNull(client.Charts);
    }

    [Fact]
    public void Constructor_WithNullOptions_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => new RevenueCatClient((RevenueCatOptions)null!));
    }

    [Fact]
    public void Constructor_WithCustomBaseUrl_CreatesInstance()
    {
        var client = new RevenueCatClient("test_api_key", options =>
        {
            options.BaseUrl = "https://custom.api.com/v2";
        });

        Assert.NotNull(client);
    }

    [Fact]
    public void Constructor_WithCustomTimeout_CreatesInstance()
    {
        var client = new RevenueCatClient("test_api_key", options =>
        {
            options.Timeout = TimeSpan.FromMinutes(2);
        });

        Assert.NotNull(client);
    }

    [Fact]
    public void Constructor_WithCustomRetrySettings_CreatesInstance()
    {
        var client = new RevenueCatClient("test_api_key", options =>
        {
            options.MaxRetryAttempts = 10;
            options.RetryDelay = TimeSpan.FromSeconds(2);
            options.EnableRetryOnRateLimit = false;
        });

        Assert.NotNull(client);
    }

    [Fact]
    public void AllServices_AreAccessible()
    {
        var client = new RevenueCatClient("test_api_key");

        // Verify all services are accessible and not null
        Assert.NotNull(client.Projects);
        Assert.NotNull(client.Apps);
        Assert.NotNull(client.Customers);
        Assert.NotNull(client.Products);
        Assert.NotNull(client.Entitlements);
        Assert.NotNull(client.Offerings);
        Assert.NotNull(client.Packages);
        Assert.NotNull(client.Subscriptions);
        Assert.NotNull(client.Purchases);
        Assert.NotNull(client.Invoices);
        Assert.NotNull(client.Paywalls);
        Assert.NotNull(client.Charts);
    }

    [Fact]
    public void Services_AreOfCorrectType()
    {
        var client = new RevenueCatClient("test_api_key");

        // Verify services implement their interfaces
        Assert.IsAssignableFrom<RevenueCat.NET.Services.IProjectService>(client.Projects);
        Assert.IsAssignableFrom<RevenueCat.NET.Services.IAppService>(client.Apps);
        Assert.IsAssignableFrom<RevenueCat.NET.Services.ICustomerService>(client.Customers);
        Assert.IsAssignableFrom<RevenueCat.NET.Services.IProductService>(client.Products);
        Assert.IsAssignableFrom<RevenueCat.NET.Services.IEntitlementService>(client.Entitlements);
        Assert.IsAssignableFrom<RevenueCat.NET.Services.IOfferingService>(client.Offerings);
        Assert.IsAssignableFrom<RevenueCat.NET.Services.IPackageService>(client.Packages);
        Assert.IsAssignableFrom<RevenueCat.NET.Services.ISubscriptionService>(client.Subscriptions);
        Assert.IsAssignableFrom<RevenueCat.NET.Services.IPurchaseService>(client.Purchases);
        Assert.IsAssignableFrom<RevenueCat.NET.Services.IInvoiceService>(client.Invoices);
        Assert.IsAssignableFrom<RevenueCat.NET.Services.IPaywallService>(client.Paywalls);
        Assert.IsAssignableFrom<RevenueCat.NET.Services.IChartsService>(client.Charts);
    }
}