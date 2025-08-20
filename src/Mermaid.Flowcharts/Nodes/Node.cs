using Mermaid.Flowcharts.Nodes.NodeText;

namespace Mermaid.Flowcharts.Nodes;

public readonly record struct Node : INode<Node>
{
    public NodeIdentifier Id { get; }
    public INodeText Text { get; }
    public NodeShape Shape { get; }

    [Obsolete(error: true, message: $"Please use the factory methods instead of the default constructor to create a new {nameof(Node)}.")]
#pragma warning disable CS8618
    public Node() { }
#pragma warning restore CS8618
    private Node(NodeIdentifier id, INodeText text, NodeShape shape)
    {
        Id = id;
        Text = text;
        Shape = shape;
    }

    public static Node CreateNew<TNodeText>(string text, NodeShape shape = NodeShape.Rectangle)
        where TNodeText : INodeText<TNodeText>
        => new(NodeIdentifier.Create(), TNodeText.FromString(text), shape);

    public static Node Create<TNodeText>(string identifier, string text, NodeShape shape = NodeShape.Rectangle)
        where TNodeText : INodeText<TNodeText>
        => new(NodeIdentifier.FromString(identifier), TNodeText.FromString(text), shape);
    
    public static Node CreateNew<TNodeText>(TNodeText text, NodeShape shape = NodeShape.Rectangle)
        where TNodeText : INodeText
        => new(NodeIdentifier.Create(), text, shape);

    public static Node Create<TNodeText>(string identifier, TNodeText text, NodeShape shape = NodeShape.Rectangle)
        where TNodeText : INodeText
        => new(NodeIdentifier.FromString(identifier), text, shape);

    public override string ToString()
        => ToMermaidString();

    public string ToMermaidString(int indentations = 0, string indentationText = "  ")
    {
        string shapeStart = Shape switch
        {
            NodeShape.RoundedEdges => "(",
            NodeShape.Stadium => "([",
            NodeShape.Subroutine => "[[",
            NodeShape.Cylindrical => "[(",
            NodeShape.Circle => "((",
            NodeShape.DoubleCircle => "(((",
            NodeShape.Asymmetric => ">",
            NodeShape.Rhombus => "{",
            NodeShape.Hexagon => "{{",
            NodeShape.Parallelogram => "[/",
            NodeShape.ParallelogramAlt => "[\\",
            NodeShape.Trapezoid => "[/",
            NodeShape.TrapezoidAlt => "[\\",
            _ => "[",
        };

        string shapeEnd = Shape switch
        {
            NodeShape.RoundedEdges => ")",
            NodeShape.Stadium => "])",
            NodeShape.Subroutine => "]]",
            NodeShape.Cylindrical => ")]",
            NodeShape.Circle => "))",
            NodeShape.DoubleCircle => ")))",
            NodeShape.Asymmetric => "]",
            NodeShape.Rhombus => "}",
            NodeShape.Hexagon => "}}",
            NodeShape.Parallelogram => "/]",
            NodeShape.ParallelogramAlt => "\\]",
            NodeShape.Trapezoid => "\\]",
            NodeShape.TrapezoidAlt => "/]",
            _ => "]",
        };

        return $"{indentationText.Repeat(indentations)}{Id}{shapeStart}\"{Text}\"{shapeEnd}";
    }
}