using Mermaid.Flowcharts.Links;
using Mermaid.Flowcharts.Nodes;
using Mermaid.Flowcharts.Subgraphs;

namespace Mermaid.Flowcharts.Tests;

public class FlowchartTests
{
    [Fact]
    public void Flowchart_ShouldContainOneNode_WhenTryingToAddTheSameNodeMultipleTimes()
    {
        // Arrange
        Flowchart flowchart = new();
        string randomId = Guid.NewGuid().ToString();
        string randomText = Guid.NewGuid().ToString();
        Node node = Node.Create(randomId, randomText);

        // Act
        flowchart.AddNode(node);
        flowchart.AddNode(node);

        // Assert
        Assert.NotEmpty(flowchart.Nodes);
        Assert.Single(flowchart.Nodes);
    }

    [Fact]
    public void Flowchart_ShouldAddLinkNode_WhenNodesAreNotPartOfSubgraphs()
    {
        // Arrange
        Flowchart flowchart = new();
        Node source = Node.Create(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
        Node destination = Node.Create(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
        Link link = new(source, destination, new());

        // Act
        flowchart.AddLink(link);

        // Assert
        Assert.NotEmpty(flowchart.Nodes);
        Assert.Equal(2, flowchart.Nodes.Count());
    }

    [Fact]
    public void Flowchart_ShouldNotAddLinkNode_WhenLinkNodesAlreadyExist()
    {
        // Arrange
        Flowchart flowchart = new();
        Node source = Node.Create(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
        Node destination = Node.Create(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
        Link link = new(source, destination, new());

        // Act
        flowchart.AddNode(source);
        flowchart.AddLink(link);

        // Assert
        Assert.NotEmpty(flowchart.Nodes);
        Assert.Equal(2, flowchart.Nodes.Count());
    }

    [Fact]
    public void Flowchart_ToMermaidString_WhenSubgraphAndNodes()
    {
        // Arrange
        Flowchart flowchart = new();

        string nodeId = Guid.NewGuid().ToString();
        string nodeText = Guid.NewGuid().ToString();
        Node node = Node.Create(nodeId, nodeText);
        flowchart.AddNode(node);

        string subgraphId = Guid.NewGuid().ToString();
        string subgraphText = Guid.NewGuid().ToString();
        Subgraph subgraph = new(NodeIdentifier.FromString(subgraphId), MermaidUnicodeText.FromString(subgraphText));

        string subnodeId = Guid.NewGuid().ToString();
        string subnodeText = Guid.NewGuid().ToString();
        Node subnode = Node.Create(subnodeId, subnodeText);
        subgraph.AddNode(subnode);

        flowchart.AddNode(subgraph);
        string expected =
        $"""
        flowchart TD
            {nodeId}["{nodeText}"]

            subgraph {subgraphId} ["{subgraphText}"]
                {subnodeId}["{subnodeText}"]
            end

        """;

        // Act
        string actual = flowchart.ToMermaidString(0, "    ");

        // Assert
        Assert.Equal(
            expected,
            actual
        );
    }
}