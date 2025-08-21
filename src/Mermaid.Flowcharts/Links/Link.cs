using Mermaid.Flowcharts.Nodes;

namespace Mermaid.Flowcharts.Links;

public readonly record struct Link : IMermaidPrintable
{
    public readonly INode Source { get; }
    public readonly INode Destination { get; }
    public readonly LinkType Type { get; }
    public readonly LinkText? Text { get; }

    [Obsolete(error: true, message: $"Please use the factory methods instead of the default constructor to create a new {nameof(Link)}.")]
#pragma warning disable CS8618 // This constructor is never used
    public Link() { }
#pragma warning restore CS8618
    private Link(
        INode source,
        INode destination,
        LinkType type,
        LinkText? text = null)
    {
        Source = source;
        Destination = destination;
        Type = type;
        Text = text;
    }

    public static Link Create(INode source, INode destination, LinkType? type = null, string? linkText = null)
        => new(source, destination, type ?? LinkType.Create(), linkText is not null ? LinkText.FromString(linkText) : null);

    public override string ToString()
        => ToMermaidString();

    public string ToMermaidString(int indentations = 0, string indentationText = "  ")
        => Text is null
            ? $"{indentationText.Repeat(indentations)}{Source.Id} {Type} {Destination.Id}"
            : $"{indentationText.Repeat(indentations)}{Source.Id} {Type}|{Text}| {Destination.Id}";
}
