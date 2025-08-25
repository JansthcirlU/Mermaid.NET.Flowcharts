using Mermaid.Flowcharts.Numerical;
using Mermaid.Flowcharts.Styling.Attributes.Enums;

namespace Mermaid.Flowcharts.Styling.Attributes.Fonts;

public abstract record FontWeight : IStyleClassComponent<FontWeight>
{
    private protected FontWeight() { }

    public sealed record RelativeFontWeight(FontWeightType Weight) : FontWeight;
    public sealed record NumericalFontWeight : FontWeight
    {
        public double Weight { get; }

        public NumericalFontWeight(double weight)
        {
            if (double.IsNaN(weight) || double.IsInfinity(weight)) throw new ArgumentOutOfRangeException(nameof(weight), "Font weight must be a real number between 1 and 1000.");
            if (weight < 1) throw new ArgumentOutOfRangeException(nameof(weight), "Font weight must be at least 1.");
            if (weight > 1000) throw new ArgumentOutOfRangeException(nameof(weight), "Font weight must be at most 1000.");

            Weight = weight;
        }
    }

    public static RelativeFontWeight Relative(FontWeightType weight) => new(weight);
    public static NumericalFontWeight Numerical(double weight) => new(weight);

    public string ToMermaidString()
        => $"font-weight:{ToSubtypeMermaidString()}";

    private string ToSubtypeMermaidString()
        => this switch
        {
            RelativeFontWeight rfw => rfw.Weight.ToFontWeightTypeString(),
            NumericalFontWeight nfw => nfw.Weight.ToNumberString()
        };
}