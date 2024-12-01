using System.Buffers;

namespace Mermaid.Flowcharts.Nodes;

public readonly record struct NodeIdentifier : IMermaidPrintable
{
    private const string AllowedLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
    private const string AllowedDigits = "0123456789";
    private const string AllowedSeparators = "_.-";
    private static readonly SearchValues<char> AllowedCharacters = SearchValues.Create(AllowedLetters + AllowedDigits + AllowedSeparators);

    public string Value { get; }

    public NodeIdentifier()
    {
        Value = Guid.NewGuid().ToString();
    }
    private NodeIdentifier(string text)
    {
        Value = text;
    }

    public static NodeIdentifier FromString(string text)
    {
        if (text.StartsWith('_') || text.StartsWith('.') || text.StartsWith('-')) throw new ArgumentException("Identifier must not start with a separator.", nameof(text));
        if (text.EndsWith('_') || text.EndsWith('.') || text.EndsWith('-')) throw new ArgumentException("Identifier must not end with a separator.", nameof(text));
        if (string.IsNullOrEmpty(text)) throw new ArgumentException("Identifier must not be empty.", nameof(text));
        if (string.IsNullOrWhiteSpace(text)) throw new ArgumentException("Identifier must not be whitespace.", nameof(text));

        bool containsDisallowedValue = text.AsSpan().IndexOfAnyExcept(AllowedCharacters) > -1;
        if (containsDisallowedValue) throw new ArgumentException("Identifier must only contain alphanumerical characters or '_', '.' or '-' as separators.", nameof(text));

        bool containsConsequentSeparators =
            text.Contains("__") ||
            text.Contains("_.") ||
            text.Contains("_-") ||
            text.Contains("._") ||
            text.Contains("..") ||
            text.Contains(".-") ||
            text.Contains("-_") ||
            text.Contains("-.") ||
            text.Contains("--");
        if (containsConsequentSeparators) throw new ArgumentException("Identifier must never contain two separators in a row.", nameof(text));
        return new(text);
    }

    public override string ToString()
        => ToMermaidString();

    public string ToMermaidString(int indentations = 0)
        => Value;
}
