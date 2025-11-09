using System.Text.Json;
using RevenueCat.NET.Models.Common;
using RevenueCat.NET.Models.Invoices;

namespace RevenueCat.NET.Tests.Models;

public class InvoiceTests
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
    };

    [Fact]
    public void Invoice_Deserialize_WithAllProperties_Success()
    {
        // Arrange
        var json = """
        {
            "object": "invoice",
            "id": "inv_123",
            "created_at": 1699564800000,
            "total_amount": {
                "currency": "USD",
                "gross": 9.99,
                "commission": 2.99,
                "tax": 0.99,
                "proceeds": 6.01
            },
            "line_items": [
                {
                    "object": "invoice.line_item",
                    "product_identifier": "prod_123",
                    "product_display_name": "Premium Subscription",
                    "product_duration": "P1M",
                    "quantity": 1,
                    "unit_amount": {
                        "currency": "USD",
                        "gross": 9.99,
                        "commission": 2.99,
                        "tax": 0.99,
                        "proceeds": 6.01
                    }
                }
            ],
            "issued_at": 1699564800000,
            "paid_at": 1699651200000,
            "invoice_url": "https://example.com/invoice.pdf"
        }
        """;

        // Act
        var invoice = JsonSerializer.Deserialize<Invoice>(json, JsonOptions);

        // Assert
        Assert.NotNull(invoice);
        Assert.Equal("invoice", invoice.Object);
        Assert.Equal("inv_123", invoice.Id);
        Assert.Equal(1699564800000, invoice.CreatedAt);
        
        Assert.NotNull(invoice.TotalAmount);
        Assert.Equal("USD", invoice.TotalAmount.Currency);
        Assert.Equal(9.99m, invoice.TotalAmount.Gross);
        Assert.Equal(2.99m, invoice.TotalAmount.Commission);
        Assert.Equal(0.99m, invoice.TotalAmount.Tax);
        Assert.Equal(6.01m, invoice.TotalAmount.Proceeds);
        
        Assert.Single(invoice.LineItems);
        Assert.Equal("invoice.line_item", invoice.LineItems[0].Object);
        Assert.Equal("prod_123", invoice.LineItems[0].ProductIdentifier);
        Assert.Equal("Premium Subscription", invoice.LineItems[0].ProductDisplayName);
        Assert.Equal("P1M", invoice.LineItems[0].ProductDuration);
        Assert.Equal(1, invoice.LineItems[0].Quantity);
        
        Assert.Equal(1699564800000, invoice.IssuedAt);
        Assert.Equal(1699651200000, invoice.PaidAt);
        Assert.Equal("https://example.com/invoice.pdf", invoice.InvoiceUrl);
    }

    [Fact]
    public void Invoice_Deserialize_WithMinimalProperties_Success()
    {
        // Arrange
        var json = """
        {
            "object": "invoice",
            "id": "inv_123",
            "created_at": 1699564800000,
            "total_amount": {
                "currency": "USD",
                "gross": 9.99,
                "commission": 2.99,
                "tax": 0.99,
                "proceeds": 6.01
            },
            "line_items": [],
            "issued_at": 1699564800000
        }
        """;

        // Act
        var invoice = JsonSerializer.Deserialize<Invoice>(json, JsonOptions);

        // Assert
        Assert.NotNull(invoice);
        Assert.Equal("inv_123", invoice.Id);
        Assert.Empty(invoice.LineItems);
        Assert.Null(invoice.PaidAt);
        Assert.Null(invoice.InvoiceUrl);
    }

    [Fact]
    public void InvoiceLineItem_Deserialize_WithAllProperties_Success()
    {
        // Arrange
        var json = """
        {
            "object": "invoice.line_item",
            "product_identifier": "prod_456",
            "product_display_name": "Gold Coins",
            "product_duration": "P1Y",
            "quantity": 5,
            "unit_amount": {
                "currency": "EUR",
                "gross": 4.99,
                "commission": 1.49,
                "tax": 0.50,
                "proceeds": 3.00
            }
        }
        """;

        // Act
        var lineItem = JsonSerializer.Deserialize<InvoiceLineItem>(json, JsonOptions);

        // Assert
        Assert.NotNull(lineItem);
        Assert.Equal("invoice.line_item", lineItem.Object);
        Assert.Equal("prod_456", lineItem.ProductIdentifier);
        Assert.Equal("Gold Coins", lineItem.ProductDisplayName);
        Assert.Equal("P1Y", lineItem.ProductDuration);
        Assert.Equal(5, lineItem.Quantity);
        
        Assert.NotNull(lineItem.UnitAmount);
        Assert.Equal("EUR", lineItem.UnitAmount.Currency);
        Assert.Equal(4.99m, lineItem.UnitAmount.Gross);
        Assert.Equal(1.49m, lineItem.UnitAmount.Commission);
        Assert.Equal(0.50m, lineItem.UnitAmount.Tax);
        Assert.Equal(3.00m, lineItem.UnitAmount.Proceeds);
    }

    [Fact]
    public void InvoiceLineItem_Deserialize_WithoutOptionalFields_Success()
    {
        // Arrange
        var json = """
        {
            "object": "invoice.line_item",
            "product_identifier": "prod_789",
            "quantity": 1,
            "unit_amount": {
                "currency": "USD",
                "gross": 1.99,
                "commission": 0.59,
                "tax": 0.20,
                "proceeds": 1.20
            }
        }
        """;

        // Act
        var lineItem = JsonSerializer.Deserialize<InvoiceLineItem>(json, JsonOptions);

        // Assert
        Assert.NotNull(lineItem);
        Assert.Equal("prod_789", lineItem.ProductIdentifier);
        Assert.Null(lineItem.ProductDisplayName);
        Assert.Null(lineItem.ProductDuration);
        Assert.Equal(1, lineItem.Quantity);
    }
}
