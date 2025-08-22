using Mermaid.Flowcharts.Numerical;
using Mermaid.Flowcharts.Styling.Attributes.Enums;

namespace Mermaid.Flowcharts.Styling.Attributes.Fonts;

public abstract record FontSize : IMermaidStyle
{
    private protected FontSize() { }

    public sealed record AbsoluteFontSize(AbsoluteSize Size) : FontSize;
    public sealed record RelativeFontSize(RelativeSize Size) : FontSize;
    public sealed record PercentageFontSize(Percentage SizePercentage) : FontSize;
    public sealed record LengthFontSize(Length SizeLength) : FontSize;

    public static AbsoluteFontSize Absolute(AbsoluteSize size) => new(size);
    public static RelativeFontSize Relative(RelativeSize size) => new(size);
    public static PercentageFontSize Percentage(double sizePercentage) => new(sizePercentage);
    public static LengthFontSize Length(Length sizeLength) => new(sizeLength);

    public string ToMermaidString()
        => $"font-size:{ToSubtypeMermaidString()}";

    private string ToSubtypeMermaidString()
        => this switch
        {
            AbsoluteFontSize afs => afs.Size.ToAbsoluteSizeString(),
            RelativeFontSize rfs => rfs.Size.ToRelativeSizeString(),
            PercentageFontSize pfs => pfs.SizePercentage.ToString(),
            LengthFontSize lfs => lfs.SizeLength.ToString()
        };
}