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

        public MazeInstance Previous { get; }

        public MazeInstance(Maze maze, MazeInstance previousInstance = null)
        {
            Maze = maze;

            if (previousInstance != null)
            {
                X = previousInstance.X;
                Y = previousInstance.Y;
                Previous = previousInstance;
            }
            else
            {
                X = maze.X;
                Y = maze.Y;
                Previous = null;
            }
        }

        public Maze Maze { get; private set; }

        public int X { get; set; } = 0;

        public int Y { get; set; } = 0;

        public int? Distance { get; private set; } = 0;

        public char? Key { get; private set; }

        public IEnumerable<Point<char>> RemainingKeys => Maze.KeyLocations.Where(x => !IsKeyCollected(x.Data));

        public int TotalDistance
        {
            get
            {
                if (Previous == null)
                {
                    return Distance ?? 0;
                }
                else
                {
                    return (Distance ?? 0) + Previous.TotalDistance;
                }
            }
        }

        public void CollectKey(char key)
        {
            Key = key;
        }

        public string GetKeys()
        {
            if (Previous == null)
            {
                if (Key.HasValue)
                {
                    return $"{Key.Value}{char.ToUpper(Key.Value)}";
                }
                else
                {
                    return string.Empty;
                }
            }
            else
            {
                if (Key.HasValue)
                {
                    return $"{Key.Value}{char.ToUpper(Key.Value)}{Previous.GetKeys()}";
                }
                else
                {
                    return Previous.GetKeys();
                }
            }
        }

        public bool IsKeyCollected(char key)
        {
            if (Previous == null)
            {
                if (Key.HasValue)
                {
                    return key == Key.Value;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return key == Key || Previous.IsKeyCollected(key);
            }
        }

        public void Move(Point<int> move)
        {
            X = move.X;
            Y = move.Y;
            Distance = move.Data;
        }

        public Regex ValidLocationRegex(char seekKey)
        {
            var keys = GetKeys();
            return new Regex(@$"[{seekKey}{keys}{keys.ToUpper()}\.@]");
        }

        public void DebugPrint(bool overrideDebug = false)
        {
            if (Debug.EnableDebugOutput || overrideDebug)
            {
                PrintGrid(Maze.Points, X, Y, GetKeys());
            }
        }

        public static void PrintGrid(IDictionary<string, Point<char>> points, int currX, int currY, string keys)
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
                        else if (p == '@' || keys.Any(k => char.ToLower(p) == k))
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
