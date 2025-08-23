namespace Mermaid.Flowcharts.Styling.Attributes;

public record DashArray(IEnumerable<DashSize> DashSizes) : IMermaidStyle
{
    public string ToMermaidString()
        => DashSizes.Any()
            ? $"stroke-dasharray: {string.Join(' ', DashSizes.Select(ds =>
            {
                string mermaid = ds.ToMermaidString();

                if (ds is DashSize.LengthDashSize lds && lds.LengthSize.Unit == Enums.Unit.Px)
                {
                    return mermaid[..^2]; // Omit 'px' unit because it is implied/default
                }
                return mermaid;   
            }))}"
            : "stroke-dasharray:none";
}
