using System.Globalization;

namespace Mermaid.Flowcharts.Numerical;

public readonly record struct UnitInterval
{
    public double Value { get; }

    public UnitInterval(double value)
    {
        if (double.IsNaN(value) || double.IsInfinity(value)) throw new ArgumentOutOfRangeException(nameof(value), "Unit interval must be a real number between 0 and 1.");
        ArgumentOutOfRangeException.ThrowIfLessThan(value, 0.0, "Unit interval should be at least 0.");
        ArgumentOutOfRangeException.ThrowIfGreaterThan(value, 1.0, "Unit interval should be at most 1.");

        Value = value;
    }

    public static implicit operator UnitInterval(double value)
        => new(value);

    public override string ToString()
        => Value.ToString("0.###", CultureInfo.InvariantCulture);
}