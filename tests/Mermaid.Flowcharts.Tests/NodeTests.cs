using Mermaid.Flowcharts.Nodes;
using Mermaid.Flowcharts.Nodes.NodeText;

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
    public void NodeToMermaidString_WhenUnicodeText_ShouldRenderCorrectShape(string identifier, string text, NodeShape shape, string expected)
    {
        // Arrange
        Node node = Node.Create<MermaidUnicodeText>(identifier, text, shape);

        // Act
        string nodeString = node.ToMermaidString();

        // Assert
        Assert.Equal(expected, nodeString);
    }

    [Theory]
    [InlineData("a", "b", NodeShape.Rectangle, "a[\"`b`\"]")]
    [InlineData("a", "b", NodeShape.RoundedEdges, "a(\"`b`\")")]
    [InlineData("a", "b", NodeShape.Stadium, "a([\"`b`\"])")]
    [InlineData("a", "b", NodeShape.Subroutine, "a[[\"`b`\"]]")]
    [InlineData("a", "b", NodeShape.Cylindrical, "a[(\"`b`\")]")]
    [InlineData("a", "b", NodeShape.Circle, "a((\"`b`\"))")]
    [InlineData("a", "b", NodeShape.DoubleCircle, "a(((\"`b`\")))")]
    [InlineData("a", "b", NodeShape.Asymmetric, "a>\"`b`\"]")]
    [InlineData("a", "b", NodeShape.Rhombus, "a{\"`b`\"}")]
    [InlineData("a", "b", NodeShape.Hexagon, "a{{\"`b`\"}}")]
    [InlineData("a", "b", NodeShape.Parallelogram, "a[/\"`b`\"/]")]
    [InlineData("a", "b", NodeShape.ParallelogramAlt, "a[\\\"`b`\"\\]")]
    [InlineData("a", "b", NodeShape.Trapezoid, "a[/\"`b`\"\\]")]
    [InlineData("a", "b", NodeShape.TrapezoidAlt, "a[\\\"`b`\"/]")]
    public void NodeToMermaidString_WhenMarkdownText_ShouldRenderCorrectShape(string identifier, string text, NodeShape shape, string expected)
    {
        // Arrange
        Node node = Node.Create<MarkdownText>(identifier, text, shape);

        // Act
        string nodeString = node.ToMermaidString();

        // Assert
        Assert.Equal(expected, nodeString);
    }

    [Theory]
    [InlineData("a", "b", 3, " ", "   a[\"b\"]")]
    [InlineData("A.B", "あ", 3, "  ", "      A.B[\"あ\"]")]
    public void ToMermaidString_WhenUnicodeTextWithIndentations_ShouldRenderCorrectly(string identifier, string text, int indentations, string indentationText, string expected)
    {
        // Arrange
        Node node = Node.Create<MermaidUnicodeText>(identifier, text);

        // Act
        string actual = node.ToMermaidString(indentations, indentationText);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData("a", "b", 3, " ", "   a[\"`b`\"]")]
    [InlineData("A.B", "あ", 3, "  ", "      A.B[\"`あ`\"]")]
    public void ToMermaidString_WhenMarkdownTextWithIndentations_ShouldRenderCorrectly(string identifier, string text, int indentations, string indentationText, string expected)
    {
        // Arrange
        Node node = Node.Create<MarkdownText>(identifier, text);

        // Act
        string actual = node.ToMermaidString(indentations, indentationText);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void CreateNew_WhenUnicodeText_ShouldHaveGuidIdentifier()
    {
        // Arrange
        Node node = Node.CreateNew<MermaidUnicodeText>("text");

        // Act
        bool parsed = Guid.TryParseExact(node.Id.Value, "D", out Guid guid);

        // Assert
        Assert.True(parsed);
        Assert.True(guid != Guid.Empty);
    }

    [Fact]
    public void CreateNew_WhenMarkdownText_ShouldHaveGuidIdentifier()
    {
        // Arrange
        Node node = Node.CreateNew<MarkdownText>("text");

        // Act
        bool parsed = Guid.TryParseExact(node.Id.Value, "D", out Guid guid);

        // Assert
        Assert.True(parsed);
        Assert.True(guid != Guid.Empty);
    }

    [Fact]
    public void CreateNew_WhenNonGeneric_ShouldHaveGuidIdentifier()
    {
        // Arrange
        Node node = Node.CreateNew("text");

        // Act
        bool parsed = Guid.TryParseExact(node.Id.Value, "D", out Guid guid);

        // Assert
        Assert.True(parsed);
        Assert.True(guid != Guid.Empty);
    }

    [Fact]
    public void Create_WhenNonGeneric_ShouldBeUnicode()
    {
        // Arrange
        Node node = Node.Create("a", "A");

        // Act
        INodeText text = node.Text;

        // Assert
        Assert.True(text is MermaidUnicodeText);
        Assert.Equal("A", text.Value);
    }
}
