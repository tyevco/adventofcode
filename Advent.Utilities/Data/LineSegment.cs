using Advent.Utilities.Data.Map;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent.Utilities.Data
{
    [DebuggerDisplay("{Start} -> {End}")]
    public class LineSegment
    {
        public LineSegment(int x1, int y1, int x2, int y2)
        {
            Start = new Point(x1, y1);
            End = new Point(x2, y2);
        }

        public Point Start { get; }

        public Point End { get; }

        public override string ToString()
        {
            return $"{Start} -> {End}";
        }
    }
}
