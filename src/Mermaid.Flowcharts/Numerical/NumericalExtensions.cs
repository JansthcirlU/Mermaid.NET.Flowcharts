using System.Globalization;

namespace Mermaid.Flowcharts.Numerical;

public static class NumericalExtensions
{
    public static string ToNumberString(this double number, string? format = null, IFormatProvider? formatProvider = null)
        => number.ToString(format ?? "0.###", formatProvider ?? CultureInfo.InvariantCulture);
}