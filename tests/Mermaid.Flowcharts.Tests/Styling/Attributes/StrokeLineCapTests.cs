using Mermaid.Flowcharts.Styling.Attributes;
using Mermaid.Flowcharts.Styling.Attributes.Enums;

namespace Mermaid.Flowcharts.Tests.Styling.Attributes;

public class StrokeLineCapTests
{
    public static TheoryData<StrokeLineCapType, string> GetStrokeLineCapTypeData()
    {
        TheoryData<StrokeLineCapType, string> data = [];
        foreach (StrokeLineCapType capType in Enum.GetValues<StrokeLineCapType>())
        {
            data.Add(capType, $"stroke-linecap:{EnumRendering.StrokeLineCapTypes[capType]}");
        }
        return data;
    }

    [Theory]
    [MemberData(nameof(GetStrokeLineCapTypeData))]
    public void StrokeLineCap_ToMermaidString(StrokeLineCapType capType, string expected)
    {
        // Arrange
        StrokeLineCap cap = new(capType);

        // Act
        string mermaid = cap.ToMermaidString();

        // Assert
        Assert.Equal(expected, mermaid);
    }
}
