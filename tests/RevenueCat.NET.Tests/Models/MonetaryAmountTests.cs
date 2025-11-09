using System.Text.Json;
using RevenueCat.NET.Models;

namespace RevenueCat.NET.Tests.Models;

public class MonetaryAmountTests
{
    [Fact]
    public void MonetaryAmount_Deserialize_AllFields_Success()
    {
        // Arrange
        var json = """
        {
            "currency": "USD",
            "gross": 9.99,
            "commission": 2.99,
            "tax": 0.50,
            "proceeds": 6.50
        }
        """;

        // Act
        var amount = JsonSerializer.Deserialize<MonetaryAmount>(json, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
        });

        // Assert
        Assert.NotNull(amount);
        Assert.Equal("USD", amount.Currency);
        Assert.Equal(9.99m, amount.Gross);
        Assert.Equal(2.99m, amount.Commission);
        Assert.Equal(0.50m, amount.Tax);
        Assert.Equal(6.50m, amount.Proceeds);
    }

    [Fact]
    public void MonetaryAmount_Deserialize_WithZeroValues_Success()
    {
        // Arrange
        var json = """
        {
            "currency": "EUR",
            "gross": 0.00,
            "commission": 0.00,
            "tax": 0.00,
            "proceeds": 0.00
        }
        """;

        // Act
        var amount = JsonSerializer.Deserialize<MonetaryAmount>(json, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
        });

        // Assert
        Assert.NotNull(amount);
        Assert.Equal("EUR", amount.Currency);
        Assert.Equal(0.00m, amount.Gross);
        Assert.Equal(0.00m, amount.Commission);
        Assert.Equal(0.00m, amount.Tax);
        Assert.Equal(0.00m, amount.Proceeds);
    }

    [Fact]
    public void MonetaryAmount_Deserialize_WithLargeValues_Success()
    {
        // Arrange
        var json = """
        {
            "currency": "JPY",
            "gross": 999999.99,
            "commission": 150000.00,
            "tax": 99999.99,
            "proceeds": 750000.00
        }
        """;

        // Act
        var amount = JsonSerializer.Deserialize<MonetaryAmount>(json, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
        });

        // Assert
        Assert.NotNull(amount);
        Assert.Equal("JPY", amount.Currency);
        Assert.Equal(999999.99m, amount.Gross);
        Assert.Equal(150000.00m, amount.Commission);
        Assert.Equal(99999.99m, amount.Tax);
        Assert.Equal(750000.00m, amount.Proceeds);
    }

    [Fact]
    public void MonetaryAmount_Serialize_ProducesCorrectJson()
    {
        // Arrange
        var amount = new MonetaryAmount
        {
            Currency = "USD",
            Gross = 19.99m,
            Commission = 5.99m,
            Tax = 1.00m,
            Proceeds = 13.00m
        };

        // Act
        var json = JsonSerializer.Serialize(amount, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
        });

        // Assert
        Assert.Contains("\"currency\":\"USD\"", json);
        Assert.Contains("\"gross\":19.99", json);
        Assert.Contains("\"commission\":5.99", json);
        Assert.Contains("\"tax\":1", json);
        Assert.Contains("\"proceeds\":13", json);
    }

    [Fact]
    public void MonetaryAmount_DefaultValues_AreCorrect()
    {
        // Arrange & Act
        var amount = new MonetaryAmount();

        // Assert
        Assert.Equal(string.Empty, amount.Currency);
        Assert.Equal(0m, amount.Gross);
        Assert.Equal(0m, amount.Commission);
        Assert.Equal(0m, amount.Tax);
        Assert.Equal(0m, amount.Proceeds);
    }

    [Fact]
    public void MonetaryAmount_Deserialize_WithNegativeValues_Success()
    {
        // Arrange - negative values can occur in refund scenarios
        var json = """
        {
            "currency": "USD",
            "gross": -9.99,
            "commission": -2.99,
            "tax": -0.50,
            "proceeds": -6.50
        }
        """;

        // Act
        var amount = JsonSerializer.Deserialize<MonetaryAmount>(json, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
        });

        // Assert
        Assert.NotNull(amount);
        Assert.Equal("USD", amount.Currency);
        Assert.Equal(-9.99m, amount.Gross);
        Assert.Equal(-2.99m, amount.Commission);
        Assert.Equal(-0.50m, amount.Tax);
        Assert.Equal(-6.50m, amount.Proceeds);
    }

    [Fact]
    public void MonetaryAmount_Deserialize_WithPrecision_Success()
    {
        // Arrange - test decimal precision
        var json = """
        {
            "currency": "USD",
            "gross": 9.999999,
            "commission": 2.123456,
            "tax": 0.505050,
            "proceeds": 7.371493
        }
        """;

        // Act
        var amount = JsonSerializer.Deserialize<MonetaryAmount>(json, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
        });

        // Assert
        Assert.NotNull(amount);
        Assert.Equal(9.999999m, amount.Gross);
        Assert.Equal(2.123456m, amount.Commission);
        Assert.Equal(0.505050m, amount.Tax);
        Assert.Equal(7.371493m, amount.Proceeds);
    }
}
