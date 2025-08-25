using Mermaid.Flowcharts.Styling.Attributes;
using Mermaid.Flowcharts.Styling.Attributes.Enums;

namespace Mermaid.Flowcharts.Tests.Styling.Attributes;

public class StrokeLineJoinTests
{
    public static TheoryData<StrokeLineJoinType, string> GetStrokeLineJoinTypeData()
    {
        TheoryData<StrokeLineJoinType, string> data = [];
        foreach (StrokeLineJoinType joinType in Enum.GetValues<StrokeLineJoinType>())
        {
            data.Add(joinType, $"stroke-linejoin:{EnumRendering.StrokeLineJoinTypes[joinType]}");
        }
        return data;
    }

    [Theory]
    [MemberData(nameof(GetStrokeLineJoinTypeData))]
    public void StrokeLineJoin_ToMermaidString(StrokeLineJoinType joinType, string expected)
    {
        // Arrange
        StrokeLineJoin join = new(joinType);

        // Act
        string mermaid = join.ToMermaidString();

        // Assert
        Assert.Equal(expected, mermaid);
    }
}
