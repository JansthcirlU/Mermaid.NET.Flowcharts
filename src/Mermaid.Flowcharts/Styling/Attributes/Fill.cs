namespace Mermaid.Flowcharts.Styling.Attributes;

public record Fill(Color Color) : IMermaidStyle
{
    public string ToMermaidString()
        => $"fill:{Color.ToCss()}";
}
