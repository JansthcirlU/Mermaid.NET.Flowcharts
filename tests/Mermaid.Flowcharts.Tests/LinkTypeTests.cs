using Mermaid.Flowcharts.Links;

namespace Mermaid.Flowcharts.Tests;

public class LinkTypeTests
{
    [Theory]
    [InlineData(LinkArrowType.Arrow, LinkDirection.FromTo, LinkThickness.Thick, "===>")]
    [InlineData(LinkArrowType.Arrow, LinkDirection.FromTo, LinkThickness.Dotted, "-.->")]
    [InlineData(LinkArrowType.Arrow, LinkDirection.ToFrom, LinkThickness.Dotted, "<-.-")]
    [InlineData(LinkArrowType.Circle, LinkDirection.Both, LinkThickness.Dotted, "o-.-o")]
    [InlineData(LinkArrowType.Arrow, LinkDirection.Both, LinkThickness.Invisible, "~~~")]
    public void LinkType_ToMermaidString(LinkArrowType arrowType, LinkDirection direction, LinkThickness thickness, string expected)
    {
        // Arrange
        LinkType linkType = LinkType.Create(arrowType, direction, thickness);

        // Act
        string actual = linkType.ToMermaidString();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(LinkArrowType.Arrow, LinkDirection.FromTo, LinkThickness.Thick, 2, "  ", "    ===>")]
    public void LinkType_ToMermaidString_WhenIndentation(LinkArrowType arrowType, LinkDirection direction, LinkThickness thickness, int indentations, string indentationText, string expected)
    {
        // Arrange
        LinkType linkType = LinkType.Create(arrowType, direction, thickness);

        // Act
        string actual = linkType.ToMermaidString(indentations, indentationText);

        // Assert
        Assert.Equal(expected, actual);
    }
}
