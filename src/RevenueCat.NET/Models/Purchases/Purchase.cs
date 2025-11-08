namespace RevenueCat.NET.Models;

public sealed record Purchase(
    string Object,
    string Id,
    string CustomerId,
    string ProductId,
    long PurchasedAt,
    string Store,
    Revenue? RevenueInUsd = null
);
