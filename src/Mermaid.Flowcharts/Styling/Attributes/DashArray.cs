using System.Collections.Immutable;

namespace Mermaid.Flowcharts.Styling.Attributes;

public sealed record DashArray : IStyleClassComponent<DashArray>
{
    public ImmutableArray<DashSize> DashSizes { get; }

    public DashArray(IEnumerable<DashSize> dashSizes)
    {
        DashSizes = [.. dashSizes];
    }

    public string ToMermaidString()
        => DashSizes.Any()
            ? $"stroke-dasharray: {string.Join(' ', DashSizes.Select(ds => ds.ToMermaidString()))}"
            : "stroke-dasharray:none";

    public bool Equals(DashArray? other)
        => other is not null && DashSizes.SequenceEqual(other.DashSizes);

    public override int GetHashCode()
        => DashSizes.Aggregate(0, (hash, item) => HashCode.Combine(hash, item));
}
