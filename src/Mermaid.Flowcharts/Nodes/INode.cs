namespace Mermaid.Flowcharts.Nodes;

public interface INode : IMermaidPrintable
{
    public NodeIdentifier Id { get; }
}