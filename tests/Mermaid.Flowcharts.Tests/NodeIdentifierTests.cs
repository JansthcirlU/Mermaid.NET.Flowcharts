using Mermaid.Flowcharts.Nodes;

namespace Mermaid.Flowcharts.Tests;

public class NodeIdentifierTests
{
    [Fact]
    public void NodeIdentifier_ShouldThrow_WhenEmpty()
    {
        // Arrange
        string id = string.Empty;

        // Act
        ArgumentException? ex = Assert.Throws<ArgumentException>(
            () => NodeIdentifier.FromString(id)
        );

        // Assert
        Assert.NotNull(ex);
        Assert.StartsWith("Identifier must not be empty.", ex.Message);
    }

    [Theory]
    [InlineData(" ")]
    [InlineData("\t")]
    [InlineData("\n")]
    [InlineData("\r")]
    public void NodeIdentifier_ShouldThrow_WhenWhitespace(string whitespace)
    {
        // Arrange
        string id = whitespace;

        // Act
        ArgumentException? ex = Assert.Throws<ArgumentException>(
            () => NodeIdentifier.FromString(id)
        );

        // Assert
        Assert.NotNull(ex);
        Assert.StartsWith("Identifier must not be whitespace.", ex.Message);
    }

    [Theory]
    [InlineData("a__b")]
    [InlineData("a_.b")]
    [InlineData("a_-b")]
    [InlineData("a._b")]
    [InlineData("a..b")]
    [InlineData("a.-b")]
    [InlineData("a-_b")]
    [InlineData("a-.b")]
    [InlineData("a--b")]
    public void NodeIdentifier_ShouldThrow_WhenTwoSeparatorsAreUsedInARow(string invalidIdentifier)
    {
        // Arrange
        string id = invalidIdentifier;

        // Act
        ArgumentException? ex = Assert.Throws<ArgumentException>(
            () => NodeIdentifier.FromString(id)
        );

        // Assert
        Assert.NotNull(ex);
        Assert.StartsWith("Identifier must never contain two separators in a row.", ex.Message);
    }

    [Theory]
    [InlineData("a_")]
    [InlineData("a.")]
    [InlineData("a-")]
    public void NodeIdentifier_ShouldThrow_WhenIdentifierEndsWithSeparator(string invalidIdentifier)
    {
        // Arrange
        string id = invalidIdentifier;

        // Act
        ArgumentException? ex = Assert.Throws<ArgumentException>(
            () => NodeIdentifier.FromString(id)
        );

        // Assert
        Assert.NotNull(ex);
        Assert.StartsWith("Identifier must not end with a separator.", ex.Message);
    }

    [Theory]
    [InlineData("_a")]
    [InlineData(".a")]
    [InlineData("-a")]
    public void NodeIdentifier_ShouldThrow_WhenIdentifierStartsWithSeparator(string invalidIdentifier)
    {
        // Arrange
        string id = invalidIdentifier;

        // Act
        ArgumentException? ex = Assert.Throws<ArgumentException>(
            () => NodeIdentifier.FromString(id)
        );

        // Assert
        Assert.NotNull(ex);
        Assert.StartsWith("Identifier must not start with a separator.", ex.Message);
    }
}