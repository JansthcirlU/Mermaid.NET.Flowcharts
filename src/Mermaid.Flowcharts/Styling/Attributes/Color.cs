using System.Globalization;
using Mermaid.Flowcharts.NonEmptyStringTypes;
using Mermaid.Flowcharts.Styling.Attributes.Base;

namespace Mermaid.Flowcharts.Styling.Attributes;

public readonly record struct Color(byte Red, byte Green, byte Blue) : ICssAttribute
{
    public static Color FromRGB(byte red, byte green, byte blue)
        => new(red, green, blue);

    public static Color FromHex(NonEmptySingleLineString hex)
    {
        ArgumentNullException.ThrowIfNull(hex);

        string s = ((string)hex).Trim();
        if (s.Length == 0)
        {
            throw new ArgumentException("Hex color must not be empty or whitespace.", nameof(hex));
        }

        if (!s.StartsWith('#'))
        {
            throw new ArgumentException("Hex color code must start with a #.", nameof(hex));
        }

        if (s.Length != 4 && s.Length != 7)
        {
            throw new ArgumentException("Hex color code must be #RGB or #RRGGBB.", nameof(hex));
        }

        string r, g, b;
        if (s.Length == 4)
        {
            r = new string(s[1], 2);
            g = new string(s[2], 2);
            b = new string(s[3], 2);
        }
        else
        {
            r = s.Substring(1, 2);
            g = s.Substring(3, 2);
            b = s.Substring(5, 2);
        }

        byte rb = byte.Parse(r, NumberStyles.HexNumber, CultureInfo.InvariantCulture);
        byte gb = byte.Parse(g, NumberStyles.HexNumber, CultureInfo.InvariantCulture);
        byte bb = byte.Parse(b, NumberStyles.HexNumber, CultureInfo.InvariantCulture);

        return new Color(rb, gb, bb);
    }

    public string ToCss()
        => $"#{Red:x2}{Green:x2}{Blue:x2}";
}
