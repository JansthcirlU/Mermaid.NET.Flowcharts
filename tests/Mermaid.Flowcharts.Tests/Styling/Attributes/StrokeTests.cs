using Mermaid.Flowcharts.Styling.Attributes;

namespace Mermaid.Flowcharts.Tests.Styling.Attributes;

public class StrokeTests
{
    [Theory]
    [InlineData(0, 0, 0, "stroke:#000000")]
    [InlineData(85, 170, 255, "stroke:#55aaff")]
    [InlineData(255, 255, 255, "stroke:#ffffff")]
    public void Fill_ToMermaidString(byte red, byte green, byte blue, string expected)
    {
        // Arrange
        Color color = new(red, green, blue);
        Stroke stroke = new(color);

        // Act
        string mermaid = stroke.ToMermaidString();

        // Assert
        Assert.Equal(expected, mermaid);
    }
}
