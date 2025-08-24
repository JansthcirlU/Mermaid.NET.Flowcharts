using System.Text.RegularExpressions;

namespace Mermaid.Flowcharts.Styling.Attributes.Fonts;

public partial record FontFamilyComponent
{
    public string Value { get; }

    public FontFamilyComponent(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Font family component must not be null or whitespace.", nameof(value));
        if (value.Contains('"') || value.Contains('\'')) throw new ArgumentException("Font family component must not contain single or double quotes.", nameof(value));
        if (!SpaceOrHyphenSeparatedWordsRegex().IsMatch(value)) throw new ArgumentException("Font family component must only contain words that are separated by at most one space.", nameof(value));

        Value = value;
    }

    [GeneratedRegex("^[a-zA-Z]+([ -][a-zA-Z]+)*$")]
    private static partial Regex SpaceOrHyphenSeparatedWordsRegex();
}