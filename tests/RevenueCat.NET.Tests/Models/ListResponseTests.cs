using System.Text.Json;
using RevenueCat.NET.Models;
using RevenueCat.NET.Models.Common;

namespace RevenueCat.NET.Tests.Models;

public class ListResponseTests
{
    private class TestItem : BaseModel
    {
        public string Name { get; set; } = string.Empty;
    }

    [Fact]
    public void ListResponse_Deserialize_WithItems_Success()
    {
        // Arrange
        var json = """
        {
            "object": "list",
            "items": [
                {
                    "object": "test_item",
                    "id": "item_1",
                    "created_at": 1699564800000,
                    "name": "First Item"
                },
                {
                    "object": "test_item",
                    "id": "item_2",
                    "created_at": 1699564900000,
                    "name": "Second Item"
                }
            ],
            "next_page": "https://api.revenuecat.com/v2/projects/proj_123/items?starting_after=item_2",
            "url": "https://api.revenuecat.com/v2/projects/proj_123/items"
        }
        """;

        // Act
        var response = JsonSerializer.Deserialize<ListResponse<TestItem>>(json, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
        });

        // Assert
        Assert.NotNull(response);
        Assert.Equal("list", response.Object);
        Assert.Equal(2, response.Items.Count);
        Assert.Equal("item_1", response.Items[0].Id);
        Assert.Equal("First Item", response.Items[0].Name);
        Assert.Equal("item_2", response.Items[1].Id);
        Assert.Equal("Second Item", response.Items[1].Name);
        Assert.Equal("https://api.revenuecat.com/v2/projects/proj_123/items?starting_after=item_2", response.NextPage);
        Assert.Equal("https://api.revenuecat.com/v2/projects/proj_123/items", response.Url);
    }

    [Fact]
    public void ListResponse_Deserialize_WithoutNextPage_Success()
    {
        // Arrange
        var json = """
        {
            "object": "list",
            "items": [
                {
                    "object": "test_item",
                    "id": "item_1",
                    "created_at": 1699564800000,
                    "name": "Only Item"
                }
            ],
            "url": "https://api.revenuecat.com/v2/projects/proj_123/items"
        }
        """;

        // Act
        var response = JsonSerializer.Deserialize<ListResponse<TestItem>>(json, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
        });

        // Assert
        Assert.NotNull(response);
        Assert.Equal("list", response.Object);
        Assert.Single(response.Items);
        Assert.Null(response.NextPage);
        Assert.Equal("https://api.revenuecat.com/v2/projects/proj_123/items", response.Url);
    }

    [Fact]
    public void ListResponse_Deserialize_EmptyList_Success()
    {
        // Arrange
        var json = """
        {
            "object": "list",
            "items": [],
            "url": "https://api.revenuecat.com/v2/projects/proj_123/items"
        }
        """;

        // Act
        var response = JsonSerializer.Deserialize<ListResponse<TestItem>>(json, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
        });

        // Assert
        Assert.NotNull(response);
        Assert.Equal("list", response.Object);
        Assert.Empty(response.Items);
        Assert.Null(response.NextPage);
    }

    [Fact]
    public void ListResponse_Serialize_ProducesCorrectJson()
    {
        // Arrange
        var response = new ListResponse<TestItem>
        {
            Object = "list",
            Items = new List<TestItem>
            {
                new() { Object = "test_item", Id = "item_1", CreatedAt = 1699564800000, Name = "Test" }
            },
            NextPage = "https://api.revenuecat.com/next",
            Url = "https://api.revenuecat.com/items"
        };

        // Act
        var json = JsonSerializer.Serialize(response, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
        });

        // Assert
        Assert.Contains("\"object\":\"list\"", json);
        Assert.Contains("\"items\":", json);
        Assert.Contains("\"next_page\":\"https://api.revenuecat.com/next\"", json);
        Assert.Contains("\"url\":\"https://api.revenuecat.com/items\"", json);
    }

    [Fact]
    public void ListResponse_DefaultValues_AreCorrect()
    {
        // Arrange & Act
        var response = new ListResponse<TestItem>();

        // Assert
        Assert.Equal("list", response.Object);
        Assert.NotNull(response.Items);
        Assert.Empty(response.Items);
        Assert.Null(response.NextPage);
        Assert.Equal(string.Empty, response.Url);
    }

    [Fact]
    public void ListResponse_WithDifferentTypes_Success()
    {
        // Arrange
        var json = """
        {
            "object": "list",
            "items": ["string1", "string2", "string3"],
            "url": "https://api.revenuecat.com/strings"
        }
        """;

        // Act
        var response = JsonSerializer.Deserialize<ListResponse<string>>(json, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
        });

        // Assert
        Assert.NotNull(response);
        Assert.Equal(3, response.Items.Count);
        Assert.Equal("string1", response.Items[0]);
        Assert.Equal("string2", response.Items[1]);
        Assert.Equal("string3", response.Items[2]);
    }
}
