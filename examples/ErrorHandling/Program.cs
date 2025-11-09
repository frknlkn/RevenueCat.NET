using RevenueCat.NET;
using RevenueCat.NET.Exceptions;
using RevenueCat.NET.Models.Enums;

// Error Handling Example
// This example demonstrates how to handle different types of errors from the RevenueCat API

var apiKey = Environment.GetEnvironmentVariable("REVENUECAT_API_KEY")
    ?? throw new InvalidOperationException("REVENUECAT_API_KEY environment variable is required");

var projectId = Environment.GetEnvironmentVariable("REVENUECAT_PROJECT_ID")
    ?? throw new InvalidOperationException("REVENUECAT_PROJECT_ID environment variable is required");

var client = new RevenueCatClient(apiKey, options =>
{
    options.Timeout = TimeSpan.FromSeconds(30);
    options.MaxRetryAttempts = 3;
    options.EnableRetryOnRateLimit = true;
});

Console.WriteLine("=== Error Handling Example ===\n");

// 1. Handle Resource Not Found
Console.WriteLine("1. Handling Resource Not Found errors...");
try
{
    var customer = await client.Customers.GetAsync(projectId, "non_existent_customer");
}
catch (RevenueCatResourceNotFoundException ex)
{
    Console.WriteLine($"✓ Caught ResourceNotFoundException: {ex.Message}");
    Console.WriteLine($"  Status Code: {ex.StatusCode}");
    if (ex.Error != null)
    {
        Console.WriteLine($"  Error Type: {ex.Error.Type}");
        Console.WriteLine($"  Doc URL: {ex.Error.DocUrl}");
    }
}

// 2. Handle Conflict (Resource Already Exists)
Console.WriteLine("\n2. Handling Conflict errors...");
try
{
    // Try to create a customer that might already exist
    var createRequest = new RevenueCat.NET.Services.CreateCustomerRequest(
        Id: "existing_customer_id"
    );
    var customer = await client.Customers.CreateAsync(projectId, createRequest);
}
catch (RevenueCatConflictException ex)
{
    Console.WriteLine($"✓ Caught ConflictException: {ex.Message}");
    Console.WriteLine($"  This typically means the resource already exists");
    if (ex.Error != null)
    {
        Console.WriteLine($"  Error Type: {ex.Error.Type}");
    }
}
catch (RevenueCatException)
{
    // Customer doesn't exist, which is fine for this example
    Console.WriteLine($"✓ Customer doesn't exist (expected for this example)");
}

// 3. Handle Parameter Errors
Console.WriteLine("\n3. Handling Parameter errors...");
try
{
    // Try to create a product with invalid parameters
    var productRequest = new RevenueCat.NET.Services.CreateProductRequest(
        StoreIdentifier: "", // Invalid: empty string
        AppId: "invalid_app_id",
        Type: ProductType.Subscription
    );
    var product = await client.Products.CreateAsync(projectId, productRequest);
}
catch (RevenueCatParameterException ex)
{
    Console.WriteLine($"✓ Caught ParameterException: {ex.Message}");
    if (ex.Error != null)
    {
        Console.WriteLine($"  Parameter: {ex.Error.Param}");
        Console.WriteLine($"  Error Type: {ex.Error.Type}");
    }
}
catch (RevenueCatException ex)
{
    Console.WriteLine($"✓ Caught API error: {ex.Message}");
}

// 4. Handle Rate Limiting
Console.WriteLine("\n4. Handling Rate Limit errors...");
Console.WriteLine("  Note: The SDK automatically retries rate-limited requests");
Console.WriteLine("  You can configure retry behavior in RevenueCatOptions");
try
{
    // Make multiple rapid requests (may trigger rate limiting)
    for (int i = 0; i < 5; i++)
    {
        var projects = await client.Projects.ListAsync(limit: 1);
        Console.WriteLine($"  Request {i + 1} succeeded");
    }
}
catch (RevenueCatRateLimitException ex)
{
    Console.WriteLine($"✓ Caught RateLimitException: {ex.Message}");
    if (ex.Error != null && ex.Error.BackoffMs.HasValue)
    {
        Console.WriteLine($"  Retry after: {ex.Error.BackoffMs.Value}ms");
        Console.WriteLine($"  Retryable: {ex.Error.Retryable}");
    }
}

