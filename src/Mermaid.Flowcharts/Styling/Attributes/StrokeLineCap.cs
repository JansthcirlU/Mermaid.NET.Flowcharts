using Mermaid.Flowcharts.Styling.Attributes.Enums;

namespace Mermaid.Flowcharts.Styling.Attributes;

public record StrokeLineCap(StrokeLineCapType StrokeLineCapType) : IStyleClassComponent<StrokeLineCap>
{
    public string ToMermaidString()
        => $"stroke-linecap:{StrokeLineCapType.ToStrokeLineCapTypeString()}";
}
