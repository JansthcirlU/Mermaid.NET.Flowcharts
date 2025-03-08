using Mermaid.Flowcharts.Nodes;
using Mermaid.Flowcharts.Subgraphs;

namespace Mermaid.Flowcharts.Tests;

public class SubgraphTests
{
    [Fact]
    public void Subgraph_ShouldContainOneNode_WhenTryingToAddTheSameNodeMultipleTimes()
    {
        // Arrange
        NodeIdentifier subgraphId = NodeIdentifier.FromString("SubgraphId");
        MermaidUnicodeText subgraphTitle = MermaidUnicodeText.FromString("Subgraph Title");
        Subgraph subgraph = new(subgraphId, subgraphTitle);
        string randomId = Guid.NewGuid().ToString();
        string randomText = Guid.NewGuid().ToString();
        Node node = Node.Create(randomId, randomText);

        // Act
        subgraph.AddNode(node);
        subgraph.AddNode(node);

        // Assert
        Assert.NotEmpty(subgraph.Nodes);
        Assert.Single(subgraph.Nodes);
    }

    [Fact]
    public void Subgraph_ShouldContainTwoNodes_WhenTryingToAddTwoNodesWithDuplicateIdsButDifferentValues()
    {
        // Arrange
        NodeIdentifier subgraphId = NodeIdentifier.FromString("SubgraphId");
        MermaidUnicodeText subgraphTitle = MermaidUnicodeText.FromString("Subgraph Title");
        Subgraph subgraph = new(subgraphId, subgraphTitle);
        string randomId = Guid.NewGuid().ToString();
        string randomText1 = Guid.NewGuid().ToString();
        string randomText2 = Guid.NewGuid().ToString();
        Node node1 = Node.Create(randomId, randomText1);
        Node node2 = Node.Create(randomId, randomText2);

        // Act
        subgraph.AddNode(node1);
        subgraph.AddNode(node2);

        // Assert
        Assert.NotEmpty(subgraph.Nodes);
        Assert.Equal(2, subgraph.Nodes.Count());
    }

    [Theory]
    [InlineData(
        "a",
        "b",
        0,
        "  ",
        """
        subgraph a ["b"]
        end
        """)]
    [InlineData(
        "a",
        "b",
        2,
        "  ",
        """
            subgraph a ["b"]
            end
        """)]
    [InlineData(
        "A.B",
        "あ",
        0,
        "  ",
        """
        subgraph A.B ["あ"]
        end
        """
    )]
    [InlineData(
        "A.B",
        "あ",
        3,
        "  ",
        """
              subgraph A.B ["あ"]
              end
        """
    )]
    public void SubgraphToMermaidString_WhenEmpty(string identifier, string title, int indentations, string indentationText, string expected)
    {
        // Arrange
        NodeIdentifier subgraphIdentifier = NodeIdentifier.FromString(identifier);
        MermaidUnicodeText subgraphTitle = MermaidUnicodeText.FromString(title);
        Subgraph subgraph = new(subgraphIdentifier, subgraphTitle);

        // Act
        string subgraphString = subgraph.ToMermaidString(indentations, indentationText);

        // Assert
        Assert.Equal(expected, subgraphString);
    }

    [Theory]
    [InlineData(
        "SubgraphId",
        "SG",
        "NodeId",
        "Node",
        """
        subgraph SubgraphId ["SG"]
          NodeId["Node"]
        end
        """
    )]
    public void SubgraphToMermaidString_WhenHasOneNode(string identifier, string title, string nodeId, string nodeText, string expected)
    {
        // Arrange
        Subgraph subgraph = new(NodeIdentifier.FromString(identifier), MermaidUnicodeText.FromString(title));
        Node node = Node.Create(nodeId, nodeText);
        subgraph.AddNode(node);

        // Act
        string actual = subgraph.ToMermaidString();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(
        "SubgraphId",
        "SG",
        "NodeId",
        "Node",
        2,
        "  ",
        """
            subgraph SubgraphId ["SG"]
              NodeId["Node"]
            end
        """
    )]
    [InlineData(
        "SubgraphId",
        "SG",
        "NodeId",
        "Node",
        1,
        "    ",
        """
            subgraph SubgraphId ["SG"]
                NodeId["Node"]
            end
        """
    )]
    public void ToMermaidString_WhenIndentations(string identifier, string title, string nodeId, string nodeText, int indentations, string indentationText, string expected)
    {
        // Arrange
        Subgraph subgraph = new(NodeIdentifier.FromString(identifier), MermaidUnicodeText.FromString(title));
        Node node = Node.Create(nodeId, nodeText);
        subgraph.AddNode(node);

        // Act
        string actual = subgraph.ToMermaidString(indentations, indentationText);

        // Assert
        Assert.Equal(expected, actual);
    }
}