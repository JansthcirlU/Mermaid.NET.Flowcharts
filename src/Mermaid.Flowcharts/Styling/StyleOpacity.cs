using Mermaid.Flowcharts.Styling.Attributes;

namespace Mermaid.Flowcharts.Styling;

public record StyleOpacity(Opacity Opacity) : IStyleClassComponent
{
    public string ToMermaidString()
        => $"opacity:{Opacity.ToCss()}";
}
