using System.Text.Json.Serialization;

namespace RevenueCat.NET.Models.Products;

/// <summary>
/// Input model for creating a subscription product in App Store Connect.
/// </summary>
public class CreateAppStoreConnectSubscriptionInput
{
    /// <summary>
    /// The product identifier in App Store Connect.
    /// </summary>
    [JsonPropertyName("product_id")]
    public string ProductId { get; set; } = string.Empty;
    
    /// <summary>
    /// The reference name for the subscription.
    /// </summary>
    [JsonPropertyName("reference_name")]
    public string ReferenceName { get; set; } = string.Empty;
    
    /// <summary>
    /// The subscription group identifier.
    /// </summary>
    [JsonPropertyName("subscription_group_id")]
    public string? SubscriptionGroupId { get; set; }
    
    /// <summary>
    /// Whether the subscription is available for all countries.
    /// </summary>
    [JsonPropertyName("available_in_all_territories")]
    public bool? AvailableInAllTerritories { get; set; }
}
