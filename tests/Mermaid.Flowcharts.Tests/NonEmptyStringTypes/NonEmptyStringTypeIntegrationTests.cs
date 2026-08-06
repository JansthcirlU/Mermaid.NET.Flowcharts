using Mermaid.Flowcharts.NonEmptyStringTypes;

namespace Mermaid.Flowcharts.Tests.NonEmptyStringTypes;

// Integration tests to verify the types work together
public class NonEmptyStringTypeIntegrationTests
{
    [Fact]
    public void NonEmptySingleLineString_CanBeAssignedToNonEmptyString()
    {
        // Arrange
        NonEmptySingleLineString nesls = NonEmptySingleLineString.FromString("test");

        // Act
        NonEmptyString nes = nesls.Value;

        // Assert
        Assert.Equal("test", nes.Value);
    }

    [Fact]
    public void ChainedImplicitConversions_ShouldWork()
    {
        // Arrange
        string original = "test string";

        // Act - chain conversions
        NonEmptySingleLineString nesls = NonEmptySingleLineString.FromString(original);
        string result = nesls;

        // Assert
        Assert.Equal(original, result);
    }
}
