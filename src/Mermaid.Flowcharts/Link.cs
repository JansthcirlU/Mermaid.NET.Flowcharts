namespace Mermaid.Flowcharts;

public readonly record struct Link
{
    public readonly Node Source { get; }
    public readonly Node Destination { get; }
    public readonly LinkStyle Style { get; }
    public readonly LinkText? Text { get; }

    public Link(
        Node source,
        Node destination,
        LinkStyle style,
        LinkText? text = null)
    {
        Source = source;
        Destination = destination;
        Style = style;
        Text = text;
    }

    public override string ToString()
        => Text is null
            ? $"{Source.Id} {Style} {Destination.Id}"
            : $"{Source.Id} {Style}|{Text}| {Destination.Id}";
}
