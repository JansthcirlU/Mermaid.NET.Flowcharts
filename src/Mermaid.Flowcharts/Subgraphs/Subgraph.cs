using System.Text;
using Mermaid.Flowcharts.Links;
using Mermaid.Flowcharts.Nodes;
using Mermaid.Flowcharts.Nodes.NodeText;

namespace Mermaid.Flowcharts.Subgraphs;

public record Subgraph : INode<Subgraph>
{
    private readonly List<INode> _nodes = [];
    private readonly List<Link> _links = [];
    internal IEnumerable<INode> AllNodeChildren => _nodes.Concat(Subgraphs.SelectMany(subgraph => subgraph.AllNodeChildren));
    internal IEnumerable<Node> AllNodes => Nodes.Concat(Subgraphs.SelectMany(subgraph => subgraph.AllNodes));

    public NodeIdentifier Id { get; }
    public INodeText Title { get; }
    public SubgraphDirection? Direction { get; }
    public NodeStyle? NodeStyle { get; }
    public IEnumerable<Node> Nodes => _nodes.OfType<Node>();
    public IEnumerable<Subgraph> Subgraphs => _nodes.OfType<Subgraph>();
    public IEnumerable<Link> Links => _links.AsReadOnly();

    private Subgraph(NodeIdentifier id, INodeText title, SubgraphDirection? direction = null, NodeStyle? nodeStyle = null)
    {
        Id = id;
        Title = title;
        Direction = direction;
        NodeStyle = nodeStyle;
    }

    public static Subgraph CreateNew<TNodeText>(string title, SubgraphDirection? direction = null, NodeStyle? nodeStyle = null)
        where TNodeText : INodeText<TNodeText>
        => new(NodeIdentifier.Create(), TNodeText.FromString(title), direction, nodeStyle);

    public static Subgraph Create<TNodeText>(string identifier, string title, SubgraphDirection? direction = null, NodeStyle? nodeStyle = null)
        where TNodeText : INodeText<TNodeText>
        => new(NodeIdentifier.FromString(identifier), TNodeText.FromString(title), direction, nodeStyle);

    public static Subgraph CreateNew<TNodeText>(TNodeText title, SubgraphDirection? direction = null, NodeStyle? nodeStyle = null)
        where TNodeText : INodeText
        => new(NodeIdentifier.Create(), title, direction, nodeStyle);

    public static Subgraph Create<TNodeText>(string identifier, TNodeText title, SubgraphDirection? direction = null, NodeStyle? nodeStyle = null)
        where TNodeText : INodeText
        => new(NodeIdentifier.FromString(identifier), title, direction, nodeStyle);

    public static Subgraph CreateNew(string title, SubgraphDirection? direction = null, NodeStyle? nodeStyle = null)
        => CreateNew<MermaidUnicodeText>(title, direction, nodeStyle);

    public static Subgraph Create(string identifier, string title, SubgraphDirection? direction = null, NodeStyle? nodeStyle = null)
        => Create<MermaidUnicodeText>(identifier, title, direction, nodeStyle);

    public Subgraph AddNode(INode node)
    {
        if (Equals(node))
        {
            throw new ArgumentException("Cannot add subgraph as a node to itself.", nameof(node));
        }

        if (node is Node nd && Nodes.Any(nd.Equals))
        {
            return this;
        }

        _nodes.Add(node);
        return this;
    }

    public Subgraph AddLink(Link link)
    {
        if (AllNodeChildren.Any(link.Source.Equals) && AllNodeChildren.Any(link.Destination.Equals))
        {
            _links.Add(link);
            return this;
        }
        throw new InvalidOperationException("Cannot add link to subgraph: the source and the destination nodes should both be present within the subgraph.");
    }

    public override string ToString()
        => ToMermaidString();

    internal string ToMermaidStringWithOrderedLinks(int indentations, string indentationText, List<Link> linksInOrderOfOccurrence)
    {
        StringBuilder subgraphStringBuilder = new();
        string indent = indentationText.Repeat(indentations);
        subgraphStringBuilder.AppendLine($"{indent}subgraph {Id} [\"{Title}\"]");
        if (Direction is not null)
        {
            subgraphStringBuilder.AppendLine($"{indent}{indentationText}direction {Direction.Value}");
        }

        // Add node declarations
        foreach (Node node in Nodes)
        {
            subgraphStringBuilder.AppendLine(node.ToMermaidString(indentations + 1, indentationText));
        }

        // Add subgraph declarations
        if (Subgraphs.Any())
        {
            subgraphStringBuilder.AppendLine();
        }

        foreach (Subgraph subgraph in Subgraphs)
        {
            subgraphStringBuilder.AppendLine(subgraph.ToMermaidStringWithOrderedLinks(indentations + 1, indentationText, linksInOrderOfOccurrence));
        }

        // Add link declarations
        if (_links.Any())
        {
            subgraphStringBuilder.AppendLine();
        }

        foreach (Link link in _links)
        {
            linksInOrderOfOccurrence.Add(link);
            subgraphStringBuilder.AppendLine(link.ToMermaidString(indentations + 1, indentationText));
        }
        subgraphStringBuilder.Append($"{indent}end");
        return subgraphStringBuilder.ToString();
    }

    public string ToMermaidString(int indentations = 0, string indentationText = "  ")
        => ToMermaidStringWithOrderedLinks(indentations, indentationText, []);
}
