using System.Text.Json.Serialization;

namespace RevenueCat.NET.Models.Customers;

/// <summary>
/// Represents an active entitlement for a customer.
/// </summary>
public class CustomerEntitlement
{
    /// <summary>
    /// String representing the object's type.
    /// </summary>
    [JsonPropertyName("object")]
    public string Object { get; set; } = "customer.active_entitlement";
    
    /// <summary>
    /// The entitlement ID.
    /// </summary>
    [JsonPropertyName("entitlement_id")]
    public string EntitlementId { get; set; } = string.Empty;
    
    /// <summary>
    /// The date when the entitlement expires in milliseconds since epoch, if applicable.
    /// </summary>
    [JsonPropertyName("expires_at")]
    public long? ExpiresAt { get; set; }
}