// 5. Handle Authentication Errors
Console.WriteLine("\n5. Handling Authentication errors...");
try
{
    var badClient = new RevenueCatClient("invalid_api_key");
    var projects = await badClient.Projects.ListAsync();
}
catch (RevenueCatAuthenticationException ex)
{
    Console.WriteLine($"✓ Caught AuthenticationException: {ex.Message}");
    Console.WriteLine($"  This means the API key is invalid or missing");
}

// 6. Handle Authorization Errors
Console.WriteLine("\n6. Handling Authorization errors...");
Console.WriteLine("  Authorization errors occur when the API key doesn't have permission");
Console.WriteLine("  for the requested operation");

// 7. Generic Error Handling Pattern
Console.WriteLine("\n7. Generic error handling pattern...");
try
{
    // Attempt some operation
    var customer = await client.Customers.GetAsync(projectId, "test_customer");
}
catch (RevenueCatResourceNotFoundException ex)
{
    // Handle specific error type
    Console.WriteLine($"  Resource not found: {ex.Message}");
}
catch (RevenueCatRateLimitException ex)
{
    // Handle rate limiting
    Console.WriteLine($"  Rate limited: {ex.Message}");
    if (ex.Error?.BackoffMs != null)
    {
        await Task.Delay(ex.Error.BackoffMs.Value);
        // Retry the operation
    }
}
catch (RevenueCatException ex)
{
    // Handle all other RevenueCat API errors
    Console.WriteLine($"  API error: {ex.Message}");
    
    if (ex.Error != null)
    {
        Console.WriteLine($"  Error details:");
        Console.WriteLine($"    Type: {ex.Error.Type}");
        Console.WriteLine($"    Message: {ex.Error.Message}");
        Console.WriteLine($"    Retryable: {ex.Error.Retryable}");
        Console.WriteLine($"    Doc URL: {ex.Error.DocUrl}");
        
        if (ex.Error.Param != null)
        {
            Console.WriteLine($"    Parameter: {ex.Error.Param}");
        }
        
        if (ex.Error.BackoffMs.HasValue)
        {
            Console.WriteLine($"    Backoff: {ex.Error.BackoffMs.Value}ms");
        }
    }
}
catch (Exception ex)
{
    // Handle unexpected errors
    Console.WriteLine($"  Unexpected error: {ex.Message}");
}

// 8. Retry Logic Example
Console.WriteLine("\n8. Implementing custom retry logic...");
async Task<T?> RetryOperation<T>(Func<Task<T>> operation, int maxRetries = 3)
{
    for (int attempt = 1; attempt <= maxRetries; attempt++)
    {
        try
        {
            return await operation();
        }
        catch (RevenueCatRateLimitException ex) when (attempt < maxRetries)
        {
            Console.WriteLine($"  Attempt {attempt} rate limited, retrying...");
            var delay = ex.Error?.BackoffMs ?? 1000;
            await Task.Delay(delay);
        }
        catch (RevenueCatException ex) when (ex.Error?.Retryable == true && attempt < maxRetries)
        {
            Console.WriteLine($"  Attempt {attempt} failed with retryable error, retrying...");
            await Task.Delay(1000 * attempt); // Exponential backoff
        }
    }
    return default;
}

try
{
    var projects = await RetryOperation(async () =>
    {
        return await client.Projects.ListAsync(limit: 5);
    });
    
    if (projects != null)
    {
        Console.WriteLine($"✓ Successfully retrieved {projects.Items.Count} projects with retry logic");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"  All retry attempts failed: {ex.Message}");
}

// 9. Cancellation Token Example
Console.WriteLine("\n9. Using cancellation tokens...");
try
{
    using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));
    
    var projects = await client.Projects.ListAsync(
        limit: 10,
        cancellationToken: cts.Token
    );
    
    Console.WriteLine($"✓ Operation completed before timeout");
}
catch (OperationCanceledException)
{
    Console.WriteLine($"  Operation was canceled (timeout or manual cancellation)");
}

Console.WriteLine("\n✅ Error handling examples completed!");
Console.WriteLine("\nKey Takeaways:");
Console.WriteLine("  - Always catch specific exception types first");
Console.WriteLine("  - Check the Error property for detailed information");
Console.WriteLine("  - Implement retry logic for retryable errors");
Console.WriteLine("  - Use cancellation tokens for long-running operations");
Console.WriteLine("  - The SDK automatically handles rate limiting with retries");
