namespace Mermaid.Flowcharts.Styling.Attributes.Fonts;

public record FontFamily(IEnumerable<string> FontNames) : IMermaidStyle
{
    public string ToMermaidString()
        => $"font-family:{string.Join("\\,", FontNames)}"
            .Replace("\"", "");
}
