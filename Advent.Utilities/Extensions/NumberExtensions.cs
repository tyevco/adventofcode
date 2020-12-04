using System;

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

        public static int ManhattanDistance(this (int x, int y) v1, (int x, int y)? v2 = null)
        {
            if (v2 == null)
            {
                v2 = (0, 0);
            }

            return Math.Abs(v1.x - v2.Value.x) + Math.Abs(v1.y - v2.Value.y);
        }

        public static int ManhattanDistance(this (int x, int y, int z) v1, (int x, int y, int z)? v2 = null)
        {
            if (v2 == null)
            {
                v2 = (0, 0, 0);
            }

            return Math.Abs(v1.x - v2.Value.x) + Math.Abs(v1.y - v2.Value.y) + Math.Abs(v1.z - v2.Value.z);
        }

        public static long ManhattanDistance(this (long x, long y) v1, (long x, long y)? v2 = null)
        {
            if (v2 == null)
            {
                v2 = (0, 0);
            }

            return Math.Abs(v1.x - v2.Value.x) + Math.Abs(v1.y - v2.Value.y);
        }

        public static long ManhattanDistance(this (long x, long y, long z) v1, (long x, long y, long z)? v2 = null)
        {
            if (v2 == null)
            {
                v2 = (0, 0, 0);
            }

            return Math.Abs(v1.x - v2.Value.x) + Math.Abs(v1.y - v2.Value.y) + Math.Abs(v1.z - v2.Value.z);
        }
    }
}
