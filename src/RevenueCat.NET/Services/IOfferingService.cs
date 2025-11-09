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
    /// <param name="projectId">The project ID.</param>
    /// <param name="limit">Maximum number of items to return (default: 20, max: 100).</param>
    /// <param name="startingAfter">Cursor for pagination.</param>
    /// <param name="expand">Optional fields to expand (e.g., "items.packages", "items.packages.products").</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A paginated list of offerings.</returns>
    /// <exception cref="ArgumentException">Thrown when projectId is null or whitespace.</exception>
    /// <exception cref="RevenueCat.NET.Exceptions.RevenueCatException">Thrown when the API returns an error.</exception>
    Task<ListResponse<Offering>> ListAsync(
        string projectId,
        int? limit = null,
        string? startingAfter = null,
        string[]? expand = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a specific offering by ID.
    /// </summary>
    /// <param name="projectId">The project ID.</param>
    /// <param name="offeringId">The offering ID.</param>
    /// <param name="expand">Optional fields to expand (e.g., "packages", "packages.products").</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The offering details.</returns>
    /// <exception cref="ArgumentException">Thrown when projectId or offeringId is null or whitespace.</exception>
    /// <exception cref="RevenueCat.NET.Exceptions.RevenueCatResourceNotFoundException">Thrown when the offering is not found.</exception>
    /// <exception cref="RevenueCat.NET.Exceptions.RevenueCatException">Thrown when the API returns an error.</exception>
    Task<Offering> GetAsync(
        string projectId,
        string offeringId,
        string[]? expand = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a new offering.
    /// </summary>
    /// <param name="projectId">The project ID.</param>
    /// <param name="request">The offering creation request with metadata support.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The created offering.</returns>
    /// <exception cref="ArgumentException">Thrown when projectId is null or whitespace.</exception>
    /// <exception cref="ArgumentNullException">Thrown when request is null.</exception>
    /// <exception cref="RevenueCat.NET.Exceptions.RevenueCatException">Thrown when the API returns an error.</exception>
    Task<Offering> CreateAsync(
        string projectId,
        CreateOfferingRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing offering.
    /// </summary>
    /// <param name="projectId">The project ID.</param>
    /// <param name="offeringId">The offering ID to update.</param>
    /// <param name="request">The offering update request with metadata support.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The updated offering.</returns>
    /// <exception cref="ArgumentException">Thrown when projectId or offeringId is null or whitespace.</exception>
    /// <exception cref="ArgumentNullException">Thrown when request is null.</exception>
    /// <exception cref="RevenueCat.NET.Exceptions.RevenueCatException">Thrown when the API returns an error.</exception>
    Task<Offering> UpdateAsync(
        string projectId,
        string offeringId,
        UpdateOfferingRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes an offering permanently.
    /// </summary>
    /// <param name="projectId">The project ID.</param>
    /// <param name="offeringId">The offering ID to delete.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A deleted object confirmation.</returns>
    /// <exception cref="ArgumentException">Thrown when projectId or offeringId is null or whitespace.</exception>
    /// <exception cref="RevenueCat.NET.Exceptions.RevenueCatResourceNotFoundException">Thrown when the offering is not found.</exception>
    /// <exception cref="RevenueCat.NET.Exceptions.RevenueCatException">Thrown when the API returns an error.</exception>
    Task<DeletedObject> DeleteAsync(
        string projectId,
        string offeringId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Sets an offering as the default (current) offering.
    /// </summary>
    /// <param name="projectId">The project ID.</param>
    /// <param name="offeringId">The offering ID to set as default.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The updated offering.</returns>
    /// <exception cref="ArgumentException">Thrown when projectId or offeringId is null or whitespace.</exception>
    /// <exception cref="RevenueCat.NET.Exceptions.RevenueCatException">Thrown when the API returns an error.</exception>
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
