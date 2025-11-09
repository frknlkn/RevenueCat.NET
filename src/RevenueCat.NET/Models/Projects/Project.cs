using System.Text.Json.Serialization;

namespace RevenueCat.NET.Models.Projects;

/// <summary>
/// Represents a project in RevenueCat.
/// </summary>
public class Project : BaseModel
{
    /// <summary>
    /// The name of the project.
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
}
