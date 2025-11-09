using System.Text.Json.Serialization;

namespace RevenueCat.NET.Models.Subscriptions;

/// <summary>
/// Represents an authenticated URL for managing a subscription.
/// </summary>
public class AuthenticatedManagementUrl
{
    /// <summary>
    /// String representing the object's type.
    /// </summary>
    [JsonPropertyName("object")]
    public string Object { get; set; } = "authenticated_management_url";
    
    /// <summary>
    /// The authenticated URL for managing the subscription.
    /// </summary>
    [JsonPropertyName("url")]
    public string Url { get; set; } = string.Empty;
    
    /// <summary>
    /// The date when the URL expires in milliseconds since epoch.
    /// </summary>
    [JsonPropertyName("expires_at")]
    public long ExpiresAt { get; set; }
}
