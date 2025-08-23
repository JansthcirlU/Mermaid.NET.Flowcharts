using System.Globalization;
using Mermaid.Flowcharts.Styling.Attributes.Base;
using Mermaid.Flowcharts.Styling.Attributes.Enums;

namespace Mermaid.Flowcharts.Styling.Attributes;

public readonly record struct Length(double Value, Unit Unit) : ICssAttribute
{
    public string ToCss()
        => $"{Value.ToString("0.###", CultureInfo.InvariantCulture)}{Unit.ToUnitString()}";
}
