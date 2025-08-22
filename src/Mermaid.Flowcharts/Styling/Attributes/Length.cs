using System.Globalization;
using Mermaid.Flowcharts.Styling.Attributes.Enums;

namespace Mermaid.Flowcharts.Styling.Attributes;

public readonly record struct Length(double Value, Unit Unit)
{
    public override string ToString()
        => $"{Value.ToString("0.###", CultureInfo.InvariantCulture)}{Unit.ToUnitString()}";
}
