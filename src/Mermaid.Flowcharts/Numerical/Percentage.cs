using System.Globalization;

namespace Mermaid.Flowcharts.Numerical;

public readonly record struct Percentage
{
    public double Value { get; }

    public Percentage(double value)
    {
        if (double.IsNaN(value) || double.IsInfinity(value)) throw new ArgumentOutOfRangeException(nameof(value), "Percentage must be a real and finite number.");
        ArgumentOutOfRangeException.ThrowIfLessThan(value, 0.0, "Percentage should be at least 0.");

        Value = value;
    }

    public static implicit operator Percentage(double value)
        => new(value);

    public override string ToString()
        => Value.ToString("0.###", CultureInfo.InvariantCulture);
}
