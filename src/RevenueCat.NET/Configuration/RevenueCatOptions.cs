namespace RevenueCat.NET.Configuration;

public sealed class RevenueCatOptions
{
    public required string ApiKey { get; set; }
    public string BaseUrl { get; set; } = "https://api.revenuecat.com/v2";
    public TimeSpan Timeout { get; set; } = TimeSpan.FromSeconds(30);
    public int MaxRetryAttempts { get; set; } = 3;
    public TimeSpan RetryDelay { get; set; } = TimeSpan.FromMilliseconds(500);
    public bool EnableRetryOnRateLimit { get; set; } = true;
    public bool ThrowOnError { get; set; } = true;
}
