using System.Buffers;

namespace Mermaid.Flowcharts.Nodes;

public readonly record struct NodeIdentifier
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
    private NodeIdentifier(string value)
    {
        Value = value;
    }

    public static NodeIdentifier FromString(string value)
    {
        if (value.StartsWith('_') || value.StartsWith('.') || value.StartsWith('-')) throw new ArgumentException("Identifier must not start with a separator.", nameof(value));
        if (value.EndsWith('_') || value.EndsWith('.') || value.EndsWith('-')) throw new ArgumentException("Identifier must not end with a separator.", nameof(value));
        if (string.IsNullOrEmpty(value)) throw new ArgumentException("Identifier must not be empty.", nameof(value));
        if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Identifier must not be whitespace.", nameof(value));

        bool containsDisallowedValue = value.AsSpan().IndexOfAnyExcept(AllowedCharacters) > -1;
        if (containsDisallowedValue) throw new ArgumentException("Identifier must only contain alphanumerical characters or '_', '.' or '-' as separators.", nameof(value));

        bool containsConsequentSeparators =
            value.Contains("__") ||
            value.Contains("_.") ||
            value.Contains("_-") ||
            value.Contains("._") ||
            value.Contains("..") ||
            value.Contains(".-") ||
            value.Contains("-_") ||
            value.Contains("-.") ||
            value.Contains("--");
        if (containsConsequentSeparators) throw new ArgumentException("Identifier must never contain two separators in a row.", nameof(value));
        return new(value);
    }

    public override string ToString()
        => Value;
}
