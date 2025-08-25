using System.Buffers;

namespace Mermaid.Flowcharts.NonEmptyStringTypes;

public readonly record struct NonEmptySingleLineString
{
    public static readonly SearchValues<char> NewLineSearchValues = SearchValues.Create("\n\r\u2028\u2029\u0085");
    public NonEmptyString Value { get; }

    public NonEmptySingleLineString(NonEmptyString value)
    {
        if (value.Value.AsSpan().IndexOfAny(NewLineSearchValues) > -1) throw new ArgumentException("Non-empty single line string must not contain any newline characters or carriage returns.", nameof(value));

        Value = value;
    }

    public static implicit operator string(NonEmptySingleLineString nesls) => nesls.Value;
    public static implicit operator NonEmptySingleLineString(string s) => new(s);

    public override string ToString()
        => Value;
}