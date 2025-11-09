using System.Text.Json.Serialization;

namespace RevenueCat.NET.Models.Paywalls;

/// <summary>
/// Represents a paywall configuration in RevenueCat.
/// </summary>
public class Paywall : BaseModel
{
    /// <summary>
    /// The offering ID this paywall is associated with.
    /// </summary>
    [JsonPropertyName("offering_id")]
    public string OfferingId { get; set; } = string.Empty;
    
    /// <summary>
    /// The name of the paywall.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }
    
    /// <summary>
    /// The date when the paywall was published in milliseconds since epoch.
    /// </summary>
    [JsonPropertyName("published_at")]
    public long? PublishedAt { get; set; }
}
