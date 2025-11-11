using Refit;
using RevenueCat.NET.Models.Common;
using RevenueCat.NET.Models.Projects;

namespace RevenueCat.NET.Services;

public interface IProjectService
{
    [Get("/v2/projects")]
    Task<ListResponse<Project>> ListAsync(
        [Query] int? limit = null,
        [AliasAs("starting_after")] [Query] string? startingAfter = null,
        CancellationToken cancellationToken = default);

    [Get("/v2/projects/{projectId}")]
    Task<Project> GetAsync(
        string projectId,
        CancellationToken cancellationToken = default);
}
