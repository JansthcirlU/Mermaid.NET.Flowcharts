using Mermaid.Flowcharts.Nodes;

namespace Mermaid.Flowcharts.Subgraphs;

public class Subgraph : INode
{
    private readonly List<INode> _nodes = [];

    public NodeIdentifier Id { get; }
    public MermaidUnicodeText Title { get; }
    public IEnumerable<Node> Nodes => _nodes.OfType<Node>();
    public IEnumerable<Subgraph> Subgraphs => _nodes.OfType<Subgraph>();

    public Subgraph(NodeIdentifier id, MermaidUnicodeText title)
    {
        Id = id;
        Title = title;
    }

    public Subgraph AddNode(INode node)
    {
        _nodes.Add(node);
        return this;
    }

    public override string ToString()
        => ToMermaidString();

    public string ToMermaidString(int indentations = 0, string indentationText = "  ")
        => _nodes.Any()
            ?
                $"""
                {indentationText.Repeat(indentations)}subgraph {Id} ["{Title}"]
                {string.Join('\n', _nodes.Select(n => n.ToMermaidString(indentations + 1)))}
                {indentationText.Repeat(indentations)}end
                """
            :
                $"""
                {indentationText.Repeat(indentations)}subgraph {Id} ["{Title}"]
                {indentationText.Repeat(indentations)}end
                """;

    internal bool ContainsNode(INode node)
        => Nodes.Any(n => n.Id == node.Id);

    internal bool ContainsNodeNested(INode node)
        => Subgraphs.Any(s => s.ContainsNode(node) || s.ContainsNodeNested(node));
}