using Mermaid.Flowcharts.Nodes.NodeText;

namespace Mermaid.Flowcharts.Tests;

public class MarkdownTextTests
{
    [Fact]
    public void TextCreation_ShouldFail_WhenEmpty()
    {
        // Arrange
        string text = string.Empty;

        // Act
        ArgumentException? ex = Assert.Throws<ArgumentException>(
            () => MarkdownText.FromString(text)
        );

        // Assert
        Assert.NotNull(ex);
        Assert.StartsWith("Mermaid text should not be empty.", ex.Message);
    }

    [Theory]
    [InlineData('"', "`&quot;`")]
    [InlineData('\\', "`#92;`")]
    public void TextCreation_ShouldReplaceConflictingCharacters_WhenEscapable(char conflictingCharacter, string replacement)
    {
        // Arrange
        string text = conflictingCharacter.ToString();

        // Act
        MarkdownText mermaidText = MarkdownText.FromString(text);

        // Assert
        Assert.Equal(mermaidText.Value, replacement);
    }
}
