using System.Text.Json.Serialization;

namespace RevenueCat.NET.Models.Charts;

/// <summary>
/// Represents overview metrics from RevenueCat.
/// </summary>
public class OverviewMetrics
{
    /// <summary>
    /// String representing the object's type.
    /// </summary>
    [JsonPropertyName("object")]
    public string Object { get; set; } = "overview_metrics";
    
    /// <summary>
    /// The list of metrics.
    /// </summary>
    [JsonPropertyName("metrics")]
    public List<OverviewMetric> Metrics { get; set; } = new();
}

/// <summary>
/// Represents a single overview metric.
/// </summary>
public class OverviewMetric
{
    /// <summary>
    /// String representing the object's type.
    /// </summary>
    [JsonPropertyName("object")]
    public string Object { get; set; } = "overview_metric";
    
    /// <summary>
    /// The metric identifier.
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;
    
    /// <summary>
    /// The metric name.
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// The metric description.
    /// </summary>
    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;
    
    /// <summary>
    /// The unit of measurement.
    /// </summary>
    [JsonPropertyName("unit")]
    public string Unit { get; set; } = string.Empty;
    
    /// <summary>
    /// The time period for this metric.
    /// </summary>
    [JsonPropertyName("period")]
    public string Period { get; set; } = string.Empty;
    
    /// <summary>
    /// The metric value.
    /// </summary>
    [JsonPropertyName("value")]
    public decimal Value { get; set; }
    
    /// <summary>
    /// The date when the metric was last updated in milliseconds since epoch.
    /// </summary>
    [JsonPropertyName("last_updated_at")]
    public long? LastUpdatedAt { get; set; }
    
    /// <summary>
    /// The date when the metric was last updated in ISO 8601 format.
    /// </summary>
    [JsonPropertyName("last_updated_at_iso8601")]
    public string? LastUpdatedAtIso8601 { get; set; }
}
