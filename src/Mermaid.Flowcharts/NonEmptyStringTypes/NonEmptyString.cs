namespace Mermaid.Flowcharts.NonEmptyStringTypes;

internal readonly record struct NonEmptyString
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

    public static NonEmptyString FromString(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Non-empty string must not be null or empty or whitespace.", nameof(value));
        }

        return new(value);
    }

    public ReadOnlySpan<char> AsSpan()
        => Value.AsSpan();

    public bool Contains(char value)
        => Value.Contains(value);

    public NonEmptyString ReplaceLineEndings(string replacementText = "\n")
        => FromString(Value.ReplaceLineEndings(replacementText));

    public NonEmptyString Trim()
        => FromString(Value.Trim());

    public static implicit operator string(NonEmptyString nes) => nes.Value;

    public override string ToString()
        => Value;
}

