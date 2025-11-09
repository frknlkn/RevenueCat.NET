using System.Text.Json;
using RevenueCat.NET.Models;

namespace RevenueCat.NET.Tests.Models;

public class BaseModelTests
{
    private class TestModel : BaseModel
    {
        // Test implementation of BaseModel
    }

    [Fact]
    public void BaseModel_Deserialize_SetsAllProperties()
    {
        // Arrange
        var json = """
        {
            "object": "test_object",
            "id": "test_id_123",
            "created_at": 1699564800000
        }
        """;

        // Act
        var model = JsonSerializer.Deserialize<TestModel>(json, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
        });

        // Assert
        Assert.NotNull(model);
        Assert.Equal("test_object", model.Object);
        Assert.Equal("test_id_123", model.Id);
        Assert.Equal(1699564800000, model.CreatedAt);
    }

    [Fact]
    public void BaseModel_Serialize_ProducesCorrectJson()
    {
        // Arrange
        var model = new TestModel
        {
            Object = "test_object",
            Id = "test_id_456",
            CreatedAt = 1699564800000
        };

        // Act
        var json = JsonSerializer.Serialize(model, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
        });

        // Assert
        Assert.Contains("\"object\":\"test_object\"", json);
        Assert.Contains("\"id\":\"test_id_456\"", json);
        Assert.Contains("\"created_at\":1699564800000", json);
    }

    [Fact]
    public void BaseModel_DefaultValues_AreCorrect()
    {
        // Arrange & Act
        var model = new TestModel();

        // Assert
        Assert.Equal(string.Empty, model.Object);
        Assert.Equal(string.Empty, model.Id);
        Assert.Equal(0, model.CreatedAt);
    }
}
