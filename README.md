, # RevenueCat.NET

[![NuGet](https://img.shields.io/nuget/v/RevenueCat.NET.svg)](https://www.nuget.org/packages/RevenueCat.NET/)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

A professional, production-ready .NET 8 client library for the RevenueCat REST API v2.

## Features

- **Full API Coverage**: Complete implementation of RevenueCat REST API v2
- **Modern .NET 8**: Built with latest C# features (primary constructors, records, file-scoped namespaces)
- **SOLID Principles**: Clean architecture with dependency injection support
- **Type-Safe**: Strong typing with nullable reference types enabled
- **Async/Await**: Fully asynchronous API with cancellation token support
- **Resilient**: Built-in retry logic, rate limiting handling, and timeout management
- **Performance**: Connection pooling, HTTP compression, and efficient JSON serialization
- **Extensible**: Interface-based design for easy testing and mocking

## Installation

```bash
dotnet add package RevenueCat.NET
```

## Quick Start

```csharp
using RevenueCat.NET;

var client = new RevenueCatClient("your_v2_api_key");

var projects = await client.Projects.ListAsync();

var customers = await client.Customers.ListAsync("proj_abc123");

var customer = await client.Customers.GetAsync(
    "proj_abc123", 
    "customer_id",
    expand: new[] { "attributes" }
);
```

## Configuration

```csharp
var client = new RevenueCatClient("your_api_key", options =>
{
    options.Timeout = TimeSpan.FromSeconds(60);
    options.MaxRetryAttempts = 5;
    options.RetryDelay = TimeSpan.FromSeconds(1);
    options.EnableRetryOnRateLimit = true;
});
```

## Usage Examples

### Projects

```csharp
var projects = await client.Projects.ListAsync(limit: 20);
```

### Apps

```csharp
var apps = await client.Apps.ListAsync("proj_abc123");

var app = await client.Apps.CreateAsync("proj_abc123", new CreateAppRequest(
    Name: "My iOS App",
    Type: AppType.AppStore,
    AppStore: new AppStoreConfig(
        BundleId: "com.example.app",
        SharedSecret: "your_shared_secret"
    )
));
```

### Customers

```csharp
var customer = await client.Customers.CreateAsync("proj_abc123", new CreateCustomerRequest(
    Id: "user_12345",
    Attributes: new[]
    {
        new CustomerAttribute("$email", "user@example.com"),
        new CustomerAttribute("$displayName", "John Doe")
    }
));

await client.Customers.TransferAsync("proj_abc123", "old_customer_id", 
    new TransferCustomerRequest("new_customer_id"));
```

### Products

```csharp
var products = await client.Products.ListAsync(
    "proj_abc123",
    appId: "app_xyz789",
    expand: new[] { "items.app" }
);

var product = await client.Products.CreateAsync("proj_abc123", new CreateProductRequest(
    StoreIdentifier: "com.example.premium.monthly",
    AppId: "app_xyz789",
    Type: ProductType.Subscription,
    DisplayName: "Premium Monthly"
));
```

### Entitlements

```csharp
var entitlement = await client.Entitlements.CreateAsync("proj_abc123", 
    new CreateEntitlementRequest(
        LookupKey: "premium",
        DisplayName: "Premium Access"
    ));

await client.Entitlements.AttachProductsAsync("proj_abc123", "ent_abc123",
    new AttachProductsRequest(new[] { "prod_123", "prod_456" }));
```

### Offerings & Packages

```csharp
var offering = await client.Offerings.CreateAsync("proj_abc123",
    new CreateOfferingRequest(
        LookupKey: "default",
        DisplayName: "Default Offering",
        IsDefault: true
    ));

var package = await client.Packages.CreateAsync("proj_abc123", "offering_id",
    new CreatePackageRequest(
        LookupKey: "monthly",
        DisplayName: "Monthly Package",
        ProductId: "prod_123",
        Position: 1
    ));
```

### Subscriptions

```csharp
var subscriptions = await client.Subscriptions.ListAsync("proj_abc123", "customer_id");

await client.Subscriptions.CancelAsync("proj_abc123", "customer_id", "sub_id");

await client.Subscriptions.RefundAsync("proj_abc123", "customer_id", "sub_id");
```

### Purchases

```csharp
var purchases = await client.Purchases.ListAsync("proj_abc123", "customer_id");

await client.Purchases.RefundAsync("proj_abc123", "customer_id", "purchase_id");
```

### Charts & Metrics

```csharp
var metrics = await client.Charts.GetMetricsAsync(
    "proj_abc123",
    ChartMetricType.Revenue,
    startDate: 1704067200000, // Unix timestamp
    endDate: 1735689600000,
    appId: "app_123"
);
```

## Error Handling

```csharp
try
{
    var customer = await client.Customers.GetAsync("proj_abc123", "customer_id");
}
catch (NotFoundException ex)
{
    Console.WriteLine($"Customer not found: {ex.Message}");
}
catch (RateLimitException ex)
{
    Console.WriteLine($"Rate limited. Retry after: {ex.ErrorResponse?.BackoffMs}ms");
}
catch (RevenueCatException ex)
{
    Console.WriteLine($"API error: {ex.Message}");
    Console.WriteLine($"Error type: {ex.ErrorResponse?.Type}");
}
```

## Dependency Injection

```csharp
services.AddSingleton<IRevenueCatClient>(sp => 
    new RevenueCatClient("your_api_key"));
```

## API Coverage

- ✅ Projects
- ✅ Apps (App Store, Play Store, Stripe, Amazon, Roku, Paddle, Web Billing)
- ✅ Customers (with transfer support)
- ✅ Products
- ✅ Entitlements (with product attachment)
- ✅ Offerings (with default offering support)
- ✅ Packages
- ✅ Paywalls
- ✅ Subscriptions (with cancel/refund actions)
- ✅ Purchases (with refund support)
- ✅ Invoices
- ✅ Charts & Metrics

## Requirements

- .NET 8.0 or higher
- RevenueCat API v2 key

## License

MIT License - see [LICENSE](LICENSE) file for details

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## Author

**frknlkn** - [GitHub](https://github.com/frknlkn)

## Support

For issues and questions:
- GitHub Issues: [Create an issue](https://github.com/frknlkn/revenuecat-dotnet/issues)
- RevenueCat Documentation: [https://www.revenuecat.com/docs/api-v2](https://www.revenuecat.com/docs/api-v2)
