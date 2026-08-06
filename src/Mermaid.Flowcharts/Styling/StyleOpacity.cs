using Mermaid.Flowcharts.Styling.Attributes;

namespace Mermaid.Flowcharts.Styling;

public record StyleOpacity : IStyleClassComponent<StyleOpacity>
{
    public Opacity Opacity { get; }

    public StyleOpacity(double opacity)
    {
        Opacity = Opacity.FromDouble(opacity);
    }

    public string ToMermaidString()
        => $"opacity:{Opacity.ToCss()}";
}
