using System.Text;
using Mermaid.Flowcharts.Links;
using Mermaid.Flowcharts.Nodes;
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
    public IEnumerable<Node> AllNodes => Nodes.Concat(Subgraphs.SelectMany(subgraph => subgraph.AllNodes));

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
        if (node is Node nd && Nodes.Any(nd.Equals)) return this;

        _nodes.Add(node);
        return this;
    }

    public Flowchart AddLink(Link link)
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
        if (Subgraphs.Any()) flowchartStringBuilder.AppendLine();
        foreach (Subgraph subgraph in Subgraphs)
        {
            flowchartStringBuilder.AppendLine(subgraph.ToMermaidString(indentations + 1, indentationText));
        }
        if (_links.Any()) flowchartStringBuilder.AppendLine();
        foreach (Link link in _links)
        {
            flowchartStringBuilder.AppendLine(link.ToMermaidString(indentations + 1, indentationText));
        }

        // Group all node styles across flowchart and all subgraphs recursively together
        Dictionary<NodeStyle, HashSet<NodeIdentifier>> distinctNodeStyles = [];
        foreach (Node node in AllNodes)
        {
            if (node.NodeStyle is null) continue;

            // Add node style declaration
            if (!distinctNodeStyles.ContainsKey(node.NodeStyle)) distinctNodeStyles[node.NodeStyle] = [];
            distinctNodeStyles[node.NodeStyle].Add(node.Id);
        }

        // Add style declarations and assignments
        if (distinctNodeStyles.Any()) flowchartStringBuilder.AppendLine();
        foreach ((NodeStyle nodeStyle, HashSet<NodeIdentifier> nodeIds) in distinctNodeStyles)
        {
            flowchartStringBuilder.AppendLine(nodeStyle.ToMermaidString(indentations + 1, indentationText));
            flowchartStringBuilder.AppendLine($"{indentationText.Repeat(indentations + 1)}class {string.Join(',', nodeIds.Select(id => id.ToMermaidString()))} {nodeStyle.Name}");
        }

        return flowchartStringBuilder.ToString();
    }
}