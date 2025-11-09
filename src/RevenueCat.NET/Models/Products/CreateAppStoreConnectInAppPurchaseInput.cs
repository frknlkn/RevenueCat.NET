using System.Text.Json.Serialization;

namespace RevenueCat.NET.Models.Products;

/// <summary>
/// Input model for creating an in-app purchase product in App Store Connect.
/// </summary>
public class CreateAppStoreConnectInAppPurchaseInput
{
    /// <summary>
    /// The product identifier in App Store Connect.
    /// </summary>
    [JsonPropertyName("product_id")]
    public string ProductId { get; set; } = string.Empty;
    
    /// <summary>
    /// The reference name for the in-app purchase.
    /// </summary>
    [JsonPropertyName("reference_name")]
    public string ReferenceName { get; set; } = string.Empty;
    
    /// <summary>
    /// The type of in-app purchase (consumable or non_consumable).
    /// </summary>
    [JsonPropertyName("in_app_purchase_type")]
    public string? InAppPurchaseType { get; set; }
    
    /// <summary>
    /// Whether the in-app purchase is available for all countries.
    /// </summary>
    [JsonPropertyName("available_in_all_territories")]
    public bool? AvailableInAllTerritories { get; set; }
}
