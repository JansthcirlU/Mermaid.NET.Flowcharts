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
    public void Flowchart_ShouldContainTwoNodes_WhenTryingToAddTwoNodesWithDuplicateIdsButDifferentValues()
    {
        // Arrange
        Flowchart flowchart = new();
        string randomId = Guid.NewGuid().ToString();
        string randomText1 = Guid.NewGuid().ToString();
        string randomText2 = Guid.NewGuid().ToString();
        Node node1 = Node.Create(randomId, randomText1);
        Node node2 = Node.Create(randomId, randomText2);

        // Act
        flowchart.AddNode(node1);
        flowchart.AddNode(node2);

        // Assert
        Assert.NotEmpty(flowchart.Nodes);
        Assert.Equal(2, flowchart.Nodes.Count());
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

        string node1Id = Guid.NewGuid().ToString();
        string node1Text = Guid.NewGuid().ToString();
        Node node1 = Node.Create(node1Id, node1Text);
        flowchart.AddNode(node1);

        string node2Id = Guid.NewGuid().ToString();
        string node2Text = Guid.NewGuid().ToString();
        Node node2 = Node.Create(node2Id, node2Text);
        flowchart.AddNode(node2);

        string subgraphId = Guid.NewGuid().ToString();
        string subgraphText = Guid.NewGuid().ToString();
        Subgraph subgraph = new(NodeIdentifier.FromString(subgraphId), MermaidUnicodeText.FromString(subgraphText));

        string subnodeId = Guid.NewGuid().ToString();
        string subnodeText = Guid.NewGuid().ToString();
        Node subnode = Node.Create(subnodeId, subnodeText);
        subgraph.AddNode(subnode);
        flowchart.AddNode(subgraph);

        Link link = new(node1, node2, new());
        flowchart.AddLink(link);
        
        string expected =
        $"""
        flowchart TD
            {node1Id}["{node1Text}"]
            {node2Id}["{node2Text}"]

            subgraph {subgraphId} ["{subgraphText}"]
                {subnodeId}["{subnodeText}"]
            end

            {node1Id} ---> {node2Id}

        """;

        // Act
        string actual = flowchart.ToMermaidString(0, "    ");

        // Assert
        Assert.Equal(
            expected,
            actual
        );
    }

    [Fact]
    public void Flowchart_ToMermaidString_WhenNestedSubgraphsAndLink()
    {
        // Arrange
        Flowchart flowchart = new();

        string node1Id = Guid.NewGuid().ToString();
        string node1Text = Guid.NewGuid().ToString();
        Node node1 = Node.Create(node1Id, node1Text);
        flowchart.AddNode(node1);

        string node2Id = Guid.NewGuid().ToString();
        string node2Text = Guid.NewGuid().ToString();
        Node node2 = Node.Create(node2Id, node2Text);
        flowchart.AddNode(node2);

        string subgraph1Id = Guid.NewGuid().ToString();
        string subgraph1Text = Guid.NewGuid().ToString();
        Subgraph subgraph1 = new(NodeIdentifier.FromString(subgraph1Id), MermaidUnicodeText.FromString(subgraph1Text));

        string subnodeId = Guid.NewGuid().ToString();
        string subnodeText = Guid.NewGuid().ToString();
        Node subnode = Node.Create(subnodeId, subnodeText);
        subgraph1.AddNode(subnode);

        string subgraph2Id = Guid.NewGuid().ToString();
        string subgraph2Text = Guid.NewGuid().ToString();
        Subgraph subgraph2 = new(NodeIdentifier.FromString(subgraph2Id), MermaidUnicodeText.FromString(subgraph2Text));

        string subnode2Id = Guid.NewGuid().ToString();
        string subnode2Text = Guid.NewGuid().ToString();
        Node subnode2 = Node.Create(subnode2Id, subnode2Text);
        subgraph2.AddNode(subnode2);

        subgraph1.AddNode(subgraph2);

        flowchart.AddNode(subgraph1);

        Link link = new(node1, node2, new());
        flowchart.AddLink(link);
        string expected =
        $"""
        flowchart TD
            {node1Id}["{node1Text}"]
            {node2Id}["{node2Text}"]

            subgraph {subgraph1Id} ["{subgraph1Text}"]
                {subnodeId}["{subnodeText}"]

                subgraph {subgraph2Id} ["{subgraph2Text}"]
                    {subnode2Id}["{subnode2Text}"]
                end
            end

            {node1Id} ---> {node2Id}

        """;
        
        // Act
        string actual = flowchart.ToMermaidString(0, "    ");

        // Assert
        Assert.Equal(expected, actual);
    }
}