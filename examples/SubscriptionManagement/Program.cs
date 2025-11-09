using RevenueCat.NET;
using RevenueCat.NET.Models.Enums;

// Subscription Management Example
// This example demonstrates how to list, search, cancel, and refund subscriptions

var apiKey = Environment.GetEnvironmentVariable("REVENUECAT_API_KEY")
    ?? throw new InvalidOperationException("REVENUECAT_API_KEY environment variable is required");

var projectId = Environment.GetEnvironmentVariable("REVENUECAT_PROJECT_ID")
    ?? throw new InvalidOperationException("REVENUECAT_PROJECT_ID environment variable is required");

var customerId = Environment.GetEnvironmentVariable("REVENUECAT_CUSTOMER_ID")
    ?? throw new InvalidOperationException("REVENUECAT_CUSTOMER_ID environment variable is required");

var client = new RevenueCatClient(apiKey);

Console.WriteLine("=== Subscription Management Example ===\n");

try
{
    // 1. List all subscriptions for a customer
    Console.WriteLine("1. Listing customer subscriptions...");
    var subscriptions = await client.Subscriptions.ListSubscriptionsAsync(
        projectId,
        customerId,
        limit: 20
    );
    
    Console.WriteLine($"✓ Found {subscriptions.Items.Count} subscription(s)");
    foreach (var sub in subscriptions.Items)
    {
        Console.WriteLine($"  - ID: {sub.Id}");
        Console.WriteLine($"    Status: {sub.Status}");
        Console.WriteLine($"    Auto-renewal: {sub.AutoRenewalStatus}");
        Console.WriteLine($"    Store: {sub.Store}");
        Console.WriteLine($"    Gives access: {sub.GivesAccess}");
        Console.WriteLine($"    Started: {DateTimeOffset.FromUnixTimeMilliseconds(sub.StartsAt)}");
        
        if (sub.CurrentPeriodEndsAt.HasValue)
        {
            Console.WriteLine($"    Current period ends: {DateTimeOffset.FromUnixTimeMilliseconds(sub.CurrentPeriodEndsAt.Value)}");
        }
        
        Console.WriteLine($"    Total revenue: {sub.TotalRevenueInUsd.Currency} {sub.TotalRevenueInUsd.Gross}");
        Console.WriteLine();
    }

    // 2. Filter subscriptions by environment
    Console.WriteLine("2. Listing production subscriptions only...");
    var productionSubs = await client.Subscriptions.ListSubscriptionsAsync(
        projectId,
        customerId,
        environment: "production"
    );
    Console.WriteLine($"✓ Found {productionSubs.Items.Count} production subscription(s)");

    // 3. Get a specific subscription
    if (subscriptions.Items.Count > 0)
    {
        var subscriptionId = subscriptions.Items[0].Id;
        
        Console.WriteLine($"\n3. Getting subscription details for {subscriptionId}...");
        var subscription = await client.Subscriptions.GetSubscriptionAsync(
            projectId,
            subscriptionId
        );
        
        Console.WriteLine($"✓ Retrieved subscription: {subscription.Id}");
        Console.WriteLine($"  Product ID: {subscription.ProductId}");
        Console.WriteLine($"  Environment: {subscription.Environment}");
        Console.WriteLine($"  Ownership: {subscription.Ownership}");
        
        if (subscription.PendingChanges != null)
        {
            Console.WriteLine($"  Pending changes: {subscription.PendingChanges.ProductId}");
        }

        // 4. List entitlements for the subscription
        Console.WriteLine($"\n4. Listing entitlements for subscription...");
        var entitlements = await client.Subscriptions.ListSubscriptionEntitlementsAsync(
            projectId,
            subscriptionId
        );
        Console.WriteLine($"✓ Subscription has {entitlements.Items.Count} entitlement(s)");
        foreach (var ent in entitlements.Items)
        {
            Console.WriteLine($"  - {ent.DisplayName} ({ent.LookupKey})");
        }

        // 5. Get authenticated management URL (for customer portal)
        Console.WriteLine($"\n5. Getting authenticated management URL...");
        try
        {
            var managementUrl = await client.Subscriptions.GetAuthenticatedManagementUrlAsync(
                projectId,
                subscriptionId
            );
            Console.WriteLine($"✓ Management URL: {managementUrl.Url}");
            Console.WriteLine($"  Expires at: {DateTimeOffset.FromUnixTimeMilliseconds(managementUrl.ExpiresAt)}");
        }
        catch (RevenueCat.NET.Exceptions.RevenueCatException ex)
        {
            Console.WriteLine($"  Note: Management URL not available for this subscription type");
        }

        // 6. Get subscription transactions (for Play Store subscriptions)
        Console.WriteLine($"\n6. Getting subscription transactions...");
        try
        {
            var transactions = await client.Subscriptions.GetSubscriptionTransactionsAsync(
                projectId,
                subscriptionId
            );
            Console.WriteLine($"✓ Found {transactions.Items.Count} transaction(s)");
            foreach (var txn in transactions.Items)
            {
                Console.WriteLine($"  - Transaction ID: {txn.Id}");
                Console.WriteLine($"    Purchased at: {DateTimeOffset.FromUnixTimeMilliseconds(txn.PurchasedAt)}");
            }
        }
        catch (RevenueCat.NET.Exceptions.RevenueCatException ex)
        {
            Console.WriteLine($"  Note: Transactions not available for this subscription type");
        }

        // 7. Cancel subscription (Web Billing only - commented out to avoid actual cancellation)
        /*
        Console.WriteLine($"\n7. Canceling subscription...");
        var canceledSub = await client.Subscriptions.CancelSubscriptionAsync(
            projectId,
            subscriptionId
        );
        Console.WriteLine($"✓ Subscription canceled: {canceledSub.Id}");
        Console.WriteLine($"  New status: {canceledSub.Status}");
        Console.WriteLine($"  Auto-renewal: {canceledSub.AutoRenewalStatus}");
        */

        // 8. Refund subscription (Web Billing only - commented out to avoid actual refund)
        /*
        Console.WriteLine($"\n8. Refunding subscription...");
        var refundedSub = await client.Subscriptions.RefundSubscriptionAsync(
            projectId,
            subscriptionId
        );
        Console.WriteLine($"✓ Subscription refunded: {refundedSub.Id}");
        */
    }

    // 9. Search subscriptions by store identifier
    Console.WriteLine($"\n9. Searching subscriptions by store identifier...");
    var storeIdentifier = "example_store_sub_id"; // Replace with actual store subscription ID
    try
    {
        var searchResults = await client.Subscriptions.SearchSubscriptionsAsync(
            projectId,
            storeIdentifier
        );
        Console.WriteLine($"✓ Found {searchResults.Items.Count} subscription(s) matching store identifier");
    }
    catch (RevenueCat.NET.Exceptions.RevenueCatResourceNotFoundException)
    {
        Console.WriteLine($"  No subscriptions found with that store identifier");
    }

    // 10. Pagination example
    Console.WriteLine($"\n10. Paginating through subscriptions...");
    var page1 = await client.Subscriptions.ListSubscriptionsAsync(
        projectId,
        customerId,
        limit: 5
    );
    Console.WriteLine($"✓ Page 1: {page1.Items.Count} subscriptions");
    
    if (page1.NextPage != null)
    {
        Console.WriteLine($"  Next page available: {page1.NextPage}");
        // Extract starting_after from NextPage URL and use it for the next request
    }

    Console.WriteLine("\n✅ All subscription management operations completed successfully!");
}
catch (RevenueCat.NET.Exceptions.RevenueCatResourceNotFoundException ex)
{
    Console.WriteLine($"\n❌ Resource not found: {ex.Message}");
}
catch (RevenueCat.NET.Exceptions.RevenueCatException ex)
{
    Console.WriteLine($"\n❌ RevenueCat API error: {ex.Message}");
    if (ex.Error != null)
    {
        Console.WriteLine($"  Error type: {ex.Error.Type}");
        Console.WriteLine($"  Retryable: {ex.Error.Retryable}");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"\n❌ Unexpected error: {ex.Message}");
}
