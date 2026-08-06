namespace Mermaid.Flowcharts.Numerical;

public readonly record struct Percentage : INumerical
{
    public double Value { get; }

    [Obsolete(error: true, message: $"Please use the factory methods instead of the default constructor to create a new {nameof(Percentage)}.")]
#pragma warning disable CS8618 // This constructor is never used
    public Percentage() { }
#pragma warning restore CS8618
    private Percentage(double value)
    {
        Value = value;
    }

    public static Percentage FromDouble(double value)
    {
        if (double.IsNaN(value) || double.IsInfinity(value))
        {
            throw new ArgumentOutOfRangeException(nameof(value), "Percentage must be a real and finite number.");
        }

        ArgumentOutOfRangeException.ThrowIfLessThan(value, 0.0, "Percentage must not be negative.");
        
        return new(value);
    }

    public string ToNumericalString()
        => $"{Value.ToNumberString()}%";
}
