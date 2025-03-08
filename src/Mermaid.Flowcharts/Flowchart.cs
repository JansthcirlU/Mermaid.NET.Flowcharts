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
    public IEnumerable<Node> Nodes => _nodes.OfType<Node>();
    public IEnumerable<Subgraph> Subgraphs => _nodes.OfType<Subgraph>();

    public Flowchart()
    {

    }
    public Flowchart(FlowchartTitle title)
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
        flowchartStringBuilder.AppendLine("flowchart TD");

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
        return flowchartStringBuilder.ToString();
    }
}