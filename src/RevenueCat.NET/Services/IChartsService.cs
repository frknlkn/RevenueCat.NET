using RevenueCat.NET.Models.Charts;

namespace RevenueCat.NET.Services;

public interface IChartsService
{
    Task<OverviewMetrics> GetMetricsAsync(
        string projectId,
        string? currency = null,
        CancellationToken cancellationToken = default);
}
