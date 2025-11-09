# RevenueCat.NET Examples

This directory contains comprehensive examples demonstrating how to use the RevenueCat.NET SDK.

## Prerequisites

Before running any example, you need to set the following environment variables:

```bash
export REVENUECAT_API_KEY="your_v2_api_key"
export REVENUECAT_PROJECT_ID="your_project_id"
```

Some examples may require additional environment variables:

```bash
export REVENUECAT_APP_ID="your_app_id"
export REVENUECAT_CUSTOMER_ID="your_customer_id"
```

## Examples

### 1. BasicUsage
**Location:** `BasicUsage/`

A simple example showing how to initialize the client and make basic API calls.

```bash
cd BasicUsage
dotnet run
```

**Demonstrates:**
- Client initialization
- Listing projects
- Listing apps
- Listing customers
- Listing products
- Listing entitlements

---

### 2. CustomerManagement
**Location:** `CustomerManagement/`

Comprehensive example of customer management operations.

```bash
cd CustomerManagement
dotnet run
```

**Demonstrates:**
- Creating customers with attributes
- Retrieving customer details with expanded fields
- Searching customers by email
- Updating customer attributes
- Listing customer aliases
- Listing active entitlements
- Transferring customer data
- Pagination through customers
- Deleting customers

---

### 3. SubscriptionManagement
**Location:** `SubscriptionManagement/`

Complete guide to managing subscriptions.

```bash
cd SubscriptionManagement
dotnet run
```

**Demonstrates:**
- Listing customer subscriptions
- Filtering by environment (production/sandbox)
- Getting subscription details
- Listing subscription entitlements
- Getting authenticated management URLs
- Getting subscription transactions (Play Store)
- Canceling subscriptions (Web Billing)
- Refunding subscriptions (Web Billing)
- Refunding transactions (Play Store)
- Searching subscriptions by store identifier

---

### 4. ProductCatalog
**Location:** `ProductCatalog/`

End-to-end example of managing your product catalog.

```bash
cd ProductCatalog
dotnet run
```

**Demonstrates:**
- Creating products
- Listing and filtering products
- Creating entitlements
- Attaching products to entitlements
- Creating offerings with metadata
- Updating offerings
- Creating packages
- Attaching products to packages with eligibility criteria
- Managing the complete product catalog hierarchy

---

### 5. ErrorHandling
**Location:** `ErrorHandling/`

Comprehensive error handling patterns and best practices.

```bash
cd ErrorHandling
dotnet run
```

**Demonstrates:**
- Handling ResourceNotFoundException
- Handling ConflictException
- Handling ParameterException
- Handling RateLimitException
- Handling AuthenticationException
- Handling AuthorizationException
- Generic error handling patterns
- Custom retry logic
- Using cancellation tokens
- Accessing error details (type, message, retryable, backoff)

---

### 6. VirtualCurrency
**Location:** `VirtualCurrency/`

Managing virtual currency balances and transactions.

```bash
cd VirtualCurrency
dotnet run
```

**Demonstrates:**
- Listing virtual currency balances
- Including/excluding empty balances
- Creating transactions (add/deduct currency)
- Updating balances directly
- Using idempotency keys
- Managing multiple currency types
- Preventing duplicate transactions

---

### 7. Pagination
**Location:** `Pagination/`

Advanced pagination techniques for large datasets.

```bash
cd Pagination
dotnet run
```

**Demonstrates:**
- Manual pagination with starting_after
- Helper methods for pagination
- Async enumeration for memory efficiency
- Pagination with filtering
- Collecting all items into a list
- Progress reporting during pagination
- Parallel pagination for independent resources
- Extracting pagination cursors from NextPage URLs

---

## Running All Examples

To run all examples in sequence:

```bash
#!/bin/bash
for dir in BasicUsage CustomerManagement SubscriptionManagement ProductCatalog ErrorHandling VirtualCurrency Pagination; do
    echo "Running $dir..."
    cd $dir
    dotnet run
    cd ..
    echo ""
done
```

## Common Patterns

### Client Initialization

```csharp
var client = new RevenueCatClient(apiKey, options =>
{
    options.Timeout = TimeSpan.FromSeconds(30);
    options.MaxRetryAttempts = 3;
    options.EnableRetryOnRateLimit = true;
});
```

### Error Handling

```csharp
try
{
    var customer = await client.Customers.GetAsync(projectId, customerId);
}
catch (RevenueCatResourceNotFoundException ex)
{
    // Handle not found
}
catch (RevenueCatException ex)
{
    // Handle other API errors
    Console.WriteLine($"Error: {ex.Error?.Type} - {ex.Message}");
}
```

### Pagination

```csharp
string? startingAfter = null;
do
{
    var page = await client.Customers.ListAsync(
        projectId,
        limit: 20,
        startingAfter: startingAfter
    );
    
    // Process page.Items
    
    // Extract cursor for next page
    if (page.NextPage != null)
    {
        var uri = new Uri(page.NextPage);
        var query = HttpUtility.ParseQueryString(uri.Query);
        startingAfter = query["starting_after"];
    }
    else
    {
        startingAfter = null;
    }
} while (startingAfter != null);
```

### Expandable Fields

```csharp
var customer = await client.Customers.GetAsync(
    projectId,
    customerId,
    expand: new[] { "attributes", "active_entitlements" }
);
```

## Additional Resources

- [RevenueCat API Documentation](https://www.revenuecat.com/docs/api-v2)
- [RevenueCat.NET GitHub Repository](https://github.com/frknlkn/revenuecat-dotnet)
- [NuGet Package](https://www.nuget.org/packages/RevenueCat.NET/)

## Support

For issues or questions:
- GitHub Issues: [Create an issue](https://github.com/frknlkn/revenuecat-dotnet/issues)
- RevenueCat Support: [https://www.revenuecat.com/support](https://www.revenuecat.com/support)
