namespace Mermaid.Flowcharts;

public readonly struct FlowchartTitle : IMermaidPrintable
{
    public string Text { get; }

    [Obsolete(error: true, message: $"Please use the factory methods instead of the default constructor to create a new {nameof(FlowchartTitle)}.")]
#pragma warning disable CS8618 // This constructor is never used
    public FlowchartTitle() { }
#pragma warning restore CS8618
    private FlowchartTitle(string text)
    {
        Text = text;
    }

    public static FlowchartTitle FromString(string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            throw new ArgumentException("Flowchart title must not be null or empty.", nameof(text));
        }

        if (text.Contains('\n') || text.Contains('\r'))
        {
            throw new ArgumentException("Flowchart title must not contain new lines.", nameof(text));
        }

        if (string.IsNullOrEmpty(text.Trim()))
        {
            throw new ArgumentException("Flowchart title must not be whitespace.", nameof(text));
        }

        return new(
        $"""
        ---
        title: {text}
        ---
        """);
    }

    public override string ToString()
        => ToMermaidString();

    public string ToMermaidString(int indentations = 0, string indentationText = "  ")
        => $"{indentationText.Repeat(indentations)}{Text.Replace("\n", $"\n{indentationText.Repeat(indentations)}")}";
}
