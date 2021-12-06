using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Advent.Utilities.Extensions
{
    public static class StringExtensions
    {
        public static string Interpolate(this string format, params (string, object)[] values)
        {
            string returned = format;
            foreach (var value in values)
            {
                returned = returned.Replace($"{{{value.Item1}}}", value.Item2.ToString());
            }

            return returned;
        }

        private static IDictionary<int, Regex> RegexCache { get; } = new Dictionary<int, Regex>();

        public static bool IsMatch(this string value, string regexString)
        {
            int key = regexString.GetHashCode();

            if (!RegexCache.TryGetValue(key, out Regex regex))
            {
                regex = new Regex(regexString);
                RegexCache.Add(key, regex);
            }

            return regex.IsMatch(value);
        }
    }
}
