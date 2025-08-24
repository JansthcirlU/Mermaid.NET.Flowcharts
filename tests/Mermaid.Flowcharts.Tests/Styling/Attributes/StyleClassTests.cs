using Mermaid.Flowcharts.Styling;

namespace Mermaid.Flowcharts.Tests.Styling.Attributes;

public class StyleClassTests
{
    [Fact]
    public void StyleClass_ToMermaidString_WhenEmpty_ShouldThrow()
    {
        // Arrange
        StyleClass empty = new();

        // Act
        InvalidOperationException? ex = Assert.Throws<InvalidOperationException>(
            () => empty.ToMermaidString()
        );

        // Assert
        Assert.NotNull(ex);
        Assert.StartsWith("At least one style class element should be set.", ex.Message);
    }
}