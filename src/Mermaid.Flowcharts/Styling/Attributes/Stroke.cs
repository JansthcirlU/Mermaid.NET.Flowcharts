namespace Mermaid.Flowcharts.Styling.Attributes;

public record Stroke(Color Color) : IStyleClassComponent<Stroke>
{
    public string ToMermaidString()
        => $"stroke:{Color.ToCss()}";
}
