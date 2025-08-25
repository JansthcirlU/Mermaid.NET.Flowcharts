namespace Mermaid.Flowcharts.NonEmptyStringTypes;

public readonly record struct NonEmptyString
{
    public string Value { get; }

    public NonEmptyString(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Non-empty string must not be null or empty or whitespace.", nameof(value));
        }

        Value = value;
    }

    public static implicit operator string(NonEmptyString nes) => nes.Value;
    public static implicit operator NonEmptyString(string s) => new(s);

    public override string ToString()
        => Value;
}
