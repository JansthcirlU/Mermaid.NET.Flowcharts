using Mermaid.Flowcharts.Nodes;

namespace Mermaid.Flowcharts.Subgraphs;

public class Subgraph : INode
{
    private readonly List<INode> _nodes = [];

    public NodeIdentifier Id { get; }
    public IEnumerable<Node> Nodes => _nodes.OfType<Node>();
    public IEnumerable<Subgraph> Subgraphs => _nodes.OfType<Subgraph>();

    public Subgraph(NodeIdentifier id)
    {
        Id = id;
    }

    public Subgraph AddNode(INode node)
    {
        _nodes.Add(node);
        return this;
    }

    public string ToMermaidString(int indentations = 0, string indentationText = "  ")
    {
        throw new NotImplementedException();
    }

    internal bool ContainsNode(INode node)
        => Nodes.Any(n => n.Id == node.Id);

    internal bool ContainsNodeNested(INode node)
        => Subgraphs.Any(s => s.ContainsNode(node) || s.ContainsNodeNested(node));
}