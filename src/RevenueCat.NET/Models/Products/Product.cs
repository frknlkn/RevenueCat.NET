using System.Text.Json.Serialization;
using RevenueCat.NET.Models.Enums;
using RevenueCat.NET.Models.Subscriptions;

namespace RevenueCat.NET.Models.Products;

/// <summary>
/// Represents a product in RevenueCat.
/// </summary>
public class Product : BaseModel
{
    /// <summary>
    /// The store-specific product identifier.
    /// </summary>
    [JsonPropertyName("store_identifier")]
    public string StoreIdentifier { get; set; } = string.Empty;
    
    /// <summary>
    /// The type of product.
    /// </summary>
    [JsonPropertyName("type")]
    public ProductType Type { get; set; }
    
    /// <summary>
    /// Subscription-specific details (only present for subscription products).
    /// </summary>
    [JsonPropertyName("subscription")]
    public SubscriptionProduct? Subscription { get; set; }
    
    /// <summary>
    /// One-time purchase details (only present for one-time products).
    /// </summary>
    [JsonPropertyName("one_time")]
    public OneTimeProduct? OneTime { get; set; }
    
    /// <summary>
    /// The app ID this product belongs to.
    /// </summary>
    [JsonPropertyName("app_id")]
    public string AppId { get; set; } = string.Empty;
    
    /// <summary>
    /// The app this product belongs to (expandable).
    /// </summary>
    [JsonPropertyName("app")]
    public object? App { get; set; }
    
    /// <summary>
    /// The display name of the product.
    /// </summary>
    [JsonPropertyName("display_name")]
    public string? DisplayName { get; set; }
}
