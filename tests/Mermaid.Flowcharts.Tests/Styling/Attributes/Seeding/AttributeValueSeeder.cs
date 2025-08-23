using Mermaid.Flowcharts.Styling.Attributes.Enums;

namespace Mermaid.Flowcharts.Tests.Styling.Attributes.Seeding;

public static class AttributeValueSeeder
{
    public static TheoryData<double, Unit, string> SeedLengthData(string attributeDeclaration)
    {
        TheoryData<double, Unit, string> data = [];
        double[] testValues = [5.0, 5.1, 5.123, 5.1234, 5.1236];
        string[] expectedValues = ["5", "5.1", "5.123", "5.123", "5.124"];
        
        foreach (var unit in Enum.GetValues<Unit>())
        {
            for (int i = 0; i < testValues.Length; i++)
            {
                string expected = $"{attributeDeclaration}{expectedValues[i]}{EnumRendering.UnitSuffixes[unit]}";
                data.Add(testValues[i], unit, expected);
            }
        }
        return data;
    }

    public static TheoryData<double, string> SeedNumericalData(string attributeDeclaration)
    {
        TheoryData<double, string> data = [];
        double[] testValues = [5.0, 5.1, 5.123, 5.1234, 5.1236];
        string[] expectedValues = ["5", "5.1", "5.123", "5.123", "5.124"];
        for (int i = 0; i < testValues.Length; i++)
        {
            string expected = $"{attributeDeclaration}{expectedValues[i]}";
            data.Add(testValues[i], expected);
        }
        return data;
    }

    public static TheoryData<double, string> SeedPercentageData(string attributeDeclaration)
    {
        TheoryData<double, string> data = [];
        double[] testValues = [5.0, 5.1, 5.123, 5.1234, 5.1236];
        string[] expectedValues = ["5%", "5.1%", "5.123%", "5.123%", "5.124%"];
        for (int i = 0; i < testValues.Length; i++)
        {
            string expected = $"{attributeDeclaration}{expectedValues[i]}";
            data.Add(testValues[i], expected);
        }
        return data;
    }
}