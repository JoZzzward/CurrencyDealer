using System.Globalization;

namespace Crawler.Core.Extensions;

public static class StringExtensions
{
    public static string ReplaceCommaOnDot(this string str)
    {
        str = str.Replace(",", CultureInfo.InvariantCulture.NumberFormat.NumberDecimalSeparator);

        return str;
    }
}