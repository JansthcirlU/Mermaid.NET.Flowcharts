using System.Text;
using Mermaid.Flowcharts.Links;
using Mermaid.Flowcharts.Nodes;
using Mermaid.Flowcharts.Styling;
using Mermaid.Flowcharts.Subgraphs;

namespace Mermaid.Flowcharts;

public class Flowchart : IMermaidPrintable
{
    private readonly List<INode> _nodes = [];
    private readonly List<Link> _links = [];

    public FlowchartTitle? Title { get; }
    public FlowchartDirection? Direction { get; }
    public IEnumerable<Node> Nodes => _nodes.OfType<Node>();
    public IEnumerable<Subgraph> Subgraphs => _nodes.OfType<Subgraph>();
    public IEnumerable<Link> Links => _links.AsReadOnly();
    public IEnumerable<INode> AllNodeChildren => _nodes.Concat(Subgraphs.SelectMany(subgraph => subgraph.AllNodeChildren));
    public IEnumerable<Node> AllNodes => Nodes.Concat(Subgraphs.SelectMany(subgraph => subgraph.AllNodes));
    public IEnumerable<Link> AllLinks => Links.Concat(Subgraphs.SelectMany(subgraph => subgraph.AllLinks));

    public Flowchart(FlowchartDirection? direction = null)
    {
        Direction = direction;
    }
    public Flowchart(FlowchartTitle title, FlowchartDirection? direction = null) : this(direction)
    {
        Title = title;
    }

    public Flowchart AddNode(INode node)
    {
        if (node is Node nd && Nodes.Any(nd.Equals))
        {
            return this;
        }

        _nodes.Add(node);
        return this;
    }

    public Flowchart AddLink(Link link)
    {
        if (AllNodeChildren.Any(link.Source.Equals) && AllNodeChildren.Any(link.Destination.Equals))
        {
            _links.Add(link);
            return this;
        }
        throw new InvalidOperationException("Cannot add link to flowchart: the source and the destination nodes should both be present within the flowchart.");
    }

    public override string ToString()
        => ToMermaidString();

    public string ToMermaidString(int indentations = 0, string indentationText = "  ")
    {
        StringBuilder flowchartStringBuilder = new();
        if (Title is FlowchartTitle title)
        {
            flowchartStringBuilder.AppendLine(title.ToMermaidString(indentations, indentationText));
        }
        flowchartStringBuilder.AppendLine($"flowchart {Direction ?? FlowchartDirection.TD}");

        foreach (Node node in Nodes)
        {
            flowchartStringBuilder.AppendLine(node.ToMermaidString(indentations + 1, indentationText));
        }
        if (Subgraphs.Any())
        {
            flowchartStringBuilder.AppendLine();
        }

        foreach (Subgraph subgraph in Subgraphs)
        {
            flowchartStringBuilder.AppendLine(subgraph.ToMermaidString(indentations + 1, indentationText));
        }
        if (_links.Any())
        {
            flowchartStringBuilder.AppendLine();
        }

        foreach (Link link in _links)
        {
            flowchartStringBuilder.AppendLine(link.ToMermaidString(indentations + 1, indentationText));
        }

        // Group all node styles across flowchart and all subgraphs recursively together
        Dictionary<NodeStyle, HashSet<NodeIdentifier>> distinctNodeStyles = [];
        foreach (Node node in AllNodes)
        {
            if (node.NodeStyle is null)
            {
                continue;
            }

            // Add node style declaration
            if (!distinctNodeStyles.TryGetValue(node.NodeStyle, out HashSet<NodeIdentifier>? value))
            {
                value = [];
                distinctNodeStyles[node.NodeStyle] = value;
            }

            value.Add(node.Id);
        }

        // Add node style declarations and assignments
        if (distinctNodeStyles.Any())
        {
            flowchartStringBuilder.AppendLine();
        }

        foreach ((NodeStyle nodeStyle, HashSet<NodeIdentifier> nodeIds) in distinctNodeStyles)
        {
            flowchartStringBuilder.AppendLine(nodeStyle.ToMermaidString(indentations + 1, indentationText));
            flowchartStringBuilder.AppendLine($"{indentationText.Repeat(indentations + 1)}class {string.Join(',', nodeIds.Select(id => id.ToMermaidString()))} {nodeStyle.Name}");
        }

        // Group all link styles across flowchart and all subgraphs recursively together
        Dictionary<StyleClass, HashSet<int>> distinctLinkStyles = [];
        foreach ((Link link, int index) in AllLinks.Select((l, i) => (l, i)))
        {
            if (link.LinkStyle is null)
            {
                continue;
            }

            // Add link style declaration
            if (!distinctLinkStyles.TryGetValue(link.LinkStyle, out HashSet<int>? value))
            {
                value = [];
                distinctLinkStyles[link.LinkStyle] = value;
            }

            value.Add(index);
        }

        // Add link style declarations and assignments
        if (distinctLinkStyles.Any())
        {
            flowchartStringBuilder.AppendLine();
        }

        foreach ((StyleClass styleClass, HashSet<int> indices) in distinctLinkStyles)
        {
            flowchartStringBuilder.AppendLine($"{indentationText.Repeat(indentations + 1)}linkStyle {string.Join(',', indices)} {styleClass.ToMermaidString()}");
        }

        return flowchartStringBuilder.ToString();
    }
}
