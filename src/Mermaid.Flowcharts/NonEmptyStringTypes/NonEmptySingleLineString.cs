using System.Buffers;
using System.Collections;

namespace Mermaid.Flowcharts.NonEmptyStringTypes;

internal readonly record struct NonEmptySingleLineString
{
    public static readonly SearchValues<char> NewLineSearchValues = SearchValues.Create("\n\r\u2028\u2029\u0085");
    public NonEmptyString Value { get; }

    [Obsolete(error: true, message: $"Please use the factory methods instead of the default constructor to create a new {nameof(NonEmptySingleLineString)}.")]
#pragma warning disable CS8618 // This constructor is never used
    public NonEmptySingleLineString() { }
#pragma warning restore CS8618
    private NonEmptySingleLineString(NonEmptyString value)
    {
        Value = value;
    }

    public static NonEmptySingleLineString FromString(string value)
    {
        NonEmptyString nonEmpty = NonEmptyString.FromString(value);

        if (nonEmpty.AsSpan().IndexOfAny(NewLineSearchValues) > -1)
        {
            throw new ArgumentException("Non-empty single line string must not contain any newline characters or carriage returns.", nameof(value));
        }

        return new(nonEmpty);
    }

    public ReadOnlySpan<char> AsSpan()
        => Value.AsSpan();

    public bool Contains(char value)
        => Value.Contains(value);

    public NonEmptySingleLineString Trim()
        => FromString(Value.Trim());

    public static implicit operator string(NonEmptySingleLineString nesls) => nesls.Value;

    public override string ToString()
        => Value;
}
