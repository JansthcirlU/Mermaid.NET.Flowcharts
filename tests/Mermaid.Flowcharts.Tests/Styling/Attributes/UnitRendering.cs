using Mermaid.Flowcharts.Styling.Attributes.Enums;

namespace Mermaid.Flowcharts.Tests.Styling.Attributes;

public static class UnitRendering
{
    public static readonly Dictionary<Unit, string> UnitSuffixes = new()
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
    };
}