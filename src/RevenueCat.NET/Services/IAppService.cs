using Refit;
using RevenueCat.NET.Models;
using RevenueCat.NET.Models.Apps;
using RevenueCat.NET.Models.Common;

namespace RevenueCat.NET.Services;

/// <summary>
/// Service for managing apps in RevenueCat.
/// </summary>
public interface IAppService
{
    /// <summary>
    /// Lists all apps for a project.
    /// </summary>
    [Get("/v2/projects/{projectId}/apps")]
    Task<ListResponse<App>> ListAsync(
        string projectId,
        [Query] int? limit = null,
        [AliasAs("starting_after")] [Query] string? startingAfter = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a specific app by ID.
    /// </summary>
    [Get("/v2/projects/{projectId}/apps/{appId}")]
    Task<App> GetAsync(
        string projectId,
        string appId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a new app in a project.
    /// </summary>
    [Post("/v2/projects/{projectId}/apps")]
    Task<App> CreateAsync(
        string projectId,
        [Body] CreateAppRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing app.
    /// </summary>
    [Post("/v2/projects/{projectId}/apps/{appId}")]
    Task<App> UpdateAsync(
        string projectId,
        string appId,
        [Body] UpdateAppRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes an app.
    /// </summary>
    [Delete("/v2/projects/{projectId}/apps/{appId}")]
    Task<DeletedObject> DeleteAsync(
        string projectId,
        string appId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Lists all public API keys for an app.
    /// </summary>
    [Get("/v2/projects/{projectId}/apps/{appId}/public_api_keys")]
    Task<ListResponse<PublicApiKey>> ListPublicApiKeysAsync(
        string projectId,
        string appId,
        [Query] int? limit = null,
        [AliasAs("starting_after")] [Query] string? startingAfter = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets the StoreKit configuration file for an App Store app.
    /// </summary>
    [Get("/v2/projects/{projectId}/apps/{appId}/storekit_config")]
    Task<StoreKitConfigFile> GetStoreKitConfigAsync(
        string projectId,
        string appId,
        CancellationToken cancellationToken = default);
}
