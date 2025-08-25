using Mermaid.Flowcharts.Numerical;
using Mermaid.Flowcharts.Styling.Attributes.Enums;

namespace Mermaid.Flowcharts.Styling.Attributes;

public abstract record DashOffset : IStyleClassComponent<DashOffset>
{
    private protected DashOffset() { }

    public sealed record LengthDashOffset : DashOffset
    {
        public Length LengthOffset { get; }

        public LengthDashOffset(double size, Unit unit)
        {
            if (size < 0) throw new ArgumentOutOfRangeException(nameof(size), "Dash length offset must not be negative.");

            LengthOffset = new(size, unit);
        }
    }

    public sealed record PercentageDashOffset : DashOffset
    {
        public Percentage PercentageOffset { get; }

        public PercentageDashOffset(Percentage percentageOffset)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(percentageOffset.Value, nameof(percentageOffset));

            PercentageOffset = percentageOffset;
        }
    }

    public sealed record NumericalDashOffset : DashOffset
    {
        public double Size { get; }

        public NumericalDashOffset(double size)
        {
            if (size < 0) throw new ArgumentOutOfRangeException(nameof(size), "Dash offset must not be negative.");

            Size = size;
        }
    }

    public static LengthDashOffset Length(double size, Unit unit) => new(size, unit);
    public static PercentageDashOffset Percentage(Percentage percentageOffset) => new(percentageOffset);
    public static NumericalDashOffset Number(double size) => new(size);

    public string ToMermaidString()
        => $"stroke-dashoffset: {ToSubtypeMermaidString()}";

    private string ToSubtypeMermaidString()
        => this switch
        {
            LengthDashOffset ldo => ldo.LengthOffset.ToCss(),
            PercentageDashOffset pdo => pdo.PercentageOffset.ToNumericalString(),
            NumericalDashOffset ndo => ndo.Size.ToNumberString()
        };
}