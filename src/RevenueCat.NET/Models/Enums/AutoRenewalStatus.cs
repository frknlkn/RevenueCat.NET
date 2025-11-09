using System.Text.Json.Serialization;
using RevenueCat.NET.Serialization;

namespace RevenueCat.NET.Models.Enums;

/// <summary>
/// The auto-renewal status of a subscription.
/// </summary>
[JsonConverter(typeof(SnakeCaseEnumConverter<AutoRenewalStatus>))]
public enum AutoRenewalStatus
{
    /// <summary>
    /// The subscription will renew at the end of the current period.
    /// </summary>
    [JsonPropertyName("will_renew")]
    WillRenew,
    
    /// <summary>
    /// The subscription will not renew and will expire at the end of the current period.
    /// </summary>
    [JsonPropertyName("will_not_renew")]
    WillNotRenew,
    
    /// <summary>
    /// The subscription will change to a different product at the end of the current period.
    /// </summary>
    [JsonPropertyName("will_change_product")]
    WillChangeProduct,
    
    /// <summary>
    /// The subscription will pause at the end of the current period.
    /// </summary>
    [JsonPropertyName("will_pause")]
    WillPause,
    
    /// <summary>
    /// The subscription requires price increase consent from the customer.
    /// </summary>
    [JsonPropertyName("requires_price_increase_consent")]
    RequiresPriceIncreaseConsent,
    
    /// <summary>
    /// The subscription has already renewed for the next period.
    /// </summary>
    [JsonPropertyName("has_already_renewed")]
    HasAlreadyRenewed
}
