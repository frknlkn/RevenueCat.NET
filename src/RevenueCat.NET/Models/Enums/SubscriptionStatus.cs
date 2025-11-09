using System.Text.Json.Serialization;
using RevenueCat.NET.Serialization;

namespace RevenueCat.NET.Models.Enums;

/// <summary>
/// The current status of a subscription.
/// </summary>
[JsonConverter(typeof(SnakeCaseEnumConverter<SubscriptionStatus>))]
public enum SubscriptionStatus
{
    /// <summary>
    /// The subscription is in a trial period.
    /// </summary>
    [JsonPropertyName("trialing")]
    Trialing,
    
    /// <summary>
    /// The subscription is active and in good standing.
    /// </summary>
    [JsonPropertyName("active")]
    Active,
    
    /// <summary>
    /// The subscription has expired and is no longer active.
    /// </summary>
    [JsonPropertyName("expired")]
    Expired,
    
    /// <summary>
    /// The subscription is in a grace period after a billing issue.
    /// </summary>
    [JsonPropertyName("in_grace_period")]
    InGracePeriod,
    
    /// <summary>
    /// The subscription is in billing retry after a payment failure.
    /// </summary>
    [JsonPropertyName("in_billing_retry")]
    InBillingRetry,
    
    /// <summary>
    /// The subscription is paused.
    /// </summary>
    [JsonPropertyName("paused")]
    Paused,
    
    /// <summary>
    /// The subscription status is unknown.
    /// </summary>
    [JsonPropertyName("unknown")]
    Unknown,
    
    /// <summary>
    /// The subscription is incomplete and awaiting payment confirmation.
    /// </summary>
    [JsonPropertyName("incomplete")]
    Incomplete
}
