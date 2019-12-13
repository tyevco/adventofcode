using System;
using System.Collections.Generic;
using System.Linq;
using Advent.Utilities;
using Advent.Utilities.Data.Map;

namespace AdventCalendar2019.D13
{
    class ElfGame
    {
        private const int DefaultSize = 0;

        public int MaxX { get; set; } = 0;

        public int MaxY { get; set; } = 0;

        public IDictionary<string, Point> Points { get; private set; } = new Dictionary<string, Point>();

        public Point Paddle { get; private set; }
        public Point Ball { get; private set; }

        public void SetTile(int x, int y, GameTile tile)
        {
            string key = $"{x},{y}";

            Point point;
            if (Points.ContainsKey(key))
            {
                point = Points[key];
                point.Data = tile;
            }
            else
            {
                point = new Point(x, y)
                {
                    Data = tile
                };
                Points.Add(key, point);
            }

            //if (x > MaxX)
            //{
            //    MaxX = x;
            //}
            //if (y > MaxY)
            //{
            //    MaxY = y;
            //}

            if (tile == GameTile.Paddle)
            {
                Paddle = point;
            }
            else if (tile == GameTile.Ball)
            {
                Ball = point;
            }

            //Console.SetCursorPosition(x, y);

            //char disp = ' ';
            //switch (tile)
            //{
            //    case GameTile.Ball:
            //        disp = 'o';
            //        break;
            //    case GameTile.Wall:
            //        disp = '█';
            //        break;
            //    case GameTile.Block:
            //        disp = '#';
            //        break;
            //    case GameTile.Paddle:
            //        disp = '=';
            //        break;
            //    default:
            //        break;
            //}
            //Console.Write(disp);

            //Console.SetCursorPosition(0, MaxY + 2);
        }

        public void DebugPrint()
        {
            if (Debug.EnableDebugOutput)
            {
                PrintGrid(Points);
            }
        }

        public static void PrintGrid(IDictionary<string, Point> points)
        {
            var xs = points.Select(x => x.Value.X);
            var ys = points.Select(y => y.Value.Y);

            var minX = xs.Any() ? Math.Min(-DefaultSize, xs.Min()) : -DefaultSize;
            var maxX = xs.Any() ? Math.Max(DefaultSize, xs.Max()) : DefaultSize;
            var minY = ys.Any() ? Math.Min(-DefaultSize, ys.Min()) : -DefaultSize;
            var maxY = ys.Any() ? Math.Max(DefaultSize, ys.Max()) : DefaultSize;

            Console.SetCursorPosition(0, 0);

            for (int y = minY - 1; y <= maxY + 1; y++)
            {
                for (int x = minX - 1; x <= maxX + 1; x++)
                {
                    string key = $"{x},{y}";
                    if (points.ContainsKey(key))
                    {
                        var data = (GameTile)points[key].Data;
                        char disp = ' ';
                        switch (data)
                        {
                            case GameTile.Ball:
                                disp = 'o';
                                break;
                            case GameTile.Wall:
                                disp = '█';
                                break;
                            case GameTile.Block:
                                disp = '#';
                                break;
                            case GameTile.Paddle:
                                disp = '=';
                                break;
                            default:
                                break;
                        }
                        Console.Write(disp);
                    }
                    else
                    {
                        Console.Write(" ");
                    }
                }

                Console.WriteLine();
            }
        }
    }
}
