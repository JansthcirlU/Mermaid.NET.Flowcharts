using Mermaid.Flowcharts.Styling.Attributes.Enums;
using Mermaid.Flowcharts.Styling.Attributes.Fonts;
using Mermaid.Flowcharts.Tests.Styling.Attributes.Seeding;

namespace Mermaid.Flowcharts.Tests.Styling.Attributes;

public class FontSizeTests
{
    public static TheoryData<double, Unit, string> LengthData => AttributeValueSeeder.SeedLengthData("font-size:");
    public static TheoryData<double, string> PercentageData => AttributeValueSeeder.SeedPercentageData("font-size:");
    public static TheoryData<AbsoluteSize, string> GetAbsoluteSizeData()
    {
        TheoryData<AbsoluteSize, string> data = [];
        foreach (AbsoluteSize size in Enum.GetValues<AbsoluteSize>())
        {
            data.Add(size, $"font-size:{EnumRendering.AbsoluteSizes[size]}");
        }
        return data;
    }

    public static TheoryData<RelativeSize, string> GetRelativeSizeData()
    {
        TheoryData<RelativeSize, string> data = [];
        foreach (RelativeSize size in Enum.GetValues<RelativeSize>())
        {
            data.Add(size, $"font-size:{EnumRendering.RelativeSizes[size]}");
        }
        return data;
    }

    [Theory]
    [InlineData(double.NegativeInfinity)]
    [InlineData(double.PositiveInfinity)]
    [InlineData(double.NaN)]
    public void FontSize_WhenInvalidLength_ShouldThrow(double invalid)
    {
        // Act
        ArgumentOutOfRangeException? ex = Assert.Throws<ArgumentOutOfRangeException>(
            () => FontSize.Length(invalid, Unit.Px)
        );

        // Assert
        Assert.NotNull(ex);
        Assert.StartsWith("Font size must be a real and finite number.", ex.Message);
    }

    [Theory]
    [MemberData(nameof(LengthData))]
    public void FontSize_WhenLength_ToMermaidString(double size, Unit unit, string expected)
    {
        // Arrange
        FontSize.LengthFontSize lengthFontSize = FontSize.Length(size, unit);

        // Act
        string mermaid = lengthFontSize.ToMermaidString();

        // Assert
        Assert.Equal(expected, mermaid);
    }

    [Theory]
    [MemberData(nameof(PercentageData))]
    public void FontSize_WhenPercentage_ToMermaidString(double size, string expected)
    {
        // Arrange
        FontSize.PercentageFontSize percentageFontSize = FontSize.Percentage(size);

        // Act
        string mermaid = percentageFontSize.ToMermaidString();

        // Assert
        Assert.Equal(expected, mermaid);
    }

    [Theory]
    [MemberData(nameof(GetAbsoluteSizeData))]
    public void FontSize_WhenAbsolute_ToMermaidString(AbsoluteSize absoluteSize, string expected)
    {
        // Arrange
        FontSize.AbsoluteFontSize absolute = FontSize.Absolute(absoluteSize);

        // Act
        string mermaid = absolute.ToMermaidString();

        // Assert
        Assert.Equal(expected, mermaid);
    }

    [Theory]
    [MemberData(nameof(GetRelativeSizeData))]
    public void FontSize_WhenRelative_ToMermaidString(RelativeSize relativeSize, string expected)
    {
        // Arrange
        FontSize.RelativeFontSize relative = FontSize.Relative(relativeSize);

        // Act
        string mermaid = relative.ToMermaidString();

        // Assert
        Assert.Equal(expected, mermaid);
    }
}
