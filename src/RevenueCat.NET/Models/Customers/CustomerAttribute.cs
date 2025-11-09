using System.Text.Json.Serialization;

namespace RevenueCat.NET.Models.Customers;

/// <summary>
/// Represents a custom attribute associated with a customer.
/// </summary>
public class CustomerAttribute
{
    /// <summary>
    /// String representing the object's type.
    /// </summary>
    [JsonPropertyName("object")]
    public string Object { get; set; } = "customer.attribute";
    
    /// <summary>
    /// The attribute key/name.
    /// </summary>
    [JsonPropertyName("key")]
    public string Key { get; set; } = string.Empty;
    
    /// <summary>
    /// The attribute value.
    /// </summary>
    [JsonPropertyName("value")]
    public string Value { get; set; } = string.Empty;
    
    /// <summary>
    /// The date when the attribute was last updated in milliseconds since epoch.
    /// </summary>
    [JsonPropertyName("updated_at")]
    public long UpdatedAt { get; set; }
}
