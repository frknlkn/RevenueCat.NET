# RevenueCat.NET v2.0.0 - Complete API Implementation

## 🎉 Major Release: Complete RevenueCat REST API v2 Coverage

This is a major release that provides **complete implementation** of the RevenueCat REST API v2 with 50+ models, 20+ enums, comprehensive service methods, and extensive documentation.

## 🚀 What's New

### Complete Model Coverage (50+ Models)
- **Core Models**: BaseModel, ListResponse<T>, DeletedObject, MonetaryAmount, RevenueCatError
- **Customer Models**: Customer, CustomerAlias, CustomerAttribute, CustomerEntitlement, ExperimentEnrollment, VirtualCurrencyBalance, Transfer
- **Subscription Models**: Subscription, SubscriptionProduct, SubscriptionTransaction, PendingChanges, AuthenticatedManagementUrl
- **Product Models**: Product, OneTimeProduct, StoreProduct, CreateAppStoreConnectSubscriptionInput, CreateAppStoreConnectInAppPurchaseInput
- **Catalog Models**: Entitlement, Offering, Package, PackageProductAssociation
- **Purchase & Invoice Models**: Purchase, Invoice, InvoiceLineItem
- **App & Project Models**: Project, App with store-specific variants (Amazon, AppStore, PlayStore, Stripe, etc.)
- **Analytics Models**: OverviewMetrics, OverviewMetric
- **Paywall Models**: Paywall

### Comprehensive Enum Support (20+ Enums)
- Environment, ProductType, Store, Ownership
- AutoRenewalStatus, SubscriptionStatus, PurchaseStatus
- AppType, EligibilityCriteria, ErrorType
- All with proper JSON serialization and XML documentation

### Enhanced Service Methods

#### CustomerService
- List, Get, Create, Delete customers
- Transfer customer data between customers
- Manage aliases and attributes
- List active entitlements
- **Virtual currency operations** with idempotency support

#### SubscriptionService
- List and get subscriptions with environment filtering
- **Search by store subscription identifier**
- Cancel and refund Web Billing subscriptions
- Get authenticated management URLs
- Manage Play Store transactions

#### Product & Catalog Services
- Complete CRUD operations for Products, Entitlements, Offerings, Packages
- **Expandable fields** to reduce API calls
- Product-entitlement and product-package relationship management
- **Create products in App Store Connect**
- Package eligibility criteria support
- Offering metadata support

#### Purchase & Invoice Services
- List and get purchases with environment filtering
- Search by store purchase identifier
- Refund Web Billing purchases
- List customer invoices
- Get invoice file URLs

#### App & Project Services
- Manage projects and apps
- Store-specific app configuration (all 8 stores supported)
- List API keys
- Get StoreKit configuration files

#### Analytics & Paywalls
- Get overview metrics with currency parameter
- Create paywalls for offerings

### Advanced Features

✨ **Expandable Fields**: Reduce API calls by expanding related resources in a single request

📄 **Comprehensive Pagination**: Cursor-based pagination with ListResponse<T> model

🔍 **Search Functionality**: Search subscriptions and purchases by store identifiers

💰 **Virtual Currency**: Complete virtual currency management with idempotency

🔄 **Customer Transfer**: Transfer customer data between customers

🛡️ **Enhanced Error Handling**: Typed exceptions with retry information and backoff times

🔁 **Automatic Retries**: Built-in retry logic for retryable errors with exponential backoff

⚡ **Rate Limiting**: Automatic handling of rate limits with backoff support

### Documentation & Examples

📚 **7 Comprehensive Examples**:
- BasicUsage - Quick start guide
- CustomerManagement - Complete customer lifecycle
- SubscriptionManagement - Subscription operations
- ProductCatalog - Product catalog management
- ErrorHandling - Error handling patterns
- VirtualCurrency - Virtual currency operations
- Pagination - Pagination techniques

📖 **Complete Documentation**:
- XML documentation for all public APIs
- Migration guide from v1.x to v2.0
- API coverage matrix
- Advanced usage patterns

## ⚠️ Breaking Changes

This is a major version with breaking changes. Please review the [MIGRATION.md](MIGRATION.md) guide before upgrading.

### Key Breaking Changes:
1. **Service method names** updated for consistency
2. **String properties** converted to strongly-typed enums
3. **Exception hierarchy** changed - use specific exception types
4. **Request models** now use strongly-typed records
5. **Model properties** may have different types (e.g., timestamps as long)

## 📦 Installation

```bash
dotnet add package RevenueCat.NET --version 2.0.0
```

## 🔧 Quick Start

```csharp
using RevenueCat.NET;
using RevenueCat.NET.Configuration;

var options = new RevenueCatOptions
{
    ApiKey = "your_api_key_here"
};

var client = new RevenueCatClient(options);

// List customers
var customers = await client.Customers.ListAsync("project_id");

// Get subscription with expandable fields
var subscription = await client.Subscriptions.GetSubscriptionAsync(
    "project_id", 
    "subscription_id"
);

// Search subscriptions by store identifier
var results = await client.Subscriptions.SearchSubscriptionsAsync(
    "project_id",
    "store_subscription_id"
);

// Manage virtual currency
var balances = await client.Customers.ListVirtualCurrencyBalancesAsync(
    "project_id",
    "customer_id"
);
```

## 📊 API Coverage

This release provides **complete coverage** of the RevenueCat REST API v2:

- ✅ Customers (100%)
- ✅ Subscriptions (100%)
- ✅ Products (100%)
- ✅ Entitlements (100%)
- ✅ Offerings (100%)
- ✅ Packages (100%)
- ✅ Purchases (100%)
- ✅ Invoices (100%)
- ✅ Apps (100%)
- ✅ Projects (100%)
- ✅ Paywalls (100%)
- ✅ Charts & Metrics (100%)

## 🙏 Acknowledgments

Thank you to the RevenueCat team for their excellent API documentation and to all contributors who helped make this release possible.

## 📝 Full Changelog

See [CHANGELOG.md](CHANGELOG.md) for the complete list of changes.

## 🔗 Links

- [GitHub Repository](https://github.com/frknlkn/revenuecat-dotnet)
- [NuGet Package](https://www.nuget.org/packages/RevenueCat.NET)
- [RevenueCat API Documentation](https://www.revenuecat.com/docs/api-v2)
- [Migration Guide](MIGRATION.md)
