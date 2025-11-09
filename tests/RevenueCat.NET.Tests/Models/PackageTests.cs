using System.Text.Json;
using RevenueCat.NET.Models.Enums;
using RevenueCat.NET.Models.Packages;

namespace RevenueCat.NET.Tests.Models;

public class PackageTests
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
    };

    [Fact]
    public void Package_Deserialize_WithBasicProperties_Success()
    {
        // Arrange
        var json = """
        {
            "object": "package",
            "id": "pkg_123",
            "created_at": 1699564800000,
            "lookup_key": "monthly",
            "display_name": "Monthly Package",
            "position": 1
        }
        """;

        // Act
        var package = JsonSerializer.Deserialize<Package>(json, JsonOptions);

        // Assert
        Assert.NotNull(package);
        Assert.Equal("package", package.Object);
        Assert.Equal("pkg_123", package.Id);
        Assert.Equal(1699564800000, package.CreatedAt);
        Assert.Equal("monthly", package.LookupKey);
        Assert.Equal("Monthly Package", package.DisplayName);
        Assert.Equal(1, package.Position);
        Assert.Null(package.Products);
    }

    [Fact]
    public void Package_Deserialize_WithNullPosition_Success()
    {
        // Arrange
        var json = """
        {
            "object": "package",
            "id": "pkg_123",
            "created_at": 1699564800000,
            "lookup_key": "monthly",
            "display_name": "Monthly Package",
            "position": null
        }
        """;

        // Act
        var package = JsonSerializer.Deserialize<Package>(json, JsonOptions);

        // Assert
        Assert.NotNull(package);
        Assert.Equal("pkg_123", package.Id);
        Assert.Null(package.Position);
    }

    [Fact]
    public void Package_Deserialize_WithExpandedProducts_Success()
    {
        // Arrange
        var json = """
        {
            "object": "package",
            "id": "pkg_123",
            "created_at": 1699564800000,
            "lookup_key": "monthly",
            "display_name": "Monthly Package",
            "position": 1,
            "products": {
                "object": "list",
                "items": [
                    {
                        "product": {
                            "object": "product",
                            "id": "prod_456",
                            "created_at": 1699564800000,
                            "store_identifier": "com.example.monthly",
                            "type": "subscription",
                            "app_id": "app_789",
                            "display_name": "Monthly Subscription"
                        },
                        "eligibility_criteria": "all"
                    }
                ],
                "url": "/projects/proj_123/offerings/off_456/packages/pkg_123/products"
            }
        }
        """;

        // Act
        var package = JsonSerializer.Deserialize<Package>(json, JsonOptions);

        // Assert
        Assert.NotNull(package);
        Assert.Equal("pkg_123", package.Id);
        Assert.Equal("monthly", package.LookupKey);
        
        Assert.NotNull(package.Products);
        Assert.Equal("list", package.Products.Object);
        Assert.Single(package.Products.Items);
        
        var productAssociation = package.Products.Items[0];
        Assert.NotNull(productAssociation.Product);
        Assert.Equal("prod_456", productAssociation.Product.Id);
        Assert.Equal("com.example.monthly", productAssociation.Product.StoreIdentifier);
    }

    [Fact]
    public void Package_Serialize_Success()
    {
        // Arrange
        var package = new Package
        {
            Object = "package",
            Id = "pkg_123",
            CreatedAt = 1699564800000,
            LookupKey = "monthly",
            DisplayName = "Monthly Package",
            Position = 1
        };

        // Act
        var json = JsonSerializer.Serialize(package, JsonOptions);
        var deserialized = JsonSerializer.Deserialize<Package>(json, JsonOptions);

        // Assert
        Assert.NotNull(deserialized);
        Assert.Equal(package.Id, deserialized.Id);
        Assert.Equal(package.LookupKey, deserialized.LookupKey);
        Assert.Equal(package.DisplayName, deserialized.DisplayName);
        Assert.Equal(package.Position, deserialized.Position);
    }

    [Fact]
    public void PackageProductAssociation_Deserialize_Success()
    {
        // Arrange
        var json = """
        {
            "product": {
                "object": "product",
                "id": "prod_456",
                "created_at": 1699564800000,
                "store_identifier": "com.example.product",
                "type": "subscription",
                "app_id": "app_789"
            },
            "eligibility_criteria": "all"
        }
        """;

        // Act
        var association = JsonSerializer.Deserialize<PackageProductAssociation>(json, JsonOptions);

        // Assert
        Assert.NotNull(association);
        Assert.NotNull(association.Product);
        Assert.Equal("prod_456", association.Product.Id);
    }
}
