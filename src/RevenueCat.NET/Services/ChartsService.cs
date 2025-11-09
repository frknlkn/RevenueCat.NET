using RevenueCat.NET.Models.Charts;

namespace RevenueCat.NET.Services;

internal sealed class ChartsService(IHttpRequestExecutor executor) : IChartsService
{
    public Task<OverviewMetrics> GetMetricsAsync(
        string projectId,
        string? currency = null,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(projectId);
        
        var parameters = new List<string>();
        
        if (!string.IsNullOrWhiteSpace(currency))
        {
            parameters.Add($"currency={Uri.EscapeDataString(currency)}");
        }

        var query = parameters.Count > 0 ? $"?{string.Join("&", parameters)}" : string.Empty;
        
        return executor.ExecuteAsync<OverviewMetrics>(
            HttpMethod.Get,
            $"/projects/{projectId}/overview_metrics{query}",
            cancellationToken: cancellationToken);
    }
}
