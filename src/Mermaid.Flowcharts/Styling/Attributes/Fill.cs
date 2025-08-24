namespace Mermaid.Flowcharts.Styling.Attributes;

public record Fill(Color Color) : IStyleClassComponent
{
    public string ToMermaidString()
        => $"fill:{Color.ToCss()}";
}
