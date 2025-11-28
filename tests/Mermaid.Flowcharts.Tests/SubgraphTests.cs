using Mermaid.Flowcharts.Links;
using Mermaid.Flowcharts.Nodes;
using Mermaid.Flowcharts.Nodes.NodeText;
using Mermaid.Flowcharts.Subgraphs;

namespace Mermaid.Flowcharts.Tests;

public class SubgraphTests
{
    [Fact]
    public void Subgraph_ShouldContainOneNode_WhenTryingToAddTheSameNodeMultipleTimes()
    {
        // Arrange
        Subgraph subgraph = Subgraph.CreateNew<MermaidUnicodeText>(Guid.NewGuid().ToString(), SubgraphDirection.TB);
        Node node = Node.CreateNew<MermaidUnicodeText>(Guid.NewGuid().ToString());

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
        Subgraph subgraph = Subgraph.CreateNew<MermaidUnicodeText>(Guid.NewGuid().ToString(), SubgraphDirection.TB);
        string randomId = Guid.NewGuid().ToString();
        Node node1 = Node.Create<MermaidUnicodeText>(randomId, Guid.NewGuid().ToString());
        Node node2 = Node.Create<MermaidUnicodeText>(randomId, Guid.NewGuid().ToString());

        // Act
        subgraph.AddNode(node1);
        subgraph.AddNode(node2);

        // Assert
        Assert.NotEmpty(subgraph.Nodes);
        Assert.Equal(2, subgraph.Nodes.Count());
    }

    [Fact]
    public void Subgraph_ShouldAddLinkNode_WhenNodesAreNotPartOfSubgraphs()
    {
        // Arrange
        Subgraph subgraph = Subgraph.CreateNew<MermaidUnicodeText>("title");
        Node source = Node.CreateNew<MermaidUnicodeText>(Guid.NewGuid().ToString());
        Node destination = Node.CreateNew<MermaidUnicodeText>(Guid.NewGuid().ToString());
        Link link = Link.Create(source, destination);

        // Act
        subgraph.AddLink(link);

        // Assert
        Assert.NotEmpty(subgraph.Nodes);
        Assert.Equal(2, subgraph.Nodes.Count());
    }

    [Fact]
    public void Subgraph_ShouldNotAddLinkNode_WhenLinkNodesAlreadyExist()
    {
        // Arrange
        Subgraph subgraph = Subgraph.CreateNew<MermaidUnicodeText>("title");
        Node source = Node.CreateNew<MermaidUnicodeText>(Guid.NewGuid().ToString());
        Node destination = Node.CreateNew<MermaidUnicodeText>(Guid.NewGuid().ToString());
        Link link = Link.Create(source, destination);

        // Act
        subgraph.AddNode(source);
        subgraph.AddLink(link);

        // Assert
        Assert.NotEmpty(subgraph.Nodes);
        Assert.Equal(2, subgraph.Nodes.Count());
    }

