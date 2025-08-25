using Mermaid.Flowcharts.Styling.Attributes;
using Mermaid.Flowcharts.Styling.Attributes.Enums;
using Mermaid.Flowcharts.Tests.Styling.Attributes.Seeding;

namespace Mermaid.Flowcharts.Tests.Styling.Attributes;

public class DashArrayTests
{
    public static TheoryData<double, Unit, string> LengthData => AttributeValueSeeder.SeedLengthData("stroke-dasharray: ");
    public static TheoryData<double, string> PercentageData => AttributeValueSeeder.SeedPercentageData("stroke-dasharray: ");
    public static TheoryData<double, string> NumericalData => AttributeValueSeeder.SeedNumericalData("stroke-dasharray: ");
    
    [Fact]
    public void DashArray_WhenEmpty_ShouldBeNone()
    {
        // Arrange
        DashArray dashArray = new([]);

        // Act
        string mermaid = dashArray.ToMermaidString();

        // Assert
        Assert.Equal("stroke-dasharray:none", mermaid);
    }

    [Theory]
    [MemberData(nameof(LengthData))]
    public void DashArray_WhenNonPixelLength_ShouldShowUnit(double lenghtValue, Unit unit, string expected)
    {
        // Arrange
        DashSize.LengthDashSize dashLength = DashSize.Length(lenghtValue, unit);
        DashArray dashArray = new([dashLength]);

        // Act
        string mermaid = dashArray.ToMermaidString();

        // Assert
        Assert.Equal(expected, mermaid);
    }

    [Theory]
    [MemberData(nameof(PercentageData))]
    public void DashArray_WhenPercentage_ToMermaidString(double size, string expected)
    {
        // Arrange
        DashSize.PercentageDashSize dashSize = DashSize.Percentage(size);
        DashArray dashArray = new([dashSize]);

        // Act
        string mermaid = dashArray.ToMermaidString();

        // Assert
        Assert.Equal(expected, mermaid);
    }

    [Theory]
    [MemberData(nameof(NumericalData))]
    public void DashArray_WhenNumericalLength_ToMermaidString(double size, string expected)
    {
        // Arrange
        DashSize.NumericalDashSize dashSize = DashSize.Number(size);
        DashArray dashArray = new([dashSize]);

        // Act
        string mermaid = dashArray.ToMermaidString();

        // Assert
        Assert.Equal(expected, mermaid);
    }
}
