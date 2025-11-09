using System.Text.Json;
using RevenueCat.NET.Models.Offerings;

namespace RevenueCat.NET.Tests.Models;

public class OfferingTests
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
    };

    [Fact]
    public void Offering_Deserialize_WithBasicProperties_Success()
    {
        // Arrange
        var json = """
        {
            "object": "offering",
            "id": "off_123",
            "created_at": 1699564800000,
            "lookup_key": "default",
            "display_name": "Default Offering",
            "is_current": true,
            "project_id": "proj_456"
        }
        """;

        // Act
        var offering = JsonSerializer.Deserialize<Offering>(json, JsonOptions);

        // Assert
        Assert.NotNull(offering);
        Assert.Equal("offering", offering.Object);
        Assert.Equal("off_123", offering.Id);
        Assert.Equal(1699564800000, offering.CreatedAt);
        Assert.Equal("default", offering.LookupKey);
        Assert.Equal("Default Offering", offering.DisplayName);
        Assert.True(offering.IsCurrent);
        Assert.Equal("proj_456", offering.ProjectId);
        Assert.Null(offering.Metadata);
        Assert.Null(offering.Packages);
    }

    [Fact]
    public void Offering_Deserialize_WithMetadata_Success()
    {
        // Arrange
        var json = """
        {
            "object": "offering",
            "id": "off_123",
            "created_at": 1699564800000,
            "lookup_key": "default",
            "display_name": "Default Offering",
            "is_current": true,
            "project_id": "proj_456",
            "metadata": {
                "campaign": "summer_sale",
                "discount_percentage": 20,
                "featured": true
            }
        }
        """;

        // Act
        var offering = JsonSerializer.Deserialize<Offering>(json, JsonOptions);

        // Assert
        Assert.NotNull(offering);
        Assert.Equal("off_123", offering.Id);
        Assert.NotNull(offering.Metadata);
        Assert.Equal(3, offering.Metadata.Count);
        Assert.True(offering.Metadata.ContainsKey("campaign"));
        Assert.True(offering.Metadata.ContainsKey("discount_percentage"));
        Assert.True(offering.Metadata.ContainsKey("featured"));
    }

    [Fact]
    public void Offering_Deserialize_WithExpandedPackages_Success()
    {
        // Arrange
        var json = """
        {
            "object": "offering",
            "id": "off_123",
            "created_at": 1699564800000,
            "lookup_key": "default",
            "display_name": "Default Offering",
            "is_current": true,
            "project_id": "proj_456",
            "packages": {
                "object": "list",
                "items": [
                    {
                        "object": "package",
                        "id": "pkg_789",
                        "created_at": 1699564800000,
                        "lookup_key": "monthly",
                        "display_name": "Monthly Package",
                        "position": 1
                    }
                ],
                "url": "/projects/proj_456/offerings/off_123/packages"
            }
        }
        """;

        // Act
        var offering = JsonSerializer.Deserialize<Offering>(json, JsonOptions);

        // Assert
        Assert.NotNull(offering);
        Assert.Equal("off_123", offering.Id);
        Assert.Equal("default", offering.LookupKey);
        Assert.Equal("Default Offering", offering.DisplayName);
        
        Assert.NotNull(offering.Packages);
        Assert.Equal("list", offering.Packages.Object);
        Assert.Single(offering.Packages.Items);
        Assert.Equal("pkg_789", offering.Packages.Items[0].Id);
        Assert.Equal("monthly", offering.Packages.Items[0].LookupKey);
    }

    [Fact]
    public void Offering_Serialize_Success()
    {
        // Arrange
        var offering = new Offering
        {
            Object = "offering",
            Id = "off_123",
            CreatedAt = 1699564800000,
            LookupKey = "default",
            DisplayName = "Default Offering",
            IsCurrent = true,
            ProjectId = "proj_456",
            Metadata = new Dictionary<string, object>
            {
                { "campaign", "summer_sale" },
                { "discount_percentage", 20 }
            }
        };

        // Act
        var json = JsonSerializer.Serialize(offering, JsonOptions);
        var deserialized = JsonSerializer.Deserialize<Offering>(json, JsonOptions);

        // Assert
        Assert.NotNull(deserialized);
        Assert.Equal(offering.Id, deserialized.Id);
        Assert.Equal(offering.LookupKey, deserialized.LookupKey);
        Assert.Equal(offering.DisplayName, deserialized.DisplayName);
        Assert.Equal(offering.IsCurrent, deserialized.IsCurrent);
        Assert.Equal(offering.ProjectId, deserialized.ProjectId);
        Assert.NotNull(deserialized.Metadata);
        Assert.Equal(2, deserialized.Metadata.Count);
    }

    [Fact]
    public void Offering_Deserialize_WithNullMetadata_Success()
    {
        // Arrange
        var json = """
        {
            "object": "offering",
            "id": "off_123",
            "created_at": 1699564800000,
            "lookup_key": "default",
            "display_name": "Default Offering",
            "is_current": false,
            "project_id": "proj_456",
            "metadata": null
        }
        """;

        // Act
        var offering = JsonSerializer.Deserialize<Offering>(json, JsonOptions);

        // Assert
        Assert.NotNull(offering);
        Assert.Equal("off_123", offering.Id);
        Assert.Null(offering.Metadata);
    }
}
