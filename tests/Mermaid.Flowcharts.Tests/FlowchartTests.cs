using Mermaid.Flowcharts.Links;
using Mermaid.Flowcharts.Nodes;

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
}