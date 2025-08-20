using Mermaid.Flowcharts.Nodes;

namespace Mermaid.Flowcharts.Links;

public readonly record struct Link : IMermaidPrintable
{
    public readonly INode Source { get; }
    public readonly INode Destination { get; }
    public readonly LinkStyle Style { get; }
    public readonly LinkText? Text { get; }

    [Obsolete(error: true, message: $"Please use the factory methods instead of the default constructor to create a new {nameof(Link)}.")]
#pragma warning disable CS8618 // This constructor is never used
    public Link() { }
#pragma warning restore CS8618
    private Link(
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

    public static Link Create(INode source, INode destination, LinkStyle? style = null, string? linkText = null)
        => new(source, destination, style ?? LinkStyle.Create(), linkText is not null ? LinkText.FromString(linkText) : null);

    public override string ToString()
        => ToMermaidString();

    public string ToMermaidString(int indentations = 0, string indentationText = "  ")
        => Text is null
            ? $"{indentationText.Repeat(indentations)}{Source.Id} {Style} {Destination.Id}"
            : $"{indentationText.Repeat(indentations)}{Source.Id} {Style}|{Text}| {Destination.Id}";
}
