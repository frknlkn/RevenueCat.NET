using System.Text.Json.Serialization;

namespace RevenueCat.NET.Models.Common;

/// <summary>
/// Generic response model for paginated list endpoints.
/// </summary>
/// <typeparam name="T">The type of items in the list</typeparam>
public class ListResponse<T>
{
    /// <summary>
    /// String representing the object's type. Always has the value "list".
    /// </summary>
    [JsonPropertyName("object")]
    public string Object { get; set; } = "list";

    /// <summary>
    /// Details about each object in the list.
    /// </summary>
    [JsonPropertyName("items")]
    public List<T> Items { get; set; } = new();

    /// <summary>
    /// URL to access the next page of results. If not present/null, there is no next page.
    /// </summary>
    [JsonPropertyName("next_page")]
    public string? NextPage { get; set; }

    /// <summary>
    /// The URL where this list can be accessed.
    /// </summary>
    [JsonPropertyName("url")]
    public string Url { get; set; } = string.Empty;
}
