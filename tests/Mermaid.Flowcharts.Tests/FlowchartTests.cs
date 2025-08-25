using Mermaid.Flowcharts.Links;
using Mermaid.Flowcharts.Nodes;
using Mermaid.Flowcharts.Nodes.NodeText;
using Mermaid.Flowcharts.Styling;
using Mermaid.Flowcharts.Styling.Attributes;
using Mermaid.Flowcharts.Subgraphs;

namespace Mermaid.Flowcharts.Tests;

public class FlowchartTests
{
    [Fact]
    public void Flowchart_ShouldContainOneNode_WhenTryingToAddTheSameNodeMultipleTimes()
    {
        // Arrange
        Flowchart flowchart = new();
        Node node = Node.CreateNew<MermaidUnicodeText>(Guid.NewGuid().ToString());

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
        Node node1 = Node.Create<MermaidUnicodeText>(randomId, Guid.NewGuid().ToString());
        Node node2 = Node.Create<MermaidUnicodeText>(randomId, Guid.NewGuid().ToString());

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
        Node source = Node.CreateNew<MermaidUnicodeText>(Guid.NewGuid().ToString());
        Node destination = Node.CreateNew<MermaidUnicodeText>(Guid.NewGuid().ToString());
        Link link = Link.Create(source, destination);

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
        Node source = Node.CreateNew<MermaidUnicodeText>(Guid.NewGuid().ToString());
        Node destination = Node.CreateNew<MermaidUnicodeText>(Guid.NewGuid().ToString());
        Link link = Link.Create(source, destination);

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
        Node node1 = Node.Create<MermaidUnicodeText>(node1Id, node1Text);
        flowchart.AddNode(node1);

        string node2Id = Guid.NewGuid().ToString();
        string node2Text = Guid.NewGuid().ToString();
        Node node2 = Node.Create<MermaidUnicodeText>(node2Id, node2Text);
        flowchart.AddNode(node2);

        string subgraphId = Guid.NewGuid().ToString();
        string subgraphText = Guid.NewGuid().ToString();
        Subgraph subgraph = Subgraph.Create<MermaidUnicodeText>(subgraphId, subgraphText);

        string subnodeId = Guid.NewGuid().ToString();
        string subnodeText = Guid.NewGuid().ToString();
        Node subnode = Node.Create<MermaidUnicodeText>(subnodeId, subnodeText);
        subgraph.AddNode(subnode);
        flowchart.AddNode(subgraph);

        Link link = Link.Create(node1, node2);
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
        Node node1 = Node.Create<MermaidUnicodeText>(node1Id, node1Text);
        flowchart.AddNode(node1);

        string node2Id = Guid.NewGuid().ToString();
        string node2Text = Guid.NewGuid().ToString();
        Node node2 = Node.Create<MermaidUnicodeText>(node2Id, node2Text);
        flowchart.AddNode(node2);

        string subgraph1Id = Guid.NewGuid().ToString();
        string subgraph1Text = Guid.NewGuid().ToString();
        Subgraph subgraph1 = Subgraph.Create<MermaidUnicodeText>(subgraph1Id, subgraph1Text);

        string subnodeId = Guid.NewGuid().ToString();
        string subnodeText = Guid.NewGuid().ToString();
        Node subnode = Node.Create<MermaidUnicodeText>(subnodeId, subnodeText);
        subgraph1.AddNode(subnode);

        string subgraph2Id = Guid.NewGuid().ToString();
        string subgraph2Text = Guid.NewGuid().ToString();
        Subgraph subgraph2 = Subgraph.Create<MermaidUnicodeText>(subgraph2Id, subgraph2Text);

        string subnode2Id = Guid.NewGuid().ToString();
        string subnode2Text = Guid.NewGuid().ToString();
        Node subnode2 = Node.Create<MermaidUnicodeText>(subnode2Id, subnode2Text);
        subgraph2.AddNode(subnode2);

        subgraph1.AddNode(subgraph2);

        flowchart.AddNode(subgraph1);

        Link link = Link.Create(node1, node2);
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

    [Fact]
    public void Flowchart_WhenOneNodeOneStyle_ToMermaidString()
    {
        // Arrange
        Flowchart flowchart = new();
        string nodeId = Guid.NewGuid().ToString();
        string nodeText = Guid.NewGuid().ToString();
        StyleClass nodeStyleClass = new(Fill: new Fill(Color.FromHex("#ff9966")));
        NodeStyle nodeStyle = new("customStyle", nodeStyleClass);
        Node node = Node.Create<MermaidUnicodeText>(nodeId, nodeText, nodeStyle: nodeStyle);
        flowchart.AddNode(node);
        string expected =
        $"""
        flowchart TD
            {nodeId}["{nodeText}"]

            classDef customStyle fill:#ff9966
            class {nodeId} customStyle

        """;

        // Act
        string actual = flowchart.ToMermaidString(0, "    ");

        // Assert
        Assert.Equal(expected, actual);
    }
}