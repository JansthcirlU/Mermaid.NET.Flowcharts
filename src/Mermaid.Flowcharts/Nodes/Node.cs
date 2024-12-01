namespace Mermaid.Flowcharts.Nodes;

public readonly record struct Node
{
    public NodeIdentifier Id { get; }
    public MermaidUnicodeText Text { get; }

    private Node(NodeIdentifier id, MermaidUnicodeText text)
    {
        Id = id;
        Text = text;
    }

    public static Node Create(string identifier, string text)
        => new(NodeIdentifier.FromString(identifier), MermaidUnicodeText.FromString(text));

    public override string ToString()
        => $"{Id}[\"{Text}\"]";
}
