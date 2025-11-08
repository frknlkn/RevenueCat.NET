namespace RevenueCat.NET.Models;

public sealed record Entitlement(
    string Object,
    string Id,
    string LookupKey,
    string DisplayName,
    long CreatedAt,
    string ProjectId,
    IReadOnlyList<string>? ProductIds = null
);
