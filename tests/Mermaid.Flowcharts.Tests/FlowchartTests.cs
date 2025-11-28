using Mermaid.Flowcharts.Links;
using Mermaid.Flowcharts.Nodes;
using Mermaid.Flowcharts.Nodes.NodeText;
using Mermaid.Flowcharts.Styling;
using Mermaid.Flowcharts.Styling.Attributes;
using Mermaid.Flowcharts.Styling.Attributes.Enums;
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
    public void Flowchart_WhenLinkNodesExist_ShouldAddLink()
    {
        // Arrange
        Flowchart flowchart = new();
        Node n1 = Node.CreateNew("Node 1");
        Node n2 = Node.CreateNew("Node 2");
        Link link = Link.Create(n1, n2);

        // Act
        flowchart
            .AddNode(n1)
            .AddNode(n2)
            .AddLink(link);

        // Assert
        Assert.NotEmpty(flowchart.AllNodes);
        Assert.Equal(2, flowchart.AllNodes.Count());
        Assert.Single(flowchart.Links);
    }

    [Fact]
    public void Flowchart_WhenLinkNodesAreNestedButExist_ShouldAddLink()
    {
        // Arrange
        Flowchart flowchart = new();
        Subgraph sg1 = Subgraph.CreateNew("Subgraph 1");
        Node sg1Node = Node.CreateNew("Node 1");
        Subgraph sg2 = Subgraph.CreateNew("Subgraph 2");
        Node sg2Node = Node.CreateNew("Node 2");
        Link link = Link.Create(sg1Node, sg2Node);

        // Act
        flowchart
            .AddNode(sg1.AddNode(sg1Node))
            .AddNode(sg2.AddNode(sg2Node))
            .AddLink(link);

        // Assert
        Assert.NotEmpty(flowchart.AllNodes);
        Assert.Equal(2, flowchart.AllNodes.Count());
        Assert.Single(flowchart.Links);
    }

    [Fact]
    public void Flowchart_WhenLinkNodeIsSubgraphAndExists_ShouldAddLink()
    {
        // Arrange
        Flowchart flowchart = new();
        Node n = Node.CreateNew("n");
        Subgraph subgraph = Subgraph.CreateNew("Subgraph");
        Link link = Link.Create(n, subgraph);

        // Act
        flowchart
            .AddNode(n)
            .AddNode(subgraph)
            .AddLink(link);

        // Assert
        Assert.NotEmpty(flowchart.AllNodeChildren);
        Assert.Equal(2, flowchart.AllNodeChildren.Count());
        Assert.Single(flowchart.Links);
    }

    [Fact]
    public void Flowchart_WhenLinkSourceNotPresent_AddLinkShouldThrow()
    {
        // Arrange
        Flowchart flowchart = new();
        Node source = Node.CreateNew(Guid.NewGuid().ToString());
        Node destination = Node.CreateNew(Guid.NewGuid().ToString());
        Link link = Link.Create(source, destination);

        // Act & Assert
        flowchart.AddNode(destination); // Forgot to add source
        InvalidOperationException exception = Assert.Throws<InvalidOperationException>(
            () => flowchart.AddLink(link)
        );
        Assert.Contains("Cannot add link to flowchart: the source and the destination nodes should both be present within the flowchart.", exception.Message);
    }

    [Fact]
    public void Flowchart_WhenLinkDestinationNotPresent_AddLinkShouldThrow()
    {
        // Arrange
        Flowchart flowchart = new();
        Node source = Node.CreateNew(Guid.NewGuid().ToString());
        Node destination = Node.CreateNew(Guid.NewGuid().ToString());
        Link link = Link.Create(source, destination);

        // Act & Assert
        flowchart.AddNode(source); // Forgot to add destination
        InvalidOperationException exception = Assert.Throws<InvalidOperationException>(
            () => flowchart.AddLink(link)
        );
        Assert.Contains("Cannot add link to flowchart: the source and the destination nodes should both be present within the flowchart.", exception.Message);
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

    [Fact]
    public void Flowchart_WhenTwoNodesOneStyle_ToMermaidString()
    {
        // Arrange
        Flowchart flowchart = new();
        StyleClass nodeStyleClass = new(Fill: new Fill(Color.FromHex("#ff9966")));
        NodeStyle nodeStyle = new("customStyle", nodeStyleClass);
        string node1Id = Guid.NewGuid().ToString();
        string node1Text = Guid.NewGuid().ToString();
        Node node1 = Node.Create<MermaidUnicodeText>(node1Id, node1Text, nodeStyle: nodeStyle);
        string node2Id = Guid.NewGuid().ToString();
        string node2Text = Guid.NewGuid().ToString();
        Node node2 = Node.Create<MermaidUnicodeText>(node2Id, node2Text, nodeStyle: nodeStyle);
        flowchart
            .AddNode(node1)
            .AddNode(node2);
        string expected =
        $"""
        flowchart TD
            {node1Id}["{node1Text}"]
            {node2Id}["{node2Text}"]

            classDef customStyle fill:#ff9966
            class {node1Id},{node2Id} customStyle

        """;

        // Act
        string actual = flowchart.ToMermaidString(0, "    ");

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Flowchart_WhenTwoNodesTwoStyles_ToMermaidString()
    {
        // Arrange
        Flowchart flowchart = new();
        StyleClass node1StyleClass = new(Fill: new Fill(Color.FromHex("#ff9966")));
        NodeStyle node1Style = new("customStyle1", node1StyleClass);
        string node1Id = Guid.NewGuid().ToString();
        string node1Text = Guid.NewGuid().ToString();
        Node node1 = Node.Create<MermaidUnicodeText>(node1Id, node1Text, nodeStyle: node1Style);
        StyleClass node2StyleClass = new(Fill: new Fill(Color.FromHex("#9966ff")));
        NodeStyle node2Style = new("customStyle2", node2StyleClass);
        string node2Id = Guid.NewGuid().ToString();
        string node2Text = Guid.NewGuid().ToString();
        Node node2 = Node.Create<MermaidUnicodeText>(node2Id, node2Text, nodeStyle: node2Style);
        flowchart
            .AddNode(node1)
            .AddNode(node2);
        string expected =
        $"""
        flowchart TD
            {node1Id}["{node1Text}"]
            {node2Id}["{node2Text}"]

            classDef customStyle1 fill:#ff9966
            class {node1Id} customStyle1
            classDef customStyle2 fill:#9966ff
            class {node2Id} customStyle2

        """;

        // Act
        string actual = flowchart.ToMermaidString(0, "    ");

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Flowchart_WhenFourNodesTwoStyles_ToMermaidString()
    {
        // Arrange
        Flowchart flowchart = new();
        StyleClass styleClass1 = new(Fill: new Fill(Color.FromHex("#ff9966")));
        NodeStyle nodeStyle1 = new("customStyle1", styleClass1);
        string node1Id = Guid.NewGuid().ToString();
        string node1Text = Guid.NewGuid().ToString();
        Node node1 = Node.Create<MermaidUnicodeText>(node1Id, node1Text, nodeStyle: nodeStyle1);
        string node2Id = Guid.NewGuid().ToString();
        string node2Text = Guid.NewGuid().ToString();
        Node node2 = Node.Create<MermaidUnicodeText>(node2Id, node2Text, nodeStyle: nodeStyle1);
        StyleClass styleClass2 = new(Fill: new Fill(Color.FromHex("#9966ff")));
        NodeStyle nodeStyle2 = new("customStyle2", styleClass2);
        string node3Id = Guid.NewGuid().ToString();
        string node3Text = Guid.NewGuid().ToString();
        Node node3 = Node.Create<MermaidUnicodeText>(node3Id, node3Text, nodeStyle: nodeStyle2);
        string node4Id = Guid.NewGuid().ToString();
        string node4Text = Guid.NewGuid().ToString();
        Node node4 = Node.Create<MermaidUnicodeText>(node4Id, node4Text, nodeStyle: nodeStyle2);
        flowchart
            .AddNode(node1)
            .AddNode(node2)
            .AddNode(node3)
            .AddNode(node4);
        string expected =
        $"""
        flowchart TD
            {node1Id}["{node1Text}"]
            {node2Id}["{node2Text}"]
            {node3Id}["{node3Text}"]
            {node4Id}["{node4Text}"]

            classDef customStyle1 fill:#ff9966
            class {node1Id},{node2Id} customStyle1
            classDef customStyle2 fill:#9966ff
            class {node3Id},{node4Id} customStyle2

        """;

        // Act
        string actual = flowchart.ToMermaidString(0, "    ");

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Flowchart_WhenOneLinkOneStyle_ToMermaidString()
    {
        // Arrange
        Flowchart flowchart = new();
        string node1Id = Guid.NewGuid().ToString();
        string node1Text = Guid.NewGuid().ToString();
        Node node1 = Node.Create<MermaidUnicodeText>(node1Id, node1Text);
        string node2Id = Guid.NewGuid().ToString();
        string node2Text = Guid.NewGuid().ToString();
        Node node2 = Node.Create<MermaidUnicodeText>(node2Id, node2Text);

        StyleClass linkStyleClass = new(Stroke: new Stroke(Color.FromHex("#ff6b6b")), StrokeWidth: StrokeWidth.Length(2, Unit.Px));
        Link link = Link.Create(node1, node2, linkStyle: linkStyleClass);

        flowchart.AddNode(node1);
        flowchart.AddNode(node2);
        flowchart.AddLink(link);

        string expected =
        $"""
        flowchart TD
            {node1Id}["{node1Text}"]
            {node2Id}["{node2Text}"]

            {node1Id} ---> {node2Id}

            linkStyle 0 stroke:#ff6b6b,stroke-width:2px

        """;

        // Act
        string actual = flowchart.ToMermaidString(0, "    ");

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Flowchart_WhenTwoLinksOneStyle_ToMermaidString()
    {
        // Arrange
        Flowchart flowchart = new();
        string node1Id = Guid.NewGuid().ToString();
        string node1Text = Guid.NewGuid().ToString();
        Node node1 = Node.Create<MermaidUnicodeText>(node1Id, node1Text);
        string node2Id = Guid.NewGuid().ToString();
        string node2Text = Guid.NewGuid().ToString();
        Node node2 = Node.Create<MermaidUnicodeText>(node2Id, node2Text);
        string node3Id = Guid.NewGuid().ToString();
        string node3Text = Guid.NewGuid().ToString();
        Node node3 = Node.Create<MermaidUnicodeText>(node3Id, node3Text);

        StyleClass linkStyleClass = new(Stroke: new Stroke(Color.FromHex("#ff6b6b")), StrokeWidth: StrokeWidth.Length(2, Unit.Px));
        Link link1 = Link.Create(node1, node2, linkStyle: linkStyleClass);
        Link link2 = Link.Create(node2, node3, linkStyle: linkStyleClass);

        flowchart.AddNode(node1);
        flowchart.AddNode(node2);
        flowchart.AddNode(node3);
        flowchart.AddLink(link1);
        flowchart.AddLink(link2);

        string expected =
        $"""
        flowchart TD
            {node1Id}["{node1Text}"]
            {node2Id}["{node2Text}"]
            {node3Id}["{node3Text}"]

            {node1Id} ---> {node2Id}
            {node2Id} ---> {node3Id}

            linkStyle 0,1 stroke:#ff6b6b,stroke-width:2px

        """;

        // Act
        string actual = flowchart.ToMermaidString(0, "    ");

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Flowchart_WhenTwoLinksTwoStyles_ToMermaidString()
    {
        // Arrange
        Flowchart flowchart = new();
        string node1Id = Guid.NewGuid().ToString();
        string node1Text = Guid.NewGuid().ToString();
        Node node1 = Node.Create<MermaidUnicodeText>(node1Id, node1Text);
        string node2Id = Guid.NewGuid().ToString();
        string node2Text = Guid.NewGuid().ToString();
        Node node2 = Node.Create<MermaidUnicodeText>(node2Id, node2Text);
        string node3Id = Guid.NewGuid().ToString();
        string node3Text = Guid.NewGuid().ToString();
        Node node3 = Node.Create<MermaidUnicodeText>(node3Id, node3Text);

        StyleClass linkStyleClass1 = new(Stroke: new Stroke(Color.FromHex("#ff6b6b")), StrokeWidth: StrokeWidth.Length(2, Unit.Px));
        StyleClass linkStyleClass2 = new(Stroke: new Stroke(Color.FromHex("#4ecdc4")), StrokeWidth: StrokeWidth.Length(3, Unit.Px));
        Link link1 = Link.Create(node1, node2, linkStyle: linkStyleClass1);
        Link link2 = Link.Create(node2, node3, linkStyle: linkStyleClass2);

        flowchart.AddNode(node1);
        flowchart.AddNode(node2);
        flowchart.AddNode(node3);
        flowchart.AddLink(link1);
        flowchart.AddLink(link2);

        string expected =
        $"""
        flowchart TD
            {node1Id}["{node1Text}"]
            {node2Id}["{node2Text}"]
            {node3Id}["{node3Text}"]

            {node1Id} ---> {node2Id}
            {node2Id} ---> {node3Id}

            linkStyle 0 stroke:#ff6b6b,stroke-width:2px
            linkStyle 1 stroke:#4ecdc4,stroke-width:3px

        """;

        // Act
        string actual = flowchart.ToMermaidString(0, "    ");

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Flowchart_WhenNodesAndLinksWithStyles_ToMermaidString()
    {
        // Arrange
        Flowchart flowchart = new();

        // Node styling
        StyleClass nodeStyleClass = new(Fill: new Fill(Color.FromHex("#ff9966")));
        NodeStyle nodeStyle = new("customNodeStyle", nodeStyleClass);
        string node1Id = Guid.NewGuid().ToString();
        string node1Text = Guid.NewGuid().ToString();
        Node node1 = Node.Create<MermaidUnicodeText>(node1Id, node1Text, nodeStyle: nodeStyle);
        string node2Id = Guid.NewGuid().ToString();
        string node2Text = Guid.NewGuid().ToString();
        Node node2 = Node.Create<MermaidUnicodeText>(node2Id, node2Text, nodeStyle: nodeStyle);

        // Link styling
        StyleClass linkStyleClass = new(Stroke: new Stroke(Color.FromHex("#ff6b6b")), StrokeWidth: StrokeWidth.Length(2, Unit.Px));
        Link link = Link.Create(node1, node2, linkStyle: linkStyleClass);

        flowchart.AddNode(node1);
        flowchart.AddNode(node2);
        flowchart.AddLink(link);

        string expected =
        $"""
        flowchart TD
            {node1Id}["{node1Text}"]
            {node2Id}["{node2Text}"]

            {node1Id} ---> {node2Id}

            classDef customNodeStyle fill:#ff9966
            class {node1Id},{node2Id} customNodeStyle

            linkStyle 0 stroke:#ff6b6b,stroke-width:2px

        """;

        // Act
        string actual = flowchart.ToMermaidString(0, "    ");

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Flowchart_WhenFourLinksTwoStyles_ToMermaidString()
    {
        // Arrange
        Flowchart flowchart = new();

        // Create nodes
        string node1Id = Guid.NewGuid().ToString();
        Node node1 = Node.Create<MermaidUnicodeText>(node1Id, Guid.NewGuid().ToString());
        string node2Id = Guid.NewGuid().ToString();
        Node node2 = Node.Create<MermaidUnicodeText>(node2Id, Guid.NewGuid().ToString());
        string node3Id = Guid.NewGuid().ToString();
        Node node3 = Node.Create<MermaidUnicodeText>(node3Id, Guid.NewGuid().ToString());
        string node4Id = Guid.NewGuid().ToString();
        Node node4 = Node.Create<MermaidUnicodeText>(node4Id, Guid.NewGuid().ToString());
        string node5Id = Guid.NewGuid().ToString();
        Node node5 = Node.Create<MermaidUnicodeText>(node5Id, Guid.NewGuid().ToString());

        // Create link styles
        StyleClass linkStyleClass1 = new(Stroke: new Stroke(Color.FromHex("#ff6b6b")), StrokeWidth: StrokeWidth.Length(2, Unit.Px));
        StyleClass linkStyleClass2 = new(Stroke: new Stroke(Color.FromHex("#4ecdc4")), StrokeWidth: StrokeWidth.Length(3, Unit.Px));

        Link link1 = Link.Create(node1, node2, linkStyle: linkStyleClass1);
        Link link2 = Link.Create(node2, node3, linkStyle: linkStyleClass1);
        Link link3 = Link.Create(node3, node4, linkStyle: linkStyleClass2);
        Link link4 = Link.Create(node4, node5, linkStyle: linkStyleClass2);

        flowchart.AddNode(node1);
        flowchart.AddNode(node2);
        flowchart.AddNode(node3);
        flowchart.AddNode(node4);
        flowchart.AddNode(node5);
        flowchart.AddLink(link1);
        flowchart.AddLink(link2);
        flowchart.AddLink(link3);
        flowchart.AddLink(link4);

        string expected =
        $"""
        flowchart TD
            {node1Id}["{node1.Text}"]
            {node2Id}["{node2.Text}"]
            {node3Id}["{node3.Text}"]
            {node4Id}["{node4.Text}"]
            {node5Id}["{node5.Text}"]

            {node1Id} ---> {node2Id}
            {node2Id} ---> {node3Id}
            {node3Id} ---> {node4Id}
            {node4Id} ---> {node5Id}

            linkStyle 0,1 stroke:#ff6b6b,stroke-width:2px
            linkStyle 2,3 stroke:#4ecdc4,stroke-width:3px

        """;

        // Act
        string actual = flowchart.ToMermaidString(0, "    ");

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Flowchart_WhenMixedLinksWithAndWithoutStyles_ToMermaidString()
    {
        // Arrange
        Flowchart flowchart = new();
        string node1Id = Guid.NewGuid().ToString();
        string node1Text = Guid.NewGuid().ToString();
        Node node1 = Node.Create<MermaidUnicodeText>(node1Id, node1Text);
        string node2Id = Guid.NewGuid().ToString();
        string node2Text = Guid.NewGuid().ToString();
        Node node2 = Node.Create<MermaidUnicodeText>(node2Id, node2Text);
        string node3Id = Guid.NewGuid().ToString();
        string node3Text = Guid.NewGuid().ToString();
        Node node3 = Node.Create<MermaidUnicodeText>(node3Id, node3Text);
        string node4Id = Guid.NewGuid().ToString();
        string node4Text = Guid.NewGuid().ToString();
        Node node4 = Node.Create<MermaidUnicodeText>(node4Id, node4Text);

        // Mix of styled and unstyled links
        StyleClass linkStyleClass = new(Stroke: new Stroke(Color.FromHex("#ff6b6b")), StrokeWidth: StrokeWidth.Length(2, Unit.Px));
        Link link1 = Link.Create(node1, node2, linkStyle: linkStyleClass); // index 0, styled
        Link link2 = Link.Create(node2, node3); // index 1, unstyled
        Link link3 = Link.Create(node3, node4, linkStyle: linkStyleClass); // index 2, styled

        flowchart.AddNode(node1);
        flowchart.AddNode(node2);
        flowchart.AddNode(node3);
        flowchart.AddNode(node4);
        flowchart.AddLink(link1);
        flowchart.AddLink(link2);
        flowchart.AddLink(link3);

        string expected =
        $"""
        flowchart TD
            {node1Id}["{node1Text}"]
            {node2Id}["{node2Text}"]
            {node3Id}["{node3Text}"]
            {node4Id}["{node4Text}"]

            {node1Id} ---> {node2Id}
            {node2Id} ---> {node3Id}
            {node3Id} ---> {node4Id}

            linkStyle 0,2 stroke:#ff6b6b,stroke-width:2px

        """;

        // Act
        string actual = flowchart.ToMermaidString(0, "    ");

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Flowchart_WhenNestedLinksParentToChild_ShouldNotAddDuplicateNodes()
    {
        // Arrange
        Flowchart flowchart = new();
        Node flowchartNode = Node.Create("n", "Inside flowchart");
        Subgraph subgraph = Subgraph.Create("sg", "Subgraph");
        Node subgraphNode = Node.Create("sgn", "Inside subgraph");
        Link nToSgn = Link.Create(flowchartNode, subgraphNode);
        Subgraph subsubgraph = Subgraph.Create("ssg", "Subsubgraph");
        Node subsubgraphNode = Node.Create("ssgn", "Inside subsubgraph");
        Link sgnToSsgn = Link.Create(subgraphNode, subsubgraphNode);
        flowchart
            .AddNode(flowchartNode)
            .AddNode(
                subgraph
                    .AddNode(subgraphNode)
                    .AddNode(
                        subsubgraph
                            .AddNode(subsubgraphNode)
                        )
                )
            .AddLink(nToSgn)
            .AddLink(sgnToSsgn);

        string expected =
        $"""
        flowchart TD
            n["Inside flowchart"]

            subgraph sg ["Subgraph"]
                sgn["Inside subgraph"]

                subgraph ssg ["Subsubgraph"]
                    ssgn["Inside subsubgraph"]
                end
            end

            n ---> sgn
            sgn ---> ssgn

        """;

        // Act
        string actual = flowchart.ToMermaidString(0, "    ");

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Flowchart_WhenNestedLinksChildToParent_ShouldNotAddDuplicateNodes()
    {
        // Arrange
        Flowchart flowchart = new();
        Node flowchartNode = Node.Create("n", "Inside flowchart");
        Subgraph subgraph = Subgraph.Create("sg", "Subgraph");
        Node subgraphNode = Node.Create("sgn", "Inside subgraph");
        Link sgnToN = Link.Create(subgraphNode, flowchartNode);
        Subgraph subsubgraph = Subgraph.Create("ssg", "Subsubgraph");
        Node subsubgraphNode = Node.Create("ssgn", "Inside subsubgraph");
        Link ssgnToSgn = Link.Create(subsubgraphNode, subgraphNode);
        flowchart
            .AddNode(flowchartNode)
            .AddNode(
                subgraph
                    .AddNode(subgraphNode)
                    .AddNode(
                        subsubgraph
                            .AddNode(subsubgraphNode)
                        )
                    .AddLink(ssgnToSgn)
                )
            .AddLink(sgnToN);

        string expected =
        $"""
        flowchart TD
            n["Inside flowchart"]

            subgraph sg ["Subgraph"]
                sgn["Inside subgraph"]

                subgraph ssg ["Subsubgraph"]
                    ssgn["Inside subsubgraph"]
                end

                ssgn ---> sgn
            end

            sgn ---> n

        """;

        // Act
        string actual = flowchart.ToMermaidString(0, "    ");

        // Assert
        Assert.Equal(expected, actual);
    }
}
