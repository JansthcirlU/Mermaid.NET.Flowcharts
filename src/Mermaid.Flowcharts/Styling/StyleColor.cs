using Mermaid.Flowcharts.Styling.Attributes;

namespace Mermaid.Flowcharts.Styling;

public record StyleColor(Color Color) : IStyleClassComponent<StyleColor>
{
    public static implicit operator StyleColor(Color color) => new(color);

    public string ToMermaidString()
        => $"color:{Color.ToCss()}";
}
