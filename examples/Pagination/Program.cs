using RevenueCat.NET;
using System.Web;

// Pagination Example
// This example demonstrates how to paginate through large result sets

var apiKey = Environment.GetEnvironmentVariable("REVENUECAT_API_KEY")
    ?? throw new InvalidOperationException("REVENUECAT_API_KEY environment variable is required");

var projectId = Environment.GetEnvironmentVariable("REVENUECAT_PROJECT_ID")
    ?? throw new InvalidOperationException("REVENUECAT_PROJECT_ID environment variable is required");

var client = new RevenueCatClient(apiKey);

Console.WriteLine("=== Pagination Example ===\n");

try
{
    // 1. Basic pagination - manual approach
    Console.WriteLine("1. Manual pagination through customers...");
    
    var pageSize = 5;
    var totalCustomers = 0;
    string? startingAfter = null;
    var pageNumber = 1;

    do
    {
        var page = await client.Customers.ListAsync(
            projectId,
            limit: pageSize,
            startingAfter: startingAfter
        );

        Console.WriteLine($"\n  Page {pageNumber}:");
        Console.WriteLine($"  Retrieved {page.Items.Count} customers");
        
        foreach (var customer in page.Items)
        {
            Console.WriteLine($"    - {customer.Id}");
            totalCustomers++;
        }

        // Extract starting_after from next_page URL
        if (page.NextPage != null)
        {
            var uri = new Uri(page.NextPage);
            var query = HttpUtility.ParseQueryString(uri.Query);
            startingAfter = query["starting_after"];
            pageNumber++;
        }
        else
        {
            startingAfter = null; // No more pages
        }

    } while (startingAfter != null);

    Console.WriteLine($"\n✓ Total customers retrieved: {totalCustomers}");

    // 2. Helper method for pagination
    Console.WriteLine("\n2. Using a helper method for pagination...");
    
    async IAsyncEnumerable<T> PaginateAsync<T>(
        Func<int?, string?, Task<RevenueCat.NET.Models.Common.ListResponse<T>>> fetchPage,
        int pageSize = 20)
    {
        string? startingAfter = null;
        
        do
        {
            var page = await fetchPage(pageSize, startingAfter);
            
            foreach (var item in page.Items)
            {
                yield return item;
            }

            // Extract starting_after from next_page
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
    }

    // Use the helper to iterate through all products
    var productCount = 0;
    await foreach (var product in PaginateAsync(
        (limit, startingAfter) => client.Products.ListAsync(projectId, limit: limit, startingAfter: startingAfter),
        pageSize: 10))
    {
        productCount++;
        if (productCount <= 5) // Only print first 5
        {
            Console.WriteLine($"  Product: {product.StoreIdentifier}");
        }
    }
    Console.WriteLine($"✓ Total products: {productCount}");

    // 3. Pagination with filtering
    Console.WriteLine("\n3. Pagination with filtering (production subscriptions only)...");
    
    var customerId = "example_customer_id"; // Replace with actual customer ID
    try
    {
        var subCount = 0;
        string? cursor = null;

        do
        {
            var page = await client.Subscriptions.ListSubscriptionsAsync(
                projectId,
                customerId,
                environment: "production",
                limit: 10,
                startingAfter: cursor
            );

            subCount += page.Items.Count;
            Console.WriteLine($"  Retrieved {page.Items.Count} production subscriptions");

            if (page.NextPage != null)
            {
                var uri = new Uri(page.NextPage);
                var query = HttpUtility.ParseQueryString(uri.Query);
                cursor = query["starting_after"];
            }
            else
            {
                cursor = null;
            }

        } while (cursor != null);

        Console.WriteLine($"✓ Total production subscriptions: {subCount}");
    }
    catch (RevenueCat.NET.Exceptions.RevenueCatResourceNotFoundException)
    {
        Console.WriteLine($"  Customer not found (expected for example)");
    }

    // 4. Collecting all items into a list
    Console.WriteLine("\n4. Collecting all items into a list...");
    
    async Task<List<T>> GetAllItemsAsync<T>(
        Func<int?, string?, Task<RevenueCat.NET.Models.Common.ListResponse<T>>> fetchPage,
        int pageSize = 20,
        int? maxItems = null)
    {
        var allItems = new List<T>();
        string? startingAfter = null;

        do
        {
            var page = await fetchPage(pageSize, startingAfter);
            allItems.AddRange(page.Items);

            if (maxItems.HasValue && allItems.Count >= maxItems.Value)
            {
                return allItems.Take(maxItems.Value).ToList();
            }

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

        return allItems;
    }

    var allEntitlements = await GetAllItemsAsync(
        (limit, startingAfter) => client.Entitlements.ListAsync(projectId, limit: limit, startingAfter: startingAfter),
        pageSize: 20,
        maxItems: 50 // Limit to first 50 items
    );
    
    Console.WriteLine($"✓ Collected {allEntitlements.Count} entitlements");

    // 5. Pagination with progress reporting
    Console.WriteLine("\n5. Pagination with progress reporting...");
    
    var totalPages = 0;
    var totalItems = 0;
    string? pageCursor = null;

    do
    {
        var page = await client.Offerings.ListAsync(
            projectId,
            limit: 10,
            startingAfter: pageCursor
        );

        totalPages++;
        totalItems += page.Items.Count;

        Console.WriteLine($"  Page {totalPages}: {page.Items.Count} items (total so far: {totalItems})");

        if (page.NextPage != null)
        {
            var uri = new Uri(page.NextPage);
            var query = HttpUtility.ParseQueryString(uri.Query);
            pageCursor = query["starting_after"];
        }
        else
        {
            pageCursor = null;
        }

    } while (pageCursor != null);

    Console.WriteLine($"✓ Completed: {totalPages} pages, {totalItems} total items");

    // 6. Parallel pagination (for independent resources)
    Console.WriteLine("\n6. Parallel pagination example...");
    
    var tasks = new[]
    {
        Task.Run(async () =>
        {
            var products = await client.Products.ListAsync(projectId, limit: 10);
            return ("Products", products.Items.Count);
        }),
        Task.Run(async () =>
        {
            var entitlements = await client.Entitlements.ListAsync(projectId, limit: 10);
            return ("Entitlements", entitlements.Items.Count);
        }),
        Task.Run(async () =>
        {
            var offerings = await client.Offerings.ListAsync(projectId, limit: 10);
            return ("Offerings", offerings.Items.Count);
        })
    };

    var results = await Task.WhenAll(tasks);
    
    foreach (var (resource, count) in results)
    {
        Console.WriteLine($"  {resource}: {count} items");
    }

    Console.WriteLine("\n✅ All pagination examples completed successfully!");
    
    Console.WriteLine("\nKey Concepts:");
    Console.WriteLine("  - Use 'limit' parameter to control page size (max 100)");
    Console.WriteLine("  - Use 'starting_after' from NextPage URL for next page");
    Console.WriteLine("  - Check NextPage for null to detect last page");
    Console.WriteLine("  - Consider memory usage when collecting all items");
    Console.WriteLine("  - Use async enumeration for memory-efficient iteration");
}
catch (RevenueCat.NET.Exceptions.RevenueCatException ex)
{
    Console.WriteLine($"\n❌ RevenueCat API error: {ex.Message}");
    if (ex.Error != null)
    {
        Console.WriteLine($"  Error type: {ex.Error.Type}");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"\n❌ Unexpected error: {ex.Message}");
}
