namespace Mermaid.Flowcharts.Tests;

public class FlowchartTitleTests
{
    [Fact]
    public void Title_ShouldThrow_WhenNull()
    {
        // Arrange
        string text = null!;

        // Act
        ArgumentException? ex = Assert.Throws<ArgumentException>(
            () => FlowchartTitle.FromString(text)
        );

        // Assert
        Assert.NotNull(ex);
        Assert.StartsWith("Flowchart title must not be null or empty.", ex.Message);
    }

    [Fact]
    public void Title_ShouldThrow_WhenEmpty()
    {
        // Arrange
        string text = string.Empty;

        // Act
        ArgumentException? ex = Assert.Throws<ArgumentException>(
            () => FlowchartTitle.FromString(text)
        );

        // Assert
        Assert.NotNull(ex);
        Assert.StartsWith("Flowchart title must not be null or empty.", ex.Message);
    }

    [Theory]
    [InlineData(" ")]
    [InlineData("\t")]
    public void Title_ShouldThrow_WhenWhitespace(string whitespace)
    {
        // Arrange
        string text = whitespace;

        // Act
        ArgumentException? ex = Assert.Throws<ArgumentException>(
            () => FlowchartTitle.FromString(text)
        );

        // Assert
        Assert.NotNull(ex);
        Assert.StartsWith("Flowchart title must not be whitespace.", ex.Message);
    }

    [Theory]
    [InlineData("\n")]
    [InlineData("\r")]
    public void Title_ShouldThrow_WhenNewline(string newline)
    {
        // Arrange
        string text = newline;

        // Act
        ArgumentException? ex = Assert.Throws<ArgumentException>(
            () => FlowchartTitle.FromString(text)
        );

        // Assert
        Assert.NotNull(ex);
        Assert.StartsWith("Flowchart title must not contain new lines.", ex.Message);
    }

    [Theory]
    [InlineData(
        'a',
        """
        ---
        title: a
        ---
        """)]
    public void Title_ShouldToMermaidString_SingleLetter(char letter, string expected)
    {
        // Arrange
        string text = letter.ToString();
        FlowchartTitle title = FlowchartTitle.FromString(text);

        // Act
        string actual = title.ToMermaidString();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(
        'a',
        2,
        "  ",
        """
            ---
            title: a
            ---
        """
    )]
    public void ToMermaidString_WhenIndentations(char letter, int indentations, string indentationText, string expected)
    {
        // Arrange
        string text = letter.ToString();
        FlowchartTitle title = FlowchartTitle.FromString(text);

        // Act
        string actual = title.ToMermaidString(indentations, indentationText);

        // Assert
        Assert.Equal(expected, actual);
    }
}
