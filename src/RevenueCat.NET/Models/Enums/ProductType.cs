using System.Text.Json.Serialization;
using RevenueCat.NET.Serialization;

namespace RevenueCat.NET.Models.Enums;

/// <summary>
/// The type of product.
/// </summary>
[JsonConverter(typeof(SnakeCaseEnumConverter<ProductType>))]
public enum ProductType
{
    /// <summary>
    /// A subscription product that renews automatically.
    /// </summary>
    [JsonPropertyName("subscription")]
    Subscription,
    
    /// <summary>
    /// A one-time purchase product.
    /// </summary>
    [JsonPropertyName("one_time")]
    OneTime,
    
    /// <summary>
    /// A consumable product that can be purchased multiple times.
    /// </summary>
    [JsonPropertyName("consumable")]
    Consumable,
    
    /// <summary>
    /// A non-consumable product that is purchased once.
    /// </summary>
    [JsonPropertyName("non_consumable")]
    NonConsumable,
    
    /// <summary>
    /// A subscription that does not auto-renew.
    /// </summary>
    [JsonPropertyName("non_renewing_subscription")]
    NonRenewingSubscription
}
