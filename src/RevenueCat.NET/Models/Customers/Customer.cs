namespace RevenueCat.NET.Models;

public sealed record Customer(
    string Object,
    string Id,
    long CreatedAt,
    string ProjectId,
    IReadOnlyList<CustomerAttribute>? Attributes = null
);

public sealed record CustomerAttribute(
    string Name,
    string Value
);
