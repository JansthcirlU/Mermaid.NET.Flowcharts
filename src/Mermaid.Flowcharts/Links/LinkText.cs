using System.Text;
using System.Text.RegularExpressions;
using Mermaid.Flowcharts.NonEmptyStringTypes;

namespace Mermaid.Flowcharts.Links;

public readonly partial record struct LinkText : IMermaidPrintable
{
    public string Value { get; }

    private static readonly Dictionary<char, string> EscapedCharacters = new()
    {
        { '"', "&quot;" },
        { '<', "&lt;" },
        { '>', "&gt;" },
    };

    // Matches all valid <br> variations based on Mermaid's actual behavior:
    // <br>, <br/>, <br />, <br//>, </br>, </br/>, </br //>, </br  /   >, etc.
    // Case-insensitive, but NO space before 'br'.
    private static readonly Regex MermaidAcceptedHtmlLineBreaks = MermaidAcceptedHtmlLineBreaksRegex();

    [Obsolete(error: true, message: $"Please use the factory methods instead of the default constructor to create a new {nameof(LinkText)}.")]
#pragma warning disable CS8618 // This constructor is never used
    public LinkText() { }
#pragma warning restore CS8618
    private LinkText(NonEmptyString text)
        => Value = text;

    public static LinkText FromString(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return new(value);
        }

        // Split on all variations of <br> tags
        string[] segments = MermaidAcceptedHtmlLineBreaks.Split(value.ReplaceLineEndings());

        StringBuilder builder = new();

        for (int i = 0; i < segments.Length; i++)
        {
            // Escape illegal characters
            foreach (char character in segments[i])
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

            // Add <br> separator if not the last segment
            if (i < segments.Length - 1)
            {
                builder.Append("<br>");
            }
        }

        return new(builder.ToString());
    }

    public override string ToString()
        => ToMermaidString();

    public string ToMermaidString(int indentations = 0, string indentationText = "  ")
        => Value;

    [GeneratedRegex(@"<\/?br\s*\/*\s*>", RegexOptions.IgnoreCase | RegexOptions.Compiled)]
    private static partial Regex MermaidAcceptedHtmlLineBreaksRegex();
}
