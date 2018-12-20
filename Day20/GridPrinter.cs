using Advent.Utilities.Data;
using System;
using System.Linq;

namespace Day20
{
    public static class GridPrinter
    {
        public static void Print(Grid<Point> points)
        {
            var validPoints = points.Data.Where(r => r != null);

            var left = validPoints.Min(r => r.X);
            var right = validPoints.Max(r => r.X);
            var top = validPoints.Min(r => r.Y);
            var bottom = validPoints.Max(r => r.Y);

            for (int y = top; y <= bottom; y++)
            {
                for (int x = left; x <= right; x++)
                {
                    var point = points[x, y];

                    if (point != null)
                    {
                        Console.Write(point.Distance.ToString().PadLeft(2, '0'));
                    }
                    else
                    {
                        Console.Write("__");
                    }

                    Console.Write(" ");
                }

                Console.WriteLine();
            }

            Console.WriteLine();
        }
    }
}
