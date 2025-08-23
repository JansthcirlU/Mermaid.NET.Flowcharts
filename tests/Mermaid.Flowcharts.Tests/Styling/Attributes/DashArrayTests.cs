using Mermaid.Flowcharts.Styling.Attributes;
using Mermaid.Flowcharts.Styling.Attributes.Enums;

namespace Mermaid.Flowcharts.Tests.Styling.Attributes;

public class DashArrayTests
{
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
    [InlineData(new double[] { 1 }, "stroke-dasharray: 1")]
    [InlineData(new double[] { 2, 5 }, "stroke-dasharray: 2 5")]
    [InlineData(new double[] { 1, 2, 3 }, "stroke-dasharray: 1 2 3")]
    [InlineData(new double[] { 2, 5, 2, 5 }, "stroke-dasharray: 2 5 2 5")]
    public void DashArray_WhenPositivePixelLength_ShouldOmitUnit(double[] dashPixels, string expected)
    {
        // Arrange
        DashSize.LengthDashSize[] pixelDashes = [.. dashPixels
                .Select(px => DashSize.Length(px, Unit.Px))
            ];
        DashArray dashArray = new(pixelDashes);

        // Act
        string mermaid = dashArray.ToMermaidString();

        // Assert
        Assert.Equal(expected, mermaid);
    }

