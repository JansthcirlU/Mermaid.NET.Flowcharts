using Mermaid.Flowcharts.Styling;

namespace Mermaid.Flowcharts.Tests.Styling.Attributes;

public class StyleOpacityTests
{
    [Theory]
    [InlineData(0, "opacity:0")]
    [InlineData(0.0004, "opacity:0")]
    [InlineData(0.0006, "opacity:0.001")]
    [InlineData(0.9994, "opacity:0.999")]
    [InlineData(0.9996, "opacity:1")]
    public void StyleOpacity_ToMermaidString(double value, string expected)
    {
        // Arrange
        StyleOpacity opacity = new(value);

        // Act
        string mermaid = opacity.ToMermaidString();

        // Assert
        Assert.Equal(expected, mermaid);
    }
}
