using System.Text.Json.Serialization;
using RevenueCat.NET.Models.Common;
using RevenueCat.NET.Models.Products;

namespace RevenueCat.NET.Models.Entitlements;

/// <summary>
/// Represents an entitlement in RevenueCat.
/// </summary>
public class Entitlement : BaseModel
{
    /// <summary>
    /// The project ID this entitlement belongs to.
    /// </summary>
    [JsonPropertyName("project_id")]
    public string ProjectId { get; set; } = string.Empty;

    /// <summary>
    /// The lookup key for this entitlement.
    /// </summary>
    [JsonPropertyName("lookup_key")]
    public string LookupKey { get; set; } = string.Empty;

    /// <summary>
    /// The display name of the entitlement.
    /// </summary>
    [JsonPropertyName("display_name")]
    public string DisplayName { get; set; } = string.Empty;

    /// <summary>
    /// The products associated with this entitlement (expandable).
    /// </summary>
    [JsonPropertyName("products")]
    public ListResponse<Product>? Products { get; set; }
}
