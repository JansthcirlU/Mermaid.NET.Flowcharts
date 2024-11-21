namespace Mermaid.Flowcharts.Tests;

public class LinkTextTests
{
    [Fact]
    public void LinkText_ShouldThrow_WhenNewedWithDefaultConstructor()
    {
        // Act
        InvalidOperationException? ex = Assert.Throws<InvalidOperationException>(
            () => new LinkText()
        );

        // Assert
        Assert.NotNull(ex);
        Assert.StartsWith("You must create a link text with a value.", ex.Message);
    }

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
}