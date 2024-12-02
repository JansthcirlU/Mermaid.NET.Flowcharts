using Mermaid.Flowcharts.Nodes;
using Mermaid.Flowcharts.Subgraphs;

namespace Mermaid.Flowcharts.Tests;

public class SubgraphTests
{
    [Theory]
    [InlineData(
        "a",
        "b",
        """
        subgraph a ["b"]
        end
        """)]
    [InlineData(
        "A.B",
        "あ",
        """
        subgraph A.B ["あ"]
        end
        """
    )]
    public void SubgraphToMermaidString_WhenEmpty(string identifier, string title, string expected)
    {
        // Arrange
        NodeIdentifier subgraphIdentifier = NodeIdentifier.FromString(identifier);
        MermaidUnicodeText subgraphTitle = MermaidUnicodeText.FromString(title);
        Subgraph subgraph = new(subgraphIdentifier, subgraphTitle);

        // Act
        string subgraphString = subgraph.ToMermaidString();

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
        string subgraphString = subgraph.ToMermaidString();

        // Assert
        Assert.Equal(expected, subgraphString);
    }
}