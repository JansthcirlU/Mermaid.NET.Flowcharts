using Mermaid.Flowcharts.Nodes;

namespace Mermaid.Flowcharts.Tests;

public class NodeTests
{
    [Theory]
    [InlineData("a", "b", NodeShape.Rectangle, "a[\"b\"]")]
    [InlineData("a", "b", NodeShape.RoundedEdges, "a(\"b\")")]
    [InlineData("a", "b", NodeShape.Stadium, "a([\"b\"])")]
    [InlineData("a", "b", NodeShape.Subroutine, "a[[\"b\"]]")]
    [InlineData("a", "b", NodeShape.Cylindrical, "a[(\"b\")]")]
    [InlineData("a", "b", NodeShape.Circle, "a((\"b\"))")]
    [InlineData("a", "b", NodeShape.DoubleCircle, "a(((\"b\")))")]
    [InlineData("a", "b", NodeShape.Asymmetric, "a>\"b\"]")]
    [InlineData("a", "b", NodeShape.Rhombus, "a{\"b\"}")]
    [InlineData("a", "b", NodeShape.Hexagon, "a{{\"b\"}}")]
    [InlineData("a", "b", NodeShape.Parallelogram, "a[/\"b\"/]")]
    [InlineData("a", "b", NodeShape.ParallelogramAlt, "a[\\\"b\"\\]")]
    [InlineData("a", "b", NodeShape.Trapezoid, "a[/\"b\"\\]")]
    [InlineData("a", "b", NodeShape.TrapezoidAlt, "a[\\\"b\"/]")]
    public void NodeToMermaidString_ShouldRenderCorrectShape(string identifier, string text, NodeShape shape, string expected)
    {
        // Arrange
        Node node = Node.Create(identifier, text, shape);

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

    [Fact]
    public void CreateNew_ShouldHaveGuidIdentifier()
    {
        // Arrange
        Node node = Node.CreateNew("text");

        // Act
        bool parsed = Guid.TryParseExact(node.Id.Value, "D", out Guid guid);

        // Assert
        Assert.True(parsed);
        Assert.True(guid != Guid.Empty);
    }
}
