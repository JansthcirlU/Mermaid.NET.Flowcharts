using Mermaid.Flowcharts.Nodes;
using Mermaid.Flowcharts.Styling;
using Mermaid.Flowcharts.Styling.Attributes;
using Mermaid.Flowcharts.Styling.Attributes.Enums;
using Mermaid.Flowcharts.Styling.Attributes.Fonts;

namespace Mermaid.Flowcharts.Tests;

public class NodeStyleTests
{
    [Fact]
    public void NodeStyles_WhenNameAndValuesAreMemberwiseEqual_ShouldEqual()
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
        NodeStyle firstStyle = new("style", first);
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
        NodeStyle secondStyle = new("style", second);

        // Assert
        Assert.Equal(firstStyle, secondStyle);
    }
}