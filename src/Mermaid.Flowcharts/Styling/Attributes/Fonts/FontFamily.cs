namespace Mermaid.Flowcharts.Styling.Attributes.Fonts;

public record FontFamily : IMermaidStyle
{
    public IEnumerable<FontFamilyComponent> FontFamilyComponents { get; }

    public FontFamily(IEnumerable<FontFamilyComponent> fontFamilyComponents)
    {
        if (!fontFamilyComponents.Any()) throw new ArgumentException("Font family must contain at least one component.", nameof(fontFamilyComponents));

        FontFamilyComponents = fontFamilyComponents;
    }

    public string ToMermaidString()
        => $"font-family:{string.Join("\\,", FontFamilyComponents.Select(c => c.Value))}";
}
