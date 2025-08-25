namespace Mermaid.Flowcharts.Nodes.NodeText;

public interface INodeText : IMermaidPrintable
{
    string Value { get; }
}

public interface INodeText<TSelf> : INodeText
    where TSelf : INodeText<TSelf>
{
    abstract static TSelf FromString(string text);
}
