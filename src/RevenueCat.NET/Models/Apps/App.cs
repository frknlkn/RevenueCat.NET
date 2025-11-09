using System.Text.Json.Serialization;
using RevenueCat.NET.Models.Enums;

namespace RevenueCat.NET.Models.Apps;

/// <summary>
/// Represents an app in RevenueCat with store-specific configuration.
/// </summary>
public class App : BaseModel
{
    /// <summary>
    /// The name of the app.
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// The type of app/store platform.
    /// </summary>
    [JsonPropertyName("type")]
    public AppType Type { get; set; }
    
    /// <summary>
    /// The ID of the project this app belongs to.
    /// </summary>
    [JsonPropertyName("project_id")]
    public string ProjectId { get; set; } = string.Empty;
    
    /// <summary>
    /// Amazon Appstore specific configuration (only populated when Type is Amazon).
    /// </summary>
    [JsonPropertyName("amazon")]
    public AmazonAppDetails? Amazon { get; set; }
    
    /// <summary>
    /// Apple App Store specific configuration (only populated when Type is AppStore).
    /// </summary>
    [JsonPropertyName("app_store")]
    public AppStoreDetails? AppStore { get; set; }
    
    /// <summary>
    /// Mac App Store specific configuration (only populated when Type is MacAppStore).
    /// </summary>
    [JsonPropertyName("mac_app_store")]
    public MacAppStoreDetails? MacAppStore { get; set; }
    
    /// <summary>
    /// Google Play Store specific configuration (only populated when Type is PlayStore).
    /// </summary>
    [JsonPropertyName("play_store")]
    public PlayStoreDetails? PlayStore { get; set; }
    
    /// <summary>
    /// Stripe specific configuration (only populated when Type is Stripe).
    /// </summary>
    [JsonPropertyName("stripe")]
    public StripeDetails? Stripe { get; set; }
    
    /// <summary>
    /// RevenueCat Web Billing specific configuration (only populated when Type is RCBilling).
    /// </summary>
    [JsonPropertyName("rc_billing")]
    public RCBillingDetails? RCBilling { get; set; }
    
    /// <summary>
    /// Roku specific configuration (only populated when Type is Roku).
    /// </summary>
    [JsonPropertyName("roku")]
    public RokuDetails? Roku { get; set; }
    
    /// <summary>
    /// Paddle specific configuration (only populated when Type is Paddle).
    /// </summary>
    [JsonPropertyName("paddle")]
    public PaddleDetails? Paddle { get; set; }
}

/// <summary>
/// Amazon Appstore specific app configuration.
/// </summary>
public class AmazonAppDetails
{
    /// <summary>
    /// The package name for the Amazon app.
    /// </summary>
    [JsonPropertyName("package_name")]
    public string PackageName { get; set; } = string.Empty;
    
    /// <summary>
    /// The shared secret for Amazon IAP verification.
    /// </summary>
    [JsonPropertyName("shared_secret")]
    public string? SharedSecret { get; set; }
}

/// <summary>
/// Apple App Store specific app configuration.
/// </summary>
public class AppStoreDetails
{
    /// <summary>
    /// The bundle identifier for the iOS app.
    /// </summary>
    [JsonPropertyName("bundle_id")]
    public string BundleId { get; set; } = string.Empty;
    
    /// <summary>
    /// The shared secret for App Store receipt verification.
    /// </summary>
    [JsonPropertyName("shared_secret")]
    public string? SharedSecret { get; set; }
}

/// <summary>
/// Mac App Store specific app configuration.
/// </summary>
public class MacAppStoreDetails
{
    /// <summary>
    /// The bundle identifier for the macOS app.
    /// </summary>
    [JsonPropertyName("bundle_id")]
    public string BundleId { get; set; } = string.Empty;
    
    /// <summary>
    /// The shared secret for Mac App Store receipt verification.
    /// </summary>
    [JsonPropertyName("shared_secret")]
    public string? SharedSecret { get; set; }
}

/// <summary>
/// Google Play Store specific app configuration.
/// </summary>
public class PlayStoreDetails
{
    /// <summary>
    /// The package name for the Android app.
    /// </summary>
    [JsonPropertyName("package_name")]
    public string PackageName { get; set; } = string.Empty;
}

/// <summary>
/// Stripe specific app configuration.
/// </summary>
public class StripeDetails
{
    /// <summary>
    /// The Stripe account ID for this app.
    /// </summary>
    [JsonPropertyName("stripe_account_id")]
    public string? StripeAccountId { get; set; }
}

/// <summary>
/// RevenueCat Web Billing specific app configuration.
/// </summary>
public class RCBillingDetails
{
    /// <summary>
    /// The Stripe account ID for RevenueCat Web Billing.
    /// </summary>
    [JsonPropertyName("stripe_account_id")]
    public string? StripeAccountId { get; set; }
    
    /// <summary>
    /// The app name displayed to customers.
    /// </summary>
    [JsonPropertyName("app_name")]
    public string? AppName { get; set; }
    
    /// <summary>
    /// The support email for customer inquiries.
    /// </summary>
    [JsonPropertyName("support_email")]
    public string? SupportEmail { get; set; }
    
    /// <summary>
    /// The default currency for pricing.
    /// </summary>
    [JsonPropertyName("default_currency")]
    public string? DefaultCurrency { get; set; }
}

/// <summary>
/// Roku specific app configuration.
/// </summary>
public class RokuDetails
{
    /// <summary>
    /// The Roku API key.
    /// </summary>
    [JsonPropertyName("roku_api_key")]
    public string? RokuApiKey { get; set; }
    
    /// <summary>
    /// The Roku channel ID.
    /// </summary>
    [JsonPropertyName("roku_channel_id")]
    public string? RokuChannelId { get; set; }
    
    /// <summary>
    /// The Roku channel name.
    /// </summary>
    [JsonPropertyName("roku_channel_name")]
    public string? RokuChannelName { get; set; }
}

/// <summary>
/// Paddle specific app configuration.
/// </summary>
public class PaddleDetails
{
    /// <summary>
    /// The Paddle API key.
    /// </summary>
    [JsonPropertyName("paddle_api_key")]
    public string? PaddleApiKey { get; set; }
    
    /// <summary>
    /// Whether this Paddle configuration is for sandbox environment.
    /// </summary>
    [JsonPropertyName("paddle_is_sandbox")]
    public bool PaddleIsSandbox { get; set; }
}
