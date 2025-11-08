namespace RevenueCat.NET.Models;

public sealed record Subscription(
    string Object,
    string Id,
    string CustomerId,
    string ProductId,
    SubscriptionStatus Status,
    long? CurrentPeriodStart,
    long? CurrentPeriodEnd,
    bool GivesAccess,
    AutoRenewalStatus AutoRenewalStatus,
    string? StoreSubscriptionIdentifier = null,
    Revenue? TotalRevenueInUsd = null,
    long? TrialEnd = null,
    long? GracePeriodEnd = null
);

public enum SubscriptionStatus
{
    Active,
    Expired,
    InTrial,
    InGracePeriod,
    Paused,
    Cancelled
}

public enum AutoRenewalStatus
{
    WillRenew,
    WillNotRenew,
    Unknown
}

public sealed record Revenue(
    decimal Gross,
    decimal Commission,
    decimal Tax,
    decimal Proceeds
);
