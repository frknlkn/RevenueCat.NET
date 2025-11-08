namespace RevenueCat.NET.Models;

public sealed record App(
    string Object,
    string Id,
    string Name,
    long CreatedAt,
    AppType Type,
    string ProjectId,
    AppStoreConfig? AppStore = null,
    PlayStoreConfig? PlayStore = null,
    StripeConfig? Stripe = null,
    AmazonConfig? Amazon = null,
    RcBillingConfig? RcBilling = null,
    RokuConfig? Roku = null,
    PaddleConfig? Paddle = null
);

public enum AppType
{
    AppStore,
    PlayStore,
    Stripe,
    Amazon,
    RcBilling,
    Roku,
    Paddle,
    MacAppStore
}

public sealed record AppStoreConfig(
    string BundleId,
    string? SharedSecret = null
);

public sealed record PlayStoreConfig(
    string PackageName
);

public sealed record StripeConfig(
    string? StripeAccountId = null
);

public sealed record AmazonConfig(
    string PackageName,
    string? SharedSecret = null
);

public sealed record RcBillingConfig(
    string? StripeAccountId = null,
    string? AppName = null,
    string? SupportEmail = null,
    string? DefaultCurrency = null
);

public sealed record RokuConfig(
    string? RokuApiKey = null,
    string? RokuChannelId = null,
    string? RokuChannelName = null
);

public sealed record PaddleConfig(
    string? PaddleApiKey = null,
    bool PaddleIsSandbox = false
);
