using Mermaid.Flowcharts.Numerical;

namespace Mermaid.Flowcharts.Styling.Attributes;

public abstract record DashSize
{
    private protected DashSize() { }

    public sealed record LengthDashSize : DashSize
    {
        public Length LengthSize { get; }

        public LengthDashSize(Length lengthSize)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(lengthSize.Value, nameof(lengthSize));

            LengthSize = lengthSize;
        }
    }

    public sealed record PercentageDashSize : DashSize
    {
        public Percentage PercentageSize { get; }

        public PercentageDashSize(Percentage percentageSize)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(percentageSize.Value, nameof(percentageSize));

            PercentageSize = percentageSize;
        }
    }

    public static LengthDashSize Length(Length lengthSize) => new(lengthSize);
    public static PercentageDashSize Percentage(Percentage percentageSize) => new(percentageSize);

    public override string ToString()
        => this switch
        {
            LengthDashSize lds => lds.LengthSize.ToString(),
            PercentageDashSize pds => $"{pds.PercentageSize}%"
        };
}
