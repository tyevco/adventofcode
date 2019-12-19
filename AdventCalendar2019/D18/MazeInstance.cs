using System;
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
            Robots = maze.Robots;
        }

        public Maze Maze { get; private set; }

        public Robot[] Robots { get; private set; }

        public int Distance { get; private set; } = 0;

        public int Keys { get; private set; } = 0;

        public int Obstacles { get; private set; } = 0;

        public DataPoint<char>[] RemainingKeys => Maze.KeyLocations.Where(x => !IsKeyCollected(x.Data)).ToArray();

        public string CollectedOrder = string.Empty;

        public char LastKey { get; set; }

        public State State => new State
        {
            Keys = Keys,
            Obstacles = Obstacles,
        };

        public bool IsKeyCollected(char key)
        {
            var mask = GetBitmask(key);

            return (Keys & mask) == mask;
        }

        public void Move(int index, DataPoint<State> move)
        {
            Robots[index].X = move.X;
            Robots[index].Y = move.Y;
            Keys |= move.Data.Keys;

            Distance += move.Distance;
            Obstacles = move.Data.Obstacles;
        }

        public bool IsMatch(char seekKey)
        {
            if (seekKey == '.' || char.IsNumber(seekKey) || char.IsLower(seekKey))
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
                PrintGrid(Maze, Robots, Keys);
            }
        }

        public static void PrintGrid(Maze maze, Robot[] robots, int keys)
        {
            var minX = maze.MinX;
            var maxX = maze.MaxX;
            var minY = maze.MinY;
            var maxY = maze.MaxY;

            for (int y = minY; y <= maxY; y++)
            {
                for (int x = minX; x <= maxX; x++)
                {
                    if (maze.Has(x, y))
                    {
                        if (robots.Any(r => r.X == x && r.Y == y))
                        {
                            Console.Write('@');
                            continue;
                        }
                        else
                        {
                            char p = maze[x, y].Data;

                            var bitmask = GetBitmask(p);
                            if (p == '.' || p == '#')
                                Console.Write(p);
                            else if (p == '@' || (keys & bitmask) == bitmask)
                                Console.Write(".");
                            else
                                Console.Write(p);
                        }
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
                Keys = Keys,
                Distance = Distance,
                CollectedOrder = CollectedOrder,
                Robots = (Robot[])Robots.Clone(),
            };
        }

        public static int GetBitmask(char key)
        {
            return (int)Math.Pow(2, char.ToLower(key) - 'a');
        }
    }
}
