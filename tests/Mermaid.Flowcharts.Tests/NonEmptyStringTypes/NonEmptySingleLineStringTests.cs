using Mermaid.Flowcharts.NonEmptyStringTypes;

namespace Mermaid.Flowcharts.Tests.NonEmptyStringTypes;

public class NonEmptySingleLineStringTests
{
    [Fact]
    public void Constructor_WithValidSingleLineString_ShouldSetValue()
    {
        // Arrange
        NonEmptyString input = new("valid single line");
        
        // Act
        NonEmptySingleLineString result = new(input);
        
        // Assert
        Assert.Equal(input, result.Value);
    }

    [Fact]
    public void Constructor_WithStringContainingNewline_ShouldThrowArgumentException()
    {
        // Arrange
        NonEmptyString input = new("line1\nline2");
        
        // Act & Assert
        ArgumentException exception = Assert.Throws<ArgumentException>(() => new NonEmptySingleLineString(input));
        Assert.Contains("Non-empty single line string must not contain any newline characters or carriage returns", exception.Message);
        Assert.Equal("value", exception.ParamName);
    }

    [Fact]
    public void Constructor_WithStringContainingCarriageReturn_ShouldThrowArgumentException()
    {
        // Arrange
        NonEmptyString input = new("line1\rline2");
        
        // Act & Assert
        ArgumentException exception = Assert.Throws<ArgumentException>(() => new NonEmptySingleLineString(input));
        Assert.Contains("Non-empty single line string must not contain any newline characters or carriage returns", exception.Message);
        Assert.Equal("value", exception.ParamName);
    }

    [Fact]
    public void Constructor_WithStringContainingBothNewlineAndCarriageReturn_ShouldThrowArgumentException()
    {
        // Arrange
        NonEmptyString input = new("line1\r\nline2");
        
        // Act & Assert
        ArgumentException exception = Assert.Throws<ArgumentException>(() => new NonEmptySingleLineString(input));
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
        NonEmptySingleLineString result = new(new NonEmptyString(validText));
        Assert.Equal(validText, result.Value);
    }

    [Fact]
    public void ImplicitConversionToString_ShouldReturnValue()
    {
        // Arrange
        NonEmptySingleLineString nesls = new(new NonEmptyString("test"));
        
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
        NonEmptySingleLineString result = input;
        
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
            NonEmptySingleLineString result = null!;
        });
    }

    [Fact]
    public void ImplicitConversionFromString_WithEmptyString_ShouldThrowArgumentException()
    {
        // This should fail at the NonEmptyString level first
        // Act & Assert
        Assert.Throws<ArgumentException>(() => 
        {
            NonEmptySingleLineString result = "";
        });
    }

    [Fact]
    public void ImplicitConversionFromString_WithNewline_ShouldThrowArgumentException()
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => 
        {
            NonEmptySingleLineString result = "line1\nline2";
        });
    }

    [Fact]
    public void ImplicitConversionFromString_WithCarriageReturn_ShouldThrowArgumentException()
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => 
        {
            NonEmptySingleLineString result = "line1\rline2";
        });
    }

    [Fact]
    public void ToString_ShouldReturnValue()
    {
        // Arrange
        NonEmptySingleLineString nesls = new(new NonEmptyString("test value"));
        
        // Act
        string result = nesls.ToString();
        
        // Assert
        Assert.Equal("test value", result);
    }

    [Fact]
    public void Equality_WithSameValue_ShouldBeEqual()
    {
        // Arrange
        NonEmptySingleLineString nesls1 = new(new NonEmptyString("same"));
        NonEmptySingleLineString nesls2 = new(new NonEmptyString("same"));
        
        // Act & Assert
        Assert.Equal(nesls1, nesls2);
        Assert.True(nesls1 == nesls2);
        Assert.False(nesls1 != nesls2);
    }

    [Fact]
    public void Equality_WithDifferentValues_ShouldNotBeEqual()
    {
        // Arrange
        NonEmptySingleLineString nesls1 = new(new NonEmptyString("different1"));
        NonEmptySingleLineString nesls2 = new(new NonEmptyString("different2"));
        
        // Act & Assert
        Assert.NotEqual(nesls1, nesls2);
        Assert.False(nesls1 == nesls2);
        Assert.True(nesls1 != nesls2);
    }

    [Fact]
    public void GetHashCode_WithSameValue_ShouldHaveSameHashCode()
    {
        // Arrange
        NonEmptySingleLineString nesls1 = new(new NonEmptyString("same"));
        NonEmptySingleLineString nesls2 = new(new NonEmptyString("same"));
        
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
        ArgumentException exception = Assert.Throws<ArgumentException>(() => new NonEmptySingleLineString(new NonEmptyString(textWithUnicodeLineSeparators)));
        Assert.Contains("Non-empty single line string must not contain any newline characters or carriage returns", exception.Message);
    }
}
