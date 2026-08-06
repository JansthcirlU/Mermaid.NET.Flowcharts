using Mermaid.Flowcharts.Numerical;
using Mermaid.Flowcharts.Styling.Attributes.Base;

namespace Mermaid.Flowcharts.Styling.Attributes;

public readonly record struct Opacity : ICssAttribute
{
    public UnitInterval Interval { get; }

    [Obsolete(error: true, message: $"Please use the factory methods instead of the default constructor to create a new {nameof(Opacity)}.")]
#pragma warning disable CS8618 // This constructor is never used
    public Opacity() { }
#pragma warning restore CS8618
    private Opacity(UnitInterval value)
    {
        Interval = value;
    }

    public static Opacity FromDouble(double value)
    {
        if (double.IsNaN(value))
        {
            throw new ArgumentOutOfRangeException(nameof(value), "Opacity must be a real number between 0 and 1.");
        }

        if (value < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(value), "Opacity must not be negative.");
        }

        if (value > 1)
        {
            throw new ArgumentOutOfRangeException(nameof(value), "Opacity must not be greater than 1.");
        }

        return new(UnitInterval.FromDouble(value));
    }

    public string ToCss()
        => Interval.Value.ToNumberString();
}
