using System.Globalization;

namespace Mermaid.Flowcharts.Styling.Attributes;

public readonly record struct Opacity
{
    public double Value { get; }

    public Opacity(double value)
    {
        if (value < 0 || 1 < value) throw new ArgumentException("Opacity must be a value between 0 and 1.", nameof(value));

        Value = value;
    }

    public override string ToString()
        => Value.ToString("0.###", CultureInfo.InvariantCulture);
}