using System.Text.Json.Serialization;
using RevenueCat.NET.Serialization;

namespace RevenueCat.NET.Models.Enums;

/// <summary>
/// The ownership type of a purchase or subscription.
/// </summary>
[JsonConverter(typeof(SnakeCaseEnumConverter<Ownership>))]
public enum Ownership
{
    /// <summary>
    /// The customer purchased the product directly.
    /// </summary>
    [JsonPropertyName("purchased")]
    Purchased,
    
    /// <summary>
    /// The customer has access through family sharing.
    /// </summary>
    [JsonPropertyName("family_shared")]
    FamilyShared
}
