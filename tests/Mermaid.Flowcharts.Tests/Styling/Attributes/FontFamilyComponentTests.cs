using Mermaid.Flowcharts.Styling.Attributes.Fonts;

namespace Mermaid.Flowcharts.Tests.Styling.Attributes;

public class FontFamilyComponentTests
{
    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("   ")]
    [InlineData("\t")]
    [InlineData("\n")]
    public void FontFamilyComponent_WhenWhitespace_ShouldThrow(string whitespace)
    {
        // Act
        ArgumentException? ex = Assert.Throws<ArgumentException>(
            () => new FontFamilyComponent(whitespace)
        );

        // Assert
        Assert.NotNull(ex);
        Assert.StartsWith("Font family component must not be null or whitespace.", ex.Message);
    }

    [Theory]
    [InlineData("\"Arial\"")]
    [InlineData("'Arial'")]
    [InlineData("Times \"New\" Roman")]
    [InlineData("Helvetica'Neue")]
    [InlineData("'")]
    [InlineData("\"")]
    public void FontFamilyComponent_WhenQuotes_ShouldThrow(string quotes)
    {
        // Act
        ArgumentException? ex = Assert.Throws<ArgumentException>(
            () => new FontFamilyComponent(quotes)
        );

        // Assert
        Assert.NotNull(ex);
        Assert.StartsWith("Font family component must not contain single or double quotes.", ex.Message);
    }

    [Theory]
    [InlineData("Arial123")]           // contains numbers
    [InlineData("Arial_Bold")]         // contains underscore
    [InlineData("Arial  Bold")]        // double space
    [InlineData("Arial--Bold")]        // double hyphen
    [InlineData("Arial   Bold")]       // triple space
    [InlineData("Arial---Bold")]       // triple hyphen
    [InlineData(" Arial")]             // leading space
    [InlineData("-Arial")]             // leading hyphen
    [InlineData("Arial ")]             // trailing space
    [InlineData("Arial-")]             // trailing hyphen
    [InlineData("123")]                // only numbers
    [InlineData("Arial@Bold")]         // contains special character

    public void FontFamilyComponent_WhenNotSpaceSeparatedWords_ShouldThrow(string invalidValue)
    {
        // Act
        ArgumentException? ex = Assert.Throws<ArgumentException>(
            () => new FontFamilyComponent(invalidValue)
        );

        // Assert
        Assert.NotNull(ex);
        Assert.StartsWith("Font family component must only contain words that are separated by at most one space.", ex.Message);
    }

    [Theory]
    [InlineData("Arial")]
    [InlineData("Times")]
    [InlineData("Times New Roman")]
    [InlineData("Helvetica Neue")]
    [InlineData("Comic Sans MS")]
    [InlineData("A")]
    [InlineData("A B")]
    [InlineData("A B C D E")]
    [InlineData("sans-serif")]
    [InlineData("ui-monospace")]
    public void FontFamilyComponent_WhenSpaceOrHyphenSeparatedWords_ShouldSucceed(string valid)
    {
        // Act
        var component = new FontFamilyComponent(valid);

        // Assert
        Assert.Equal(valid, component.Value);
    }
}
