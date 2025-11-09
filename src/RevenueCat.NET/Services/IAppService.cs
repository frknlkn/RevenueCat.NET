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
    /// <param name="projectId">The project ID.</param>
    /// <param name="limit">Maximum number of items to return.</param>
    /// <param name="startingAfter">Cursor for pagination.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A paginated list of apps.</returns>
    Task<ListResponse<App>> ListAsync(
        string projectId,
        int? limit = null,
        string? startingAfter = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a specific app by ID.
    /// </summary>
    /// <param name="projectId">The project ID.</param>
    /// <param name="appId">The app ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The app details.</returns>
    Task<App> GetAsync(
        string projectId,
        string appId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a new app in a project.
    /// </summary>
    /// <param name="projectId">The project ID.</param>
    /// <param name="request">The app creation request.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The created app.</returns>
    Task<App> CreateAsync(
        string projectId,
        CreateAppRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing app.
    /// </summary>
    /// <param name="projectId">The project ID.</param>
    /// <param name="appId">The app ID.</param>
    /// <param name="request">The app update request.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The updated app.</returns>
    Task<App> UpdateAsync(
        string projectId,
        string appId,
        UpdateAppRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes an app.
    /// </summary>
    /// <param name="projectId">The project ID.</param>
    /// <param name="appId">The app ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A deleted object confirmation.</returns>
    Task<DeletedObject> DeleteAsync(
        string projectId,
        string appId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Lists all public API keys for an app.
    /// </summary>
    /// <param name="projectId">The project ID.</param>
    /// <param name="appId">The app ID.</param>
    /// <param name="limit">Maximum number of items to return.</param>
    /// <param name="startingAfter">Cursor for pagination.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A paginated list of public API keys.</returns>
    Task<ListResponse<PublicApiKey>> ListPublicApiKeysAsync(
        string projectId,
        string appId,
        int? limit = null,
        string? startingAfter = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets the StoreKit configuration file for an App Store app.
    /// </summary>
    /// <param name="projectId">The project ID.</param>
    /// <param name="appId">The app ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The StoreKit configuration file.</returns>
    Task<StoreKitConfigFile> GetStoreKitConfigAsync(
        string projectId,
        string appId,
        CancellationToken cancellationToken = default);
}
