namespace GameShop.Domain.ValueObjects;

public record PriceEuros
{
    public double Value { get; init; }

    internal PriceEuros(double value)
    {
        Value = value;
    }

    public static PriceEuros Create(double value) =>
        new(value);

    public override string ToString() => Value.ToString();
}
