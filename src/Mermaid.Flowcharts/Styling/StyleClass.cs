using Mermaid.Flowcharts.Styling.Attributes;
using Mermaid.Flowcharts.Styling.Attributes.Fonts;

namespace Mermaid.Flowcharts.Styling;

public record StyleClass(
    Fill? Fill = null,
    Stroke? Stroke = null,
    DashArray? StrokeDashArray = null,
    DashOffset? StrokeDashOffset = null,
    StrokeWidth? StrokeWidth = null,
    StrokeLineCap? StrokeLineCap = null,
    StrokeLineJoin? StrokeLineJoin = null,
    StyleColor? Color = null,
    StyleOpacity? Opacity = null,
    FontFamily? FontFamily = null,
    FontSize? FontSize = null,
    FontWeight? FontWeight = null) : IMermaidPrintable
{
    public StyleClass Merge(StyleClass other) => new(
        Fill: other.Fill ?? Fill,
        Stroke: other.Stroke ?? Stroke,
        StrokeDashArray: other.StrokeDashArray ?? StrokeDashArray,
        StrokeDashOffset: other.StrokeDashOffset ?? StrokeDashOffset,
        StrokeWidth: other.StrokeWidth ?? StrokeWidth,
        StrokeLineCap: other.StrokeLineCap ?? StrokeLineCap,
        StrokeLineJoin: other.StrokeLineJoin ?? StrokeLineJoin,
        Color: other.Color ?? Color,
        Opacity: other.Opacity ?? Opacity,
        FontFamily: other.FontFamily ?? FontFamily,
        FontSize: other.FontSize ?? FontSize,
        FontWeight: other.FontWeight ?? FontWeight);

    public string ToMermaidString(int indentations = 0, string indentationText = "  ")
    {
        if (!GetInstantiatedStyleComponents().Any()) throw new InvalidOperationException("At least one style class element should be set.");

        return $"{indentationText.Repeat(indentations)}{string.Join(',', GetInstantiatedStyleComponents().Select(ims => ims.ToMermaidString()))}";
    }

    private IEnumerable<IStyleClassComponent> GetInstantiatedStyleComponents()
    {
        if (Fill is not null) yield return Fill;
        if (Stroke is not null) yield return Stroke;
        if (StrokeDashArray is not null) yield return StrokeDashArray;
        if (StrokeDashOffset is not null) yield return StrokeDashOffset;
        if (StrokeWidth is not null) yield return StrokeWidth;
        if (StrokeLineCap is not null) yield return StrokeLineCap;
        if (StrokeLineJoin is not null) yield return StrokeLineJoin;
        if (Color is not null) yield return Color;
        if (Opacity is not null) yield return Opacity;
        if (FontFamily is not null) yield return FontFamily;
        if (FontSize is not null) yield return FontSize;
        if (FontWeight is not null) yield return FontWeight;
    }
}
