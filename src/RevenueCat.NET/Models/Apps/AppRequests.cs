using System.Text.Json.Serialization;
using RevenueCat.NET.Models.Enums;

namespace RevenueCat.NET.Models.Apps;

/// <summary>
/// Request model for creating a new app.
/// </summary>
public class CreateAppRequest
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
    /// Amazon Appstore configuration (required when Type is Amazon).
    /// </summary>
    [JsonPropertyName("amazon")]
    public AmazonAppDetails? Amazon { get; set; }
    
    /// <summary>
    /// Apple App Store configuration (required when Type is AppStore).
    /// </summary>
    [JsonPropertyName("app_store")]
    public AppStoreDetails? AppStore { get; set; }
    
    /// <summary>
    /// Mac App Store configuration (required when Type is MacAppStore).
    /// </summary>
    [JsonPropertyName("mac_app_store")]
    public MacAppStoreDetails? MacAppStore { get; set; }
    
    /// <summary>
    /// Google Play Store configuration (required when Type is PlayStore).
    /// </summary>
    [JsonPropertyName("play_store")]
    public PlayStoreDetails? PlayStore { get; set; }
    
    /// <summary>
    /// Stripe configuration (required when Type is Stripe).
    /// </summary>
    [JsonPropertyName("stripe")]
    public StripeDetails? Stripe { get; set; }
    
    /// <summary>
    /// RevenueCat Web Billing configuration (required when Type is RCBilling).
    /// </summary>
    [JsonPropertyName("rc_billing")]
    public RCBillingDetails? RCBilling { get; set; }
    
    /// <summary>
    /// Roku configuration (required when Type is Roku).
    /// </summary>
    [JsonPropertyName("roku")]
    public RokuDetails? Roku { get; set; }
    
    /// <summary>
    /// Paddle configuration (required when Type is Paddle).
    /// </summary>
    [JsonPropertyName("paddle")]
    public PaddleDetails? Paddle { get; set; }
}

/// <summary>
/// Request model for updating an existing app.
/// </summary>
public class UpdateAppRequest
{
    /// <summary>
    /// The name of the app (optional).
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }
    
    /// <summary>
    /// Amazon Appstore configuration (optional).
    /// </summary>
    [JsonPropertyName("amazon")]
    public AmazonAppDetails? Amazon { get; set; }
    
    /// <summary>
    /// Apple App Store configuration (optional).
    /// </summary>
    [JsonPropertyName("app_store")]
    public AppStoreDetails? AppStore { get; set; }
    
    /// <summary>
    /// Mac App Store configuration (optional).
    /// </summary>
    [JsonPropertyName("mac_app_store")]
    public MacAppStoreDetails? MacAppStore { get; set; }
    
    /// <summary>
    /// Google Play Store configuration (optional).
    /// </summary>
    [JsonPropertyName("play_store")]
    public PlayStoreDetails? PlayStore { get; set; }
    
    /// <summary>
    /// Stripe configuration (optional).
    /// </summary>
    [JsonPropertyName("stripe")]
    public StripeDetails? Stripe { get; set; }
    
    /// <summary>
    /// RevenueCat Web Billing configuration (optional).
    /// </summary>
    [JsonPropertyName("rc_billing")]
    public RCBillingDetails? RCBilling { get; set; }
    
    /// <summary>
    /// Roku configuration (optional).
    /// </summary>
    [JsonPropertyName("roku")]
    public RokuDetails? Roku { get; set; }
    
    /// <summary>
    /// Paddle configuration (optional).
    /// </summary>
    [JsonPropertyName("paddle")]
    public PaddleDetails? Paddle { get; set; }
}
