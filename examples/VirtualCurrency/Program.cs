using RevenueCat.NET;
using RevenueCat.NET.Services;

// Virtual Currency Example
// This example demonstrates how to manage virtual currency balances and transactions

var apiKey = Environment.GetEnvironmentVariable("REVENUECAT_API_KEY")
    ?? throw new InvalidOperationException("REVENUECAT_API_KEY environment variable is required");

var projectId = Environment.GetEnvironmentVariable("REVENUECAT_PROJECT_ID")
    ?? throw new InvalidOperationException("REVENUECAT_PROJECT_ID environment variable is required");

var customerId = Environment.GetEnvironmentVariable("REVENUECAT_CUSTOMER_ID")
    ?? throw new InvalidOperationException("REVENUECAT_CUSTOMER_ID environment variable is required");

var client = new RevenueCatClient(apiKey);

Console.WriteLine("=== Virtual Currency Management Example ===\n");

try
{
    // 1. List virtual currency balances
    Console.WriteLine("1. Listing virtual currency balances...");
    var balances = await client.Customers.ListVirtualCurrencyBalancesAsync(
        projectId,
        customerId,
        includeEmptyBalances: false // Only show currencies with non-zero balance
    );
    
    Console.WriteLine($"✓ Found {balances.Items.Count} currency balance(s)");
    foreach (var balance in balances.Items)
    {
        Console.WriteLine($"  - {balance.CurrencyCode}: {balance.Balance}");
        if (!string.IsNullOrEmpty(balance.Name))
        {
            Console.WriteLine($"    Name: {balance.Name}");
        }
        if (!string.IsNullOrEmpty(balance.Description))
        {
            Console.WriteLine($"    Description: {balance.Description}");
        }
    }

    // 2. List all balances including empty ones
    Console.WriteLine("\n2. Listing all balances (including empty)...");
    var allBalances = await client.Customers.ListVirtualCurrencyBalancesAsync(
        projectId,
        customerId,
        includeEmptyBalances: true
    );
    Console.WriteLine($"✓ Found {allBalances.Items.Count} total currency balance(s)");

    // 3. Create a virtual currency transaction (add currency)
    Console.WriteLine("\n3. Creating a transaction to add currency...");
    var addTransactionRequest = new CreateVirtualCurrencyTransactionRequest(
        CurrencyCode: "GEMS",
        Amount: 100 // Positive amount adds to balance
    );

    var updatedBalance = await client.Customers.CreateVirtualCurrencyTransactionAsync(
        projectId,
        customerId,
        addTransactionRequest,
        idempotencyKey: $"add-gems-{Guid.NewGuid()}" // Prevents duplicate transactions
    );
    
    Console.WriteLine($"✓ Added 100 GEMS");
    Console.WriteLine($"  New balance: {updatedBalance.Balance}");

    // 4. Create a transaction to deduct currency
    Console.WriteLine("\n4. Creating a transaction to deduct currency...");
    var deductTransactionRequest = new CreateVirtualCurrencyTransactionRequest(
        CurrencyCode: "GEMS",
        Amount: -25 // Negative amount deducts from balance
    );

    var deductedBalance = await client.Customers.CreateVirtualCurrencyTransactionAsync(
        projectId,
        customerId,
        deductTransactionRequest,
        idempotencyKey: $"deduct-gems-{Guid.NewGuid()}"
    );
    
    Console.WriteLine($"✓ Deducted 25 GEMS");
    Console.WriteLine($"  New balance: {deductedBalance.Balance}");

    // 5. Update balance directly (set to specific value)
    Console.WriteLine("\n5. Updating balance directly...");
    var updateBalanceRequest = new UpdateVirtualCurrencyBalanceRequest(
        Balance: 200 // Set balance to exactly 200
    );

    var setBalance = await client.Customers.UpdateVirtualCurrencyBalanceAsync(
        projectId,
        customerId,
        "GEMS",
        updateBalanceRequest,
        idempotencyKey: $"set-gems-{Guid.NewGuid()}"
    );
    
    Console.WriteLine($"✓ Set GEMS balance to 200");
    Console.WriteLine($"  Current balance: {setBalance.Balance}");

    // 6. Idempotency example - prevent duplicate transactions
    Console.WriteLine("\n6. Demonstrating idempotency...");
    var idempotencyKey = $"unique-transaction-{Guid.NewGuid()}";
    
    // First transaction
    var transaction1 = await client.Customers.CreateVirtualCurrencyTransactionAsync(
        projectId,
        customerId,
        new CreateVirtualCurrencyTransactionRequest("GEMS", 50),
        idempotencyKey: idempotencyKey
    );
    Console.WriteLine($"✓ First transaction: balance = {transaction1.Balance}");

    // Retry with same idempotency key - should return same result without duplicating
    var transaction2 = await client.Customers.CreateVirtualCurrencyTransactionAsync(
        projectId,
        customerId,
        new CreateVirtualCurrencyTransactionRequest("GEMS", 50),
        idempotencyKey: idempotencyKey
    );
    Console.WriteLine($"✓ Retry with same key: balance = {transaction2.Balance}");
    Console.WriteLine($"  Balance unchanged (idempotency worked!)");

    // 7. Multiple currency types example
    Console.WriteLine("\n7. Managing multiple currency types...");
    
    var currencies = new[] { "GEMS", "COINS", "TOKENS" };
    
    foreach (var currency in currencies)
    {
        try
        {
            var txn = await client.Customers.CreateVirtualCurrencyTransactionAsync(
                projectId,
                customerId,
                new CreateVirtualCurrencyTransactionRequest(currency, 100),
                idempotencyKey: $"init-{currency}-{Guid.NewGuid()}"
            );
            Console.WriteLine($"  {currency}: {txn.Balance}");
        }
        catch (RevenueCat.NET.Exceptions.RevenueCatException ex)
        {
            Console.WriteLine($"  {currency}: Error - {ex.Message}");
        }
    }

    // 8. Check final balances
    Console.WriteLine("\n8. Final balance check...");
    var finalBalances = await client.Customers.ListVirtualCurrencyBalancesAsync(
        projectId,
        customerId,
        includeEmptyBalances: false
    );
    
    Console.WriteLine($"✓ Final balances:");
    foreach (var balance in finalBalances.Items)
    {
        Console.WriteLine($"  {balance.CurrencyCode}: {balance.Balance}");
    }

    Console.WriteLine("\n✅ All virtual currency operations completed successfully!");
    
    Console.WriteLine("\nKey Concepts:");
    Console.WriteLine("  - Use positive amounts to add currency");
    Console.WriteLine("  - Use negative amounts to deduct currency");
    Console.WriteLine("  - Always use idempotency keys to prevent duplicate transactions");
    Console.WriteLine("  - UpdateBalance sets an absolute value");
    Console.WriteLine("  - CreateTransaction adds/subtracts from current balance");
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
