using Mermaid.Flowcharts.Links;

namespace Mermaid.Flowcharts.Tests;

public class LinkTextTests
{
    [Fact]
    public void LinkText_ShouldThrow_WhenNull()
    {
        // Arrange
        string text = null!;

        // Act
        ArgumentException? ex = Assert.Throws<ArgumentException>(
            () => LinkText.FromString(text)
        );

        // Assert
        Assert.NotNull(ex);
        Assert.StartsWith("Link text must not be null or empty.", ex.Message);
    }

    [Fact]
    public void LinkText_ShouldThrow_WhenEmpty()
    {
        // Arrange
        string text = string.Empty;

        // Act
        ArgumentException? ex = Assert.Throws<ArgumentException>(
            () => LinkText.FromString(text)
        );

        // Assert
        Assert.NotNull(ex);
        Assert.StartsWith("Link text must not be null or empty.", ex.Message);
    }

    [Theory]
    [InlineData("text", 2, "  ", "\"text\"")]
    [InlineData("mermaid is fun", 1, " ", "\"mermaid is fun\"")]
    [InlineData("text with () parentheses", 2, "  ", "\"text with () parentheses\"")]
    [InlineData(
        """
        text with
            new line
        """, 2, "  ",
        """
        "text with
            new line"
        """)]
    public void ToMermaidString_ShouldIgnoreIndentationsAndAlwaysEscape(string text, int indentations, string indentationText, string expected)
    {
        // Arrange
        LinkText linkText = LinkText.FromString(text);

        // Act
        string actual = linkText.ToMermaidString(indentations, indentationText);

        // Assert
        Assert.Equal(expected, actual);
    }
}
