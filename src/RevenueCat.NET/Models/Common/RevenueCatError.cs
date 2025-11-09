using System.Text.Json.Serialization;
using RevenueCat.NET.Models.Enums;

namespace RevenueCat.NET.Models;

/// <summary>
/// Represents an error response from the RevenueCat API.
/// </summary>
public class RevenueCatError
{
    /// <summary>
    /// String representing the object's type. Always has the value "error".
    /// </summary>
    [JsonPropertyName("object")]
    public string Object { get; set; } = "error";
    
    /// <summary>
    /// The type of error.
    /// </summary>
    [JsonPropertyName("type")]
    public ErrorType Type { get; set; }
    
    /// <summary>
    /// The parameter that caused the error, if applicable.
    /// </summary>
    [JsonPropertyName("param")]
    public string? Param { get; set; }
    
    /// <summary>
    /// URL to documentation about this error.
    /// </summary>
    [JsonPropertyName("doc_url")]
    public string DocUrl { get; set; } = string.Empty;
    
    /// <summary>
    /// Human-readable error message.
    /// </summary>
    [JsonPropertyName("message")]
    public string Message { get; set; } = string.Empty;
    
    /// <summary>
    /// Whether the request can be retried.
    /// </summary>
    [JsonPropertyName("retryable")]
    public bool Retryable { get; set; }
    
    /// <summary>
    /// Suggested backoff time in milliseconds before retrying, if applicable.
    /// </summary>
    [JsonPropertyName("backoff_ms")]
    public int? BackoffMs { get; set; }
}
