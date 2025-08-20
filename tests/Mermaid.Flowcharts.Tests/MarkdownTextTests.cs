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
    [InlineData("a\nb", "`a\nb`")]
    [InlineData(
        """
        a

        b
        """,
        """
        `a

        b`
        """
    )]
    [InlineData(
        """
        
        a

        b
        """,
        """
        `
        a
        
        b`
        """
    )]
    [InlineData(
        """
        
        a

        b
        
        """,
        """
        `
        a
        
        b
        `
        """
    )]
    public void TextCreation_ShouldPreserveSingleNewline(string input, string expected)
    {
        // Arrange & act
        MarkdownText mermaidText = MarkdownText.FromString(input);

        // Assert
        Assert.Equal(expected, mermaidText.Value);
    }

    [Theory]
    [InlineData("\"", "`&quot;`")]
    [InlineData("\\", "`#92;`")]
    [InlineData("`", "`#96;`")]
    [InlineData("\"\\`", "`&quot;#92;#96;`")]
    public void TextCreation_ShouldReplaceConflictingCharacters_WhenEscapable(string conflictingString, string replacement)
    {
        // Arrange & act
        MarkdownText mermaidText = MarkdownText.FromString(conflictingString);

        // Assert
        Assert.Equal(replacement, mermaidText.Value);
    }
}
