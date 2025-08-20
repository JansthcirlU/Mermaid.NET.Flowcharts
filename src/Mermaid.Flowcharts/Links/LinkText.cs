using System.Buffers;

namespace Mermaid.Flowcharts.Links;

public readonly record struct LinkText : IMermaidPrintable
{
    public string Value { get; }

    private static readonly SearchValues<char> IllegalCharacters = SearchValues.Create("\"|");

    [Obsolete(error: true, message: $"Please use the factory methods instead of the default constructor to create a new {nameof(LinkText)}.")]
#pragma warning disable CS8618 // This constructor is never used
    public LinkText() { }
#pragma warning restore CS8618
    private LinkText(string text)
        => Value = text;

    public static LinkText FromString(string value)
    {
        if (string.IsNullOrEmpty(value)) throw new ArgumentException("Link text must not be null or empty.", nameof(value));
        if (value.Contains('\n') || value.Contains('\r')) throw new ArgumentException("Link text must not contain new lines.", nameof(value));
        int illegalCharacterIndex = value.AsSpan().IndexOfAny(IllegalCharacters);
        if (illegalCharacterIndex > -1) throw new ArgumentException($"Link text must not contain illegal character \"{value[illegalCharacterIndex]}\".", nameof(value));
        return new(value);
    }

    public override string ToString()
        => ToMermaidString();

    public string ToMermaidString(int indentations = 0, string indentationText = "  ")
        => $"{indentationText.Repeat(indentations)}{Value}";
}