using System.Collections.ObjectModel;
using Mermaid.Flowcharts.Styling.Attributes.Enums;

namespace Mermaid.Flowcharts.Tests.Styling.Attributes;

public static class EnumRendering
{
    public static readonly ReadOnlyDictionary<Unit, string> UnitSuffixes = new Dictionary<Unit, string>()
    {
        [Unit.Px] = "px",
        [Unit.Rem] = "rem",
        [Unit.Em] = "em",
        [Unit.Pt] = "pt",
        [Unit.Pc] = "pc",
        [Unit.Ch] = "ch",
        [Unit.Ex] = "ex",
        [Unit.Vw] = "vw",
        [Unit.Vh] = "vh",
        [Unit.Vmin] = "vmin",
        [Unit.Vmax] = "vmax",
        [Unit.Mm] = "mm",
        [Unit.Cm] = "cm",
        [Unit.In] = "in"
    }.AsReadOnly();

    public static readonly ReadOnlyDictionary<FontWeightType, string> FontWeightTypes = new Dictionary<FontWeightType, string>()
    {
        [FontWeightType.Normal] = "normal",
        [FontWeightType.Bold] = "bold",
        [FontWeightType.Bolder] = "bolder",
        [FontWeightType.Lighter] = "lighter"
    }.AsReadOnly();

    public static readonly ReadOnlyDictionary<AbsoluteSize, string> AbsoluteSizes = new Dictionary<AbsoluteSize, string>()
    {
        [AbsoluteSize.XxSmall] = "xx-small",
        [AbsoluteSize.XSmall] = "x-small",
        [AbsoluteSize.Small] = "small",
        [AbsoluteSize.Medium] = "medium",
        [AbsoluteSize.Large] = "large",
        [AbsoluteSize.XLarge] = "x-large",
        [AbsoluteSize.XxLarge] = "xx-large",
        [AbsoluteSize.XxxLarge] = "xxx-large"
    }.AsReadOnly();

    public static readonly ReadOnlyDictionary<RelativeSize, string> RelativeSizes = new Dictionary<RelativeSize, string>()
    {
        [RelativeSize.Larger] = "larger",
        [RelativeSize.Smaller] = "smaller"
    }.AsReadOnly();

    public static readonly ReadOnlyDictionary<StrokeLineCapType, string> StrokeLineCapTypes = new Dictionary<StrokeLineCapType, string>()
    {
        [StrokeLineCapType.Butt] = "butt",
        [StrokeLineCapType.Round] = "round",
        [StrokeLineCapType.Square] = "square",
    }.AsReadOnly();

    public static readonly ReadOnlyDictionary<StrokeLineJoinType, string> StrokeLineJoinTypes = new Dictionary<StrokeLineJoinType, string>()
    {
        [StrokeLineJoinType.Miter] = "miter",
        [StrokeLineJoinType.Arcs] = "arcs",
        [StrokeLineJoinType.Bevel] = "bevel",
        [StrokeLineJoinType.MiterClip] = "miter-clip",
        [StrokeLineJoinType.Round] = "round"
    }.AsReadOnly();
}
