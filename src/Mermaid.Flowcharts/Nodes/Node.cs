namespace Mermaid.Flowcharts.Nodes;

public readonly record struct Node : INode<Node>
{
    public NodeIdentifier Id { get; }
    public MermaidUnicodeText Text { get; }
    public NodeShape Shape { get; }

    [Obsolete(error: true, message: $"Please use the factory methods instead of the default constructor to create a new {nameof(Node)}.")]
    public Node() { }
    private Node(NodeIdentifier id, MermaidUnicodeText text, NodeShape shape)
    {
        Id = id;
        Text = text;
        Shape = shape;
    }

    public static Node CreateNew(string text, NodeShape shape = NodeShape.Rectangle)
        => new(NodeIdentifier.Create(), MermaidUnicodeText.FromString(text), shape);

    public static Node Create(string identifier, string text, NodeShape shape = NodeShape.Rectangle)
        => new(NodeIdentifier.FromString(identifier), MermaidUnicodeText.FromString(text), shape);

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