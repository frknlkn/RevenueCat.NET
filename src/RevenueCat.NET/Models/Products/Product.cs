namespace RevenueCat.NET.Models;

public sealed record Product(
    string Object,
    string Id,
    string StoreIdentifier,
    ProductType Type,
    long CreatedAt,
    string AppId,
    string? DisplayName = null,
    SubscriptionDetails? Subscription = null,
    App? App = null
);

public enum ProductType
{
    Subscription,
    OneTime
}

public sealed record SubscriptionDetails(
    string Duration,
    string? GracePeriodDuration = null,
    string? TrialDuration = null
);
