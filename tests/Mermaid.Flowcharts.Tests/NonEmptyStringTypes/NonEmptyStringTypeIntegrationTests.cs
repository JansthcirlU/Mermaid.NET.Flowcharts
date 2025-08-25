using Mermaid.Flowcharts.NonEmptyStringTypes;

namespace Mermaid.Flowcharts.Tests.NonEmptyStringTypes;

// Integration tests to verify the types work together
public class NonEmptyStringTypeIntegrationTests
{
    [Fact]
    public void NonEmptySingleLineString_CanBeAssignedToNonEmptyString()
    {
        // Arrange
        NonEmptySingleLineString nesls = new(new NonEmptyString("test"));

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
        NonEmptySingleLineString nesls = original;
        string result = nesls;

        // Assert
        Assert.Equal(original, result);
    }

    [Fact]
    public void MixedUsageInCollections_ShouldWork()
    {
        // Arrange
        string[] strings = ["first", "second", "third"];

        // Act
        NonEmptyString[] nonEmptyStrings = [.. strings.Select(s => (NonEmptyString)s)];
        NonEmptySingleLineString[] singleLineStrings = [.. nonEmptyStrings.Select(nes => new NonEmptySingleLineString(nes))];

        // Assert
        Assert.Equal(3, singleLineStrings.Length);
        Assert.Equal("first", singleLineStrings[0].Value);
        Assert.Equal("second", singleLineStrings[1].Value);
        Assert.Equal("third", singleLineStrings[2].Value);
    }
}
