using System.Text.Json.Serialization;

namespace RevenueCat.NET.Models.Invoices;

/// <summary>
/// Represents a line item in an invoice.
/// </summary>
public class InvoiceLineItem
{
    /// <summary>
    /// String representing the object's type.
    /// </summary>
    [JsonPropertyName("object")]
    public string Object { get; set; } = "invoice.line_item";
    
    /// <summary>
    /// The product identifier for this line item.
    /// </summary>
    [JsonPropertyName("product_identifier")]
    public string ProductIdentifier { get; set; } = string.Empty;
    
    /// <summary>
    /// The display name of the product.
    /// </summary>
    [JsonPropertyName("product_display_name")]
    public string? ProductDisplayName { get; set; }
    
    /// <summary>
    /// The duration of the product (for subscriptions).
    /// </summary>
    [JsonPropertyName("product_duration")]
    public string? ProductDuration { get; set; }
    
    /// <summary>
    /// The quantity of this line item.
    /// </summary>
    [JsonPropertyName("quantity")]
    public int Quantity { get; set; }
    
    /// <summary>
    /// The unit amount for this line item.
    /// </summary>
    [JsonPropertyName("unit_amount")]
    public MonetaryAmount UnitAmount { get; set; } = new();
}
