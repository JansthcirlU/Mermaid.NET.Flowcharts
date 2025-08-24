using Mermaid.Flowcharts.Styling.Attributes.Enums;

namespace Mermaid.Flowcharts.Styling.Attributes;

public record StrokeLineJoin(StrokeLineJoinType StrokeLineJoinType) : IStyleClassComponent
{
    public string ToMermaidString()
        => $"stroke-linejoin:{StrokeLineJoinType.ToStrokeLineJoinTypeString()}";
}
