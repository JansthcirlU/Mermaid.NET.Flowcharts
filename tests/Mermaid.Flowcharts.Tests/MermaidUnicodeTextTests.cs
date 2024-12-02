using Mermaid.Flowcharts.Nodes;

namespace Mermaid.Flowcharts.Tests;

public class MermaidUnicodeTextTests
{
    [Fact]
    public void TextCreation_ShouldFail_WhenEmpty()
    {
        // Arrange
        string text = string.Empty;

        // Act
        ArgumentException? ex = Assert.Throws<ArgumentException>(
            () => MermaidUnicodeText.FromString(text)
        );

        // Assert
        Assert.NotNull(ex);
        Assert.StartsWith("Mermaid text should not be empty.", ex.Message);
    }

    [Theory]
    [InlineData("\n")]
    [InlineData("\r")]
    public void TextCreation_ShouldFail_WhenNewline(string newline)
    {
        // Arrange
        string text = newline;

        // Act
        ArgumentException? ex = Assert.Throws<ArgumentException>(
            () => MermaidUnicodeText.FromString(text)
        );

        // Assert
        Assert.NotNull(ex);
        Assert.StartsWith("Mermaid text should not contain new lines.", ex.Message);
    }

    [Theory]
    [InlineData('"', "&quot;")]
    [InlineData('#', "#35;")]
    [InlineData('<', "&lt;")]
    [InlineData('>', "&gt;")]
    [InlineData('&', "&amp;")]
    [InlineData('\\', "#92;")]
    [InlineData('\'', "&apos;")]
    public void TextCreation_ShouldReplaceConflictingCharacters_WhenEscapable(char conflictingCharacter, string replacement)
    {
        // Arrange
        string text = conflictingCharacter.ToString();

        // Act
        MermaidUnicodeText mermaidText = MermaidUnicodeText.FromString(text);

        // Assert
        Assert.Equal(mermaidText.Value, replacement);
    }
}
