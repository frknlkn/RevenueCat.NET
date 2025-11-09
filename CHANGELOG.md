# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [2.0.0] - 2024-11-09

### Added

#### Core Models (50+ new models)
- `BaseModel` - Base class for all API models with common properties
- `ListResponse<T>` - Generic paginated list response model
- `DeletedObject` - Response model for delete operations
- `MonetaryAmount` - Comprehensive monetary value representation with currency, gross, commission, tax, and proceeds
- `RevenueCatError` - Detailed error model with type, message, retryable flag, backoff time, and documentation URL

#### Customer Models
- `Customer` - Complete customer model with all API properties
- `CustomerAlias` - Customer alias representation
- `CustomerAttribute` - Customer attribute model
- `CustomerEntitlement` - Active entitlement model
- `ExperimentEnrollment` - Experiment enrollment data
- `VirtualCurrencyBalance` - Virtual currency balance model
- `Transfer` - Customer transfer response model

#### Subscription Models
- `Subscription` - Complete subscription model with status, auto-renewal, revenue tracking
- `SubscriptionProduct` - Subscription-specific product details
- `SubscriptionTransaction` - Transaction representation for Play Store subscriptions
- `PendingChanges` - Pending subscription changes model
- `AuthenticatedManagementUrl` - Customer portal URL model

#### Product Models
- `Product` - Complete product model with store identifier and type
- `OneTimeProduct` - One-time purchase product details
- `StoreProduct` - Store product representation
- `CreateAppStoreConnectSubscriptionInput` - App Store Connect subscription creation
- `CreateAppStoreConnectInAppPurchaseInput` - App Store Connect IAP creation

#### Entitlement & Offering Models
- `Entitlement` - Entitlement model with product relationships
- `Offering` - Offering model with metadata support
- `Package` - Package model with position and eligibility
- `PackageProductAssociation` - Product-package association with eligibility criteria

#### Purchase & Invoice Models
- `Purchase` - Complete purchase model with revenue tracking
- `Invoice` - Invoice model with line items
- `InvoiceLineItem` - Invoice line item details

#### App & Project Models
- `Project` - Project model
- `App` - Polymorphic app model with store-specific variants
- `AmazonAppDetails`, `AppStoreDetails`, `MacAppStoreDetails`, `PlayStoreDetails`, `StripeDetails`, `RCBillingDetails`, `RokuDetails`, `PaddleDetails` - Store-specific app configurations
- `PublicApiKey` - API key model
- `StoreKitConfigFile` - StoreKit configuration file model

#### Analytics Models
- `OverviewMetrics` - Container for overview metrics
- `OverviewMetric` - Individual metric model with value, unit, and period

#### Paywall Models
- `Paywall` - Paywall configuration model

#### Enumeration Types (20+ enums)
- `Environment` - Production, Sandbox
- `ProductType` - Subscription, OneTime, Consumable, NonConsumable, NonRenewingSubscription
- `Store` - Amazon, AppStore, MacAppStore, PlayStore, Promotional, Stripe, RCBilling, Roku, Paddle
- `Ownership` - Purchased, FamilyShared
- `AutoRenewalStatus` - WillRenew, WillNotRenew, WillChangeProduct, WillPause, RequiresPriceIncreaseConsent, HasAlreadyRenewed
- `SubscriptionStatus` - Trialing, Active, Expired, InGracePeriod, InBillingRetry, Paused, Unknown, Incomplete
- `PurchaseStatus` - Owned, Refunded
- `AppType` - Amazon, AppStore, MacAppStore, PlayStore, Stripe, RCBilling, Roku, Paddle
- `EligibilityCriteria` - All, GoogleSdkLessThan6, GoogleSdkGreaterOrEqual6
- `ErrorType` - ParameterError, ResourceAlreadyExists, ResourceMissing, IdempotencyError, RateLimitError, AuthenticationError, AuthorizationError, StoreError, ServerError, ResourceLockedError, UnprocessableEntityError, InvalidRequest

