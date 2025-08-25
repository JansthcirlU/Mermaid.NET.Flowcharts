using Mermaid.Flowcharts.Numerical;
using Mermaid.Flowcharts.Styling.Attributes.Enums;

namespace Mermaid.Flowcharts.Styling.Attributes;

public abstract record DashSize : IStyleClassComponent<DashSize>
{
    private protected DashSize() { }

    public sealed record LengthDashSize : DashSize
    {
        public Length LengthSize { get; }

        public LengthDashSize(double size, Unit unit)
        {
            if (size < 0) throw new ArgumentOutOfRangeException(nameof(size), "Dash length size must not be negative.");

            LengthSize = new(size, unit);
        }
    }

    public sealed record PercentageDashSize : DashSize
    {
        public Percentage PercentageSize { get; }

        public PercentageDashSize(Percentage percentageSize)
        {
            if (percentageSize.Value < 0) throw new ArgumentOutOfRangeException(nameof(percentageSize), "Dash percentage size must not be negative.");

            PercentageSize = percentageSize;
        }
    }

    public sealed record NumericalDashSize : DashSize
    {
        public double Size { get; }

        public NumericalDashSize(double size)
        {
            if (size < 0) throw new ArgumentOutOfRangeException(nameof(size), "Dash size must not be negative.");

            Size = size;
        }
    }

    public static LengthDashSize Length(double size, Unit unit) => new(size, unit);
    public static PercentageDashSize Percentage(Percentage percentageSize) => new(percentageSize);
    public static NumericalDashSize Number(double size) => new(size);

    public string ToMermaidString()
        => this switch
        {
            LengthDashSize lds => lds.LengthSize.ToCss(),
            PercentageDashSize pds => pds.PercentageSize.ToNumericalString(),
            NumericalDashSize nds => nds.Size.ToNumberString()
        };
}
