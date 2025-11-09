using System.Text.Json;
using RevenueCat.NET.Models.Common;
using RevenueCat.NET.Models.Entitlements;
using RevenueCat.NET.Models.Purchases;
using Environment = RevenueCat.NET.Models.Enums.Environment;
using PurchaseStatus = RevenueCat.NET.Models.Enums.PurchaseStatus;
using Store = RevenueCat.NET.Models.Enums.Store;
using Ownership = RevenueCat.NET.Models.Enums.Ownership;

namespace RevenueCat.NET.Tests.Models;

public class PurchaseTests
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
    };

    [Fact]
    public void Purchase_Deserialize_WithAllProperties_Success()
    {
        // Arrange
        var json = """
        {
            "object": "purchase",
            "id": "pur_123",
            "created_at": 1699564800000,
            "customer_id": "cust_456",
            "original_customer_id": "cust_456",
            "product_id": "prod_789",
            "purchased_at": 1699564800000,
            "revenue_in_usd": {
                "currency": "USD",
                "gross": 9.99,
                "commission": 2.99,
                "tax": 0.50,
                "proceeds": 6.50
            },
            "quantity": 1,
            "status": "owned",
            "presented_offering_id": "off_123",
            "entitlements": {
                "object": "list",
                "items": [],
                "url": "/purchases/pur_123/entitlements"
            },
            "environment": "production",
            "store": "app_store",
            "store_purchase_identifier": "1000000123456789",
            "ownership": "purchased",
            "country": "US"
        }
        """;

        // Act
        var purchase = JsonSerializer.Deserialize<Purchase>(json, JsonOptions);

        // Assert
        Assert.NotNull(purchase);
        Assert.Equal("purchase", purchase.Object);
        Assert.Equal("pur_123", purchase.Id);
        Assert.Equal(1699564800000, purchase.CreatedAt);
        Assert.Equal("cust_456", purchase.CustomerId);
        Assert.Equal("cust_456", purchase.OriginalCustomerId);
        Assert.Equal("prod_789", purchase.ProductId);
        Assert.Equal(1699564800000, purchase.PurchasedAt);
        Assert.Equal(1, purchase.Quantity);
        Assert.Equal(PurchaseStatus.Owned, purchase.Status);
        Assert.Equal("off_123", purchase.PresentedOfferingId);
        Assert.Equal(Environment.Production, purchase.Environment);
        Assert.Equal(Store.AppStore, purchase.Store);
        Assert.Equal("1000000123456789", purchase.StorePurchaseIdentifier);
        Assert.Equal(Ownership.Purchased, purchase.Ownership);
        Assert.Equal("US", purchase.Country);
        
        Assert.NotNull(purchase.RevenueInUsd);
        Assert.Equal("USD", purchase.RevenueInUsd.Currency);
        Assert.Equal(9.99m, purchase.RevenueInUsd.Gross);
        Assert.Equal(2.99m, purchase.RevenueInUsd.Commission);
        Assert.Equal(0.50m, purchase.RevenueInUsd.Tax);
        Assert.Equal(6.50m, purchase.RevenueInUsd.Proceeds);
        
        Assert.NotNull(purchase.Entitlements);
        Assert.Equal("list", purchase.Entitlements.Object);
    }

    [Fact]
    public void Purchase_Deserialize_WithMinimalProperties_Success()
    {
        // Arrange
        var json = """
        {
            "object": "purchase",
            "id": "pur_123",
            "created_at": 1699564800000,
            "customer_id": "cust_456",
            "original_customer_id": "cust_456",
            "product_id": "prod_789",
            "purchased_at": 1699564800000,
            "revenue_in_usd": {
                "currency": "USD",
                "gross": 0,
                "commission": 0,
                "tax": 0,
                "proceeds": 0
            },
            "quantity": 1,
            "status": "owned",
            "entitlements": {
                "object": "list",
                "items": [],
                "url": "/purchases/pur_123/entitlements"
            },
            "environment": "sandbox",
            "store": "play_store",
            "store_purchase_identifier": "GPA.1234-5678-9012-34567",
            "ownership": "purchased"
        }
        """;

        // Act
        var purchase = JsonSerializer.Deserialize<Purchase>(json, JsonOptions);

        // Assert
        Assert.NotNull(purchase);
        Assert.Equal("pur_123", purchase.Id);
        Assert.Equal("cust_456", purchase.CustomerId);
        Assert.Equal("prod_789", purchase.ProductId);
        Assert.Equal(PurchaseStatus.Owned, purchase.Status);
        Assert.Equal(Environment.Sandbox, purchase.Environment);
        Assert.Equal(Store.PlayStore, purchase.Store);
        Assert.Null(purchase.PresentedOfferingId);
        Assert.Null(purchase.Country);
    }

    [Fact]
    public void Purchase_Deserialize_WithRefundedStatus_Success()
    {
        // Arrange
        var json = """
        {
            "object": "purchase",
            "id": "pur_123",
            "created_at": 1699564800000,
            "customer_id": "cust_456",
            "original_customer_id": "cust_456",
            "product_id": "prod_789",
            "purchased_at": 1699564800000,
            "revenue_in_usd": {
                "currency": "USD",
                "gross": 0,
                "commission": 0,
                "tax": 0,
                "proceeds": 0
            },
            "quantity": 1,
            "status": "refunded",
            "entitlements": {
                "object": "list",
                "items": [],
                "url": "/purchases/pur_123/entitlements"
            },
            "environment": "production",
            "store": "stripe",
            "store_purchase_identifier": "pi_1234567890",
            "ownership": "purchased"
        }
        """;

        // Act
        var purchase = JsonSerializer.Deserialize<Purchase>(json, JsonOptions);

        // Assert
        Assert.NotNull(purchase);
        Assert.Equal(PurchaseStatus.Refunded, purchase.Status);
        Assert.Equal(Store.Stripe, purchase.Store);
    }

    [Fact]
    public void Purchase_Deserialize_WithFamilySharedOwnership_Success()
    {
        // Arrange
        var json = """
        {
            "object": "purchase",
            "id": "pur_123",
            "created_at": 1699564800000,
            "customer_id": "cust_456",
            "original_customer_id": "cust_789",
            "product_id": "prod_789",
            "purchased_at": 1699564800000,
            "revenue_in_usd": {
                "currency": "USD",
                "gross": 0,
                "commission": 0,
                "tax": 0,
                "proceeds": 0
            },
            "quantity": 1,
            "status": "owned",
            "entitlements": {
                "object": "list",
                "items": [],
                "url": "/purchases/pur_123/entitlements"
            },
            "environment": "production",
            "store": "app_store",
            "store_purchase_identifier": "1000000123456789",
            "ownership": "family_shared"
        }
        """;

        // Act
        var purchase = JsonSerializer.Deserialize<Purchase>(json, JsonOptions);

        // Assert
        Assert.NotNull(purchase);
        Assert.Equal(Ownership.FamilyShared, purchase.Ownership);
        Assert.NotEqual(purchase.CustomerId, purchase.OriginalCustomerId);
    }

    [Fact]
    public void Purchase_Deserialize_WithEntitlements_Success()
    {
        // Arrange
        var json = """
        {
            "object": "purchase",
            "id": "pur_123",
            "created_at": 1699564800000,
            "customer_id": "cust_456",
            "original_customer_id": "cust_456",
            "product_id": "prod_789",
            "purchased_at": 1699564800000,
            "revenue_in_usd": {
                "currency": "USD",
                "gross": 9.99,
                "commission": 2.99,
                "tax": 0.50,
                "proceeds": 6.50
            },
            "quantity": 1,
            "status": "owned",
            "entitlements": {
                "object": "list",
                "items": [
                    {
                        "object": "entitlement",
                        "id": "ent_123",
                        "created_at": 1699564800000,
                        "project_id": "proj_123",
                        "lookup_key": "premium",
                        "display_name": "Premium Access"
                    }
                ],
                "url": "/purchases/pur_123/entitlements"
            },
            "environment": "production",
            "store": "app_store",
            "store_purchase_identifier": "1000000123456789",
            "ownership": "purchased"
        }
        """;

        // Act
        var purchase = JsonSerializer.Deserialize<Purchase>(json, JsonOptions);

        // Assert
        Assert.NotNull(purchase);
        Assert.NotNull(purchase.Entitlements);
        Assert.Single(purchase.Entitlements.Items);
        Assert.Equal("ent_123", purchase.Entitlements.Items[0].Id);
        Assert.Equal("premium", purchase.Entitlements.Items[0].LookupKey);
    }
}