    [Fact]
    public void Subgraph_ToMermaidString_WhenSubgraphAndNodes()
    {
        // Arrange
        string subgraphId = Guid.NewGuid().ToString();
        string subgraphTitle = Guid.NewGuid().ToString();
        Subgraph subgraph = Subgraph.Create<MermaidUnicodeText>(subgraphId, subgraphTitle);

        string node1Id = Guid.NewGuid().ToString();
        string node1Text = Guid.NewGuid().ToString();
        Node node1 = Node.Create<MermaidUnicodeText>(node1Id, node1Text);

        string node2Id = Guid.NewGuid().ToString();
        string node2Text = Guid.NewGuid().ToString();
        Node node2 = Node.Create<MermaidUnicodeText>(node2Id, node2Text);

        Link link = Link.Create(node1, node2);
        subgraph.AddLink(link);

        string subsubgraphId = Guid.NewGuid().ToString();
        string subsubgraphTitle = Guid.NewGuid().ToString();
        Subgraph subsubgraph = Subgraph.Create<MermaidUnicodeText>(subsubgraphId, subsubgraphTitle);

        string subnodeId = Guid.NewGuid().ToString();
        string subnodeText = Guid.NewGuid().ToString();
        Node subnode = Node.Create<MermaidUnicodeText>(subnodeId, subnodeText);
        subsubgraph.AddNode(subnode);
        subgraph.AddNode(subsubgraph);

        string expected =
        $"""
        subgraph {subgraphId} ["{subgraphTitle}"]
            {node1Id}["{node1Text}"]
            {node2Id}["{node2Text}"]

            subgraph {subsubgraphId} ["{subsubgraphTitle}"]
                {subnodeId}["{subnodeText}"]
            end

            {node1Id} ---> {node2Id}
        end
        """;

        // Act
        string actual = subgraph.ToMermaidString(0, "    ");

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Subgraph_ToMermaidString_WhenNodeLinkedToSubgraph()
    {
        // Arrange
        string subgraphId = Guid.NewGuid().ToString();
        string subgraphTitle = Guid.NewGuid().ToString();
        Subgraph subgraph = Subgraph.Create<MermaidUnicodeText>(subgraphId, subgraphTitle);

        string nodeId = Guid.NewGuid().ToString();
        string nodeText = Guid.NewGuid().ToString();
        Node node = Node.Create<MermaidUnicodeText>(nodeId, nodeText);

        string subsubgraphId = Guid.NewGuid().ToString();
        string subsubgraphTitle = Guid.NewGuid().ToString();
        Subgraph subsubgraph = Subgraph.Create<MermaidUnicodeText>(subsubgraphId, subsubgraphTitle);

        string subnode1Id = Guid.NewGuid().ToString();
        string subnode1Text = Guid.NewGuid().ToString();
        Node subnode1 = Node.Create<MermaidUnicodeText>(subnode1Id, subnode1Text);

        string subnode2Id = Guid.NewGuid().ToString();
        string subnode2Text = Guid.NewGuid().ToString();
        Node subnode2 = Node.Create<MermaidUnicodeText>(subnode2Id, subnode2Text);

        Link sublink = Link.Create(subnode1, subnode2);
        subsubgraph.AddLink(sublink);

        Link link = Link.Create(node, subsubgraph);
        subgraph.AddLink(link);

        string expected =
        $"""
        subgraph {subgraphId} ["{subgraphTitle}"]
            {nodeId}["{nodeText}"]

            subgraph {subsubgraphId} ["{subsubgraphTitle}"]
                {subnode1Id}["{subnode1Text}"]
                {subnode2Id}["{subnode2Text}"]

                {subnode1Id} ---> {subnode2Id}
            end

            {nodeId} ---> {subsubgraphId}
        end
        """;

        // Act
        string actual = subgraph.ToMermaidString(0, "    ");

        // Assert
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(
        "a",
        "b",
        SubgraphDirection.TB,
        0,
        "  ",
        """
        subgraph a ["b"]
          direction TB
        end
        """)]
    [InlineData(
        "a",
        "b",
        SubgraphDirection.BT,
        2,
        "  ",
        """
            subgraph a ["b"]
              direction BT
            end
        """)]
    [InlineData(
        "A.B",
        "あ",
        SubgraphDirection.LR,
        0,
        "  ",
        """
        subgraph A.B ["あ"]
          direction LR
        end
        """
    )]
    [InlineData(
        "A.B",
        "あ",
        SubgraphDirection.RL,
        3,
        "  ",
        """
              subgraph A.B ["あ"]
                direction RL
              end
        """
    )]
    public void SubgraphToMermaidString_WhenEmpty(string identifier, string title, SubgraphDirection direction, int indentations, string indentationText, string expected)
    {
        // Arrange
        Subgraph subgraph = Subgraph.Create<MermaidUnicodeText>(identifier, title, direction);

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
          direction TB
          NodeId["Node"]
        end
        """
    )]
    public void SubgraphToMermaidString_WhenHasOneNode(string identifier, string title, string nodeId, string nodeText, string expected)
    {
        // Arrange
        Subgraph subgraph = Subgraph.Create<MermaidUnicodeText>(identifier, title, SubgraphDirection.TB);
        Node node = Node.Create<MermaidUnicodeText>(nodeId, nodeText);
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
        SubgraphDirection.TB,
        2,
        "  ",
        """
            subgraph SubgraphId ["SG"]
              direction TB
              NodeId["Node"]
            end
        """
    )]
    [InlineData(
        "SubgraphId",
        "SG",
        "NodeId",
        "Node",
        SubgraphDirection.LR,
        1,
        "    ",
        """
            subgraph SubgraphId ["SG"]
                direction LR
                NodeId["Node"]
            end
        """
    )]
    public void ToMermaidString_WhenIndentations(string identifier, string title, string nodeId, string nodeText, SubgraphDirection direction, int indentations, string indentationText, string expected)
    {
        // Arrange
        Subgraph subgraph = Subgraph.Create<MermaidUnicodeText>(identifier, title, direction);
        Node node = Node.Create<MermaidUnicodeText>(nodeId, nodeText);
        subgraph.AddNode(node);

        // Act
        string actual = subgraph.ToMermaidString(indentations, indentationText);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void CreateNew_WhenNonGeneric_ShouldHaveGuidIdentifier()
    {
        // Arrange
        Subgraph subgraph = Subgraph.CreateNew("text");

        // Act
        bool parsed = Guid.TryParseExact(subgraph.Id.Value, "D", out Guid guid);

        // Assert
        Assert.True(parsed);
        Assert.True(guid != Guid.Empty);
    }

    [Fact]
    public void Create_WhenNonGeneric_ShouldBeUnicode()
    {
        // Arrange
        Subgraph subgraph = Subgraph.Create("sg", "SG");

        // Act
        INodeText title = subgraph.Title;

        // Assert
        Assert.True(title is MermaidUnicodeText);
        Assert.Equal("SG", title.Value);
    }
}
