using RevenueCat.NET.Models;

namespace RevenueCat.NET.Services;

internal sealed class ProjectService(IHttpRequestExecutor executor) : IProjectService
{
    public Task<ListResponse<Project>> ListAsync(
        int? limit = null,
        string? startingAfter = null,
        CancellationToken cancellationToken = default)
    {
        var query = BuildQueryString(limit, startingAfter);
        return executor.ExecuteAsync<ListResponse<Project>>(
            HttpMethod.Get,
            $"/projects{query}",
            cancellationToken: cancellationToken);
    }

    public Task<Project> GetAsync(string projectId, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(projectId);
        
        return executor.ExecuteAsync<Project>(
            HttpMethod.Get,
            $"/projects/{projectId}",
            cancellationToken: cancellationToken);
    }

    private static string BuildQueryString(int? limit, string? startingAfter)
    {
        var parameters = new List<string>();
        
        if (limit.HasValue)
        {
            parameters.Add($"limit={limit.Value}");
        }
        
        if (!string.IsNullOrWhiteSpace(startingAfter))
        {
            parameters.Add($"starting_after={Uri.EscapeDataString(startingAfter)}");
        }

        return parameters.Count > 0 ? $"?{string.Join("&", parameters)}" : string.Empty;
    }
}
