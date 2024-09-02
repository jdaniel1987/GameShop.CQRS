using GameShop.Domain.ValueObjects;

namespace GameShop.Domain.UnitTests.ValueObjects;

public sealed class PriceEurosTests
{
    [Theory, AutoData]
    public void Create_ShouldInitializePriceEurosCorrectly(
        double value)
    {
        // Act
        var priceEuros = PriceEuros.Create(value);

        // Assert
        priceEuros.Value.Should().Be(value);
    }

    [Theory, AutoData]
    public void ToString_ShouldReturnValueAsString(
        double value)
    {
        // Arrange
        var priceEuros = PriceEuros.Create(value);

        // Act
        var result = priceEuros.ToString();

        // Assert
        result.Should().Be(value.ToString());
    }
}

