namespace RevenueCat.NET.Models;

public sealed record ChartMetric(
    string Metric,
    IReadOnlyList<DataPoint> Data
);

public sealed record DataPoint(
    long Timestamp,
    decimal Value
);

public sealed record ChartResponse(
    string Object,
    IReadOnlyList<ChartMetric> Metrics
);
