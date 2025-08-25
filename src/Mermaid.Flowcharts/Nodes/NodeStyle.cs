using Mermaid.Flowcharts.Styling;

namespace Mermaid.Flowcharts.Nodes;

public record NodeStyle(string Name, StyleClass StyleClass) : IMermaidPrintable
{
    public string ToMermaidString(int indentations = 0, string indentationText = "  ")
        => $"{indentationText.Repeat(indentations)}classDef {Name} {StyleClass.ToMermaidString()}";
}