    [Theory]
    [InlineData(5, Unit.Px, "stroke-dasharray: 5")]
    [InlineData(5.1, Unit.Px, "stroke-dasharray: 5.1")]
    [InlineData(5.12, Unit.Px, "stroke-dasharray: 5.12")]
    [InlineData(5.123, Unit.Px, "stroke-dasharray: 5.123")]
    [InlineData(5.1234, Unit.Px, "stroke-dasharray: 5.123")]
    [InlineData(5.1236, Unit.Px, "stroke-dasharray: 5.124")]
    [InlineData(5, Unit.Em, "stroke-dasharray: 5em")]
    [InlineData(5.1, Unit.Em, "stroke-dasharray: 5.1em")]
    [InlineData(5.12, Unit.Em, "stroke-dasharray: 5.12em")]
    [InlineData(5.123, Unit.Em, "stroke-dasharray: 5.123em")]
    [InlineData(5.1234, Unit.Em, "stroke-dasharray: 5.123em")]
    [InlineData(5.1236, Unit.Em, "stroke-dasharray: 5.124em")]
    [InlineData(5, Unit.Rem, "stroke-dasharray: 5rem")]
    [InlineData(5.1, Unit.Rem, "stroke-dasharray: 5.1rem")]
    [InlineData(5.12, Unit.Rem, "stroke-dasharray: 5.12rem")]
    [InlineData(5.123, Unit.Rem, "stroke-dasharray: 5.123rem")]
    [InlineData(5.1234, Unit.Rem, "stroke-dasharray: 5.123rem")]
    [InlineData(5.1236, Unit.Rem, "stroke-dasharray: 5.124rem")]
    [InlineData(5, Unit.Pt, "stroke-dasharray: 5pt")]
    [InlineData(5.1, Unit.Pt, "stroke-dasharray: 5.1pt")]
    [InlineData(5.12, Unit.Pt, "stroke-dasharray: 5.12pt")]
    [InlineData(5.123, Unit.Pt, "stroke-dasharray: 5.123pt")]
    [InlineData(5.1234, Unit.Pt, "stroke-dasharray: 5.123pt")]
    [InlineData(5.1236, Unit.Pt, "stroke-dasharray: 5.124pt")]
    [InlineData(5, Unit.Pc, "stroke-dasharray: 5pc")]
    [InlineData(5.1, Unit.Pc, "stroke-dasharray: 5.1pc")]
    [InlineData(5.12, Unit.Pc, "stroke-dasharray: 5.12pc")]
    [InlineData(5.123, Unit.Pc, "stroke-dasharray: 5.123pc")]
    [InlineData(5.1234, Unit.Pc, "stroke-dasharray: 5.123pc")]
    [InlineData(5.1236, Unit.Pc, "stroke-dasharray: 5.124pc")]
    [InlineData(5, Unit.Ch, "stroke-dasharray: 5ch")]
    [InlineData(5.1, Unit.Ch, "stroke-dasharray: 5.1ch")]
    [InlineData(5.12, Unit.Ch, "stroke-dasharray: 5.12ch")]
    [InlineData(5.123, Unit.Ch, "stroke-dasharray: 5.123ch")]
    [InlineData(5.1234, Unit.Ch, "stroke-dasharray: 5.123ch")]
    [InlineData(5.1236, Unit.Ch, "stroke-dasharray: 5.124ch")]
    [InlineData(5, Unit.Ex, "stroke-dasharray: 5ex")]
    [InlineData(5.1, Unit.Ex, "stroke-dasharray: 5.1ex")]
    [InlineData(5.12, Unit.Ex, "stroke-dasharray: 5.12ex")]
    [InlineData(5.123, Unit.Ex, "stroke-dasharray: 5.123ex")]
    [InlineData(5.1234, Unit.Ex, "stroke-dasharray: 5.123ex")]
    [InlineData(5.1236, Unit.Ex, "stroke-dasharray: 5.124ex")]
    [InlineData(5, Unit.Vw, "stroke-dasharray: 5vw")]
    [InlineData(5.1, Unit.Vw, "stroke-dasharray: 5.1vw")]
    [InlineData(5.12, Unit.Vw, "stroke-dasharray: 5.12vw")]
    [InlineData(5.123, Unit.Vw, "stroke-dasharray: 5.123vw")]
    [InlineData(5.1234, Unit.Vw, "stroke-dasharray: 5.123vw")]
    [InlineData(5.1236, Unit.Vw, "stroke-dasharray: 5.124vw")]
    [InlineData(5, Unit.Vh, "stroke-dasharray: 5vh")]
    [InlineData(5.1, Unit.Vh, "stroke-dasharray: 5.1vh")]
    [InlineData(5.12, Unit.Vh, "stroke-dasharray: 5.12vh")]
    [InlineData(5.123, Unit.Vh, "stroke-dasharray: 5.123vh")]
    [InlineData(5.1234, Unit.Vh, "stroke-dasharray: 5.123vh")]
    [InlineData(5.1236, Unit.Vh, "stroke-dasharray: 5.124vh")]
    [InlineData(5, Unit.Vmin, "stroke-dasharray: 5vmin")]
    [InlineData(5.1, Unit.Vmin, "stroke-dasharray: 5.1vmin")]
    [InlineData(5.12, Unit.Vmin, "stroke-dasharray: 5.12vmin")]
    [InlineData(5.123, Unit.Vmin, "stroke-dasharray: 5.123vmin")]
    [InlineData(5.1234, Unit.Vmin, "stroke-dasharray: 5.123vmin")]
    [InlineData(5.1236, Unit.Vmin, "stroke-dasharray: 5.124vmin")]
    [InlineData(5, Unit.Vmax, "stroke-dasharray: 5vmax")]
    [InlineData(5.1, Unit.Vmax, "stroke-dasharray: 5.1vmax")]
    [InlineData(5.12, Unit.Vmax, "stroke-dasharray: 5.12vmax")]
    [InlineData(5.123, Unit.Vmax, "stroke-dasharray: 5.123vmax")]
    [InlineData(5.1234, Unit.Vmax, "stroke-dasharray: 5.123vmax")]
    [InlineData(5.1236, Unit.Vmax, "stroke-dasharray: 5.124vmax")]
    [InlineData(5, Unit.Mm, "stroke-dasharray: 5mm")]
    [InlineData(5.1, Unit.Mm, "stroke-dasharray: 5.1mm")]
    [InlineData(5.12, Unit.Mm, "stroke-dasharray: 5.12mm")]
    [InlineData(5.123, Unit.Mm, "stroke-dasharray: 5.123mm")]
    [InlineData(5.1234, Unit.Mm, "stroke-dasharray: 5.123mm")]
    [InlineData(5.1236, Unit.Mm, "stroke-dasharray: 5.124mm")]
    [InlineData(5, Unit.Cm, "stroke-dasharray: 5cm")]
    [InlineData(5.1, Unit.Cm, "stroke-dasharray: 5.1cm")]
    [InlineData(5.12, Unit.Cm, "stroke-dasharray: 5.12cm")]
    [InlineData(5.123, Unit.Cm, "stroke-dasharray: 5.123cm")]
    [InlineData(5.1234, Unit.Cm, "stroke-dasharray: 5.123cm")]
    [InlineData(5.1236, Unit.Cm, "stroke-dasharray: 5.124cm")]
    [InlineData(5, Unit.In, "stroke-dasharray: 5in")]
    [InlineData(5.1, Unit.In, "stroke-dasharray: 5.1in")]
    [InlineData(5.12, Unit.In, "stroke-dasharray: 5.12in")]
    [InlineData(5.123, Unit.In, "stroke-dasharray: 5.123in")]
    [InlineData(5.1234, Unit.In, "stroke-dasharray: 5.123in")]
    [InlineData(5.1236, Unit.In, "stroke-dasharray: 5.124in")]
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
}
