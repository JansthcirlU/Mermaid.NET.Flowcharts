using Mermaid.Flowcharts.NonEmptyStringTypes;

namespace Mermaid.Flowcharts.Tests.NonEmptyStringTypes;

public class NonEmptyStringTests
{
    [Fact]
    public void Constructor_WithValidString_ShouldSetValue()
    {
        // Arrange
        string input = "valid string";

        // Act
        NonEmptyString result = new(input);

        // Assert
        Assert.Equal(input, result.Value);
    }

    [Fact]
    public void Constructor_WithNull_ShouldThrowArgumentException()
    {
        // Act & Assert
        ArgumentException exception = Assert.Throws<ArgumentException>(() => new NonEmptyString(null!));
        Assert.Contains("Non-empty string must not be null or empty", exception.Message);
        Assert.Equal("value", exception.ParamName);
    }

    [Fact]
    public void Constructor_WithEmptyString_ShouldThrowArgumentException()
    {
        // Act & Assert
        ArgumentException exception = Assert.Throws<ArgumentException>(() => new NonEmptyString(""));
        Assert.Contains("Non-empty string must not be null or empty", exception.Message);
        Assert.Equal("value", exception.ParamName);
    }

    [Theory]
    [InlineData(" ")]
    [InlineData("\t")]
    [InlineData("\n")]
    [InlineData("\r")]
    [InlineData("  \t  ")]
    public void Constructor_WithWhitespaceOnly_ShouldThrowArgumentException(string whitespace)
    {
        // Updated: whitespace-only strings should now be rejected
        // Act & Assert
        ArgumentException exception = Assert.Throws<ArgumentException>(() => new NonEmptyString(whitespace));
        Assert.Contains("Non-empty string must not be null or empty", exception.Message);
        Assert.Equal("value", exception.ParamName);
    }

    [Fact]
    public void ImplicitConversionToString_ShouldReturnValue()
    {
        // Arrange
        NonEmptyString nes = new("test");

        // Act
        string result = nes;

        // Assert
        Assert.Equal("test", result);
    }

    [Fact]
    public void ImplicitConversionFromString_WithValidString_ShouldCreateInstance()
    {
        // Arrange
        string input = "test";

        // Act
        NonEmptyString result = input;

        // Assert
        Assert.Equal(input, result.Value);
    }

    [Fact]
    public void ImplicitConversionFromString_WithNull_ShouldThrowArgumentException()
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() =>
        {
            NonEmptyString result = null!;
        });
    }

    [Fact]
    public void ImplicitConversionFromString_WithEmptyString_ShouldThrowArgumentException()
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() =>
        {
            NonEmptyString result = "";
        });
    }

    [Fact]
    public void ToString_ShouldReturnValue()
    {
        // Arrange
        NonEmptyString nes = new("test value");

        // Act
        string result = nes.ToString();

        // Assert
        Assert.Equal("test value", result);
    }

    [Fact]
    public void Equality_WithSameValue_ShouldBeEqual()
    {
        // Arrange
        NonEmptyString nes1 = new("same");
        NonEmptyString nes2 = new("same");

        // Act & Assert
        Assert.Equal(nes1, nes2);
        Assert.True(nes1 == nes2);
        Assert.False(nes1 != nes2);
    }

    [Fact]
    public void Equality_WithDifferentValues_ShouldNotBeEqual()
    {
        // Arrange
        NonEmptyString nes1 = new("different1");
        NonEmptyString nes2 = new("different2");

        // Act & Assert
        Assert.NotEqual(nes1, nes2);
        Assert.False(nes1 == nes2);
        Assert.True(nes1 != nes2);
    }

    [Fact]
    public void GetHashCode_WithSameValue_ShouldHaveSameHashCode()
    {
        // Arrange
        NonEmptyString nes1 = new("same");
        NonEmptyString nes2 = new("same");

        // Act & Assert
        Assert.Equal(nes1.GetHashCode(), nes2.GetHashCode());
    }
}
