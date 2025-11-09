using System.Text.Json.Serialization;

namespace RevenueCat.NET.Models.Customers;

/// <summary>
/// Represents a virtual currency balance for a customer.
/// </summary>
public class VirtualCurrencyBalance
{
    /// <summary>
    /// String representing the object's type.
    /// </summary>
    [JsonPropertyName("object")]
    public string Object { get; set; } = "virtual_currency_balance";
    
    /// <summary>
    /// The currency code identifier.
    /// </summary>
    [JsonPropertyName("currency_code")]
    public string CurrencyCode { get; set; } = string.Empty;
    
    /// <summary>
    /// The current balance amount.
    /// </summary>
    [JsonPropertyName("balance")]
    public int Balance { get; set; }
    
    /// <summary>
    /// Optional description of the currency.
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; }
    
    /// <summary>
    /// Optional display name of the currency.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }
}
