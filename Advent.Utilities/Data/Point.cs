using System;
using System.Diagnostics;

namespace Advent.Utilities.Data.Map
{
    public class Point<T> : Point
    {
        public Point()
        : base(0, 0)
        {
        }

        public Point(int x)
            : base(x, 0)
        {
        }

        public Point(int x, int y)
            : base(x, y)
        {
        }

        public T Data { get; set; }
    }

    [DebuggerDisplay("{X},{Y}")]
    public class Point
    {
        public Point()
            : this(0, 0)
        {
        }

        public Point(int x)
            : this(x, 0)
        {
        }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }

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

        public override bool Equals(object obj)
        {
            return obj is Point point &&
                   X == point.X &&
                   Y == point.Y;
        }

        public Point Step(Point toward)
        {
            int targetX = this.X;
            int targetY = this.Y;

            if (this.X > toward.X)
            {
                targetX--;
            }
            else if (this.X < toward.X)
            {
                targetX++;
            }

            if (this.Y > toward.Y)
            {
                targetY--;
            }
            else if (this.Y < toward.Y)
            {
                targetY++;
            }

            return new Point(targetX, targetY);
        }

        public override int GetHashCode()
        {
            var hashCode = 1307379088;
            hashCode = hashCode * -1521134295 + X.GetHashCode();
            hashCode = hashCode * -1521134295 + Y.GetHashCode();
            return hashCode;
        }
    }
}