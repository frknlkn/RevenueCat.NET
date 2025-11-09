using System.Text.Json.Serialization;
using RevenueCat.NET.Serialization;

namespace RevenueCat.NET.Models.Enums;

/// <summary>
/// The store where the purchase or subscription was made.
/// </summary>
[JsonConverter(typeof(SnakeCaseEnumConverter<Store>))]
public enum Store
{
    /// <summary>
    /// Amazon Appstore.
    /// </summary>
    [JsonPropertyName("amazon")]
    Amazon,
    
    /// <summary>
    /// Apple App Store.
    /// </summary>
    [JsonPropertyName("app_store")]
    AppStore,
    
    /// <summary>
    /// Legacy Mac App Store.
    /// </summary>
    [JsonPropertyName("mac_app_store")]
    MacAppStore,
    
    /// <summary>
    /// Google Play Store.
    /// </summary>
    [JsonPropertyName("play_store")]
    PlayStore,
    
    /// <summary>
    /// Promotional/free access granted by RevenueCat.
    /// </summary>
    [JsonPropertyName("promotional")]
    Promotional,
    
    /// <summary>
    /// Stripe payment processor.
    /// </summary>
    [JsonPropertyName("stripe")]
    Stripe,
    
    /// <summary>
    /// RevenueCat Web Billing.
    /// </summary>
    [JsonPropertyName("rc_billing")]
    RCBilling,
    
    /// <summary>
    /// Roku Channel Store.
    /// </summary>
    [JsonPropertyName("roku")]
    Roku,
    
    /// <summary>
    /// Paddle Billing.
    /// </summary>
    [JsonPropertyName("paddle")]
    Paddle
}
