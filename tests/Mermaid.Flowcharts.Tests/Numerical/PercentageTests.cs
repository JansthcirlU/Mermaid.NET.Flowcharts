using System.Globalization;
using Mermaid.Flowcharts.Numerical;

namespace Mermaid.Flowcharts.Tests.Numerical;

public class PercentageTests
{
    [Theory]
    [InlineData(double.NegativeInfinity)]
    [InlineData(double.PositiveInfinity)]
    [InlineData(double.NaN)]
    public void Percentage_WhenValueIsNonFinite_ShouldThrow(double invalid)
    {
        // Act
        ArgumentOutOfRangeException? ex = Assert.Throws<ArgumentOutOfRangeException>(
            () => new Percentage(invalid)
        );

        // Assert
        Assert.NotNull(ex);
        Assert.StartsWith("Percentage must be a real and finite number.", ex.Message);
    }

    [Theory]
    [InlineData(-double.Epsilon)]
    [InlineData(double.MinValue)]
    public void Percentage_WhenNegative_ShouldThrow(double negative)
    {
        // Act
        ArgumentOutOfRangeException? ex = Assert.Throws<ArgumentOutOfRangeException>(
            () => new Percentage(negative)
        );

        // Assert
        Assert.NotNull(ex);
        Assert.StartsWith("Percentage must not be negative.", ex.Message);
    }

    [Theory]
    [InlineData(0.0004, "0%")]
    [InlineData(0.0005, "0.001%")]
    [InlineData(99.9994, "99.999%")]
    [InlineData(99.9995, "100%")]
    public void Percentage_ShouldRoundToThreeDecimals(double value, string output)
    {
        // Arrange
        Percentage percentage = new(value);

        // Act
        string percentageString = percentage.ToNumericalString();
        string percentageValue = percentageString[..^1]; // Get value without % sign
        double parsed = double.Parse(percentageValue, CultureInfo.InvariantCulture);
        double difference = double.Abs(parsed - value);

        // Assert
        Assert.Equal(output, percentageString);
        Assert.True(difference <= 0.001);
    }
}