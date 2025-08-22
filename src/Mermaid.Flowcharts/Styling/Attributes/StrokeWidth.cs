using Mermaid.Flowcharts.Numerical;

namespace Mermaid.Flowcharts.Styling.Attributes;

public abstract record StrokeWidth : IMermaidStyle
{
    private protected StrokeWidth() { }

    public sealed record LengthStrokeWidth(Length LengthWidth) : StrokeWidth;
    public sealed record PercentageStrokeWidth(Percentage PercentageWidth) : StrokeWidth;

    public static LengthStrokeWidth Length(Length lengthWidth) => new(lengthWidth);
    public static PercentageStrokeWidth Percentage(Percentage percentageWidth) => new(percentageWidth);

    public string ToMermaidString()
        => $"stroke-width:{ToSubtypeMermaidString()}";

    private string ToSubtypeMermaidString()
        => this switch
        {
            LengthStrokeWidth lsw => lsw.LengthWidth.ToString(),
            PercentageStrokeWidth psw => psw.PercentageWidth.ToString()
        };
}