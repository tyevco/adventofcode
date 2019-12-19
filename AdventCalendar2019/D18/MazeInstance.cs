using System;
using System.Collections.Generic;
using System.Linq;
using Advent.Utilities;
using Advent.Utilities.Data.Map;

namespace AdventCalendar2019.D18
{
    public class MazeInstance
    {
        private const int DefaultSize = 0;

        public MazeInstance(Maze maze)
        {
            Maze = maze;
        }

        public Maze Maze { get; private set; }

        public int X { get; set; } = 0;

        public int Y { get; set; } = 0;

        public int Distance { get; private set; } = 0;

        public int Keys { get; private set; } = 0;

        public int Obstacles { get; private set; } = 0;

        public DataPoint<char>[] RemainingKeys => Maze.KeyLocations.Where(x => !IsKeyCollected(x.Data)).ToArray();

        public string CollectedOrder = string.Empty;

        public char LastKey { get; set; }

        public DataPoint<char> Current => Maze[X, Y];

        public void CollectKey(char key)
        {
            CollectedOrder += key;
            Keys |= GetBitmask(key);
        }

        public bool IsKeyCollected(char key)
        {
            var mask = GetBitmask(key);

            return (Keys & mask) == mask;
        }

        public void Move(DataPoint<int> move)
        {
            X = move.X;
            Y = move.Y;
            Distance += move.Distance;
            Obstacles = move.Data;
        }

        public bool IsMatch(char seekKey)
        {
            if (seekKey == '.' || seekKey == '@' || char.IsLower(seekKey))
                return true;
            else if (seekKey == '#')
                return false;
            else
            {
                var bitmask = GetBitmask(seekKey);
                return (Keys & bitmask) == bitmask;
            }
        }

        public void DebugPrint(bool overrideDebug = false)
        {
            if (Debug.EnableDebugOutput || overrideDebug)
            {
                PrintGrid(Maze.Points, X, Y, Keys);
            }
        }

        public static void PrintGrid(IDictionary<string, DataPoint<char>> points, int currX, int currY, int keys)
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
                        var bitmask = GetBitmask(p);
                        if (currX == x && currY == y)
                            Console.Write('@');
                        else if (p == '.' || p == '#')
                            Console.Write(points[key].Data);
                        else if (p == '@' || (keys & bitmask) == bitmask)
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
            return new MazeInstance(Maze)
            {
                X = X,
                Y = Y,
                Keys = Keys,
                Distance = Distance,
                CollectedOrder = CollectedOrder,
            };
        }

        public static int GetBitmask(char key)
        {
            return (int)Math.Pow(2, char.ToLower(key) - 'a');
        }
    }
}
