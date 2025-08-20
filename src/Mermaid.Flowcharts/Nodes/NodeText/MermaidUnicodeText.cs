using System.Text;

namespace Mermaid.Flowcharts.Nodes.NodeText;

public readonly record struct MermaidUnicodeText : INodeText<MermaidUnicodeText>
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

    [Obsolete(error: true, message: $"Please use the factory methods instead of the default constructor to create a new {nameof(MermaidUnicodeText)}.")]
#pragma warning disable CS8618 // This constructor is never used
    public MermaidUnicodeText() { }
#pragma warning restore CS8618
    private MermaidUnicodeText(string text)
    {
        Value = text;
    }

    public static MermaidUnicodeText FromString(string text)
    {
        if (text.Contains('\n') || text.Contains('\r')) throw new ArgumentException("Mermaid text should not contain new lines.");
        if (string.IsNullOrEmpty(text)) throw new ArgumentException("Mermaid text should not be empty.", nameof(text));
        if (string.IsNullOrWhiteSpace(text)) return new(text);

        StringBuilder builder = new();
        foreach (char character in text)
        {
            if (EscapedCharacters.TryGetValue(character, out string? escapedValue))
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
