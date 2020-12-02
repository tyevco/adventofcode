namespace Advent.Utilities.Extensions
{
    public static class NumberExtensions
    {
        public static bool IsBetween(this int value, int lower, int upper)
        {
            return value > lower && value < upper;
        }

        public static bool IsBetweenInclusive(this int value, int lower, int upper)
        {
            return value >= lower && value <= upper;
        }
    }
}
