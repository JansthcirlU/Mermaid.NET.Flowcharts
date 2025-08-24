using Mermaid.Flowcharts.Styling.Attributes;

namespace Mermaid.Flowcharts.Styling;

public record StyleColor(Color Color) : IMermaidStyle
{
    public static implicit operator StyleColor(Color color) => new(color);

    public string ToMermaidString()
        => $"color:{Color.ToCss()}";
}
