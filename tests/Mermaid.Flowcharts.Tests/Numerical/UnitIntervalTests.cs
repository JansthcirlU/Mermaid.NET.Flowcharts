using System.Globalization;
using Mermaid.Flowcharts.Numerical;

namespace Mermaid.Flowcharts.Tests.Numerical;

public class UnitIntervalTests
{
    [Theory]
    [InlineData(double.NegativeInfinity)]
    [InlineData(double.PositiveInfinity)]
    [InlineData(double.NaN)]
    public void UnitInterval_WhenValueIsNonFinite_ShouldThrow(double invalid)
    {
        // Act
        ArgumentOutOfRangeException? ex = Assert.Throws<ArgumentOutOfRangeException>(
            () => new UnitInterval(invalid)
        );

        // Assert
        Assert.NotNull(ex);
        Assert.StartsWith("Unit interval must be a real number between 0 and 1.", ex.Message);
    }

    [Theory]
    [InlineData(-double.Epsilon)]
    [InlineData(double.MinValue)]
    public void UnitInterval_WhenNegative_ShouldThrow(double negative)
    {
        // Act
        ArgumentOutOfRangeException? ex = Assert.Throws<ArgumentOutOfRangeException>(
            () => new UnitInterval(negative)
        );

        // Assert
        Assert.NotNull(ex);
        Assert.StartsWith("Unit interval should be at least 0.", ex.Message);
    }

    [Theory]
    [InlineData(1.0001)]
    [InlineData(double.MaxValue)]
    public void UnitInterval_WhenGreaterThanOne_ShouldThrow(double greaterThanOne)
    {
        // Act
        ArgumentOutOfRangeException? ex = Assert.Throws<ArgumentOutOfRangeException>(
            () => new UnitInterval(greaterThanOne)
        );

        // Assert
        Assert.NotNull(ex);
        Assert.StartsWith("Unit interval should be at most 1.", ex.Message);
    }

    [Theory]
    [InlineData(0.0004, "0")]
    [InlineData(0.0005, "0.001")]
    [InlineData(0.9994, "0.999")]
    [InlineData(0.9995, "1")]
    public void UnitInterval_ShouldRoundToThreeDecimals(double value, string output)
    {
        // Arrange
        UnitInterval interval = new(value);

        // Act
        string intervalString = interval.ToNumericalString();
        double parsed = double.Parse(intervalString, CultureInfo.InvariantCulture);
        double difference = double.Abs(parsed - value);

        // Assert
        Assert.Equal(output, intervalString);
        Assert.True(difference <= 0.0005);
    }
}