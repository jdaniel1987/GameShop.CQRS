using GameShop.Domain.ValueObjects;

namespace GameShop.Domain.UnitTests.ValueObjects;

public sealed class PriceTests
{
    [Theory, AutoData]
    public void Create_ShouldInitializePriceCorrectly(
        double expectedValue)
    {
        // Arrange

        // Act
        var price = Price.Create(expectedValue);

        // Assert
        expectedValue.Should().Be(price.Value);
    }

    [Theory, AutoData]
    public void ImplicitConversionToPriceEuros_ShouldConvertCorrectly(
        double usdValue)
    {
        // Arrange
        var price = Price.Create(usdValue);
        var expectedEurosValue = usdValue * 1.1;

        // Act
        PriceEuros priceEuros = price;

        // Assert
        expectedEurosValue.Should().Be(priceEuros.Value);
    }

    [Theory, AutoData]
    public void ToString_ShouldReturnValueAsString(
        double value)
    {
        // Arrange
        var price = Price.Create(value);
        var expectedString = value.ToString();

        // Act
        var result = price.ToString();

        // Assert
        expectedString.Should().Be(result);
    }
}

