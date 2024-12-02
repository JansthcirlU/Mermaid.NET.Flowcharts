using System.Text;
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
    {
        StringBuilder subgraphStringBuilder = new();
        string indent = indentationText.Repeat(indentations);
        subgraphStringBuilder.AppendLine($"{indent}subgraph {Id} [\"{Title}\"]");
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

    internal bool ContainsNode(INode node)
        => Nodes.Any(n => n.Id == node.Id);

    internal bool ContainsNodeNested(INode node)
        => Subgraphs.Any(s => s.ContainsNode(node) || s.ContainsNodeNested(node));
}