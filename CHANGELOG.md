# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

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
