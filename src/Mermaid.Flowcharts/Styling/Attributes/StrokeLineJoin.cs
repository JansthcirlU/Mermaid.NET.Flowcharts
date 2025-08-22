using Mermaid.Flowcharts.Styling.Attributes.Enums;

namespace Mermaid.Flowcharts.Styling.Attributes;

public record StrokeLineJoin(StrokeLineJoinType StrokeLineJoinType) : IMermaidStyle
{
    public string ToMermaidString()
        => $"stroke-linejoin:{StrokeLineJoinType.ToStrokeLineJoinTypeString()}";
}
