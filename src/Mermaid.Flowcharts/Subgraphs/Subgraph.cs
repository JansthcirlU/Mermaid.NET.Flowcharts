using Mermaid.Flowcharts.Nodes;

namespace Mermaid.Flowcharts.Subgraphs;

public class Subgraph : INode
{
    private readonly List<INode> _nodes = [];

    public NodeIdentifier Id { get; }
    public IEnumerable<INode> Nodes => _nodes.AsReadOnly();

    public Subgraph(NodeIdentifier id)
    {
        Id = id;
    }

    public Subgraph AddNode(INode node)
    {
        _nodes.Add(node);
        return this;
    }
}