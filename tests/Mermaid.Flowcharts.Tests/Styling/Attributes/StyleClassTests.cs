using Mermaid.Flowcharts.Styling;
using Mermaid.Flowcharts.Styling.Attributes;
using Mermaid.Flowcharts.Styling.Attributes.Enums;
using Mermaid.Flowcharts.Styling.Attributes.Fonts;

namespace Mermaid.Flowcharts.Tests.Styling.Attributes;

public class StyleClassTests
{
    [Fact]
    public void StyleClass_ToMermaidString_WhenEmpty_ShouldThrow()
    {
        // Arrange
        StyleClass empty = new();

        // Act
        InvalidOperationException? ex = Assert.Throws<InvalidOperationException>(
            () => empty.ToMermaidString()
        );

        // Assert
        Assert.NotNull(ex);
        Assert.StartsWith("At least one style class element should be set.", ex.Message);
    }

    [Fact]
    public void StyleClass_WhenMemberWiseEqual_ShouldEqual()
    {
        // Arrange (two complete style class definitions)
        StyleClass first = new(
            new Fill(Color.FromRGB(85, 170, 255)),
            new Stroke(Color.FromRGB(170, 255, 85)),
            new DashArray([
                DashSize.Length(5, Unit.Px),
                DashSize.Number(2)
            ]),
            DashOffset.Number(3),
            StrokeWidth.Length(3, Unit.Px),
            new StrokeLineCap(StrokeLineCapType.Round),
            new StrokeLineJoin(StrokeLineJoinType.Round),
            new StyleColor(Color.FromRGB(255, 85, 170)),
            new StyleOpacity(0.75),
            new FontFamily([
                new FontFamilyComponent("Gill Sans"),
                new FontFamilyComponent("sans-serif")
            ]),
            FontSize.Length(1.25, Unit.Rem),
            FontWeight.Relative(FontWeightType.Bold)
        );
        StyleClass second = new(
            new Fill(Color.FromRGB(85, 170, 255)),
            new Stroke(Color.FromRGB(170, 255, 85)),
            new DashArray([
                DashSize.Length(5, Unit.Px),
                DashSize.Number(2)
            ]),
            DashOffset.Number(3),
            StrokeWidth.Length(3, Unit.Px),
            new StrokeLineCap(StrokeLineCapType.Round),
            new StrokeLineJoin(StrokeLineJoinType.Round),
            new StyleColor(Color.FromRGB(255, 85, 170)),
            new StyleOpacity(0.75),
            new FontFamily([
                new FontFamilyComponent("Gill Sans"),
                new FontFamilyComponent("sans-serif")
            ]),
            FontSize.Length(1.25, Unit.Rem),
            FontWeight.Relative(FontWeightType.Bold)
        );

        // Assert
        Assert.Equal(first, second);
    }

    [Fact]
    public void StyleClass_ToMermaidString()
    {
        // Arrange (two complete style class definitions)
        StyleClass styleClass = new(
            new Fill(Color.FromRGB(0, 0, 0)),
            new Stroke(Color.FromRGB(255, 255, 255)),
            new DashArray([
                DashSize.Length(5, Unit.Px),
                DashSize.Number(2)
            ]),
            DashOffset.Number(3),
            StrokeWidth.Length(3, Unit.Px),
            new StrokeLineCap(StrokeLineCapType.Round),
            new StrokeLineJoin(StrokeLineJoinType.Round),
            new StyleColor(Color.FromRGB(170, 170, 170)),
            new StyleOpacity(0.75),
            new FontFamily([
                new FontFamilyComponent("Gill Sans"),
                new FontFamilyComponent("sans-serif")
            ]),
            FontSize.Length(1.25, Unit.Rem),
            FontWeight.Relative(FontWeightType.Bold)
        );
        string expected = "fill:#000000,stroke:#ffffff,stroke-dasharray: 5px 2,stroke-dashoffset: 3,stroke-width:3px,stroke-linecap:round,stroke-linejoin:round,color:#aaaaaa,opacity:0.75,font-family:Gill Sans\\,sans-serif,font-size:1.25rem,font-weight:bold";

        // Act
        string mermaid = styleClass.ToMermaidString();

        // Assert
        Assert.Equal(expected, mermaid);
    }
}
