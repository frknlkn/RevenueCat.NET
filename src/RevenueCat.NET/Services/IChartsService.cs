using RevenueCat.NET.Models;

namespace RevenueCat.NET.Services;

public interface IChartsService
{
    Task<ChartResponse> GetMetricsAsync(
        string projectId,
        ChartMetricType metric,
        long startDate,
        long endDate,
        string? appId = null,
        CancellationToken cancellationToken = default);
}

public enum ChartMetricType
{
    ActiveSubscriptions,
    ActiveTrials,
    Revenue,
    Mrr,
    Arr,
    NewSubscribers,
    Churn,
    Refunds
}
