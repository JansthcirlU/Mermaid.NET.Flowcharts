using Mermaid.Flowcharts.Styling.Attributes;
using Mermaid.Flowcharts.Styling.Attributes.Enums;
using Mermaid.Flowcharts.Tests.Styling.Attributes.Seeding;

namespace Mermaid.Flowcharts.Tests.Styling.Attributes;

public class StrokeWidthTests
{
    public static TheoryData<double, Unit, string> LengthData => AttributeValueSeeder.SeedLengthData("stroke-width:");
    public static TheoryData<double, string> PercentageData => AttributeValueSeeder.SeedPercentageData("stroke-width:");
    public static TheoryData<double, string> NumericalData => AttributeValueSeeder.SeedNumericalData("stroke-width:");

    [Theory]
    [InlineData(-1)]
    [InlineData(double.MinValue)]
    public void StrokeWidth_WhenNegativePercentage_ShouldThrow(double value)
    {
        // Act
        ArgumentOutOfRangeException? ex = Assert.Throws<ArgumentOutOfRangeException>(
            () => StrokeWidth.Percentage(value)
        );

        // Assert
        Assert.NotNull(ex);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(double.MinValue)]
    public void StrokeWidth_WhenNegativeLength_ShouldThrow(double value)
    {
        // Act
        ArgumentOutOfRangeException? ex = Assert.Throws<ArgumentOutOfRangeException>(
            () => StrokeWidth.Length(value, Unit.Px)
        );

        // Assert
        Assert.NotNull(ex);
        Assert.StartsWith("Stroke width must not be negative.", ex.Message);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(double.MinValue)]
    public void StrokeWidth_WhenNegativeNumber_ShouldThrow(double value)
    {
        // Act
        ArgumentOutOfRangeException? ex = Assert.Throws<ArgumentOutOfRangeException>(
            () => StrokeWidth.Number(value)
        );

        // Assert
        Assert.NotNull(ex);
        Assert.StartsWith("Stroke width must not be negative.", ex.Message);
    }

    [Theory]
    [MemberData(nameof(LengthData))]
    public void StrokeWidth_WhenNonPixelLength_ShouldShowUnit(double lenghtValue, Unit unit, string expected)
    {
        // Arrange
        StrokeWidth.LengthStrokeWidth strokeWidth = StrokeWidth.Length(lenghtValue, unit);

        // Act
        string mermaid = strokeWidth.ToMermaidString();

        // Assert
        Assert.Equal(expected, mermaid);
    }

    [Theory]
    [MemberData(nameof(PercentageData))]
    public void StrokeWidth_WhenPercentage_ToMermaidString(double size, string expected)
    {
        // Arrange
        StrokeWidth.PercentageStrokeWidth strokeWidth = StrokeWidth.Percentage(size);

        // Act
        string mermaid = strokeWidth.ToMermaidString();

        // Assert
        Assert.Equal(expected, mermaid);
    }

    [Theory]
    [MemberData(nameof(NumericalData))]
    public void StrokeWidth_WhenNumericalLength_ToMermaidString(double size, string expected)
    {
        // Arrange
        StrokeWidth.NumericalStrokeWidth strokeWidth = StrokeWidth.Number(size);

        // Act
        string mermaid = strokeWidth.ToMermaidString();

        // Assert
        Assert.Equal(expected, mermaid);
    }
}
