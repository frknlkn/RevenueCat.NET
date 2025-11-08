using RevenueCat.NET;
using RevenueCat.NET.Models;
using RevenueCat.NET.Services;

var apiKey = Environment.GetEnvironmentVariable("REVENUECAT_API_KEY") 
    ?? throw new InvalidOperationException("REVENUECAT_API_KEY environment variable is required");

var client = new RevenueCatClient(apiKey, options =>
{
    options.Timeout = TimeSpan.FromSeconds(30);
    options.MaxRetryAttempts = 3;
    options.EnableRetryOnRateLimit = true;
});

try
{
    Console.WriteLine("=== RevenueCat.NET Example ===\n");

    Console.WriteLine("Fetching projects...");
    var projects = await client.Projects.ListAsync(limit: 10);
    Console.WriteLine($"Found {projects.Items.Count} projects");
    
    if (projects.Items.Count > 0)
    {
        var projectId = projects.Items[0].Id;
        Console.WriteLine($"\nUsing project: {projects.Items[0].Name} ({projectId})");

        Console.WriteLine("\nFetching apps...");
        var apps = await client.Apps.ListAsync(projectId, limit: 5);
        Console.WriteLine($"Found {apps.Items.Count} apps");

        Console.WriteLine("\nFetching customers...");
        var customers = await client.Customers.ListAsync(projectId, limit: 5);
        Console.WriteLine($"Found {customers.Items.Count} customers");

        Console.WriteLine("\nFetching products...");
        var products = await client.Products.ListAsync(projectId, limit: 5);
        Console.WriteLine($"Found {products.Items.Count} products");

        Console.WriteLine("\nFetching entitlements...");
        var entitlements = await client.Entitlements.ListAsync(projectId, limit: 5);
        Console.WriteLine($"Found {entitlements.Items.Count} entitlements");
    }

    Console.WriteLine("\n✅ All operations completed successfully!");
}
catch (Exception ex)
{
    Console.WriteLine($"\n❌ Error: {ex.Message}");
    if (ex is RevenueCat.NET.Exceptions.RevenueCatException rcEx && rcEx.ErrorResponse != null)
    {
        Console.WriteLine($"Error Type: {rcEx.ErrorResponse.Type}");
        Console.WriteLine($"Retryable: {rcEx.ErrorResponse.Retryable}");
    }
}