#### Exception Types
- `RevenueCatException` - Base exception with Error and StatusCode properties
- `RevenueCatParameterException` - Invalid parameter errors
- `RevenueCatResourceNotFoundException` - Resource not found errors
- `RevenueCatConflictException` - Resource conflict errors
- `RevenueCatRateLimitException` - Rate limiting errors
- `RevenueCatAuthenticationException` - Authentication errors
- `RevenueCatAuthorizationException` - Authorization errors

#### Service Methods

**CustomerService:**
- `ListAsync` - List customers with search support
- `GetAsync` - Get customer with expandable fields
- `CreateAsync` - Create customer with attributes
- `DeleteAsync` - Delete customer
- `TransferAsync` - Transfer customer data between customers
- `ListAliasesAsync` - List customer aliases
- `ListAttributesAsync` - List customer attributes
- `SetAttributesAsync` - Set customer attributes in bulk
- `ListActiveEntitlementsAsync` - List active entitlements
- `ListVirtualCurrencyBalancesAsync` - List virtual currency balances
- `CreateVirtualCurrencyTransactionAsync` - Create virtual currency transaction with idempotency
- `UpdateVirtualCurrencyBalanceAsync` - Update virtual currency balance with idempotency

**SubscriptionService:**
- `ListSubscriptionsAsync` - List subscriptions with environment filter
- `GetSubscriptionAsync` - Get subscription details
- `SearchSubscriptionsAsync` - Search by store subscription identifier
- `CancelSubscriptionAsync` - Cancel Web Billing subscription
- `RefundSubscriptionAsync` - Refund Web Billing subscription
- `GetAuthenticatedManagementUrlAsync` - Get customer portal URL
- `GetSubscriptionTransactionsAsync` - Get Play Store transactions
- `RefundSubscriptionTransactionAsync` - Refund Play Store transaction
- `ListSubscriptionEntitlementsAsync` - List subscription entitlements

**ProductService:**
- `ListAsync` - List products with app filter and expandable fields
- `GetAsync` - Get product with expandable fields
- `CreateAsync` - Create product
- `DeleteAsync` - Delete product
- `CreateProductInStoreAsync` - Create product in App Store Connect

**EntitlementService:**
- `ListAsync` - List entitlements with expandable fields
- `GetAsync` - Get entitlement with expandable fields
- `CreateAsync` - Create entitlement
- `UpdateAsync` - Update entitlement
- `DeleteAsync` - Delete entitlement
- `AttachProductsAsync` - Attach products to entitlement
- `DetachProductsAsync` - Detach products from entitlement
- `GetProductsAsync` - Get products in entitlement

**OfferingService:**
- `ListAsync` - List offerings with expandable fields
- `GetAsync` - Get offering with expandable fields
- `CreateAsync` - Create offering with metadata
- `UpdateAsync` - Update offering with metadata
- `DeleteAsync` - Delete offering
- `SetDefaultAsync` - Set offering as default

**PackageService:**
- `ListAsync` - List packages with expandable fields
- `GetAsync` - Get package with expandable fields
- `CreateAsync` - Create package
- `UpdateAsync` - Update package with position
- `DeleteAsync` - Delete package
- `AttachProductsAsync` - Attach products with eligibility criteria
- `DetachProductsAsync` - Detach products from package
- `GetProductsAsync` - Get products in package

**PurchaseService:**
- `ListAsync` - List purchases with environment filter
- `GetAsync` - Get purchase details
- `SearchAsync` - Search by store purchase identifier
- `RefundAsync` - Refund Web Billing purchase
- `ListPurchaseEntitlementsAsync` - List purchase entitlements

**InvoiceService:**
- `ListAsync` - List customer invoices
- `GetInvoiceFileAsync` - Get invoice file URL

**AppService:**
- `ListAsync` - List apps
- `GetAsync` - Get app details
- `CreateAsync` - Create app with store-specific configuration
- `UpdateAsync` - Update app
- `DeleteAsync` - Delete app
- `ListPublicApiKeysAsync` - List app API keys
- `GetStoreKitConfigAsync` - Get StoreKit configuration file

**ProjectService:**
- `ListAsync` - List projects

**PaywallService:**
- `CreateAsync` - Create paywall for offering

**ChartsService:**
- `GetMetricsAsync` - Get overview metrics with currency parameter

