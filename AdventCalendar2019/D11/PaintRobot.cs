using Advent.Utilities;
using Advent.Utilities.Data.Map;
using Advent.Utilities.Spatial;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventCalendar2019.D11
{
    class PaintRobot : Grid<PointData<long>, long>
    {
        private const int DefaultSize = 2;

        public Direction Direction { get; private set; } = Direction.Up;

        public long Operate(long paintColor, long turnDir)
        {
            string key = $"{X},{Y}";

            if (Points.ContainsKey(key))
            {
                var point = Points[key];
                point.Data = paintColor;
            }
            else
            {
                Points.Add(key, new PointData<long>(X, Y, paintColor));
            }

            if (turnDir == 0)
            {
                // turn left
                Direction = (Direction)(((int)Direction + 1) % 4);
            }
            else
            {
                // turn right
                Direction = (Direction)(((int)Direction == 0 ? 4 : (int)Direction) - 1);
            }

            switch (Direction)
            {
                case Direction.Up:
                    Y--;
                    break;
                case Direction.Left:
                    X--;
                    break;
                case Direction.Down:
                    Y++;
                    break;
                case Direction.Right:
                    X++;
                    break;
            }

            DebugPrint();

            var nextKey = $"{X},{Y}";
            if (Points.ContainsKey(nextKey))
            {
                var point = Points[nextKey];
                return (long)point.Data;
            }
            else
            {
                return 0;
            }
        }

        public void DebugPrint()
        {
            if (Debug.EnableDebugOutput)
            {
                PrintGrid(Points, X, Y, Direction);
            }
        }

        public static void PrintGrid(IDictionary<string, PointData<long>> points, int currX, int currY, Direction dir)
        {
            var xs = points.Select(x => x.Value.X);
            var ys = points.Select(y => y.Value.Y);

            var minX = xs.Any() ? Math.Min(-DefaultSize, xs.Min()) : -DefaultSize;
            var maxX = xs.Any() ? Math.Max(DefaultSize, xs.Max()) : DefaultSize;
            var minY = ys.Any() ? Math.Min(-DefaultSize, ys.Min()) : -DefaultSize;
            var maxY = ys.Any() ? Math.Max(DefaultSize, ys.Max()) : DefaultSize;

            for (int y = minY - 1; y <= maxY + 1; y++)
            {
                for (int x = minX - 1; x <= maxX + 1; x++)
                {
                    if (x == minX - 1 && y == minY - 1)
                        Console.Write("┏");
                    else if (x == maxX + 1 && y == maxY + 1)
                        Console.Write("┛");
                    else if (x == minX - 1 && y == maxY + 1)
                        Console.Write("┗");
                    else if (x == maxX + 1 && y == minY - 1)
                        Console.Write("┓");
                    else if (x == minX - 1 || x == maxX + 1)
                        Console.Write("┃");
                    else if (y == minY - 1 || y == maxY + 1)
                        Console.Write("━");
                    else
                    {
                        string key = $"{x},{y}";
                        if (x == currX && y == currY)
                        {
                            switch (dir)
                            {
                                case Direction.Up:
                                    Console.Write("^");
                                    break;
                                case Direction.Left:
                                    Console.Write("<");
                                    break;
                                case Direction.Down:
                                    Console.Write("v");
                                    break;
                                case Direction.Right:
                                    Console.Write(">");
                                    break;
                            }
                        }
                        else if (points.ContainsKey(key))
                        {
                            var data = (long)points[key].Data;
                            Console.Write(data == 0 ? " " : "█");
                        }
                        else
                        {
                            Console.Write(" ");
                        }
                    }
                }

                Console.WriteLine();
            }
        }
    }
}
