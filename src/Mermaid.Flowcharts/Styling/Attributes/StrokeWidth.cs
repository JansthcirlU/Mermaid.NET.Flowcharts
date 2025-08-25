using Mermaid.Flowcharts.Numerical;
using Mermaid.Flowcharts.Styling.Attributes.Enums;

namespace Mermaid.Flowcharts.Styling.Attributes;

public abstract record StrokeWidth : IStyleClassComponent<StrokeWidth>
{
    private protected StrokeWidth() { }

    public sealed record LengthStrokeWidth : StrokeWidth
    {
        public Length Width { get; }

        public LengthStrokeWidth(double width, Unit unit)
        {
            if (width < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(width), "Stroke width must not be negative.");
            }

            Width = new(width, unit);
        }
    }

    public sealed record PercentageStrokeWidth(Percentage PercentageWidth) : StrokeWidth;

    public sealed record NumericalStrokeWidth : StrokeWidth
    {
        public double Width { get; }

        public NumericalStrokeWidth(double width)
        {
            if (width < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(width), "Stroke width must not be negative.");
            }

            Width = width;
        }
    }

    public static LengthStrokeWidth Length(double width, Unit unit) => new(width, unit);
    public static PercentageStrokeWidth Percentage(Percentage percentageWidth) => new(percentageWidth);
    public static NumericalStrokeWidth Number(double width) => new(width);

    public string ToMermaidString()
        => $"stroke-width:{ToSubtypeMermaidString()}";

    private string ToSubtypeMermaidString()
        => this switch
        {
            LengthStrokeWidth lsw => lsw.Width.ToCss(),
            PercentageStrokeWidth psw => psw.PercentageWidth.ToNumericalString(),
            NumericalStrokeWidth nsw => nsw.Width.ToNumberString()
        };
}
