using RevenueCat.NET.Models;
using RevenueCat.NET.Models.Common;
using Offering = RevenueCat.NET.Models.Offerings.Offering;

namespace RevenueCat.NET.Services;

internal sealed class OfferingService(IHttpRequestExecutor executor) : IOfferingService
{
    public Task<ListResponse<Offering>> ListAsync(
        string projectId,
        int? limit = null,
        string? startingAfter = null,
        string[]? expand = null,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(projectId);

        var parameters = new List<string>();
        if (limit.HasValue)
        {
            parameters.Add($"limit={limit.Value}");
        }
        if (!string.IsNullOrWhiteSpace(startingAfter))
        {
            parameters.Add($"starting_after={Uri.EscapeDataString(startingAfter)}");
        }
        if (expand is { Length: > 0 })
        {
            parameters.Add($"expand={string.Join(",", expand.Select(Uri.EscapeDataString))}");
        }

        var query = parameters.Count > 0 ? $"?{string.Join("&", parameters)}" : string.Empty;

        return executor.ExecuteAsync<ListResponse<Offering>>(
            HttpMethod.Get,
            $"/projects/{projectId}/offerings{query}",
            cancellationToken: cancellationToken);
    }

    public Task<Offering> GetAsync(
        string projectId,
        string offeringId,
        string[]? expand = null,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(projectId);
        ArgumentException.ThrowIfNullOrWhiteSpace(offeringId);

        var query = QueryStringBuilder.BuildExpand(expand);
        return executor.ExecuteAsync<Offering>(
            HttpMethod.Get,
            $"/projects/{projectId}/offerings/{offeringId}{query}",
            cancellationToken: cancellationToken);
    }

    public Task<Offering> CreateAsync(
        string projectId,
        CreateOfferingRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(projectId);
        ArgumentNullException.ThrowIfNull(request);

        return executor.ExecuteAsync<Offering>(
            HttpMethod.Post,
            $"/projects/{projectId}/offerings",
            request,
            cancellationToken);
    }

    public Task<Offering> UpdateAsync(
        string projectId,
        string offeringId,
        UpdateOfferingRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(projectId);
        ArgumentException.ThrowIfNullOrWhiteSpace(offeringId);
        ArgumentNullException.ThrowIfNull(request);

        return executor.ExecuteAsync<Offering>(
            HttpMethod.Post,
            $"/projects/{projectId}/offerings/{offeringId}",
            request,
            cancellationToken);
    }

    public Task<DeletedObject> DeleteAsync(
        string projectId,
        string offeringId,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(projectId);
        ArgumentException.ThrowIfNullOrWhiteSpace(offeringId);

        return executor.ExecuteAsync<DeletedObject>(
            HttpMethod.Delete,
            $"/projects/{projectId}/offerings/{offeringId}",
            cancellationToken: cancellationToken);
    }

    public Task<Offering> SetDefaultAsync(
        string projectId,
        string offeringId,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(projectId);
        ArgumentException.ThrowIfNullOrWhiteSpace(offeringId);

        return executor.ExecuteAsync<Offering>(
            HttpMethod.Post,
            $"/projects/{projectId}/offerings/{offeringId}/actions/set_default",
            cancellationToken: cancellationToken);
    }
}

