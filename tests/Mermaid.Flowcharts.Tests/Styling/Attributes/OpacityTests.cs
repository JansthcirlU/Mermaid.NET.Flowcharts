using Mermaid.Flowcharts.Styling.Attributes;

namespace Mermaid.Flowcharts.Tests.Styling.Attributes;

public class OpacityTests
{
    [Fact]
    public void Opacity_WhenNaN_ShouldThrow()
    {
        // Act
        ArgumentOutOfRangeException? ex = Assert.Throws<ArgumentOutOfRangeException>(
            () => new Opacity(double.NaN)
        );

        // Assert
        Assert.NotNull(ex);
        Assert.StartsWith("Opacity must be a real number between 0 and 1.", ex.Message);
    }

    [Theory]
    [InlineData(double.NegativeInfinity)]
    [InlineData(double.MinValue)]
    [InlineData(-1)]
    public void Opacity_WhenNegative_ShouldThrow(double negative)
    {
        // Act
        ArgumentOutOfRangeException? ex = Assert.Throws<ArgumentOutOfRangeException>(
            () => new Opacity(negative)
        );

        // Assert
        Assert.NotNull(ex);
        Assert.StartsWith("Opacity must not be negative.", ex.Message);
    }

    [Theory]
    [InlineData(1.1)]
    [InlineData(double.MaxValue)]
    [InlineData(double.PositiveInfinity)]
    public void Opacity_WhenGreaterThanOne_ShouldThrow(double greaterThanOne)
    {
        // Act
        ArgumentOutOfRangeException? ex = Assert.Throws<ArgumentOutOfRangeException>(
            () => new Opacity(greaterThanOne)
        );

        // Assert
        Assert.NotNull(ex);
        Assert.StartsWith("Opacity must not be greater than 1.", ex.Message);
    }
}