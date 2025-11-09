using System.Text.Json.Serialization;

namespace RevenueCat.NET.Models.Subscriptions;

/// <summary>
/// Represents pending changes to a subscription.
/// </summary>
public class PendingChanges
{
    /// <summary>
    /// String representing the object's type.
    /// </summary>
    [JsonPropertyName("object")]
    public string Object { get; set; } = "pending_changes";
    
    /// <summary>
    /// The product ID that the subscription will change to.
    /// </summary>
    [JsonPropertyName("product_id")]
    public string? ProductId { get; set; }
    
    /// <summary>
    /// The date when the change will take effect in milliseconds since epoch.
    /// </summary>
    [JsonPropertyName("effective_at")]
    public long? EffectiveAt { get; set; }
}
