using System.Globalization;

namespace Mermaid.Flowcharts.Styling.Attributes.Fonts;

public abstract record FontWeight : IMermaidStyle
{
    private protected FontWeight() { }

    public sealed record RelativeFontWeight(Enums.FontWeight Weight) : FontWeight;
    public sealed record NumericalFontWeight(double Weight) : FontWeight;

    public static RelativeFontWeight Relative(Enums.FontWeight weight) => new(weight);
    public static NumericalFontWeight Numerical(double weight) => new(weight);

    public string ToMermaidString()
        => $"font-weight:{ToSubtypeMermaidString()}";

    private string ToSubtypeMermaidString()
        => this switch
        {
            RelativeFontWeight rfw => rfw.Weight.ToFontWeightString(),
            NumericalFontWeight nfw => nfw.Weight.ToString("0.###", CultureInfo.InvariantCulture)
        };
}