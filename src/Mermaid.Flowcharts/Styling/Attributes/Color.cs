using System.Globalization;

namespace Mermaid.Flowcharts.Styling.Attributes;

public readonly record struct Color(byte Red, byte Green, byte Blue)
{
    public static Color FromHex(string hex)
    {
        ArgumentNullException.ThrowIfNull(hex);

        string s = hex.Trim();
        if (s.Length == 0) throw new ArgumentException("Hex color must not be empty or whitespace.", nameof(hex));
        if (!s.StartsWith('#')) throw new ArgumentException("Hex color code must start with a #.", nameof(hex));
        if (s.Length != 4 && s.Length != 7) throw new ArgumentException("Hex color code must be #RGB or #RRGGBB.", nameof(hex));

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

    public override string ToString()
        => $"#{Red:x2}{Green:x2}{Blue:x2}";
}