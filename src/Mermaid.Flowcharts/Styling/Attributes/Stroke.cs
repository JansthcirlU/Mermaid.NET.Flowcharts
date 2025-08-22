namespace Mermaid.Flowcharts.Styling.Attributes;

public record Stroke(Color Color) : IMermaidStyle
{
    public string ToMermaidString()
        => $"stroke:{Color}";
}
