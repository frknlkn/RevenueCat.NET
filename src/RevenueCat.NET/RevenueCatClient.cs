using RevenueCat.NET.Configuration;
using RevenueCat.NET.Services;

namespace RevenueCat.NET;

public sealed class RevenueCatClient : IRevenueCatClient
{
    public IProjectService Projects { get; }
    public IAppService Apps { get; }
    public ICustomerService Customers { get; }
    public IProductService Products { get; }
    public IEntitlementService Entitlements { get; }
    public IOfferingService Offerings { get; }
    public IPackageService Packages { get; }
    public ISubscriptionService Subscriptions { get; }
    public IPurchaseService Purchases { get; }
    public IInvoiceService Invoices { get; }
    public IPaywallService Paywalls { get; }
    public IChartsService Charts { get; }

    public RevenueCatClient(string apiKey, Action<RevenueCatOptions>? configure = null)
        : this(CreateOptions(apiKey, configure))
    {
    }

    public RevenueCatClient(RevenueCatOptions options)
    {
        ArgumentNullException.ThrowIfNull(options);
        
        var httpClient = HttpClientFactory.Create(options);
        var requestExecutor = new HttpRequestExecutor(httpClient, options);

        Projects = new ProjectService(requestExecutor);
        Apps = new AppService(requestExecutor);
        Customers = new CustomerService(requestExecutor);
        Products = new ProductService(requestExecutor);
        Entitlements = new EntitlementService(requestExecutor);
        Offerings = new OfferingService(requestExecutor);
        Packages = new PackageService(requestExecutor);
        Subscriptions = new SubscriptionService(requestExecutor);
        Purchases = new PurchaseService(requestExecutor);
        Invoices = new InvoiceService(requestExecutor);
        Paywalls = new PaywallService(requestExecutor);
        Charts = new ChartsService(requestExecutor);
    }

    private static RevenueCatOptions CreateOptions(string apiKey, Action<RevenueCatOptions>? configure)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(apiKey);
        
        var options = new RevenueCatOptions { ApiKey = apiKey };
        configure?.Invoke(options);
        return options;
    }
}
