namespace Mermaid.Flowcharts.Links;

public readonly record struct LinkStyle : IMermaidPrintable
{
    public readonly LinkArrowType ArrowType { get; } = LinkArrowType.Arrow;
    public readonly LinkDirection Direction { get; } = LinkDirection.LeftToRight;
    public readonly LinkThickness Thickness { get; } = LinkThickness.Normal;

    public LinkStyle(
        LinkArrowType arrowType,
        LinkDirection direction,
        LinkThickness thickness)
    {
        ArrowType = arrowType;
        Direction = direction;
        Thickness = thickness;
    }

    public override string ToString()
        => ToMermaidString();

    public string ToMermaidString(int indentations = 0, string indentationText = "  ")
    {
        string thickness = Thickness switch
        {
            LinkThickness.Dotted => "-.-",
            LinkThickness.Thick => "===",
            LinkThickness.Invisible => "~~~",
            _ => "---"
        };
        if (Thickness is LinkThickness.Invisible) return thickness;

        string arrowLeft = (ArrowType, Direction) switch
        {
            (LinkArrowType.Arrow, LinkDirection.RightToLeft or LinkDirection.Both) => "<",
            (LinkArrowType.Circle, _) => "o",
            (LinkArrowType.Cross, _) => "x",
            _ => string.Empty
        };
        string arrowRight = (ArrowType, Direction) switch
        {
            (LinkArrowType.Arrow, LinkDirection.LeftToRight or LinkDirection.Both) => ">",
            (LinkArrowType.Circle, _) => "o",
            (LinkArrowType.Cross, _) => "x",
            _ => string.Empty
        };
        return $"{indentationText.Repeat(indentations)}{Direction switch
        {
            LinkDirection.RightToLeft => $"{arrowLeft}{thickness}",
            LinkDirection.Both => $"{arrowLeft}{thickness}{arrowRight}",
            _ => $"{thickness}{arrowRight}",
        }}";
    }
}
