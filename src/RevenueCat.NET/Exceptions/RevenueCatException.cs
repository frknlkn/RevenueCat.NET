using RevenueCat.NET.Models;

namespace RevenueCat.NET.Exceptions;

/// <summary>
/// Base exception for all RevenueCat API errors.
/// </summary>
public class RevenueCatException : Exception
{
    /// <summary>
    /// The error response from the API (legacy).
    /// </summary>
    public ErrorResponse? ErrorResponse { get; }
    
    /// <summary>
    /// The structured error from the API.
    /// </summary>
    public RevenueCatError? Error { get; }
    
    /// <summary>
    /// The HTTP status code of the response.
    /// </summary>
    public int? StatusCode { get; }

    public RevenueCatException(string message, ErrorResponse? errorResponse = null)
        : base(message)
    {
        ErrorResponse = errorResponse;
    }
    
    public RevenueCatException(RevenueCatError error, int? statusCode = null)
        : base(error.Message)
    {
        Error = error;
        StatusCode = statusCode;
    }

    public RevenueCatException(string message, Exception? innerException)
        : base(message, innerException)
    {
    }
}

/// <summary>
/// Exception thrown when a parameter error occurs.
/// </summary>
public sealed class RevenueCatParameterException : RevenueCatException
{
    public RevenueCatParameterException(RevenueCatError error, int? statusCode = null) 
        : base(error, statusCode) { }
}

/// <summary>
/// Exception thrown when a resource is not found.
/// </summary>
public sealed class RevenueCatResourceNotFoundException : RevenueCatException
{
    public RevenueCatResourceNotFoundException(RevenueCatError error, int? statusCode = null) 
        : base(error, statusCode) { }
}

/// <summary>
/// Exception thrown when a resource conflict occurs.
/// </summary>
public sealed class RevenueCatConflictException : RevenueCatException
{
    public RevenueCatConflictException(RevenueCatError error, int? statusCode = null) 
        : base(error, statusCode) { }
}

/// <summary>
/// Exception thrown when rate limit is exceeded.
/// </summary>
public sealed class RevenueCatRateLimitException : RevenueCatException
{
    public RevenueCatRateLimitException(RevenueCatError error, int? statusCode = null) 
        : base(error, statusCode) { }
}

/// <summary>
/// Exception thrown when authentication fails.
/// </summary>
public sealed class RevenueCatAuthenticationException : RevenueCatException
{
    public RevenueCatAuthenticationException(RevenueCatError error, int? statusCode = null) 
        : base(error, statusCode) { }
}

/// <summary>
/// Exception thrown when authorization fails.
/// </summary>
public sealed class RevenueCatAuthorizationException : RevenueCatException
{
    public RevenueCatAuthorizationException(RevenueCatError error, int? statusCode = null) 
        : base(error, statusCode) { }
}

// Legacy exceptions for backward compatibility
public sealed class BadRequestException(string message, ErrorResponse? errorResponse = null) 
    : RevenueCatException(message, errorResponse);

public sealed class UnauthorizedException(string message, ErrorResponse? errorResponse = null) 
    : RevenueCatException(message, errorResponse);

public sealed class ForbiddenException(string message, ErrorResponse? errorResponse = null) 
    : RevenueCatException(message, errorResponse);

public sealed class NotFoundException(string message, ErrorResponse? errorResponse = null) 
    : RevenueCatException(message, errorResponse);

public sealed class ConflictException(string message, ErrorResponse? errorResponse = null) 
    : RevenueCatException(message, errorResponse);

public sealed class UnprocessableEntityException(string message, ErrorResponse? errorResponse = null) 
    : RevenueCatException(message, errorResponse);

public sealed class LockedException(string message, ErrorResponse? errorResponse = null) 
    : RevenueCatException(message, errorResponse);

public sealed class RateLimitException(string message, ErrorResponse? errorResponse = null) 
    : RevenueCatException(message, errorResponse);
