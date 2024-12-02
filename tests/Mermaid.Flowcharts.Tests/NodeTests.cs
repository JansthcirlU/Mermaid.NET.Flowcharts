using Mermaid.Flowcharts.Nodes;

namespace Mermaid.Flowcharts.Tests;

public class NodeTests
{
    [Theory]
    [InlineData("a", "b", "a[\"b\"]")]
    [InlineData("A.B", "あ", "A.B[\"あ\"]")]
    public void NodeToMermaidString_ShouldBeRectangular_ByDefault(string identifier, string text, string expected)
    {
        // Arrange
        Node node = Node.Create(identifier, text);

        // Act
        string nodeString = node.ToMermaidString();

        // Assert
        Assert.Equal(expected, nodeString);
    }

    [Theory]
    [InlineData("a", "b", 3, " ", "   a[\"b\"]")]
    [InlineData("A.B", "あ", 3, "  ", "      A.B[\"あ\"]")]
    public void ToMermaidString_WhenIndentations(string identifier, string text, int indentations, string indentationText, string expected)
    {
        // Arrange
        Node node = Node.Create(identifier, text);

        // Act
        string actual = node.ToMermaidString(indentations, indentationText);

        // Assert
        Assert.Equal(expected, actual);
    }
}
