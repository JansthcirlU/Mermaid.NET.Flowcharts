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
}
