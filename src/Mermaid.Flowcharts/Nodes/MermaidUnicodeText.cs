using System.Text;

namespace Mermaid.Flowcharts.Nodes;

public readonly record struct MermaidUnicodeText : IMermaidPrintable
{
    public string Value { get; }

    private static readonly Dictionary<char, string> EscapedCharacters = new()
    {
        { '"', "&quot;" },
        { '#', "#35;" },
        { '<', "&lt;" },
        { '>', "&gt;" },
        { '&', "&amp;" },
        { '\\', "#92;" },
        { '\'', "&apos;" }
    };

    public MermaidUnicodeText()
    {
        Value = string.Empty;
    }
    private MermaidUnicodeText(string text)
    {
        Value = text;
    }

    public static MermaidUnicodeText FromString(string text)
    {
        if (text.Contains('\n') || text.Contains('\r')) throw new ArgumentException("Mermaid text should not contain new lines.");
        if (string.IsNullOrEmpty(text)) throw new ArgumentException("Mermaid text should not be empty.", nameof(text));
        if (string.IsNullOrWhiteSpace(text)) return new();

        var builder = new StringBuilder();
        foreach (var character in text)
        {
            if (EscapedCharacters.TryGetValue(character, out var escapedValue))
            {
                builder.Append(escapedValue);
            }
            else
            {
                builder.Append(character);
            }
        }
        return new MermaidUnicodeText(builder.ToString());
    }

    public override string ToString()
        => ToMermaidString();

    public string ToMermaidString(int indentations = 0, string indentationText = "  ")
        => Value;
}
