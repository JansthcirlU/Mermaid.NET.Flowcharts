using System.Text;
using Mermaid.Flowcharts.Nodes;

namespace Mermaid.Flowcharts.Subgraphs;

public readonly record struct Subgraph : INode<Subgraph>
{
    private readonly List<INode> _nodes = [];

    public NodeIdentifier Id { get; }
    public MermaidUnicodeText Title { get; }
    public SubgraphDirection? Direction { get; }
    public IEnumerable<Node> Nodes => _nodes.OfType<Node>();
    public IEnumerable<Subgraph> Subgraphs => _nodes.OfType<Subgraph>();

    public Subgraph(NodeIdentifier id, MermaidUnicodeText title, SubgraphDirection? direction = null)
    {
        Id = id;
        Title = title;
        Direction = direction;
    }

    public Subgraph AddNode(INode node)
    {
        if (node is Node nd && Nodes.Any(nd.Equals)) return this;
        
        _nodes.Add(node);
        return this;
    }

    public override string ToString()
        => ToMermaidString();

    public string ToMermaidString(int indentations = 0, string indentationText = "  ")
    {
        StringBuilder subgraphStringBuilder = new();
        string indent = indentationText.Repeat(indentations);
        subgraphStringBuilder.AppendLine($"{indent}subgraph {Id} [\"{Title}\"]");
        if (Direction is not null) subgraphStringBuilder.AppendLine($"{indent}{indentationText}direction {Direction.Value}");
        foreach (Node node in Nodes)
        {
            subgraphStringBuilder.AppendLine(node.ToMermaidString(indentations + 1, indentationText));
        }
        if (Subgraphs.Any()) subgraphStringBuilder.AppendLine();
        foreach (Subgraph subgraph in Subgraphs)
        {
            subgraphStringBuilder.AppendLine(subgraph.ToMermaidString(indentations + 1, indentationText));
        }
        subgraphStringBuilder.Append($"{indent}end");
        return subgraphStringBuilder.ToString();
    }
}