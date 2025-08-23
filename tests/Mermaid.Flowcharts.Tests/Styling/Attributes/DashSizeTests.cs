using Mermaid.Flowcharts.Styling.Attributes;
using Mermaid.Flowcharts.Styling.Attributes.Enums;

namespace Mermaid.Flowcharts.Tests.Styling.Attributes;

public class DashSizeTests
{
    [Theory]
    [InlineData(-1)]
    [InlineData(double.MinValue)]
    public void DashSize_WhenNegativePercentage_ShouldThrow(double value)
    {
        // Act
        ArgumentOutOfRangeException? ex = Assert.Throws<ArgumentOutOfRangeException>(
            () => DashSize.Percentage(value)
        );

        // Assert
        Assert.NotNull(ex);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(double.MinValue)]
    public void DashSize_WhenNegativeLength_ShouldThrow(double value)
    {
        // Act
        ArgumentOutOfRangeException? ex = Assert.Throws<ArgumentOutOfRangeException>(
            () => DashSize.Length(value, Unit.Px)
        );

        // Assert
        Assert.NotNull(ex);
        Assert.StartsWith("Dash length size must not be negative.", ex.Message);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(double.MinValue)]
    public void DashSize_WhenNegativeNumber_ShouldThrow(double value)
    {
        // Act
        ArgumentOutOfRangeException? ex = Assert.Throws<ArgumentOutOfRangeException>(
            () => DashSize.Number(value)
        );

        // Assert
        Assert.NotNull(ex);
        Assert.StartsWith("Dash size must not be negative.", ex.Message);
    }
}

public class DashOffsetTests
{

}

public class FillTests
{

}

public class FontFamilyTests
{

}

public class FontSizeTests
{

}

public class FontWeightTests
{

}

public class StrokeTests
{

}

public class StrokeLineCapTests
{

}

public class StrokeLineJoinTests
{

}

public class StyleColorTests
{

}

public class StyleOpacityTests
{

}

public class StyleClassTests
{

}