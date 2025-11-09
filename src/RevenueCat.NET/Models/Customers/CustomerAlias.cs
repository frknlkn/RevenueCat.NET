using System.Text.Json.Serialization;

namespace RevenueCat.NET.Models.Customers;

/// <summary>
/// Represents an alias for a customer.
/// </summary>
public class CustomerAlias
{
    /// <summary>
    /// String representing the object's type.
    /// </summary>
    [JsonPropertyName("object")]
    public string Object { get; set; } = "customer.alias";
    
    /// <summary>
    /// The alias identifier.
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;
    
    /// <summary>
    /// The date when the alias was created in milliseconds since epoch.
    /// </summary>
    [JsonPropertyName("created_at")]
    public long CreatedAt { get; set; }
}
