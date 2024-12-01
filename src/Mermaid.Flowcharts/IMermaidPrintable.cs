namespace Mermaid.Flowcharts;

public interface IMermaidPrintable
{
    string ToMermaidString(int indentations = 0, string indentationText = "  ");
}