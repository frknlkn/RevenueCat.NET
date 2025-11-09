using System.Text.Json.Serialization;

namespace RevenueCat.NET.Models;

/// <summary>
/// Base class for all RevenueCat API models containing common properties.
/// </summary>
public abstract class BaseModel
{
    /// <summary>
    /// String representing the object's type. Objects of the same type share the same value.
    /// </summary>
    [JsonPropertyName("object")]
    public string Object { get; set; } = string.Empty;
    
    /// <summary>
    /// The unique identifier of the object.
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;
    
    /// <summary>
    /// The date when the object was created in milliseconds since epoch.
    /// </summary>
    [JsonPropertyName("created_at")]
    public long CreatedAt { get; set; }
}
