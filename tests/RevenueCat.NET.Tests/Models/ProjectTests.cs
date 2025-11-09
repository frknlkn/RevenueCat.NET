using System.Text.Json;
using RevenueCat.NET.Models.Projects;

namespace RevenueCat.NET.Tests.Models;

public class ProjectTests
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
    };

    [Fact]
    public void Project_Deserialize_Success()
    {
        // Arrange
        var json = """
        {
            "object": "project",
            "id": "proj_123",
            "created_at": 1699564800000,
            "name": "My Project"
        }
        """;

        // Act
        var project = JsonSerializer.Deserialize<Project>(json, JsonOptions);

        // Assert
        Assert.NotNull(project);
        Assert.Equal("project", project.Object);
        Assert.Equal("proj_123", project.Id);
        Assert.Equal(1699564800000, project.CreatedAt);
        Assert.Equal("My Project", project.Name);
    }

    [Fact]
    public void Project_Deserialize_WithMinimalProperties_Success()
    {
        // Arrange
        var json = """
        {
            "object": "project",
            "id": "proj_456",
            "created_at": 1699564800000,
            "name": ""
        }
        """;

        // Act
        var project = JsonSerializer.Deserialize<Project>(json, JsonOptions);

        // Assert
        Assert.NotNull(project);
        Assert.Equal("proj_456", project.Id);
        Assert.Equal(string.Empty, project.Name);
    }
}
