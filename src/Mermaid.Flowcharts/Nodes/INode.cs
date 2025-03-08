namespace Mermaid.Flowcharts.Nodes;

public interface INode : IMermaidPrintable
{
    public NodeIdentifier Id { get; }
}

public interface INode<TNode> : INode, IEquatable<TNode>
    where TNode : INode<TNode>
{
    
}