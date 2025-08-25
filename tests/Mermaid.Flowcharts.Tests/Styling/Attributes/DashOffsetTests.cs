using Mermaid.Flowcharts.Styling.Attributes;
using Mermaid.Flowcharts.Styling.Attributes.Enums;
using Mermaid.Flowcharts.Tests.Styling.Attributes.Seeding;

namespace Mermaid.Flowcharts.Tests.Styling.Attributes;

public class DashOffsetTests
{
    public static TheoryData<double, Unit, string> LengthData => AttributeValueSeeder.SeedLengthData("stroke-dashoffset: ");
    public static TheoryData<double, string> PercentageData => AttributeValueSeeder.SeedPercentageData("stroke-dashoffset: ");
    public static TheoryData<double, string> NumericalData => AttributeValueSeeder.SeedNumericalData("stroke-dashoffset: ");

    [Theory]
    [InlineData(-1)]
    [InlineData(double.MinValue)]
    public void DashOffset_WhenNegativePercentage_ShouldThrow(double value)
    {
        // Act
        ArgumentOutOfRangeException? ex = Assert.Throws<ArgumentOutOfRangeException>(
            () => DashOffset.Percentage(value)
        );

        // Assert
        Assert.NotNull(ex);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(double.MinValue)]
    public void DashOffset_WhenNegativeLength_ShouldThrow(double value)
    {
        // Act
        ArgumentOutOfRangeException? ex = Assert.Throws<ArgumentOutOfRangeException>(
            () => DashOffset.Length(value, Unit.Px)
        );

        // Assert
        Assert.NotNull(ex);
        Assert.StartsWith("Dash length offset must not be negative.", ex.Message);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(double.MinValue)]
    public void DashOffset_WhenNegativeNumber_ShouldThrow(double value)
    {
        // Act
        ArgumentOutOfRangeException? ex = Assert.Throws<ArgumentOutOfRangeException>(
            () => DashOffset.Number(value)
        );

        // Assert
        Assert.NotNull(ex);
        Assert.StartsWith("Dash offset must not be negative.", ex.Message);
    }

    [Theory]
    [MemberData(nameof(LengthData))]
    public void DashOffset_WhenLength_ShouldShowUnit(double lenghtValue, Unit unit, string expected)
    {
        // Arrange
        DashOffset.LengthDashOffset dashOffset = DashOffset.Length(lenghtValue, unit);

        // Act
        string mermaid = dashOffset.ToMermaidString();

        // Assert
        Assert.Equal(expected, mermaid);
    }

    [Theory]
    [MemberData(nameof(PercentageData))]
    public void DashOffset_WhenPercentage_ToMermaidString(double size, string expected)
    {
        // Arrange
        DashOffset.PercentageDashOffset dashOffset = DashOffset.Percentage(size);

        // Act
        string mermaid = dashOffset.ToMermaidString();

        // Assert
        Assert.Equal(expected, mermaid);
    }

    [Theory]
    [MemberData(nameof(NumericalData))]
    public void DashOffset_WhenNumericalLength_ToMermaidString(double size, string expected)
    {
        // Arrange
        DashOffset.NumericalDashOffset dashOffset = DashOffset.Number(size);

        // Act
        string mermaid = dashOffset.ToMermaidString();

        // Assert
        Assert.Equal(expected, mermaid);
    }
}
