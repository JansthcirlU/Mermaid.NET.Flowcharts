using System.Text;

namespace Mermaid.Flowcharts.Nodes.NodeText;

public readonly record struct MarkdownText : INodeText<MarkdownText>
{
    public string Value { get; }

    private static readonly Dictionary<char, string> EscapedCharacters = new()
    {
        { '"', "&quot;" },
        { '\\', "#92;" },
        { '`', "#96;" }
    };

    [Obsolete(error: true, message: $"Please use the factory methods instead of the default constructor to create a new {nameof(MarkdownText)}.")]
#pragma warning disable CS8618
    public MarkdownText() { }
#pragma warning restore CS8618
    private MarkdownText(string text)
        => Value = text;

    public static MarkdownText FromString(string text)
    {
        if (string.IsNullOrEmpty(text)) throw new ArgumentException("Mermaid text should not be empty.");
        if (string.IsNullOrWhiteSpace(text)) return new(text);

        StringBuilder builder = new();
        builder.Append('`');
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
        builder.Append('`');
        return new MarkdownText(builder.ToString());
    }

    public override string ToString()
        => ToMermaidString();

    public string ToMermaidString(int indentations = 0, string indentationText = "  ")
        => Value;
}