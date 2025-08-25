using Mermaid.Flowcharts.Styling;
using Mermaid.Flowcharts.Styling.Attributes;

namespace Mermaid.Flowcharts.Tests.Styling.Attributes;

public class StyleColorTests
{
    [Theory]
    [InlineData(0, 0, 0, "color:#000000")]
    [InlineData(255, 255, 255, "color:#ffffff")]
    public void StyleColor_WhenRGB_ToMermaidString(byte red, byte green, byte blue, string expected)
    {
        // Arrange
        StyleColor color = Color.FromRGB(red, green, blue);

        // Act
        string mermaid = color.ToMermaidString();

        // Assert
        Assert.Equal(expected, mermaid);
    }

    [Theory]
    [InlineData("#000000", "color:#000000")]
    [InlineData("#000", "color:#000000")]
    [InlineData("#ffffff", "color:#ffffff")]
    [InlineData("#fff", "color:#ffffff")]
    public void StyleColor_WhenHex_ToMermaidString(string hex, string expected)
    {
        // Arrange
        StyleColor color = Color.FromHex(hex);

        // Act
        string mermaid = color.ToMermaidString();

        // Assert
        Assert.Equal(expected, mermaid);
    }
}
