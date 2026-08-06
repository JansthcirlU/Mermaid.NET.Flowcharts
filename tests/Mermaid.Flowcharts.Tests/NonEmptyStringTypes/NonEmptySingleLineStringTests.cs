using Mermaid.Flowcharts.NonEmptyStringTypes;

namespace Mermaid.Flowcharts.Tests.NonEmptyStringTypes;

public class NonEmptySingleLineStringTests
{
    [Fact]
    public void Constructor_WithValidSingleLineString_ShouldSetValue()
    {
        // Arrange
        string input = "valid single line";

        // Act
        NonEmptySingleLineString result = NonEmptySingleLineString.FromString(input);

        // Assert
        Assert.Equal(input, result.Value);
    }

    [Fact]
    public void Constructor_WithStringContainingNewline_ShouldThrowArgumentException()
    {
        // Arrange
        string input = "line1\nline2";

        // Act & Assert
        ArgumentException exception = Assert.Throws<ArgumentException>(() => NonEmptySingleLineString.FromString(input));
        Assert.Contains("Non-empty single line string must not contain any newline characters or carriage returns", exception.Message);
        Assert.Equal("value", exception.ParamName);
    }

    [Fact]
    public void Constructor_WithStringContainingCarriageReturn_ShouldThrowArgumentException()
    {
        // Arrange
        string input = "line1\rline2";

        // Act & Assert
        ArgumentException exception = Assert.Throws<ArgumentException>(() => NonEmptySingleLineString.FromString(input));
        Assert.Contains("Non-empty single line string must not contain any newline characters or carriage returns", exception.Message);
        Assert.Equal("value", exception.ParamName);
    }

    [Fact]
    public void Constructor_WithStringContainingBothNewlineAndCarriageReturn_ShouldThrowArgumentException()
    {
        // Arrange
        string input = "line1\r\nline2";

        // Act & Assert
        ArgumentException exception = Assert.Throws<ArgumentException>(() => NonEmptySingleLineString.FromString(input));
        Assert.Contains("Non-empty single line string must not contain any newline characters or carriage returns", exception.Message);
    }

    [Theory]
    [InlineData("simple text")]
    [InlineData("text with\ttabs")]
    [InlineData("text with  spaces")]
    [InlineData("text with other whitespace like \u00A0")]
    public void Constructor_WithValidSingleLineStrings_ShouldSucceed(string validText)
    {
        // Act & Assert (should not throw)
        NonEmptySingleLineString result = NonEmptySingleLineString.FromString(validText);
        Assert.Equal(validText, result.Value);
    }

    [Fact]
    public void ImplicitConversionToString_ShouldReturnValue()
    {
        // Arrange
        NonEmptySingleLineString nesls = NonEmptySingleLineString.FromString("test");

        // Act
        string result = nesls;

        // Assert
        Assert.Equal("test", result);
    }

    [Fact]
    public void ImplicitConversionFromString_WithValidString_ShouldCreateInstance()
    {
        // Arrange
        string input = "test";

        // Act
        NonEmptySingleLineString result = NonEmptySingleLineString.FromString(input);

        // Assert
        Assert.Equal(input, result.Value);
    }

    [Fact]
    public void ImplicitConversionFromString_WithNull_ShouldThrowArgumentException()
    {
        // This should fail at the NonEmptyString level first
        // Act & Assert
        Assert.Throws<ArgumentException>(() =>
        {
            NonEmptySingleLineString result = NonEmptySingleLineString.FromString(null!);
        });
    }

    [Fact]
    public void ImplicitConversionFromString_WithEmptyString_ShouldThrowArgumentException()
    {
        // This should fail at the NonEmptyString level first
        // Act & Assert
        Assert.Throws<ArgumentException>(() =>
        {
            NonEmptySingleLineString result = NonEmptySingleLineString.FromString("");
        });
    }

    [Fact]
    public void ImplicitConversionFromString_WithNewline_ShouldThrowArgumentException()
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() =>
        {
            NonEmptySingleLineString result = NonEmptySingleLineString.FromString("line1\nline2");
        });
    }

    [Fact]
    public void ImplicitConversionFromString_WithCarriageReturn_ShouldThrowArgumentException()
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() =>
        {
            NonEmptySingleLineString result = NonEmptySingleLineString.FromString("line1\rline2");
        });
    }

    [Fact]
    public void ToString_ShouldReturnValue()
    {
        // Arrange
        NonEmptySingleLineString nesls = NonEmptySingleLineString.FromString("test value");

        // Act
        string result = nesls.ToString();

        // Assert
        Assert.Equal("test value", result);
    }

    [Fact]
    public void Equality_WithSameValue_ShouldBeEqual()
    {
        // Arrange
        NonEmptySingleLineString nesls1 = NonEmptySingleLineString.FromString("same");
        NonEmptySingleLineString nesls2 = NonEmptySingleLineString.FromString("same");

        // Act & Assert
        Assert.Equal(nesls1, nesls2);
        Assert.True(nesls1 == nesls2);
        Assert.False(nesls1 != nesls2);
    }

    [Fact]
    public void Equality_WithDifferentValues_ShouldNotBeEqual()
    {
        // Arrange
        NonEmptySingleLineString nesls1 = NonEmptySingleLineString.FromString("different1");
        NonEmptySingleLineString nesls2 = NonEmptySingleLineString.FromString("different2");

        // Act & Assert
        Assert.NotEqual(nesls1, nesls2);
        Assert.False(nesls1 == nesls2);
        Assert.True(nesls1 != nesls2);
    }

    [Fact]
    public void GetHashCode_WithSameValue_ShouldHaveSameHashCode()
    {
        // Arrange
        NonEmptySingleLineString nesls1 = NonEmptySingleLineString.FromString("same");
        NonEmptySingleLineString nesls2 = NonEmptySingleLineString.FromString("same");

        // Act & Assert
        Assert.Equal(nesls1.GetHashCode(), nesls2.GetHashCode());
    }

    // Updated: Unicode line separators should now throw exceptions
    [Theory]
    [InlineData("text\u2028text")] // Line Separator
    [InlineData("text\u2029text")] // Paragraph Separator
    [InlineData("text\u0085text")]  // Next Line (NEL)
    public void Constructor_WithUnicodeLineSeparators_ShouldThrowArgumentException(string textWithUnicodeLineSeparators)
    {
        // Updated: These should now be rejected as line separators
        // Act & Assert
        ArgumentException exception = Assert.Throws<ArgumentException>(() => NonEmptySingleLineString.FromString(textWithUnicodeLineSeparators));
        Assert.Contains("Non-empty single line string must not contain any newline characters or carriage returns", exception.Message);
    }
}
