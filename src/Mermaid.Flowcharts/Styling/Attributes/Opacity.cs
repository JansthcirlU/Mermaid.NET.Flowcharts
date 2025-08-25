using Mermaid.Flowcharts.Numerical;
using Mermaid.Flowcharts.Styling.Attributes.Base;

namespace Mermaid.Flowcharts.Styling.Attributes;

public readonly record struct Opacity : ICssAttribute
{
    public double Value { get; }

    public Opacity(double value)
    {
        if (double.IsNaN(value)) throw new ArgumentOutOfRangeException(nameof(value), "Opacity must be a real number between 0 and 1.");
        if (value < 0) throw new ArgumentOutOfRangeException(nameof(value), "Opacity must not be negative.");
        if (value > 1) throw new ArgumentOutOfRangeException(nameof(value), "Opacity must not be greater than 1.");

        Value = value;
    }

    public static implicit operator Opacity(double value) => new(value);

    public string ToCss()
        => Value.ToNumberString();
}