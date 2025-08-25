using Mermaid.Flowcharts.Styling.Attributes.Enums;

namespace Mermaid.Flowcharts.Styling.Attributes;

public record StrokeLineJoin(StrokeLineJoinType StrokeLineJoinType) : IStyleClassComponent<StrokeLineJoin>
{
    public string ToMermaidString()
        => $"stroke-linejoin:{StrokeLineJoinType.ToStrokeLineJoinTypeString()}";
}
