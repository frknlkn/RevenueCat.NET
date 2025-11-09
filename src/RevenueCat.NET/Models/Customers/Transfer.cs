using System.Text.Json.Serialization;

namespace RevenueCat.NET.Models.Customers;

/// <summary>
/// Represents the result of a customer data transfer operation.
/// </summary>
public class Transfer
{
    /// <summary>
    /// String representing the object's type.
    /// </summary>
    [JsonPropertyName("object")]
    public string Object { get; set; } = "transfer";
    
    /// <summary>
    /// The source customer ID.
    /// </summary>
    [JsonPropertyName("from_customer_id")]
    public string FromCustomerId { get; set; } = string.Empty;
    
    /// <summary>
    /// The destination customer ID.
    /// </summary>
    [JsonPropertyName("to_customer_id")]
    public string ToCustomerId { get; set; } = string.Empty;
    
    /// <summary>
    /// The date when the transfer was completed in milliseconds since epoch.
    /// </summary>
    [JsonPropertyName("transferred_at")]
    public long TransferredAt { get; set; }
}
