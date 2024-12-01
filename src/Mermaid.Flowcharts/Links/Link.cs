using Mermaid.Flowcharts.Nodes;

namespace Mermaid.Flowcharts.Links;

public readonly record struct Link : IMermaidPrintable
{
    public readonly INode Source { get; }
    public readonly INode Destination { get; }
    public readonly LinkStyle Style { get; }
    public readonly LinkText? Text { get; }

    public Link(
        INode source,
        INode destination,
        LinkStyle style,
        LinkText? text = null)
    {
        Source = source;
        Destination = destination;
        Style = style;
        Text = text;
    }

    public override string ToString()
        => ToMermaidString();

    public string ToMermaidString(int indentations = 0)
        => Text is null
            ? $"{Source.Id} {Style} {Destination.Id}"
            : $"{Source.Id} {Style}|{Text}| {Destination.Id}";
}
