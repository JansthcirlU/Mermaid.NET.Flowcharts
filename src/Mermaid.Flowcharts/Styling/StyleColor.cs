using Mermaid.Flowcharts.Styling.Attributes;

namespace Mermaid.Flowcharts.Styling;

public record StyleColor(Color Color) : IMermaidStyle
{
    public string ToMermaidString()
        => $"color:{Color}";
}
