using RevenueCat.NET.Models;

namespace RevenueCat.NET.Services;

public interface IProjectService
{
    Task<ListResponse<Project>> ListAsync(
        int? limit = null,
        string? startingAfter = null,
        CancellationToken cancellationToken = default);

    Task<Project> GetAsync(
        string projectId,
        CancellationToken cancellationToken = default);
}
