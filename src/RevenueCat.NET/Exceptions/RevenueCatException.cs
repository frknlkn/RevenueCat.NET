using RevenueCat.NET.Models;

namespace RevenueCat.NET.Exceptions;

public class RevenueCatException : Exception
{
    public ErrorResponse? ErrorResponse { get; }

    public RevenueCatException(string message, ErrorResponse? errorResponse = null)
        : base(message)
    {
        ErrorResponse = errorResponse;
    }

    public RevenueCatException(string message, Exception? innerException)
        : base(message, innerException)
    {
    }
}

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
