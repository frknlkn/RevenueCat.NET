namespace RevenueCat.NET.Models;

public sealed record Offering(
    string Object,
    string Id,
    string LookupKey,
    string DisplayName,
    long CreatedAt,
    string ProjectId,
    bool IsDefault,
    IReadOnlyList<string>? PackageIds = null
);
