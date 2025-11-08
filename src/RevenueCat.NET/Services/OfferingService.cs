using RevenueCat.NET.Models;

namespace RevenueCat.NET.Services;

internal sealed class OfferingService(IHttpRequestExecutor executor) : IOfferingService
{
    public Task<ListResponse<Offering>> ListAsync(
        string projectId,
        int? limit = null,
        string? startingAfter = null,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(projectId);
        
        var query = QueryStringBuilder.Build(limit, startingAfter);
        return executor.ExecuteAsync<ListResponse<Offering>>(
            HttpMethod.Get,
            $"/projects/{projectId}/offerings{query}",
            cancellationToken: cancellationToken);
    }

    public Task<Offering> GetAsync(
        string projectId,
        string offeringId,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(projectId);
        ArgumentException.ThrowIfNullOrWhiteSpace(offeringId);
        
        return executor.ExecuteAsync<Offering>(
            HttpMethod.Get,
            $"/projects/{projectId}/offerings/{offeringId}",
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
