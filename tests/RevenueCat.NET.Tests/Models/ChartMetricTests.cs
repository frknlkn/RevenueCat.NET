using System.Text.Json;
using RevenueCat.NET.Models.Charts;

namespace RevenueCat.NET.Tests.Models;

public class ChartMetricTests
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
    };

    [Fact]
    public void OverviewMetrics_Deserialize_Success()
    {
        // Arrange
        var json = """
        {
            "object": "overview_metrics",
            "metrics": [
                {
                    "object": "overview_metric",
                    "id": "active_subscriptions",
                    "name": "Active Subscriptions",
                    "description": "Number of active subscriptions",
                    "unit": "count",
                    "period": "current",
                    "value": 1234.56,
                    "last_updated_at": 1699564800000,
                    "last_updated_at_iso8601": "2023-11-09T20:00:00Z"
                }
            ]
        }
        """;

        // Act
        var metrics = JsonSerializer.Deserialize<OverviewMetrics>(json, JsonOptions);

        // Assert
        Assert.NotNull(metrics);
        Assert.Equal("overview_metrics", metrics.Object);
        Assert.Single(metrics.Metrics);
        
        var metric = metrics.Metrics[0];
        Assert.Equal("overview_metric", metric.Object);
        Assert.Equal("active_subscriptions", metric.Id);
        Assert.Equal("Active Subscriptions", metric.Name);
        Assert.Equal("Number of active subscriptions", metric.Description);
        Assert.Equal("count", metric.Unit);
        Assert.Equal("current", metric.Period);
        Assert.Equal(1234.56m, metric.Value);
        Assert.Equal(1699564800000, metric.LastUpdatedAt);
        Assert.Equal("2023-11-09T20:00:00Z", metric.LastUpdatedAtIso8601);
    }

    [Fact]
    public void OverviewMetric_Deserialize_Success()
    {
        // Arrange
        var json = """
        {
            "object": "overview_metric",
            "id": "mrr",
            "name": "Monthly Recurring Revenue",
            "description": "Total MRR in USD",
            "unit": "usd",
            "period": "month",
            "value": 50000.00,
            "last_updated_at": 1699564800000,
            "last_updated_at_iso8601": "2023-11-09T20:00:00Z"
        }
        """;

        // Act
        var metric = JsonSerializer.Deserialize<OverviewMetric>(json, JsonOptions);

        // Assert
        Assert.NotNull(metric);
        Assert.Equal("overview_metric", metric.Object);
        Assert.Equal("mrr", metric.Id);
        Assert.Equal("Monthly Recurring Revenue", metric.Name);
        Assert.Equal("Total MRR in USD", metric.Description);
        Assert.Equal("usd", metric.Unit);
        Assert.Equal("month", metric.Period);
        Assert.Equal(50000.00m, metric.Value);
        Assert.Equal(1699564800000, metric.LastUpdatedAt);
        Assert.Equal("2023-11-09T20:00:00Z", metric.LastUpdatedAtIso8601);
    }

    [Fact]
    public void OverviewMetric_Deserialize_WithNullValues_Success()
    {
        // Arrange
        var json = """
        {
            "object": "overview_metric",
            "id": "revenue",
            "name": "Total Revenue",
            "description": "Total revenue",
            "unit": "usd",
            "period": "all_time",
            "value": 0,
            "last_updated_at": null,
            "last_updated_at_iso8601": null
        }
        """;

        // Act
        var metric = JsonSerializer.Deserialize<OverviewMetric>(json, JsonOptions);

        // Assert
        Assert.NotNull(metric);
        Assert.Equal("revenue", metric.Id);
        Assert.Equal(0m, metric.Value);
        Assert.Null(metric.LastUpdatedAt);
        Assert.Null(metric.LastUpdatedAtIso8601);
    }

    [Fact]
    public void OverviewMetrics_Deserialize_WithEmptyMetrics_Success()
    {
        // Arrange
        var json = """
        {
            "object": "overview_metrics",
            "metrics": []
        }
        """;

        // Act
        var metrics = JsonSerializer.Deserialize<OverviewMetrics>(json, JsonOptions);

        // Assert
        Assert.NotNull(metrics);
        Assert.Equal("overview_metrics", metrics.Object);
        Assert.Empty(metrics.Metrics);
    }

    [Fact]
    public void OverviewMetrics_Deserialize_WithMultipleMetrics_Success()
    {
        // Arrange
        var json = """
        {
            "object": "overview_metrics",
            "metrics": [
                {
                    "object": "overview_metric",
                    "id": "active_subscriptions",
                    "name": "Active Subscriptions",
                    "description": "Number of active subscriptions",
                    "unit": "count",
                    "period": "current",
                    "value": 1234,
                    "last_updated_at": 1699564800000,
                    "last_updated_at_iso8601": "2023-11-09T20:00:00Z"
                },
                {
                    "object": "overview_metric",
                    "id": "mrr",
                    "name": "MRR",
                    "description": "Monthly recurring revenue",
                    "unit": "usd",
                    "period": "month",
                    "value": 50000.50,
                    "last_updated_at": 1699564800000,
                    "last_updated_at_iso8601": "2023-11-09T20:00:00Z"
                }
            ]
        }
        """;

        // Act
        var metrics = JsonSerializer.Deserialize<OverviewMetrics>(json, JsonOptions);

        // Assert
        Assert.NotNull(metrics);
        Assert.Equal(2, metrics.Metrics.Count);
        Assert.Equal("active_subscriptions", metrics.Metrics[0].Id);
        Assert.Equal(1234m, metrics.Metrics[0].Value);
        Assert.Equal("mrr", metrics.Metrics[1].Id);
        Assert.Equal(50000.50m, metrics.Metrics[1].Value);
    }

    [Fact]
    public void OverviewMetric_Deserialize_WithDecimalValue_Success()
    {
        // Arrange
        var json = """
        {
            "object": "overview_metric",
            "id": "arpu",
            "name": "ARPU",
            "description": "Average revenue per user",
            "unit": "usd",
            "period": "month",
            "value": 12.99,
            "last_updated_at": 1699564800000,
            "last_updated_at_iso8601": "2023-11-09T20:00:00Z"
        }
        """;

        // Act
        var metric = JsonSerializer.Deserialize<OverviewMetric>(json, JsonOptions);

        // Assert
        Assert.NotNull(metric);
        Assert.Equal(12.99m, metric.Value);
    }
}
