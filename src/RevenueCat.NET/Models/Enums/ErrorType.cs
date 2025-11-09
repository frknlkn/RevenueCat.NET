using System.Text.Json.Serialization;
using RevenueCat.NET.Serialization;

namespace RevenueCat.NET.Models.Enums;

/// <summary>
/// The type of error returned by the RevenueCat API.
/// </summary>
[JsonConverter(typeof(SnakeCaseEnumConverter<ErrorType>))]
public enum ErrorType
{
    /// <summary>
    /// Invalid parameter provided in the request.
    /// </summary>
    [JsonPropertyName("parameter_error")]
    ParameterError,
    
    /// <summary>
    /// Resource already exists.
    /// </summary>
    [JsonPropertyName("resource_already_exists")]
    ResourceAlreadyExists,
    
    /// <summary>
    /// Requested resource was not found.
    /// </summary>
    [JsonPropertyName("resource_missing")]
    ResourceMissing,
    
    /// <summary>
    /// Idempotency key conflict.
    /// </summary>
    [JsonPropertyName("idempotency_error")]
    IdempotencyError,
    
    /// <summary>
    /// Rate limit exceeded.
    /// </summary>
    [JsonPropertyName("rate_limit_error")]
    RateLimitError,
    
    /// <summary>
    /// Authentication failed.
    /// </summary>
    [JsonPropertyName("authentication_error")]
    AuthenticationError,
    
    /// <summary>
    /// Authorization failed - insufficient permissions.
    /// </summary>
    [JsonPropertyName("authorization_error")]
    AuthorizationError,
    
    /// <summary>
    /// Error from the app store.
    /// </summary>
    [JsonPropertyName("store_error")]
    StoreError,
    
    /// <summary>
    /// Internal server error.
    /// </summary>
    [JsonPropertyName("server_error")]
    ServerError,
    
    /// <summary>
    /// Resource is locked and cannot be modified.
    /// </summary>
    [JsonPropertyName("resource_locked_error")]
    ResourceLockedError,
    
    /// <summary>
    /// Request cannot be processed.
    /// </summary>
    [JsonPropertyName("unprocessable_entity_error")]
    UnprocessableEntityError,
    
    /// <summary>
    /// Invalid request format.
    /// </summary>
    [JsonPropertyName("invalid_request")]
    InvalidRequest
}
