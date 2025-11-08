using RevenueCat.NET.Models;

namespace RevenueCat.NET.Services;

public interface IAppService
{
    Task<ListResponse<App>> ListAsync(
        string projectId,
        int? limit = null,
        string? startingAfter = null,
        CancellationToken cancellationToken = default);

    Task<App> GetAsync(
        string projectId,
        string appId,
        CancellationToken cancellationToken = default);

    Task<App> CreateAsync(
        string projectId,
        CreateAppRequest request,
        CancellationToken cancellationToken = default);

    Task<App> UpdateAsync(
        string projectId,
        string appId,
        UpdateAppRequest request,
        CancellationToken cancellationToken = default);

    Task<DeletedObject> DeleteAsync(
        string projectId,
        string appId,
        CancellationToken cancellationToken = default);
}

public sealed record CreateAppRequest(
    string Name,
    AppType Type,
    AppStoreConfig? AppStore = null,
    PlayStoreConfig? PlayStore = null,
    StripeConfig? Stripe = null,
    AmazonConfig? Amazon = null,
    RcBillingConfig? RcBilling = null,
    RokuConfig? Roku = null,
    PaddleConfig? Paddle = null
);

public sealed record UpdateAppRequest(
    string? Name = null,
    AppStoreConfig? AppStore = null,
    PlayStoreConfig? PlayStore = null,
    StripeConfig? Stripe = null,
    AmazonConfig? Amazon = null,
    RcBillingConfig? RcBilling = null,
    RokuConfig? Roku = null,
    PaddleConfig? Paddle = null
);
