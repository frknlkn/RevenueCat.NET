using System.Text.Json.Serialization;

namespace RevenueCat.NET.Models.Products;

/// <summary>
/// Represents a product as it exists in the app store.
/// </summary>
public class StoreProduct
{
    /// <summary>
    /// String representing the object's type.
    /// </summary>
    [JsonPropertyName("object")]
    public string Object { get; set; } = "store_product";
    
    /// <summary>
    /// The store-specific product identifier.
    /// </summary>
    [JsonPropertyName("store_identifier")]
    public string StoreIdentifier { get; set; } = string.Empty;
    
    /// <summary>
    /// The product display name in the store.
    /// </summary>
    [JsonPropertyName("display_name")]
    public string? DisplayName { get; set; }
    
    /// <summary>
    /// The product description in the store.
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; }
}
