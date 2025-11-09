using System.Text.Json.Serialization;

namespace RevenueCat.NET.Models.Subscriptions;

/// <summary>
/// Represents subscription-specific product details.
/// </summary>
public class SubscriptionProduct
{
    /// <summary>
    /// The duration of the subscription period (e.g., "P1M" for 1 month).
    /// </summary>
    [JsonPropertyName("duration")]
    public string? Duration { get; set; }
    
    /// <summary>
    /// The duration of the grace period (e.g., "P3D" for 3 days).
    /// </summary>
    [JsonPropertyName("grace_period_duration")]
    public string? GracePeriodDuration { get; set; }
    
    /// <summary>
    /// The duration of the trial period (e.g., "P7D" for 7 days).
    /// </summary>
    [JsonPropertyName("trial_duration")]
    public string? TrialDuration { get; set; }
}
