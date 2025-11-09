using System.Text.Json;
using RevenueCat.NET.Models.Paywalls;

namespace RevenueCat.NET.Tests.Models;

public class PaywallTests
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
    };

    [Fact]
    public void Paywall_Deserialize_WithBasicProperties_Success()
    {
        // Arrange
        var json = """
        {
            "object": "paywall",
            "id": "pw_123",
            "created_at": 1699564800000,
            "offering_id": "off_456",
            "name": "Summer Sale Paywall"
        }
        """;

        // Act
        var paywall = JsonSerializer.Deserialize<Paywall>(json, JsonOptions);

        // Assert
        Assert.NotNull(paywall);
        Assert.Equal("paywall", paywall.Object);
        Assert.Equal("pw_123", paywall.Id);
        Assert.Equal(1699564800000, paywall.CreatedAt);
        Assert.Equal("off_456", paywall.OfferingId);
        Assert.Equal("Summer Sale Paywall", paywall.Name);
        Assert.Null(paywall.PublishedAt);
    }

    [Fact]
    public void Paywall_Deserialize_WithPublishedAt_Success()
    {
        // Arrange
        var json = """
        {
            "object": "paywall",
            "id": "pw_123",
            "created_at": 1699564800000,
            "offering_id": "off_456",
            "name": "Summer Sale Paywall",
            "published_at": 1699651200000
        }
        """;

        // Act
        var paywall = JsonSerializer.Deserialize<Paywall>(json, JsonOptions);

        // Assert
        Assert.NotNull(paywall);
        Assert.Equal("pw_123", paywall.Id);
        Assert.Equal("off_456", paywall.OfferingId);
        Assert.Equal("Summer Sale Paywall", paywall.Name);
        Assert.Equal(1699651200000, paywall.PublishedAt);
    }

    [Fact]
    public void Paywall_Deserialize_WithNullName_Success()
    {
        // Arrange
        var json = """
        {
            "object": "paywall",
            "id": "pw_123",
            "created_at": 1699564800000,
            "offering_id": "off_456",
            "name": null
        }
        """;

        // Act
        var paywall = JsonSerializer.Deserialize<Paywall>(json, JsonOptions);

        // Assert
        Assert.NotNull(paywall);
        Assert.Equal("pw_123", paywall.Id);
        Assert.Equal("off_456", paywall.OfferingId);
        Assert.Null(paywall.Name);
    }

    [Fact]
    public void Paywall_Deserialize_WithoutOptionalFields_Success()
    {
        // Arrange
        var json = """
        {
            "object": "paywall",
            "id": "pw_123",
            "created_at": 1699564800000,
            "offering_id": "off_456"
        }
        """;

        // Act
        var paywall = JsonSerializer.Deserialize<Paywall>(json, JsonOptions);

        // Assert
        Assert.NotNull(paywall);
        Assert.Equal("pw_123", paywall.Id);
        Assert.Equal("off_456", paywall.OfferingId);
        Assert.Null(paywall.Name);
        Assert.Null(paywall.PublishedAt);
    }

    [Fact]
    public void Paywall_Serialize_Success()
    {
        // Arrange
        var paywall = new Paywall
        {
            Object = "paywall",
            Id = "pw_123",
            CreatedAt = 1699564800000,
            OfferingId = "off_456",
            Name = "Summer Sale Paywall",
            PublishedAt = 1699651200000
        };

        // Act
        var json = JsonSerializer.Serialize(paywall, JsonOptions);
        var deserialized = JsonSerializer.Deserialize<Paywall>(json, JsonOptions);

        // Assert
        Assert.NotNull(deserialized);
        Assert.Equal(paywall.Id, deserialized.Id);
        Assert.Equal(paywall.OfferingId, deserialized.OfferingId);
        Assert.Equal(paywall.Name, deserialized.Name);
        Assert.Equal(paywall.PublishedAt, deserialized.PublishedAt);
    }

    [Fact]
    public void Paywall_Serialize_WithNullOptionalFields_Success()
    {
        // Arrange
        var paywall = new Paywall
        {
            Object = "paywall",
            Id = "pw_123",
            CreatedAt = 1699564800000,
            OfferingId = "off_456",
            Name = null,
            PublishedAt = null
        };

        // Act
        var json = JsonSerializer.Serialize(paywall, JsonOptions);
        var deserialized = JsonSerializer.Deserialize<Paywall>(json, JsonOptions);

        // Assert
        Assert.NotNull(deserialized);
        Assert.Equal(paywall.Id, deserialized.Id);
        Assert.Equal(paywall.OfferingId, deserialized.OfferingId);
        Assert.Null(deserialized.Name);
        Assert.Null(deserialized.PublishedAt);
    }
}
