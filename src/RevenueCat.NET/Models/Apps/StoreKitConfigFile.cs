using System.Text.Json.Serialization;

namespace RevenueCat.NET.Models.Apps;

/// <summary>
/// Represents a StoreKit configuration file for testing in-app purchases.
/// </summary>
public class StoreKitConfigFile
{
    /// <summary>
    /// String representing the object's type.
    /// </summary>
    [JsonPropertyName("object")]
    public string Object { get; set; } = "storekit_config_file";
    
    /// <summary>
    /// The StoreKit configuration file content.
    /// </summary>
    [JsonPropertyName("content")]
    public string Content { get; set; } = string.Empty;
}
