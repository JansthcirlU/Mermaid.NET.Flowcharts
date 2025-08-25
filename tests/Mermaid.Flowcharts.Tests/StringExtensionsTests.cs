namespace Mermaid.Flowcharts.Tests;

public class StringExtensionsTests
{
    [Fact]
    public void RepeatString_ShouldReturnEmpty_WhenTextIsNull()
    {
        // Arrange
        string text = null!;

        // Act
        string repeated = text.Repeat(5);

        // Assert
        Assert.Equal(string.Empty, repeated);
    }

    [Fact]
    public void RepeatString_ShouldReturnEmpty_WhenTextIsEmpty()
    {
        // Arrange
        string text = string.Empty;

        // Act
        string repeated = text.Repeat(5);

        // Assert
        Assert.Equal(string.Empty, repeated);
    }

    [Theory]
    [InlineData("a", 3, "aaa")]
    [InlineData("test", 5, "testtesttesttesttest")]
    public void RepeatString(string text, int count, string expected)
    {
        // Act
        string repeated = text.Repeat(count);

        // Assert
        Assert.Equal(expected, repeated);
    }
}
