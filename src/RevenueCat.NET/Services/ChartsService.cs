using RevenueCat.NET.Models;

namespace RevenueCat.NET.Services;

internal sealed class ChartsService(IHttpRequestExecutor executor) : IChartsService
{
    public Task<ChartResponse> GetMetricsAsync(
        string projectId,
        ChartMetricType metric,
        long startDate,
        long endDate,
        string? appId = null,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(projectId);
        
        var parameters = new List<string>
        {
            $"metric={metric.ToString().ToLowerInvariant()}",
            $"start_date={startDate}",
            $"end_date={endDate}"
        };
        
        if (!string.IsNullOrWhiteSpace(appId))
        {
            parameters.Add($"app_id={Uri.EscapeDataString(appId)}");
        }

        var query = $"?{string.Join("&", parameters)}";
        
        return executor.ExecuteAsync<ChartResponse>(
            HttpMethod.Get,
            $"/projects/{projectId}/metrics{query}",
            cancellationToken: cancellationToken);
    }
}
