using System.Text.Json.Serialization;
using RevenueCat.NET.Models.Common;

namespace RevenueCat.NET.Models.Customers;

/// <summary>
/// Represents a customer in RevenueCat.
/// </summary>
public class Customer : BaseModel
{
    /// <summary>
    /// The project ID this customer belongs to.
    /// </summary>
    [JsonPropertyName("project_id")]
    public string ProjectId { get; set; } = string.Empty;

    /// <summary>
    /// The date when the customer was first seen in milliseconds since epoch.
    /// </summary>
    [JsonPropertyName("first_seen_at")]
    public long FirstSeenAt { get; set; }

    /// <summary>
    /// The date when the customer was last seen in milliseconds since epoch.
    /// </summary>
    [JsonPropertyName("last_seen_at")]
    public long? LastSeenAt { get; set; }

    /// <summary>
    /// The app version when the customer was last seen.
    /// </summary>
    [JsonPropertyName("last_seen_app_version")]
    public string? LastSeenAppVersion { get; set; }

    /// <summary>
    /// The country code (ISO alpha-2) where the customer was last seen.
    /// </summary>
    [JsonPropertyName("last_seen_country")]
    public string? LastSeenCountry { get; set; }

    /// <summary>
    /// The platform where the customer was last seen.
    /// </summary>
    [JsonPropertyName("last_seen_platform")]
    public string? LastSeenPlatform { get; set; }

    /// <summary>
    /// The platform version where the customer was last seen.
    /// </summary>
    [JsonPropertyName("last_seen_platform_version")]
    public string? LastSeenPlatformVersion { get; set; }

    /// <summary>
    /// The customer's active entitlements (expandable).
    /// </summary>
    [JsonPropertyName("active_entitlements")]
    public ListResponse<CustomerEntitlement>? ActiveEntitlements { get; set; }

    /// <summary>
    /// The customer's experiment enrollment data.
    /// </summary>
    [JsonPropertyName("experiment")]
    public ExperimentEnrollment? Experiment { get; set; }

    /// <summary>
    /// The customer's attributes (expandable).
    /// </summary>
    [JsonPropertyName("attributes")]
    public ListResponse<CustomerAttribute>? Attributes { get; set; }
}
