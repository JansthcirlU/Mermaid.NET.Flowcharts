using Mermaid.Flowcharts.Links;
using Mermaid.Flowcharts.Nodes;

namespace Mermaid.Flowcharts.Tests;

public class LinkTests
{
    [Theory]
    [InlineData("A", "a", "B", "b", 2, " ", "  A ---> B")]
    public void ToMermaidString_WhenIndentations(string sourceId, string sourceText, string destinationId, string destinationText, int indentations, string indentationText, string expected)
    {
        // Arrange
        Node source = Node.Create(sourceId, sourceText);
        Node destination = Node.Create(destinationId, destinationText);
        Link link = Link.Create(source, destination);

        // Act
        string actual = link.ToMermaidString(indentations, indentationText);

        // Assert
        Assert.Equal(expected, actual);
    }
}