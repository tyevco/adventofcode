using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Advent.Utilities;
using Advent.Utilities.Data.Map;

namespace AdventCalendar2019.D18
{
    public class Maze : Grid<DataPoint<char>, char>
    {
        private const int DefaultSize = 0;

        public static Regex KeysRegex { get; } = new Regex("[a-z]");

        public IList<DataPoint<char>> KeyLocations { get; } = new List<DataPoint<char>>();

        public Maze()
        {
        }

        public void SetTile(int x, int y, char tile)
        {
            string key = $"{x},{y}";

            DataPoint<char> point;
            if (Points.ContainsKey(key))
            {
                point = Points[key];
                point.Data = tile;
            }
            else
            {
                point = new DataPoint<char>(x, y)
                {
                    Data = tile
                };
                Points.Add(key, point);
            }

            if (KeysRegex.IsMatch(point.Data.ToString()))
            {
                KeyLocations.Add(point);
            }
        }

        public void DebugPrint(bool overrideDebug = false)
        {
            if (Debug.EnableDebugOutput || overrideDebug)
                PrintGrid(Points, X, Y);
        }


        public static void PrintGrid(IDictionary<string, DataPoint<char>> points, int currX, int currY)
        {
            var xs = points.Select(x => x.Value.X);
            var ys = points.Select(y => y.Value.Y);

            var minX = xs.Any() ? Math.Min(-DefaultSize, xs.Min()) : -DefaultSize;
            var maxX = xs.Any() ? Math.Max(DefaultSize, xs.Max()) : DefaultSize;
            var minY = ys.Any() ? Math.Min(-DefaultSize, ys.Min()) : -DefaultSize;
            var maxY = ys.Any() ? Math.Max(DefaultSize, ys.Max()) : DefaultSize;

            for (int y = minY; y <= maxY; y++)
            {
                for (int x = minX; x <= maxX; x++)
                {
                    string key = $"{x},{y}";
                    if (points.ContainsKey(key))
                    {
                        Console.Write(points[key].Data);
                    }
                    else
                    {
                        Console.Write('?');
                    }
                }

                Console.WriteLine();
            }
        }
    }
}
