using Mermaid.Flowcharts.Links;
using Mermaid.Flowcharts.Nodes;
using Mermaid.Flowcharts.Nodes.NodeText;

namespace Mermaid.Flowcharts.Tests;

public class LinkTests
{
    [Theory]
    [InlineData("A", "a", "B", "b", "link", 2, " ", "  A --->|\"link\"| B")]
    public void ToMermaidString_WhenIndentations(string sourceId, string sourceText, string destinationId, string destinationText, string linkText, int indentations, string indentationText, string expected)
    {
        // Arrange
        Node source = Node.Create<MermaidUnicodeText>(sourceId, sourceText);
        Node destination = Node.Create<MermaidUnicodeText>(destinationId, destinationText);
        Link link = Link.Create(source, destination, linkText: linkText);

        // Act
        string actual = link.ToMermaidString(indentations, indentationText);

        // Assert
        Assert.Equal(expected, actual);
    }
}