#### Features
- Expandable fields support to reduce API calls
- Comprehensive pagination support with cursor-based navigation
- Search functionality for subscriptions and purchases by store identifiers
- Virtual currency management with idempotency support
- Customer data transfer between customers
- Store-specific app configuration support
- Subscription lifecycle management (cancel, refund, management URLs)
- Play Store transaction management
- Product-entitlement and product-package relationship management
- Offering metadata support
- Package eligibility criteria support
- Invoice file access
- Analytics and metrics retrieval
- Enhanced error handling with typed exceptions and retry information
- Automatic retry logic for retryable errors with exponential backoff
- Rate limiting handling with backoff support

#### Documentation
- Comprehensive XML documentation for Customer service interface
- 7 detailed usage examples:
  - BasicUsage - Quick start guide
  - CustomerManagement - Complete customer lifecycle
  - SubscriptionManagement - Subscription operations
  - ProductCatalog - Product catalog management
  - ErrorHandling - Error handling patterns
  - VirtualCurrency - Virtual currency operations
  - Pagination - Pagination techniques
- Examples README with setup instructions
- Updated main README with API coverage matrix
- Migration guide (MIGRATION.md) from v1.x to v2.0
- Advanced usage documentation for expandable fields, pagination, search, and virtual currency

### Changed
- Updated `HttpRequestExecutor` to deserialize error responses and throw typed exceptions
- Enhanced JSON serialization configuration with snake_case naming policy
- Improved error handling with specific exception types based on error type and status code
- Updated service method signatures to return proper response types
- Enhanced pagination support with ListResponse<T> model

### Breaking Changes
- Service method names updated for consistency (e.g., `ListAsync` → `ListSubscriptionsAsync` for subscriptions)
- String properties converted to strongly-typed enums (Store, Environment, ProductType, etc.)
- Exception hierarchy changed - catch specific exception types instead of generic RevenueCatException
- Request models now use strongly-typed records instead of anonymous objects
- Model properties may have different types (e.g., timestamps as long instead of DateTime)

### Migration
- See [MIGRATION.md](MIGRATION.md) for detailed migration instructions
- Update exception handling to use specific exception types
- Replace string comparisons with enum comparisons
- Update service method calls to use new method names
- Use strongly-typed request models

## [1.0.1] - 2025-11-08

### Added
- Offering management endpoints (list, get, create, update, delete, set default)
- Package management endpoints (list, get, create, update, delete)
- Subscription management endpoints (list, get, cancel, refund)
- Purchase management endpoints (list, get, refund)
- Invoice management endpoints (list, get)
- Charts & Metrics endpoints (revenue, MRR, ARR, active subscriptions, churn, etc.)
- Package icon for NuGet gallery
- Complete models for Subscription, Purchase, Invoice, Offering, Package, Charts
- Revenue tracking models with gross, commission, tax, and proceeds breakdown
- Subscription status and auto-renewal status enums
- Invoice status enum
- Chart metric types enum

### Changed
- Updated README with examples for all new endpoints
- Enhanced documentation with complete API coverage

## [1.0.0] - 2025-11-08

### Added
- Initial release of RevenueCat.NET
- Full support for RevenueCat REST API v2
- Project management endpoints
- App management endpoints (App Store, Play Store, Stripe, Amazon, Roku, Paddle, Web Billing)
- Customer management endpoints with transfer support
- Product management endpoints
- Entitlement management endpoints with product attachment
- Paywall creation endpoints
- Comprehensive error handling with typed exceptions
- Automatic retry logic with exponential backoff
- Rate limiting support with automatic retry
- Connection pooling and HTTP compression
- Strongly typed models with nullable reference types
- Async/await support with cancellation tokens
- Expandable fields support
- Pagination support
- Query string builder utilities
- Configuration options for timeout, retry, and base URL
- .NET 8 optimizations (primary constructors, records, file-scoped namespaces)
- SOLID principles implementation
- Interface-based design for testability
- Comprehensive XML documentation
- Unit test infrastructure

### Planned
- Webhook signature verification
- Batch operations support
- Response caching
- Request/response logging
- Metrics and telemetry
- Promotional entitlements
