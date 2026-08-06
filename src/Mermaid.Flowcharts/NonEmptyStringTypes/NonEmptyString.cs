namespace Mermaid.Flowcharts.NonEmptyStringTypes;

public readonly record struct NonEmptyString
{
    private readonly string _value;
    public string Value => _value ?? throw new InvalidOperationException($"{nameof(NonEmptyString)} value must never be null. Make sure you use the {nameof(FromString)} factory method to construct a new {nameof(NonEmptyString)}.");

    [Obsolete(error: true, message: $"Please use the factory methods instead of the default constructor to create a new {nameof(NonEmptyString)}.")]
#pragma warning disable CS8618 // This constructor is never used
    public NonEmptyString() { }
#pragma warning restore CS8618
    private NonEmptyString(string value)
    {
        _value = value;
    }

    public static NonEmptyString FromString(string s)
    {
        if (string.IsNullOrWhiteSpace(s))
        {
            throw new ArgumentException("Non-empty string must not be null or empty or whitespace.", nameof(s));
        }

        return new(s);
    }

    public static implicit operator string(NonEmptyString nes) => nes.Value;

    public override string ToString()
        => Value;
}
