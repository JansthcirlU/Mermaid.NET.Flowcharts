using Mermaid.Flowcharts.Styling.Attributes.Fonts;

namespace Mermaid.Flowcharts.Tests.Styling.Attributes;

public class FontFamilyTests
{
    [Theory]
    [InlineData(new[] { "Arial" }, "font-family:Arial")]
    [InlineData(new[] { "Times New Roman" }, "font-family:Times New Roman")]
    [InlineData(new[] { "Arial", "Helvetica" }, "font-family:Arial\\,Helvetica")]
    [InlineData(new[] { "Times New Roman", "serif" }, "font-family:Times New Roman\\,serif")]
    [InlineData(new[] { "Arial", "Helvetica", "sans-serif" }, "font-family:Arial\\,Helvetica\\,sans-serif")]
    [InlineData(new[] { "A", "B", "C", "D", "E" }, "font-family:A\\,B\\,C\\,D\\,E")]

    public void ToMermaidString_ShouldReturnCorrectFormat(string[] componentValues, string expected)
    {
        // Arrange
        var components = componentValues.Select(v => new FontFamilyComponent(v));
        var fontFamily = new FontFamily(components);
        
        // Act
        var result = fontFamily.ToMermaidString();
        
        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void FontFamily_WhenEmpty_ShouldThrow()
    {
        // Act
        ArgumentException? ex = Assert.Throws<ArgumentException>(
            () => new FontFamily([])
        );

        // Assert
        Assert.NotNull(ex);
        Assert.StartsWith("Font family must contain at least one component.", ex.Message);
    }
}
