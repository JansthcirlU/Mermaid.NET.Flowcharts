using System.Text;
using Mermaid.Flowcharts.Links;
using Mermaid.Flowcharts.Nodes;

namespace Mermaid.Flowcharts;

public class Flowchart : IMermaidPrintable
{
    private readonly List<INode> _nodes = [];
    private readonly List<Link> _links = [];

    public FlowchartTitle? Title { get; }

    public Flowchart()
    {

    }
    public Flowchart(FlowchartTitle title)
    {
        Title = title;
    }

    public Flowchart AddNode(INode node)
    {
        if (!_nodes.Any(n => n.Id == node.Id)) _nodes.Add(node);
        return this;
    }

    public Flowchart AddLink(Link link)
    {
        _links.Add(link);
        return this;
    }

    public override string ToString()
        => ToMermaidString();

    public string ToMermaidString(int indentations = 0, string indentationText = "  ")
    {
        StringBuilder flowchartStringBuilder = new();
        if (Title is not null)
        {
            flowchartStringBuilder.AppendLine(Title.ToString());
        }
        flowchartStringBuilder.AppendLine("flowchart TD");

        IEnumerable<INode> allNodes = _nodes
            .Concat(_links.Select(link => link.Source))
            .Concat(_links.Select(link => link.Destination))
            .OrderBy(node => node.Id.Value)
            .DistinctBy(node => node.Id);
        foreach (INode node in allNodes)
        {
            flowchartStringBuilder.AppendLine($"    {node.ToString()}");
        }
        flowchartStringBuilder.AppendLine();
        foreach (Link link in _links)
        {
            flowchartStringBuilder.AppendLine($"    {link.ToString()}");
        }
        return flowchartStringBuilder.ToString();
    }
}