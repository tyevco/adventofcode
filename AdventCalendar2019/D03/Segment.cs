using System;
using System.Diagnostics;

namespace AdventCalendar2019.D03
{
    [DebuggerDisplay("{Start} ~ {End}")]
    public class Segment
    {
        public Point Start { get; set; }

        public Point End { get; set; }

        public Point Intersection(Segment other, bool skipOrigin = false)
        {
            int x1 = Start.X, y1 = Start.Y,
                x2 = End.X, y2 = End.Y,
                x3 = other.Start.X, y3 = other.Start.Y,
                x4 = other.End.X, y4 = other.End.Y;

            if (skipOrigin && x1 == 0 && y1 == 00 && x3 == 0 && y3 == 0)
            {
                return null;
            }

            if (Math.Abs(x1 - x2) == 0 && Math.Abs(x3 - x4) == 0 && Math.Abs(x1 - x3) == 0)
            {
                return null;
            }

            if (Math.Abs(y1 - y2) == 0 && Math.Abs(y3 - y4) == 0 && Math.Abs(y1 - y3) == 0)
            {
                return null;
            }

            if (Math.Abs(x1 - x2) == 0 && Math.Abs(x3 - x4) == 0)
            {
                return null;
            }

            if (Math.Abs(y1 - y2) == 0 && Math.Abs(y3 - y4) == 0)
            {
                return null;
            }

            int x, y;

            if (Math.Abs(x1 - x2) == 0)
            {
                double m2 = (y4 - y3) / (x4 - x3);
                double c2 = -m2 * x3 + y3;

                x = (int)x1;
                y = (int)(c2 + m2 * x1);
            }
            else if (Math.Abs(x3 - x4) == 0)
            {
                double m1 = (y2 - y1) / (x2 - x1);
                double c1 = -m1 * x1 + y1;

                x = (int)x3;
                y = (int)(c1 + m1 * x3);
            }
            else
            {
                double m1 = (y2 - y1) / (x2 - x1);
                double c1 = -m1 * x1 + y1;

                double m2 = (y4 - y3) / (x4 - x3);
                double c2 = -m2 * x3 + y3;

                x = (int)((c1 - c2) / (m2 - m1));
                y = (int)(c2 + m2 * x);

                if (Math.Abs(-m1 * x + y - c1) == 0 &&
                    Math.Abs(-m2 * x + y - c2) == 0)
                {
                    return null;
                }
            }

            if (IsInside(x, y) && other.IsInside(x, y))
            {
                return new Point { X = x, Y = y };
            }

            return null;
        }

        public bool IsInside(Point p)
        {
            return IsInside(p.X, p.Y);
        }

        public bool IsInside(int x, int y)
        {
            return (x >= Start.X && x <= End.X
                        || x >= End.X && x <= Start.X)
                    && (y >= Start.Y && y <= End.Y
                        || y >= End.Y && y <= Start.Y);
        }
    }
}