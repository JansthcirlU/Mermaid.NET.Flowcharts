namespace Mermaid.Flowcharts;

public static class StringExtensions
{
    public static string Repeat(this string text, int count)
    {
        if (string.IsNullOrEmpty(text)) return string.Empty;
        if (count < 1) return string.Empty;

        ReadOnlySpan<char> textSpan = text.AsSpan();
        Span<char> repeatedTextSpan = new(new char[textSpan.Length * count]);
        for (int i = 0; i < count; i++)
        {
            textSpan.CopyTo(repeatedTextSpan.Slice(i * textSpan.Length, textSpan.Length));
        }
        return repeatedTextSpan.ToString();
    }
}