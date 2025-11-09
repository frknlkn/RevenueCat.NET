using System.Text.Json.Serialization;

namespace RevenueCat.NET.Models;

/// <summary>
/// Represents a monetary amount with currency and breakdown of fees.
/// </summary>
public class MonetaryAmount
{
    /// <summary>
    /// ISO 4217 currency code.
    /// </summary>
    [JsonPropertyName("currency")]
    public string Currency { get; set; } = string.Empty;
    
    /// <summary>
    /// Total revenue generated (excluding taxes and commission).
    /// </summary>
    [JsonPropertyName("gross")]
    public decimal Gross { get; set; }
    
    /// <summary>
    /// Store commission or payment processor fees deducted from gross revenue (if any).
    /// </summary>
    [JsonPropertyName("commission")]
    public decimal Commission { get; set; }
    
    /// <summary>
    /// Estimated taxes deducted from gross revenue.
    /// </summary>
    [JsonPropertyName("tax")]
    public decimal Tax { get; set; }
    
    /// <summary>
    /// Net revenue after store commission/fees and taxes.
    /// </summary>
    [JsonPropertyName("proceeds")]
    public decimal Proceeds { get; set; }
}
