using System;
using System.Diagnostics;

namespace Advent.Utilities.Data.Map
{
    [DebuggerDisplay("{X},{Y}")]
    public class Point
    {
        public Point()
        {

        }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }
        public object Data { get; set; }

        public static bool operator ==(Point first, Point second)
        {
            return first.X == second.X && first.Y == second.Y;
        }

        public static bool operator !=(Point first, Point second)
        {
            return first.X != second.X || first.Y != second.Y;
        }

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

        public Vector2i CalculateVector(Point other)
        {
            return new Vector2i(other.X - X, other.Y - Y);
        }
    }
}