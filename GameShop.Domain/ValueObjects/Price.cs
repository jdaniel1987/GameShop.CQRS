using System.Text.Json.Serialization;

namespace GameShop.Domain.ValueObjects;

public record Price
{
    public double Value { get; init; }

    [JsonConstructor]
    internal Price(double value)
    {
        Value = value;
    }

    public static Price Create(double value) =>
        new(value);

    public static implicit operator PriceEuros(Price usd) =>
        PriceEuros.Create(usd.Value * 1.1);

    public override string ToString() => Value.ToString();
}
