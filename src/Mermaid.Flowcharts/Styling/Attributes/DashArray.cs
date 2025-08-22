namespace Mermaid.Flowcharts.Styling.Attributes;

public record DashArray(IEnumerable<DashSize> DashSizes) : IMermaidStyle
{
    public string ToMermaidString()
        => DashSizes.Any()
            ? $"stroke-dasharray: {string.Join(' ', DashSizes.Select(ds => ds.ToString()))}"
            : "stroke-dasharray:none";
}
