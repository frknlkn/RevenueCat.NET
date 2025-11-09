using System.Text.Json.Serialization;

namespace RevenueCat.NET.Models.Apps;

/// <summary>
/// Represents a public API key for an app.
/// </summary>
public class PublicApiKey : BaseModel
{
    /// <summary>
    /// The public API key value.
    /// </summary>
    [JsonPropertyName("key")]
    public string Key { get; set; } = string.Empty;
    
    /// <summary>
    /// The app ID this API key belongs to.
    /// </summary>
    [JsonPropertyName("app_id")]
    public string AppId { get; set; } = string.Empty;
}
