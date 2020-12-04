using Advent.Utilities;
using Advent.Utilities.Extensions;
using System;

namespace AdventCalendar2018.D23
{
    public class NanoBot
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public int Z { get; private set; }
        public int R { get; private set; }

        public NanoBot(int X, int Y, int Z, int R)
        {
            this.X = X;
            this.Y = Y;
            this.Z = Z;
            this.R = R;
        }

        public int DistanceTo(int x, int y, int z)
        {
            return Math.Abs(X - x) + Math.Abs(Y - y) + Math.Abs(Z - z);
        }

        public bool InRange(int x, int y, int z)
        {
            var inRange = (X, Y, Z).ManhattanDistance((x, y, z)) <= R;

            if (inRange)
            {
                Debug.WriteLine($"pos=<{X},{Y},{Z}>, r={R}");
            }

            return inRange;
        }
    }
}