using System.Text.RegularExpressions;
using Mermaid.Flowcharts.NonEmptyStringTypes;

namespace Mermaid.Flowcharts.Styling.Attributes.Fonts;

public partial record FontFamilyComponent
{
    public NonEmptySingleLineString Value { get; }

    public FontFamilyComponent(NonEmptySingleLineString value)
    {
        if (((string)value).Contains('"') || ((string)value).Contains('\'')) throw new ArgumentException("Font family component must not contain single or double quotes.", nameof(value));
        if (!SpaceOrHyphenSeparatedWordsRegex().IsMatch(value)) throw new ArgumentException("Font family component must only contain words that are separated by at most one space.", nameof(value));

        Value = value;
    }

    [GeneratedRegex("^[a-zA-Z]+([ -][a-zA-Z]+)*$")]
    private static partial Regex SpaceOrHyphenSeparatedWordsRegex();
}