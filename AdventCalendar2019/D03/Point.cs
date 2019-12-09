using System;
using System.Diagnostics;

namespace AdventCalendar2019.D03
{
    [DebuggerDisplay("{X},{Y}")]
    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Distance { get; set; }

        public override string ToString()
        {
            return $"{X},{Y}";
        }


        public int CalculateDistance(int x, int y)
        {
            return Math.Abs(X - x) + Math.Abs(Y - y);
        }


        public int CalculateDistance(Point other)
        {
            return CalculateDistance(other.X, other.Y);
        }
    }
}