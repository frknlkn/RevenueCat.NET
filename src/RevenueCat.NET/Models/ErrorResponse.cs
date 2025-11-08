namespace RevenueCat.NET.Models;

public sealed record ErrorResponse(
    string Type,
    string Message,
    bool Retryable,
    string? DocUrl = null,
    int? BackoffMs = null,
    string? Param = null
);
