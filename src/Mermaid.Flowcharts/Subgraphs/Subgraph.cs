using System.Text;
using Mermaid.Flowcharts.Links;
using Mermaid.Flowcharts.Nodes;
using Mermaid.Flowcharts.Nodes.NodeText;

namespace Mermaid.Flowcharts.Subgraphs;

public class Subgraph : INode<Subgraph>
{
    private readonly List<INode> _nodes = [];
    private readonly List<Link> _links = [];

    public NodeIdentifier Id { get; }
    public INodeText Title { get; }
    public SubgraphDirection? Direction { get; }
    public IEnumerable<Node> Nodes => _nodes.OfType<Node>();
    public IEnumerable<Subgraph> Subgraphs => _nodes.OfType<Subgraph>();

    private Subgraph(NodeIdentifier id, INodeText title, SubgraphDirection? direction = null)
    {
        Id = id;
        Title = title;
        Direction = direction;
    }

    public static Subgraph CreateNew<TNodeText>(string title, SubgraphDirection? direction = null)
        where TNodeText : INodeText<TNodeText>
        => new(NodeIdentifier.Create(), TNodeText.FromString(title), direction);

    public static Subgraph Create<TNodeText>(string identifier, string title, SubgraphDirection? direction = null)
        where TNodeText : INodeText<TNodeText>
        => new(NodeIdentifier.FromString(identifier), TNodeText.FromString(title), direction);
    
    public static Subgraph CreateNew<TNodeText>(TNodeText title, SubgraphDirection? direction = null)
        where TNodeText : INodeText
        => new(NodeIdentifier.Create(), title, direction);

    public static Subgraph Create<TNodeText>(string identifier, TNodeText title, SubgraphDirection? direction = null)
        where TNodeText : INodeText
        => new(NodeIdentifier.FromString(identifier), title, direction);

    public Subgraph AddNode(INode node)
    {
        if (node is Node nd && Nodes.Any(nd.Equals)) return this;

        _nodes.Add(node);
        return this;
    }

    public Subgraph AddLink(Link link)
    {
        _links.Add(link);
        AddNode(link.Source);
        AddNode(link.Destination);
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
        if (_links.Any()) subgraphStringBuilder.AppendLine();
        foreach (Link link in _links)
        {
            subgraphStringBuilder.AppendLine(link.ToMermaidString(indentations + 1, indentationText));
        }
        subgraphStringBuilder.Append($"{indent}end");
        return subgraphStringBuilder.ToString();
    }

    public bool Equals(Subgraph? other)
        => other is Subgraph s && Id.Equals(s.Id);

    public override bool Equals(object? obj)
        => Equals(obj as Subgraph);

    public override int GetHashCode()
        => Id.GetHashCode();
}