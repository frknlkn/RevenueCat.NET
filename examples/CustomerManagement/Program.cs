using RevenueCat.NET;
using RevenueCat.NET.Models.Customers;
using RevenueCat.NET.Services;

// Customer Management Example
// This example demonstrates how to create, retrieve, update, and manage customers

var apiKey = Environment.GetEnvironmentVariable("REVENUECAT_API_KEY")
    ?? throw new InvalidOperationException("REVENUECAT_API_KEY environment variable is required");

var projectId = Environment.GetEnvironmentVariable("REVENUECAT_PROJECT_ID")
    ?? throw new InvalidOperationException("REVENUECAT_PROJECT_ID environment variable is required");

var client = new RevenueCatClient(apiKey);

Console.WriteLine("=== Customer Management Example ===\n");

try
{
    // 1. Create a new customer with attributes
    Console.WriteLine("1. Creating a new customer...");
    var customerId = $"user_{Guid.NewGuid():N}";
    
    var createRequest = new CreateCustomerRequest(
        Id: customerId,
        Attributes: new List<CustomerAttribute>
        {
            new() { Key = "$email", Value = "john.doe@example.com" },
            new() { Key = "$displayName", Value = "John Doe" },
            new() { Key = "plan", Value = "premium" }
        }
    );

    var customer = await client.Customers.CreateAsync(projectId, createRequest);
    Console.WriteLine($"✓ Created customer: {customer.Id}");
    Console.WriteLine($"  First seen: {DateTimeOffset.FromUnixTimeMilliseconds(customer.FirstSeenAt)}");

    // 2. Get customer details with expanded fields
    Console.WriteLine("\n2. Retrieving customer with expanded fields...");
    var customerDetails = await client.Customers.GetAsync(
        projectId,
        customerId,
        expand: new[] { "attributes", "active_entitlements" }
    );
    
    Console.WriteLine($"✓ Retrieved customer: {customerDetails.Id}");
    if (customerDetails.Attributes?.Items != null)
    {
        Console.WriteLine($"  Attributes count: {customerDetails.Attributes.Items.Count}");
        foreach (var attr in customerDetails.Attributes.Items)
        {
            Console.WriteLine($"    - {attr.Key}: {attr.Value}");
        }
    }

    // 3. List all customers with search
    Console.WriteLine("\n3. Searching for customers by email...");
    var searchResults = await client.Customers.ListAsync(
        projectId,
        search: "john.doe@example.com",
        limit: 10
    );
    Console.WriteLine($"✓ Found {searchResults.Items.Count} customer(s)");

    // 4. Update customer attributes
    Console.WriteLine("\n4. Updating customer attributes...");
    var updateRequest = new SetCustomerAttributesRequest(
        Attributes: new List<CustomerAttribute>
        {
            new() { Key = "plan", Value = "enterprise" },
            new() { Key = "subscription_date", Value = DateTime.UtcNow.ToString("yyyy-MM-dd") }
        }
    );

    var updatedAttributes = await client.Customers.SetAttributesAsync(
        projectId,
        customerId,
        updateRequest
    );
    Console.WriteLine($"✓ Updated attributes: {updatedAttributes.Items.Count} total");

    // 5. List customer aliases
    Console.WriteLine("\n5. Listing customer aliases...");
    var aliases = await client.Customers.ListAliasesAsync(projectId, customerId);
    Console.WriteLine($"✓ Customer has {aliases.Items.Count} alias(es)");

    // 6. List active entitlements
    Console.WriteLine("\n6. Listing active entitlements...");
    var entitlements = await client.Customers.ListActiveEntitlementsAsync(projectId, customerId);
    Console.WriteLine($"✓ Customer has {entitlements.Items.Count} active entitlement(s)");

    // 7. Transfer customer data (example - commented out to avoid actual transfer)
    /*
    Console.WriteLine("\n7. Transferring customer data...");
    var targetCustomerId = $"user_{Guid.NewGuid():N}";
    var transferRequest = new TransferCustomerRequest(
        TargetCustomerId: targetCustomerId,
        AppIds: null // Transfer all apps
    );

    var transferResponse = await client.Customers.TransferAsync(
        projectId,
        customerId,
        transferRequest
    );
    Console.WriteLine($"✓ Transferred from {transferResponse.SourceCustomerId} to {transferResponse.TargetCustomerId}");
    Console.WriteLine($"  Transferred at: {DateTimeOffset.FromUnixTimeMilliseconds(transferResponse.TransferredAt)}");
    */

    // 8. Pagination example
    Console.WriteLine("\n8. Paginating through customers...");
    var firstPage = await client.Customers.ListAsync(projectId, limit: 5);
    Console.WriteLine($"✓ First page: {firstPage.Items.Count} customers");
    
    if (firstPage.NextPage != null)
    {
        Console.WriteLine("  More pages available...");
        // To get the next page, extract the starting_after parameter from NextPage URL
        // and use it in the next request
    }

    // 9. Delete customer (cleanup)
    Console.WriteLine("\n9. Deleting customer...");
    var deleted = await client.Customers.DeleteAsync(projectId, customerId);
    Console.WriteLine($"✓ Deleted customer: {deleted.Id}");

    Console.WriteLine("\n✅ All customer management operations completed successfully!");
}
catch (RevenueCat.NET.Exceptions.RevenueCatResourceNotFoundException ex)
{
    Console.WriteLine($"\n❌ Resource not found: {ex.Message}");
}
catch (RevenueCat.NET.Exceptions.RevenueCatConflictException ex)
{
    Console.WriteLine($"\n❌ Conflict error: {ex.Message}");
}
catch (RevenueCat.NET.Exceptions.RevenueCatException ex)
{
    Console.WriteLine($"\n❌ RevenueCat API error: {ex.Message}");
    if (ex.Error != null)
    {
        Console.WriteLine($"  Error type: {ex.Error.Type}");
        Console.WriteLine($"  Retryable: {ex.Error.Retryable}");
        Console.WriteLine($"  Doc URL: {ex.Error.DocUrl}");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"\n❌ Unexpected error: {ex.Message}");
}
