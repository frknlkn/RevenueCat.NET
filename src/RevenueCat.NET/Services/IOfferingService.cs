using Refit;
using RevenueCat.NET.Models;
using RevenueCat.NET.Models.Common;
using RevenueCat.NET.Models.Offerings;
using System.Text.Json.Serialization;

namespace RevenueCat.NET.Services;

/// <summary>
/// Service for managing offerings in RevenueCat.
/// </summary>
/// <remarks>
/// API Documentation: <see href="https://www.revenuecat.com/docs/api-v2#tag/Offerings"/>
/// </remarks>
public interface IOfferingService
{
    /// <summary>
    /// Lists all offerings for a project.
    /// </summary>
    [Get("/v2/projects/{projectId}/offerings")]
    Task<ListResponse<Offering>> ListAsync(
        string projectId,
        [Query] int? limit = null,
        [AliasAs("starting_after")] [Query] string? startingAfter = null,
        [Query(CollectionFormat.Multi)] string[]? expand = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a specific offering by ID.
    /// </summary>
    [Get("/v2/projects/{projectId}/offerings/{offeringId}")]
    Task<Offering> GetAsync(
        string projectId,
        string offeringId,
        [Query(CollectionFormat.Multi)] string[]? expand = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a new offering.
    /// </summary>
    [Post("/v2/projects/{projectId}/offerings")]
    Task<Offering> CreateAsync(
        string projectId,
        [Body] CreateOfferingRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing offering.
    /// </summary>
    [Post("/v2/projects/{projectId}/offerings/{offeringId}")]
    Task<Offering> UpdateAsync(
        string projectId,
        string offeringId,
        [Body] UpdateOfferingRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes an offering permanently.
    /// </summary>
    [Delete("/v2/projects/{projectId}/offerings/{offeringId}")]
    Task<DeletedObject> DeleteAsync(
        string projectId,
        string offeringId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Sets an offering as the default (current) offering.
    /// </summary>
    [Post("/v2/projects/{projectId}/offerings/{offeringId}/actions/set_default")]
    Task<Offering> SetDefaultAsync(
        string projectId,
        string offeringId,
        CancellationToken cancellationToken = default);
}

/// <summary>
/// Request to create a new offering.
/// </summary>
/// <param name="LookupKey">The unique lookup key for the offering.</param>
/// <param name="DisplayName">The display name for the offering.</param>
/// <param name="IsCurrent">Whether this offering should be the current (default) offering.</param>
/// <param name="Metadata">Optional metadata dictionary for custom data.</param>
public sealed record CreateOfferingRequest(
    [property: JsonPropertyName("lookup_key")] string LookupKey,
    [property: JsonPropertyName("display_name")] string DisplayName,
    [property: JsonPropertyName("is_current")] bool? IsCurrent = null,
    [property: JsonPropertyName("metadata")] Dictionary<string, object>? Metadata = null
);

/// <summary>
/// Request to update an offering.
/// </summary>
/// <param name="DisplayName">The new display name for the offering.</param>
/// <param name="IsCurrent">Whether this offering should be the current (default) offering.</param>
/// <param name="Metadata">Optional metadata dictionary for custom data.</param>
public sealed record UpdateOfferingRequest(
    [property: JsonPropertyName("display_name")] string? DisplayName = null,
    [property: JsonPropertyName("is_current")] bool? IsCurrent = null,
    [property: JsonPropertyName("metadata")] Dictionary<string, object>? Metadata = null
);
