namespace RevenueCat.NET.Models;

public sealed record Paywall(
    string Object,
    string Id,
    string OfferingId,
    long CreatedAt
);
