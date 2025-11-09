using System.Text.Json.Serialization;

namespace RevenueCat.NET.Models.Invoices;

/// <summary>
/// Represents an invoice in RevenueCat.
/// </summary>
public class Invoice : BaseModel
{
    /// <summary>
    /// The total amount of the invoice.
    /// </summary>
    [JsonPropertyName("total_amount")]
    public MonetaryAmount TotalAmount { get; set; } = new();
    
    /// <summary>
    /// The line items in this invoice.
    /// </summary>
    [JsonPropertyName("line_items")]
    public List<InvoiceLineItem> LineItems { get; set; } = new();
    
    /// <summary>
    /// The date when the invoice was issued in milliseconds since epoch.
    /// </summary>
    [JsonPropertyName("issued_at")]
    public long IssuedAt { get; set; }
    
    /// <summary>
    /// The date when the invoice was paid in milliseconds since epoch.
    /// </summary>
    [JsonPropertyName("paid_at")]
    public long? PaidAt { get; set; }
    
    /// <summary>
    /// URL to view/download the invoice.
    /// </summary>
    [JsonPropertyName("invoice_url")]
    public string? InvoiceUrl { get; set; }
}
