using System;
using System.Diagnostics;

namespace Advent.Utilities.Data.Manhattan
{
    [DebuggerDisplay("{X},{Y}")]
    public class ManhattanPoint
    {
        public bool IsInfinite { get; set; }

        public int X { get; set; }

        public int Y { get; set; }

        public int Id { get; set; }

        public int CalculateDistance(int x, int y)
        {
            return Math.Abs(X - x) + Math.Abs(Y - y);
        }
    }
}
