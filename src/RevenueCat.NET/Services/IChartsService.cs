using Refit;
using RevenueCat.NET.Models.Charts;

namespace RevenueCat.NET.Services;

public interface IChartsService
{
    [Get("/v2/projects/{projectId}/overview_metrics")]
    Task<OverviewMetrics> GetMetricsAsync(
        string projectId,
        [Query] string? currency = null,
        CancellationToken cancellationToken = default);
}
