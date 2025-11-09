using System.Text.Json.Serialization;
using RevenueCat.NET.Models.Common;
using RevenueCat.NET.Models.Packages;

namespace RevenueCat.NET.Models.Offerings;

/// <summary>
/// Represents an offering in RevenueCat.
/// </summary>
public class Offering : BaseModel
{
    /// <summary>
    /// The lookup key for this offering.
    /// </summary>
    [JsonPropertyName("lookup_key")]
    public string LookupKey { get; set; } = string.Empty;

    /// <summary>
    /// The display name of the offering.
    /// </summary>
    [JsonPropertyName("display_name")]
    public string DisplayName { get; set; } = string.Empty;

    /// <summary>
    /// Whether this is the current/default offering.
    /// </summary>
    [JsonPropertyName("is_current")]
    public bool IsCurrent { get; set; }

    /// <summary>
    /// The project ID this offering belongs to.
    /// </summary>
    [JsonPropertyName("project_id")]
    public string ProjectId { get; set; } = string.Empty;

    /// <summary>
    /// Custom metadata associated with this offering.
    /// </summary>
    [JsonPropertyName("metadata")]
    public Dictionary<string, object>? Metadata { get; set; }

    /// <summary>
    /// The packages in this offering (expandable).
    /// </summary>
    [JsonPropertyName("packages")]
    public ListResponse<Package>? Packages { get; set; }
}
