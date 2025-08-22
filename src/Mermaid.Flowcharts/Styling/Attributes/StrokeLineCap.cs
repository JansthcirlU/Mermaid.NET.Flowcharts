using Mermaid.Flowcharts.Styling.Attributes.Enums;

namespace Mermaid.Flowcharts.Styling.Attributes;

public record StrokeLineCap(StrokeLineCapType StrokeLineCapType) : IMermaidStyle
{
    public string ToMermaidString()
        => $"stroke-linecap:{StrokeLineCapType.ToStrokeLineCapTypeString()}";
}
