using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Advent.Utilities;
using Advent.Utilities.Data.Map;

namespace AdventCalendar2019.D18
{
    class Maze : Grid<char>, ICloneable
    {
        private const int DefaultSize = 0;

        public IList<char> Keys { get; private set; } = new List<char>();

        public IList<Point<int>> Moves { get; private set; } = new List<Point<int>>();

        public Regex KeysRegex { get; } = new Regex("[a-z]");

        public Regex ValidLocationRegex(char seekKey) => new Regex(@$"[{seekKey}{string.Join(string.Empty, Keys.Select(c => $"{c}" + c.ToString().ToUpper()))}\.@]");

        public Maze()
        {
        }

        public void CollectKey(char key)
        {
            Keys.Add(key);
        }

        public void Move(Point<int> move)
        {
            X = move.X;
            Y = move.Y;
            Moves.Add(move);
        }

        public void SetTile(int x, int y, char tile)
        {
            string key = $"{x},{y}";

            Point<char> point;
            if (Points.ContainsKey(key))
            {
                point = Points[key];
                point.Data = tile;
            }
            else
            {
                point = new Point<char>(x, y)
                {
                    Data = tile
                };
                Points.Add(key, point);
            }
        }

        public void DebugPrint(bool overrideDebug = false)
        {
            if (Debug.EnableDebugOutput || overrideDebug)
                PrintGrid(Points, X, Y, Keys);
        }

        public IList<Point<char>> KeyLocations => Points.Values.Where(x => KeysRegex.IsMatch(x.Data.ToString()) && !Keys.Any(k => k == x.Data)).ToList();

        public static void PrintGrid(IDictionary<string, Point<char>> points, int currX, int currY, IList<char> keys)
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
                        var p = points[key].Data;
                        if (currX == x && currY == y)
                            Console.Write('@');
                        else if (p == '@' || keys.Any(k => p.ToString().ToLower()[0] == k))
                            Console.Write(".");
                        else
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

        public object Clone()
        {
            return new Maze()
            {
                X = X,
                Y = Y,
                Points = Points.ToDictionary(entry => entry.Key,
                                               entry => (Point<char>)entry.Value.Clone()),
                Keys = Keys.ToList(),
                Moves = Moves.Select(m => (Point<int>)m.Clone()).ToList(),
            };
        }
    }
}
