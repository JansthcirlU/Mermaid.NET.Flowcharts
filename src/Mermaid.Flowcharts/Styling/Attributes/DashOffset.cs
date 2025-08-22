using Mermaid.Flowcharts.Numerical;

namespace Mermaid.Flowcharts.Styling.Attributes;

public abstract record DashOffset : IMermaidStyle
{
    private protected DashOffset() { }

    public sealed record LengthDashOffset : DashOffset
    {
        public Length LengthOffset { get; }

        public LengthDashOffset(Length lengthOffset)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(lengthOffset.Value, nameof(lengthOffset));

            LengthOffset = lengthOffset;
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

    public static LengthDashOffset Length(Length lengthOffset) => new(lengthOffset);
    public static PercentageDashOffset Percentage(Percentage percentageOffset) => new(percentageOffset);

    public string ToMermaidString()
        => $"stroke-dashoffset:{ToSubtypeMermaidString()}";

    private string ToSubtypeMermaidString()
        => this switch
        {
            LengthDashOffset ldo => ldo.LengthOffset.ToString(),
            PercentageDashOffset pdo => pdo.PercentageOffset.ToString()
        };
}