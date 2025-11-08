using RevenueCat.NET.Services;

namespace RevenueCat.NET;

public interface IRevenueCatClient
{
    IProjectService Projects { get; }
    IAppService Apps { get; }
    ICustomerService Customers { get; }
    IProductService Products { get; }
    IEntitlementService Entitlements { get; }
    IOfferingService Offerings { get; }
    IPackageService Packages { get; }
    ISubscriptionService Subscriptions { get; }
    IPurchaseService Purchases { get; }
    IInvoiceService Invoices { get; }
    IPaywallService Paywalls { get; }
    IChartsService Charts { get; }
}
