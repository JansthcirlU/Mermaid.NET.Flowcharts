using Mermaid.Flowcharts.Styling.Attributes;

namespace Mermaid.Flowcharts.Tests.Styling.Attributes;

public class FillTests
{
    [Theory]
    [InlineData(0, 0, 0, "fill:#000000")]
    [InlineData(85, 170, 255, "fill:#55aaff")]
    [InlineData(255, 255, 255, "fill:#ffffff")]
    public void Fill_ToMermaidString(byte red, byte green, byte blue, string expected)
    {
        // Arrange
        Color color = new(red, green, blue);
        Fill fill = new(color);

        // Act
        string mermaid = fill.ToMermaidString();

        // Assert
        Assert.Equal(expected, mermaid);
    }
}
