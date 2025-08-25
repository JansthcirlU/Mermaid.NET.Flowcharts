using Mermaid.Flowcharts.Styling.Attributes;
using Mermaid.Flowcharts.Styling.Attributes.Enums;

namespace Mermaid.Flowcharts.Tests.Styling.Attributes;

public class LengthTests
{
    public static TheoryData<Unit> GetUnitData()
    {
        TheoryData<Unit> data = [];
        foreach (Unit unit in Enum.GetValues<Unit>())
        {
            data.Add(unit);
        }
        return data;
    }

    [Theory]
    [MemberData(nameof(GetUnitData))]
    public void Length_WhenNaN_ShouldThrow(Unit unit)
    {
        // Act
        ArgumentOutOfRangeException? ex = Assert.Throws<ArgumentOutOfRangeException>(
            () => new Length(double.NaN, unit)
        );

        // Assert
        Assert.NotNull(ex);
        Assert.StartsWith("Length must be a real number.", ex.Message);
    }
}