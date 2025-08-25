namespace Mermaid.Flowcharts.Styling.Attributes;

public record Fill(Color Color) : IStyleClassComponent<Fill>
{
    public string ToMermaidString()
        => $"fill:{Color.ToCss()}";
}
