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
    [InlineData("\n")]
    [InlineData("\r")]
    public void LinkText_ShouldThrow_WhenNewline(string newline)
    {
        // Arrange
        string text = newline;

        // Act
        ArgumentException? ex = Assert.Throws<ArgumentException>(
            () => LinkText.FromString(text)
        );

        // Assert
        Assert.NotNull(ex);
        Assert.StartsWith("Link text must not contain new lines.", ex.Message);
    }

    [Theory]
    [InlineData("\"")]
    [InlineData("|")]
    public void LinkText_ShouldThrow_WhenContainsEscapeCharacter(string escapeCharacter)
    {
        // Arrange
        string text = escapeCharacter;

        // Act
        ArgumentException? ex = Assert.Throws<ArgumentException>(
            () => LinkText.FromString(text)
        );

        // Assert
        Assert.NotNull(ex);
        Assert.StartsWith($"Link text must not contain illegal character \"{escapeCharacter}\".", ex.Message);
    }

    [Theory]
    [InlineData("text", 2, "  ", "    text")]
    [InlineData("mermaid is fun", 1, " ", " mermaid is fun")]
    public void ToMermaidString_WhenIndentations(string text, int indentations, string indentationText, string expected)
    {
        // Arrange
        LinkText linkText = LinkText.FromString(text);

        // Act
        string actual = linkText.ToMermaidString(indentations, indentationText);

        // Assert
        Assert.Equal(expected, actual);
    }
}
