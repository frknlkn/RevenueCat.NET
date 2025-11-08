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
    }

    [Fact]
    public void Constructor_WithNullApiKey_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentNullException>(() => new RevenueCatClient(null!));
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
}