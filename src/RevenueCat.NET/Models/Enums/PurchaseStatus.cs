using System.Text.Json.Serialization;
using RevenueCat.NET.Serialization;

namespace RevenueCat.NET.Models.Enums;

/// <summary>
/// The status of a purchase.
/// </summary>
[JsonConverter(typeof(SnakeCaseEnumConverter<PurchaseStatus>))]
public enum PurchaseStatus
{
    /// <summary>
    /// The purchase is owned by the customer.
    /// </summary>
    [JsonPropertyName("owned")]
    Owned,
    
    /// <summary>
    /// The purchase has been refunded.
    /// </summary>
    [JsonPropertyName("refunded")]
    Refunded
}
