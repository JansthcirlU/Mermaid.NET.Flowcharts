namespace Mermaid.Flowcharts.Numerical;

public readonly record struct UnitInterval : INumerical
{
    public double Value { get; }

    [Obsolete(error: true, message: $"Please use the factory method instead of the default constructor to create a new {nameof(UnitInterval)}.")]
#pragma warning disable CS8618 // This constructor is never used
    public UnitInterval() { }
#pragma warning restore CS8618
    private UnitInterval(double value)
    {
        Value = value;
    }

    public static UnitInterval FromDouble(double value)
    {
        if (double.IsNaN(value) || double.IsInfinity(value))
        {
            throw new ArgumentOutOfRangeException(nameof(value), "Unit interval must be a real number between 0 and 1.");
        }

        if (value < 0.0)
        {
            throw new ArgumentOutOfRangeException(nameof(value), "Unit interval should be at least 0.");
        }

        if (value > 1.0)
        {
            throw new ArgumentOutOfRangeException(nameof(value), "Unit interval should be at most 1.");
        }

        return new(value);
    }

    public static implicit operator double(UnitInterval i) => i.Value;

    public string ToNumericalString()
        => Value.ToNumberString();
}
