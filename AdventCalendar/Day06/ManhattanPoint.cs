using System;
using System.Collections.Generic;
using System.Text;

namespace AdventCalendar.Day6
{
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
