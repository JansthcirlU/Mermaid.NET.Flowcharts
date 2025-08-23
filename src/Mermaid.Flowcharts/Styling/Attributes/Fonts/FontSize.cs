using Mermaid.Flowcharts.Numerical;
using Mermaid.Flowcharts.Styling.Attributes.Enums;

namespace Mermaid.Flowcharts.Styling.Attributes.Fonts;

public abstract record FontSize : IMermaidStyle
{
    private protected FontSize() { }

    public sealed record AbsoluteFontSize(AbsoluteSize Size) : FontSize;
    public sealed record RelativeFontSize(RelativeSize Size) : FontSize;
    public sealed record PercentageFontSize(Percentage SizePercentage) : FontSize;
    public sealed record LengthFontSize : FontSize
    {
        public Length SizeLength { get; }

        public LengthFontSize(double size, Unit unit)
        {
            if (double.IsNaN(size) || double.IsInfinity(size)) throw new ArgumentOutOfRangeException(nameof(size), "Font size must be a real and finite number.");

            SizeLength = new(size, unit);
        }
    }

    public static AbsoluteFontSize Absolute(AbsoluteSize size) => new(size);
    public static RelativeFontSize Relative(RelativeSize size) => new(size);
    public static PercentageFontSize Percentage(Percentage sizePercentage) => new(sizePercentage);
    public static LengthFontSize Length(double size, Unit unit) => new(size, unit);

    public string ToMermaidString()
        => $"font-size:{ToSubtypeMermaidString()}";

    private string ToSubtypeMermaidString()
        => this switch
        {
            AbsoluteFontSize afs => afs.Size.ToAbsoluteSizeString(),
            RelativeFontSize rfs => rfs.Size.ToRelativeSizeString(),
            PercentageFontSize pfs => pfs.SizePercentage.ToNumericalString(),
            LengthFontSize lfs => lfs.SizeLength.ToCss()
        };
}