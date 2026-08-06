using System.Text.RegularExpressions;
using Mermaid.Flowcharts.NonEmptyStringTypes;

namespace Mermaid.Flowcharts.Styling.Attributes.Fonts;

public partial record FontFamilyComponent
{
    public string Value { get; }

    public FontFamilyComponent(string name)
    {
        NonEmptySingleLineString nes = NonEmptySingleLineString.FromString(name);
        if (nes.Contains('"') || nes.Contains('\''))
        {
            throw new ArgumentException("Font family component must not contain single or double quotes.", nameof(name));
        }

        if (!SpaceOrHyphenSeparatedWordsRegex().IsMatch(nes.AsSpan()))
        {
            throw new ArgumentException("Font family component must only contain words that are separated by at most one space.", nameof(name));
        }

        Value = nes;
    }

    [GeneratedRegex("^[a-zA-Z]+([ -][a-zA-Z]+)*$")]
    private static partial Regex SpaceOrHyphenSeparatedWordsRegex();
}
