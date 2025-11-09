using System.Text.Json.Serialization;
using RevenueCat.NET.Serialization;

namespace RevenueCat.NET.Models.Enums;

/// <summary>
/// The store environment.
/// </summary>
[JsonConverter(typeof(SnakeCaseEnumConverter<Environment>))]
public enum Environment
{
    /// <summary>
    /// Production environment.
    /// </summary>
    [JsonPropertyName("production")]
    Production,
    
    /// <summary>
    /// Sandbox environment for testing.
    /// </summary>
    [JsonPropertyName("sandbox")]
    Sandbox
}
