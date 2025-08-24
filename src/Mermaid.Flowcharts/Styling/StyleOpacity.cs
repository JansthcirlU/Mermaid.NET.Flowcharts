using Mermaid.Flowcharts.Styling.Attributes;

namespace Mermaid.Flowcharts.Styling;

public record StyleOpacity(Opacity Opacity) : IMermaidStyle
{
    public string ToMermaidString()
        => $"opacity:{Opacity.ToCss()}";
}
