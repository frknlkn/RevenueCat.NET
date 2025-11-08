namespace RevenueCat.NET.Models;

public sealed record DeletedObject(
    string Object,
    string Id,
    long DeletedAt
);
