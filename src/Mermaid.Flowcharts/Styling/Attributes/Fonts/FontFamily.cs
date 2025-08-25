using System.Collections.Immutable;

namespace Mermaid.Flowcharts.Styling.Attributes.Fonts;

public sealed record FontFamily : IStyleClassComponent<FontFamily>
{
    public ImmutableArray<FontFamilyComponent> FontFamilyComponents { get; }

    public FontFamily(IEnumerable<FontFamilyComponent> fontFamilyComponents)
    {
        if (!fontFamilyComponents.Any()) throw new ArgumentException("Font family must contain at least one component.", nameof(fontFamilyComponents));

        FontFamilyComponents = [.. fontFamilyComponents];
    }

    public string ToMermaidString()
        => $"font-family:{string.Join("\\,", FontFamilyComponents.Select(c => c.Value))}";

    public bool Equals(FontFamily? other)
        => other is not null && FontFamilyComponents.SequenceEqual(other.FontFamilyComponents);

    public override int GetHashCode()
        => FontFamilyComponents.Aggregate(0, (hash, item) => HashCode.Combine(hash, item));
}
