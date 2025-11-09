using RevenueCat.NET;
using RevenueCat.NET.Models.Enums;
using RevenueCat.NET.Services;

// Product Catalog Management Example
// This example demonstrates how to manage products, entitlements, offerings, and packages

var apiKey = Environment.GetEnvironmentVariable("REVENUECAT_API_KEY")
    ?? throw new InvalidOperationException("REVENUECAT_API_KEY environment variable is required");

var projectId = Environment.GetEnvironmentVariable("REVENUECAT_PROJECT_ID")
    ?? throw new InvalidOperationException("REVENUECAT_PROJECT_ID environment variable is required");

var appId = Environment.GetEnvironmentVariable("REVENUECAT_APP_ID")
    ?? throw new InvalidOperationException("REVENUECAT_APP_ID environment variable is required");

var client = new RevenueCatClient(apiKey);

Console.WriteLine("=== Product Catalog Management Example ===\n");

try
{
    // ===== PRODUCTS =====
    Console.WriteLine("--- PRODUCTS ---\n");

    // 1. Create a product
    Console.WriteLine("1. Creating a new product...");
    var productRequest = new CreateProductRequest(
        StoreIdentifier: "com.example.premium.monthly",
        AppId: appId,
        Type: ProductType.Subscription,
        DisplayName: "Premium Monthly"
    );

    var product = await client.Products.CreateAsync(projectId, productRequest);
    Console.WriteLine($"✓ Created product: {product.Id}");
    Console.WriteLine($"  Store identifier: {product.StoreIdentifier}");
    Console.WriteLine($"  Type: {product.Type}");
    Console.WriteLine($"  Display name: {product.DisplayName}");

    // 2. List products
    Console.WriteLine("\n2. Listing products...");
    var products = await client.Products.ListAsync(
        projectId,
        appId: appId,
        limit: 20
    );
    Console.WriteLine($"✓ Found {products.Items.Count} product(s)");

    // 3. Get product with expanded app field
    Console.WriteLine($"\n3. Getting product with expanded app...");
    var productDetails = await client.Products.GetAsync(
        projectId,
        product.Id,
        expand: new[] { "app" }
    );
    Console.WriteLine($"✓ Retrieved product: {productDetails.Id}");
    if (productDetails.App != null)
    {
        Console.WriteLine($"  App name: {productDetails.App.Name}");
        Console.WriteLine($"  App type: {productDetails.App.Type}");
    }

    // ===== ENTITLEMENTS =====
    Console.WriteLine("\n--- ENTITLEMENTS ---\n");

    // 4. Create an entitlement
    Console.WriteLine("4. Creating an entitlement...");
    var entitlementRequest = new CreateEntitlementRequest(
        LookupKey: "premium",
        DisplayName: "Premium Access"
    );

    var entitlement = await client.Entitlements.CreateAsync(projectId, entitlementRequest);
    Console.WriteLine($"✓ Created entitlement: {entitlement.Id}");
    Console.WriteLine($"  Lookup key: {entitlement.LookupKey}");
    Console.WriteLine($"  Display name: {entitlement.DisplayName}");

    // 5. Attach products to entitlement
    Console.WriteLine($"\n5. Attaching products to entitlement...");
    var attachRequest = new AttachProductsRequest(
        ProductIds: new[] { product.Id }
    );

    var updatedEntitlement = await client.Entitlements.AttachProductsAsync(
        projectId,
        entitlement.Id,
        attachRequest
    );
    Console.WriteLine($"✓ Attached products to entitlement");

    // 6. List products in entitlement
    Console.WriteLine($"\n6. Listing products in entitlement...");
    var entitlementProducts = await client.Entitlements.GetProductsAsync(
        projectId,
        entitlement.Id
    );
    Console.WriteLine($"✓ Entitlement has {entitlementProducts.Items.Count} product(s)");

    // 7. List all entitlements
    Console.WriteLine($"\n7. Listing all entitlements...");
    var entitlements = await client.Entitlements.ListAsync(projectId, limit: 20);
    Console.WriteLine($"✓ Found {entitlements.Items.Count} entitlement(s)");

    // ===== OFFERINGS =====
    Console.WriteLine("\n--- OFFERINGS ---\n");

    // 8. Create an offering
    Console.WriteLine("8. Creating an offering...");
    var offeringRequest = new CreateOfferingRequest(
        LookupKey: "default",
        DisplayName: "Default Offering",
        IsCurrent: true,
        Metadata: new Dictionary<string, object>
        {
            { "description", "Our standard offering" },
            { "priority", 1 }
        }
    );

    var offering = await client.Offerings.CreateAsync(projectId, offeringRequest);
    Console.WriteLine($"✓ Created offering: {offering.Id}");
    Console.WriteLine($"  Lookup key: {offering.LookupKey}");
    Console.WriteLine($"  Is current: {offering.IsCurrent}");
    if (offering.Metadata != null)
    {
        Console.WriteLine($"  Metadata: {offering.Metadata.Count} item(s)");
    }

    // 9. List offerings
    Console.WriteLine($"\n9. Listing offerings...");
    var offerings = await client.Offerings.ListAsync(projectId, limit: 20);
    Console.WriteLine($"✓ Found {offerings.Items.Count} offering(s)");

    // 10. Update offering
    Console.WriteLine($"\n10. Updating offering...");
    var updateOfferingRequest = new UpdateOfferingRequest(
        DisplayName: "Updated Default Offering",
        Metadata: new Dictionary<string, object>
        {
            { "description", "Our updated standard offering" },
            { "priority", 2 },
            { "featured", true }
        }
    );

    var updatedOffering = await client.Offerings.UpdateAsync(
        projectId,
        offering.Id,
        updateOfferingRequest
    );
    Console.WriteLine($"✓ Updated offering: {updatedOffering.DisplayName}");

    // ===== PACKAGES =====
    Console.WriteLine("\n--- PACKAGES ---\n");

    // 11. Create a package
    Console.WriteLine("11. Creating a package...");
    var packageRequest = new CreatePackageRequest(
        LookupKey: "monthly",
        DisplayName: "Monthly Package",
        Position: 1
    );

    var package = await client.Packages.CreateAsync(projectId, offering.Id, packageRequest);
    Console.WriteLine($"✓ Created package: {package.Id}");
    Console.WriteLine($"  Lookup key: {package.LookupKey}");
    Console.WriteLine($"  Position: {package.Position}");

    // 12. Attach products to package with eligibility criteria
    Console.WriteLine($"\n12. Attaching products to package...");
    var attachPackageRequest = new AttachPackageProductsRequest(
        Products: new[]
        {
            new PackageProductInput(
                ProductId: product.Id,
                EligibilityCriteria: EligibilityCriteria.All
            )
        }
    );

    var updatedPackage = await client.Packages.AttachProductsAsync(
        projectId,
        package.Id,
        attachPackageRequest
    );
    Console.WriteLine($"✓ Attached products to package");

    // 13. List packages in offering
    Console.WriteLine($"\n13. Listing packages in offering...");
    var packages = await client.Packages.ListAsync(
        projectId,
        offeringId: offering.Id,
        limit: 20
    );
    Console.WriteLine($"✓ Found {packages.Items.Count} package(s)");

    // 14. Get package with expanded products
    Console.WriteLine($"\n14. Getting package with expanded products...");
    var packageDetails = await client.Packages.GetAsync(
        projectId,
        package.Id,
        expand: new[] { "products.product" }
    );
    Console.WriteLine($"✓ Retrieved package: {packageDetails.Id}");
    if (packageDetails.Products?.Items != null)
    {
        Console.WriteLine($"  Products: {packageDetails.Products.Items.Count}");
    }

    // ===== CLEANUP =====
    Console.WriteLine("\n--- CLEANUP ---\n");

    // 15. Delete resources (commented out to avoid accidental deletion)
    /*
    Console.WriteLine("15. Cleaning up resources...");
    
    await client.Packages.DeleteAsync(projectId, package.Id);
    Console.WriteLine($"✓ Deleted package");

    await client.Offerings.DeleteAsync(projectId, offering.Id);
    Console.WriteLine($"✓ Deleted offering");

    await client.Entitlements.DetachProductsAsync(
        projectId,
        entitlement.Id,
        new DetachProductsRequest(new[] { product.Id })
    );
    await client.Entitlements.DeleteAsync(projectId, entitlement.Id);
    Console.WriteLine($"✓ Deleted entitlement");

    await client.Products.DeleteAsync(projectId, product.Id);
    Console.WriteLine($"✓ Deleted product");
    */

    Console.WriteLine("\n✅ All product catalog operations completed successfully!");
}
catch (RevenueCat.NET.Exceptions.RevenueCatConflictException ex)
{
    Console.WriteLine($"\n❌ Conflict error (resource may already exist): {ex.Message}");
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
