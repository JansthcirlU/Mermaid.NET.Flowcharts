using System.Text;
using Mermaid.Flowcharts.NonEmptyStringTypes;

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
    private MermaidUnicodeText(NonEmptySingleLineString text)
    {
        Value = text;
    }

    public static MermaidUnicodeText FromString(string text)
    {
        NonEmptySingleLineString nonEmptySingleLine = text;
        StringBuilder builder = new();
        foreach (char character in (string)nonEmptySingleLine)
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
