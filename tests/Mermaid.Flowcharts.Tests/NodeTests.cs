namespace Mermaid.Flowcharts.Tests;

public class NodeTests
{
    [Theory]
    [InlineData("a", "b", "a [\"b\"]")]
    [InlineData("A.B", "あ", "A.B [\"あ\"]")]
    public void NodeToString_ShouldBeRectangular_ByDefault(string identifier, string text, string expected)
    {
        // Arrange
        Node node = Node.Create(identifier, text);

        // Act
        string nodeString = node.ToString();

        // Assert
        Assert.Equal(expected, nodeString);
    }
}
