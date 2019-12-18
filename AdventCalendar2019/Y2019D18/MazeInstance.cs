using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Advent.Utilities;
using Advent.Utilities.Data.Map;
using AdventCalendar2019.D18;

namespace Advent.Calendar.Y2019D18
{
    public class MazeInstance
    {

        private const int DefaultSize = 0;

        public MazeInstance(Maze maze, MazeInstance previousInstance = null)
        {
            Maze = maze;

            if (previousInstance != null)
            {
                Keys = previousInstance.Keys.ToList();
                Moves = previousInstance.Moves.ToList();
                X = previousInstance.X;
                Y = previousInstance.Y;
            }
            else
            {
                X = maze.X;
                Y = maze.Y;
            }
        }

        public Maze Maze { get; private set; }

        public int X { get; set; } = 0;

        public int Y { get; set; } = 0;

        public IList<char> Keys { get; private set; } = new List<char>();

        public IList<Point<int>> Moves { get; private set; } = new List<Point<int>>();

        public Regex ValidLocationRegex(char seekKey) => new Regex(@$"[{seekKey}{string.Join(string.Empty, Keys.Select(c => $"{c}" + c.ToString().ToUpper()))}\.@]");

        public IEnumerable<Point<char>> RemainingKeys => Maze.KeyLocations.Where(x => !Keys.Any(k => k == x.Data));

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


        public void DebugPrint(bool overrideDebug = false)
        {
            if (Debug.EnableDebugOutput || overrideDebug)
                PrintGrid(Maze.Points, X, Y, Keys);
        }

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

        public MazeInstance Clone()
        {
            return new MazeInstance(Maze, this);
        }
    }
}
