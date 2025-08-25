using Mermaid.Flowcharts.Numerical;
using Mermaid.Flowcharts.Styling.Attributes.Base;
using Mermaid.Flowcharts.Styling.Attributes.Enums;

namespace Mermaid.Flowcharts.Styling.Attributes;

public readonly record struct Length : ICssAttribute
{
    public double Value { get; }
    public Unit Unit { get; }

    public Length(double value, Unit unit)
    {
        if (double.IsNaN(value))
        {
            throw new ArgumentOutOfRangeException(nameof(value), "Length must be a real number.");
        }

        Value = value;
        Unit = unit;
    }

    public string ToCss()
        => $"{Value.ToNumberString()}{Unit.ToUnitString()}";
}
