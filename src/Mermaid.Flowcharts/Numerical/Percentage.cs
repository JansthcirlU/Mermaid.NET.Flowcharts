namespace Mermaid.Flowcharts.Numerical;

public readonly record struct Percentage : INumerical
{
    public double Value { get; }

    public Percentage(double value)
    {
        if (double.IsNaN(value) || double.IsInfinity(value)) throw new ArgumentOutOfRangeException(nameof(value), "Percentage must be a real and finite number.");
        ArgumentOutOfRangeException.ThrowIfLessThan(value, 0.0, "Percentage must not be negative.");

        Value = value;
    }

    public static implicit operator Percentage(double value)
        => new(value);

    public string ToNumericalString()
        => $"{Value.ToNumberString()}%";
}
