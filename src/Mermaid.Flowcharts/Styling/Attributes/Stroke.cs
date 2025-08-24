namespace Mermaid.Flowcharts.Styling.Attributes;

public record Stroke(Color Color) : IStyleClassComponent
{
    public string ToMermaidString()
        => $"stroke:{Color}";
}
