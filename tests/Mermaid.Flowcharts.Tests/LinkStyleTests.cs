namespace Mermaid.Flowcharts.Tests;

public class LinkStyleTests
{
    [Theory]
    [InlineData(LinkArrowType.Arrow, LinkDirection.LeftToRight, LinkThickness.Thick, "===>")]
    [InlineData(LinkArrowType.Arrow, LinkDirection.LeftToRight, LinkThickness.Dotted, "-.->")]
    [InlineData(LinkArrowType.Arrow, LinkDirection.RightToLeft, LinkThickness.Dotted, "<-.-")]
    [InlineData(LinkArrowType.Circle, LinkDirection.Both, LinkThickness.Dotted, "o-.-o")]
    [InlineData(LinkArrowType.Arrow, LinkDirection.Both, LinkThickness.Invisible, "~~~")]
    public void LinkStyle_ToString(LinkArrowType arrowType, LinkDirection direction, LinkThickness thickness, string expected)
    {
        // Arrange
        LinkStyle linkStyle = new(arrowType, direction, thickness);

        // Act
        string actual = linkStyle.ToString();

        // Assert
        Assert.Equal(expected, actual);
    }
}
