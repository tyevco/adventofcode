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
    }
}
