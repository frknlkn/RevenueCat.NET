using System.Text.Json;
using RevenueCat.NET.Models.Common;
using RevenueCat.NET.Models.Customers;

namespace RevenueCat.NET.Tests.Models;

public class CustomerTests
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
    };

    [Fact]
    public void Customer_Deserialize_WithAllProperties_Success()
    {
        // Arrange
        var json = """
        {
            "object": "customer",
            "id": "cust_123",
            "created_at": 1699564800000,
            "project_id": "proj_456",
            "first_seen_at": 1699564800000,
            "last_seen_at": 1699651200000,
            "last_seen_app_version": "1.2.3",
            "last_seen_country": "US",
            "last_seen_platform": "iOS",
            "last_seen_platform_version": "17.0",
            "active_entitlements": {
                "object": "list",
                "items": [
                    {
                        "object": "customer.active_entitlement",
                        "entitlement_id": "ent_789",
                        "expires_at": 1699737600000
                    }
                ],
                "url": "/customers/cust_123/active_entitlements"
            },
            "experiment": {
                "object": "experiment_enrollment",
                "id": "exp_123",
                "variant": "control"
            },
            "attributes": {
                "object": "list",
                "items": [
                    {
                        "object": "customer.attribute",
                        "key": "email",
                        "value": "test@example.com",
                        "updated_at": 1699564800000
                    }
                ],
                "url": "/customers/cust_123/attributes"
            }
        }
        """;

        // Act
        var customer = JsonSerializer.Deserialize<Customer>(json, JsonOptions);

        // Assert
        Assert.NotNull(customer);
        Assert.Equal("customer", customer.Object);
        Assert.Equal("cust_123", customer.Id);
        Assert.Equal(1699564800000, customer.CreatedAt);
        Assert.Equal("proj_456", customer.ProjectId);
        Assert.Equal(1699564800000, customer.FirstSeenAt);
        Assert.Equal(1699651200000, customer.LastSeenAt);
        Assert.Equal("1.2.3", customer.LastSeenAppVersion);
        Assert.Equal("US", customer.LastSeenCountry);
        Assert.Equal("iOS", customer.LastSeenPlatform);
        Assert.Equal("17.0", customer.LastSeenPlatformVersion);
        
        Assert.NotNull(customer.ActiveEntitlements);
        Assert.Single(customer.ActiveEntitlements.Items);
        Assert.Equal("ent_789", customer.ActiveEntitlements.Items[0].EntitlementId);
        
        Assert.NotNull(customer.Experiment);
        Assert.Equal("exp_123", customer.Experiment.Id);
        Assert.Equal("control", customer.Experiment.Variant);
        
        Assert.NotNull(customer.Attributes);
        Assert.Single(customer.Attributes.Items);
        Assert.Equal("email", customer.Attributes.Items[0].Key);
        Assert.Equal("test@example.com", customer.Attributes.Items[0].Value);
    }

    [Fact]
    public void Customer_Deserialize_WithMinimalProperties_Success()
    {
        // Arrange
        var json = """
        {
            "object": "customer",
            "id": "cust_123",
            "created_at": 1699564800000,
            "project_id": "proj_456",
            "first_seen_at": 1699564800000
        }
        """;

        // Act
        var customer = JsonSerializer.Deserialize<Customer>(json, JsonOptions);

        // Assert
        Assert.NotNull(customer);
        Assert.Equal("cust_123", customer.Id);
        Assert.Equal("proj_456", customer.ProjectId);
        Assert.Null(customer.LastSeenAt);
        Assert.Null(customer.LastSeenAppVersion);
        Assert.Null(customer.ActiveEntitlements);
        Assert.Null(customer.Experiment);
        Assert.Null(customer.Attributes);
    }

    [Fact]
    public void CustomerAlias_Deserialize_Success()
    {
        // Arrange
        var json = """
        {
            "object": "customer.alias",
            "id": "alias_123",
            "created_at": 1699564800000
        }
        """;

        // Act
        var alias = JsonSerializer.Deserialize<CustomerAlias>(json, JsonOptions);

        // Assert
        Assert.NotNull(alias);
        Assert.Equal("customer.alias", alias.Object);
        Assert.Equal("alias_123", alias.Id);
        Assert.Equal(1699564800000, alias.CreatedAt);
    }

    [Fact]
    public void CustomerAttribute_Deserialize_Success()
    {
        // Arrange
        var json = """
        {
            "object": "customer.attribute",
            "key": "email",
            "value": "test@example.com",
            "updated_at": 1699564800000
        }
        """;

        // Act
        var attribute = JsonSerializer.Deserialize<CustomerAttribute>(json, JsonOptions);

        // Assert
        Assert.NotNull(attribute);
        Assert.Equal("customer.attribute", attribute.Object);
        Assert.Equal("email", attribute.Key);
        Assert.Equal("test@example.com", attribute.Value);
        Assert.Equal(1699564800000, attribute.UpdatedAt);
    }

    [Fact]
    public void CustomerEntitlement_Deserialize_Success()
    {
        // Arrange
        var json = """
        {
            "object": "customer.active_entitlement",
            "entitlement_id": "ent_123",
            "expires_at": 1699737600000
        }
        """;

        // Act
        var entitlement = JsonSerializer.Deserialize<CustomerEntitlement>(json, JsonOptions);

        // Assert
        Assert.NotNull(entitlement);
        Assert.Equal("customer.active_entitlement", entitlement.Object);
        Assert.Equal("ent_123", entitlement.EntitlementId);
        Assert.Equal(1699737600000, entitlement.ExpiresAt);
    }

    [Fact]
    public void CustomerEntitlement_Deserialize_WithoutExpiry_Success()
    {
        // Arrange
        var json = """
        {
            "object": "customer.active_entitlement",
            "entitlement_id": "ent_123"
        }
        """;

        // Act
        var entitlement = JsonSerializer.Deserialize<CustomerEntitlement>(json, JsonOptions);

        // Assert
        Assert.NotNull(entitlement);
        Assert.Equal("ent_123", entitlement.EntitlementId);
        Assert.Null(entitlement.ExpiresAt);
    }

    [Fact]
    public void ExperimentEnrollment_Deserialize_Success()
    {
        // Arrange
        var json = """
        {
            "object": "experiment_enrollment",
            "id": "exp_123",
            "variant": "treatment_a"
        }
        """;

        // Act
        var experiment = JsonSerializer.Deserialize<ExperimentEnrollment>(json, JsonOptions);

        // Assert
        Assert.NotNull(experiment);
        Assert.Equal("experiment_enrollment", experiment.Object);
        Assert.Equal("exp_123", experiment.Id);
        Assert.Equal("treatment_a", experiment.Variant);
    }

    [Fact]
    public void Transfer_Deserialize_Success()
    {
        // Arrange
        var json = """
        {
            "object": "transfer",
            "from_customer_id": "cust_123",
            "to_customer_id": "cust_456",
            "transferred_at": 1699564800000
        }
        """;

        // Act
        var transfer = JsonSerializer.Deserialize<Transfer>(json, JsonOptions);

        // Assert
        Assert.NotNull(transfer);
        Assert.Equal("transfer", transfer.Object);
        Assert.Equal("cust_123", transfer.FromCustomerId);
        Assert.Equal("cust_456", transfer.ToCustomerId);
        Assert.Equal(1699564800000, transfer.TransferredAt);
    }

    [Fact]
    public void VirtualCurrencyBalance_Deserialize_Success()
    {
        // Arrange
        var json = """
        {
            "object": "virtual_currency_balance",
            "currency_code": "gold",
            "balance": 1000,
            "description": "Gold coins",
            "name": "Gold"
        }
        """;

        // Act
        var balance = JsonSerializer.Deserialize<VirtualCurrencyBalance>(json, JsonOptions);

        // Assert
        Assert.NotNull(balance);
        Assert.Equal("virtual_currency_balance", balance.Object);
        Assert.Equal("gold", balance.CurrencyCode);
        Assert.Equal(1000, balance.Balance);
        Assert.Equal("Gold coins", balance.Description);
        Assert.Equal("Gold", balance.Name);
    }

    [Fact]
    public void VirtualCurrencyBalance_Deserialize_WithoutOptionalFields_Success()
    {
        // Arrange
        var json = """
        {
            "object": "virtual_currency_balance",
            "currency_code": "silver",
            "balance": 500
        }
        """;

        // Act
        var balance = JsonSerializer.Deserialize<VirtualCurrencyBalance>(json, JsonOptions);

        // Assert
        Assert.NotNull(balance);
        Assert.Equal("silver", balance.CurrencyCode);
        Assert.Equal(500, balance.Balance);
        Assert.Null(balance.Description);
        Assert.Null(balance.Name);
    }
}
