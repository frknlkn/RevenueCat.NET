using System.Text.Json.Serialization;

namespace RevenueCat.NET.Models.Products;

/// <summary>
/// Represents one-time purchase product details.
/// </summary>
public class OneTimeProduct
{
    /// <summary>
    /// Whether the product is consumable.
    /// </summary>
    [JsonPropertyName("is_consumable")]
    public bool? IsConsumable { get; set; }
}
