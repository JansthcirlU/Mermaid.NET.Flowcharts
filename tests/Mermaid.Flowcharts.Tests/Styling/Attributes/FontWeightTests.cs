using Mermaid.Flowcharts.Styling.Attributes.Fonts;

namespace Mermaid.Flowcharts.Tests.Styling.Attributes;

public class FontWeightTests
{
    public static TheoryData<Flowcharts.Styling.Attributes.Enums.FontWeight, string> GetRelativeFontWeightData()
    {
        TheoryData<Flowcharts.Styling.Attributes.Enums.FontWeight, string> data = [];
        foreach (Flowcharts.Styling.Attributes.Enums.FontWeight weight in Enum.GetValues<Flowcharts.Styling.Attributes.Enums.FontWeight>())
        {
            data.Add(weight, $"font-weight:{EnumRendering.FontWeights[weight]}");
        }
        return data;
    }

    [Theory]
    [MemberData(nameof(GetRelativeFontWeightData))]
    public void FontWeight_WhenRelative_ToMermaidString(Flowcharts.Styling.Attributes.Enums.FontWeight weight, string expected)
    {
        // Arrange
        FontWeight.RelativeFontWeight relative = FontWeight.Relative(weight);

        // Act
        string mermaid = relative.ToMermaidString();

        // Assert
        Assert.Equal(expected, mermaid);
    }

    [Theory]
    [InlineData(double.MinValue)]
    [InlineData(-1)]
    [InlineData(0)]
    public void FontWeight_WhenLessThanOne_ShouldThrow(double lessThanOne)
    {
        // Act
        ArgumentOutOfRangeException? ex = Assert.Throws<ArgumentOutOfRangeException>(
            () => FontWeight.Numerical(lessThanOne)
        );

        // Assert
        Assert.NotNull(ex);
        Assert.StartsWith("Font weight must be at least 1.", ex.Message);
    }

    [Theory]
    [InlineData(double.MaxValue)]
    [InlineData(1001)]
    public void FontWeight_WhenGreaterThan1000_ShouldThrow(double greaterThan1000)
    {
        // Act
        ArgumentOutOfRangeException? ex = Assert.Throws<ArgumentOutOfRangeException>(
            () => FontWeight.Numerical(greaterThan1000)
        );

        // Assert
        Assert.NotNull(ex);
        Assert.StartsWith("Font weight must be at most 1000.", ex.Message);
    }

    [Theory]
    [InlineData(double.NegativeInfinity)]
    [InlineData(double.PositiveInfinity)]
    [InlineData(double.NaN)]
    public void FontWeight_WhenNotFiniteOrInvalid_ShouldThrow(double notFiniteOrInvalid)
    {
        // Act
        ArgumentOutOfRangeException? ex = Assert.Throws<ArgumentOutOfRangeException>(
            () => FontWeight.Numerical(notFiniteOrInvalid)
        );

        // Assert
        Assert.NotNull(ex);
        Assert.StartsWith("Font weight must be a real number between 1 and 1000.", ex.Message);
    }
}
