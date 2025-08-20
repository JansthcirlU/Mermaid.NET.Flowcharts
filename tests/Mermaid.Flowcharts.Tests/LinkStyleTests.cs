using Mermaid.Flowcharts.Links;

namespace Mermaid.Flowcharts.Tests;

public class LinkStyleTests
{
    [Theory]
    [InlineData(LinkArrowType.Arrow, LinkDirection.FromTo, LinkThickness.Thick, "===>")]
    [InlineData(LinkArrowType.Arrow, LinkDirection.FromTo, LinkThickness.Dotted, "-.->")]
    [InlineData(LinkArrowType.Arrow, LinkDirection.ToFrom, LinkThickness.Dotted, "<-.-")]
    [InlineData(LinkArrowType.Circle, LinkDirection.Both, LinkThickness.Dotted, "o-.-o")]
    [InlineData(LinkArrowType.Arrow, LinkDirection.Both, LinkThickness.Invisible, "~~~")]
    public void LinkStyle_ToMermaidString(LinkArrowType arrowType, LinkDirection direction, LinkThickness thickness, string expected)
    {
        // Arrange
        LinkStyle linkStyle = LinkStyle.Create(arrowType, direction, thickness);

        // Act
        string actual = linkStyle.ToMermaidString();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(LinkArrowType.Arrow, LinkDirection.FromTo, LinkThickness.Thick, 2, "  ", "    ===>")]
    public void LinkStyle_ToMermaidString_WhenIndentation(LinkArrowType arrowType, LinkDirection direction, LinkThickness thickness, int indentations, string indentationText, string expected)
    {
        // Arrange
        LinkStyle linkStyle = LinkStyle.Create(arrowType, direction, thickness);

        // Act
        string actual = linkStyle.ToMermaidString(indentations, indentationText);

        // Assert
        Assert.Equal(expected, actual);
    }
}
