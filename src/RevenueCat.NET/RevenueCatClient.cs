using Refit;
using RevenueCat.NET.Configuration;
using RevenueCat.NET.Services;
using System.Text.Json;
using System.Text.Json.Serialization;

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

        var refitSettings = new RefitSettings
        {
            ContentSerializer = new SystemTextJsonContentSerializer(new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                PropertyNameCaseInsensitive = true,
                WriteIndented = false,
                Converters =
                {
                    new JsonStringEnumConverter(JsonNamingPolicy.SnakeCaseLower, allowIntegerValues: false)
                }
            })
        };

        Projects = RestService.For<IProjectService>(httpClient, refitSettings);
        Apps = RestService.For<IAppService>(httpClient, refitSettings);
        Customers = RestService.For<ICustomerService>(httpClient, refitSettings);
        Products = RestService.For<IProductService>(httpClient, refitSettings);
        Entitlements = RestService.For<IEntitlementService>(httpClient, refitSettings);
        Offerings = RestService.For<IOfferingService>(httpClient, refitSettings);
        Packages = RestService.For<IPackageService>(httpClient, refitSettings);
        Subscriptions = RestService.For<ISubscriptionService>(httpClient, refitSettings);
        Purchases = RestService.For<IPurchaseService>(httpClient, refitSettings);
        Invoices = RestService.For<IInvoiceService>(httpClient, refitSettings);
        Paywalls = RestService.For<IPaywallService>(httpClient, refitSettings);
        Charts = RestService.For<IChartsService>(httpClient, refitSettings);
    }

    private static RevenueCatOptions CreateOptions(string apiKey, Action<RevenueCatOptions>? configure)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(apiKey);

        var options = new RevenueCatOptions { ApiKey = apiKey };
        configure?.Invoke(options);
        return options;
    }
}
