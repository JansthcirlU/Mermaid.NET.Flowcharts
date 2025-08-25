using Mermaid.Flowcharts.Styling.Attributes.Enums;

namespace Mermaid.Flowcharts.Styling;

public static class EnumExtensions
{
    public static string ToAbsoluteSizeString(this AbsoluteSize absoluteSize)
        => absoluteSize switch
        {
            AbsoluteSize.XxSmall => "xx-small",
            AbsoluteSize.XSmall => "x-small",
            AbsoluteSize.Small => "small",
            AbsoluteSize.Medium => "medium",
            AbsoluteSize.Large => "large",
            AbsoluteSize.XLarge => "x-large",
            AbsoluteSize.XxLarge => "xx-large",
            AbsoluteSize.XxxLarge => "xxx-large",
        };

    public static string ToRelativeSizeString(this RelativeSize relativeSize)
        => relativeSize switch
        {
            RelativeSize.Larger => "larger",
            RelativeSize.Smaller => "smaller",
        };

    public static string ToFontWeightTypeString(this FontWeightType fontWeight)
        => fontWeight switch
        {
            FontWeightType.Normal => "normal",
            FontWeightType.Bold => "bold",
            FontWeightType.Bolder => "bolder",
            FontWeightType.Lighter => "lighter",
        };

    public static string ToStrokeLineCapTypeString(this StrokeLineCapType strokeLineCapType)
        => strokeLineCapType switch
        {
            StrokeLineCapType.Butt => "butt",
            StrokeLineCapType.Round => "round",
            StrokeLineCapType.Square => "square",
        };

    public static string ToStrokeLineJoinTypeString(this StrokeLineJoinType strokeLineJoinType)
        => strokeLineJoinType switch
        {
            StrokeLineJoinType.Miter => "miter",
            StrokeLineJoinType.Arcs => "arcs",
            StrokeLineJoinType.Bevel => "bevel",
            StrokeLineJoinType.MiterClip => "miter-clip",
            StrokeLineJoinType.Round => "round",
        };

    public static string ToUnitString(this Unit unit)
        => unit switch
        {
            Unit.Px => "px",
            Unit.Rem => "rem",
            Unit.Em => "em",
            Unit.Pt => "pt",
            Unit.Pc => "pc",
            Unit.Ch => "ch",
            Unit.Ex => "ex",
            Unit.Vw => "vw",
            Unit.Vh => "vh",
            Unit.Vmin => "vmin",
            Unit.Vmax => "vmax",
            Unit.Mm => "mm",
            Unit.Cm => "cm",
            Unit.In => "in",
        };
}
