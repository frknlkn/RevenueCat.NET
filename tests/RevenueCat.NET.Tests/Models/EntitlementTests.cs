using System.Text.Json;
using RevenueCat.NET.Models.Entitlements;

namespace RevenueCat.NET.Tests.Models;

public class EntitlementTests
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
    };

    [Fact]
    public void Entitlement_Deserialize_WithBasicProperties_Success()
    {
        // Arrange
        var json = """
        {
            "object": "entitlement",
            "id": "ent_123",
            "created_at": 1699564800000,
            "project_id": "proj_456",
            "lookup_key": "premium",
            "display_name": "Premium Access"
        }
        """;

        // Act
        var entitlement = JsonSerializer.Deserialize<Entitlement>(json, JsonOptions);

        // Assert
        Assert.NotNull(entitlement);
        Assert.Equal("entitlement", entitlement.Object);
        Assert.Equal("ent_123", entitlement.Id);
        Assert.Equal(1699564800000, entitlement.CreatedAt);
        Assert.Equal("proj_456", entitlement.ProjectId);
        Assert.Equal("premium", entitlement.LookupKey);
        Assert.Equal("Premium Access", entitlement.DisplayName);
        Assert.Null(entitlement.Products);
    }

    [Fact]
    public void Entitlement_Deserialize_WithExpandedProducts_Success()
    {
        // Arrange
        var json = """
        {
            "object": "entitlement",
            "id": "ent_123",
            "created_at": 1699564800000,
            "project_id": "proj_456",
            "lookup_key": "premium",
            "display_name": "Premium Access",
            "products": {
                "object": "list",
                "items": [
                    {
                        "object": "product",
                        "id": "prod_789",
                        "created_at": 1699564800000,
                        "store_identifier": "com.example.monthly",
                        "type": "subscription",
                        "app_id": "app_456"
                    }
                ],
                "url": "/projects/proj_456/entitlements/ent_123/products"
            }
        }
        """;

        // Act
        var entitlement = JsonSerializer.Deserialize<Entitlement>(json, JsonOptions);

        // Assert
        Assert.NotNull(entitlement);
        Assert.Equal("ent_123", entitlement.Id);
        Assert.Equal("premium", entitlement.LookupKey);
        Assert.Equal("Premium Access", entitlement.DisplayName);
        
        Assert.NotNull(entitlement.Products);
        Assert.Equal("list", entitlement.Products.Object);
        Assert.Single(entitlement.Products.Items);
        Assert.Equal("prod_789", entitlement.Products.Items[0].Id);
    }

    [Fact]
    public void Entitlement_Serialize_Success()
    {
        // Arrange
        var entitlement = new Entitlement
        {
            Object = "entitlement",
            Id = "ent_123",
            CreatedAt = 1699564800000,
            ProjectId = "proj_456",
            LookupKey = "premium",
            DisplayName = "Premium Access"
        };

        // Act
        var json = JsonSerializer.Serialize(entitlement, JsonOptions);
        var deserialized = JsonSerializer.Deserialize<Entitlement>(json, JsonOptions);

        // Assert
        Assert.NotNull(deserialized);
        Assert.Equal(entitlement.Id, deserialized.Id);
        Assert.Equal(entitlement.ProjectId, deserialized.ProjectId);
        Assert.Equal(entitlement.LookupKey, deserialized.LookupKey);
        Assert.Equal(entitlement.DisplayName, deserialized.DisplayName);
    }
}
