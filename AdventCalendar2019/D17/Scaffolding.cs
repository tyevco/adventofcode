using Advent.Utilities.Data.Map;
using Advent.Utilities.Spatial;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventCalendar2019.D17
{
    class Scaffolding : Grid<char>
    {
        private const int DefaultSize = 0;

        public Scaffolding()
        {
            SetTile(0, 0, '.');
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

            // Console.SetCursorPosition(DefaultSize + x + 1, DefaultSize + y + 1);

            //if (x == DroidX && y == DroidY)
            //{
            //    Console.Write((char)Tile.Open);
            //}
            //else if (Points.ContainsKey(key))
            //{
            //    var data = (long)Points[key].Data;
            //    Console.Write((char)data);
            //}
            //else
            //{
            //    Console.Write((char)Tile.Empty);
            //}
        }

        public char GetTile(int x, int y)
        {
            string key = $"{x},{y}";

            if (!Points.ContainsKey(key))
            {
                return '?';
            }
            else
            {
                return (char)(Points[key].Data);
            }
        }

        public char GetTile(Compass direction)
        {
            string key = string.Empty;
            switch (direction)
            {
                case Compass.North:
                    key = $"{X},{Y - 1}";
                    break;
                case Compass.South:
                    key = $"{X},{Y + 1}";
                    break;
                case Compass.West:
                    key = $"{X - 1},{Y}";
                    break;
                case Compass.East:
                    key = $"{X + 1},{Y}";
                    break;

            }

            if (!Points.ContainsKey(key))
            {
                return '?';
            }
            else
            {
                return (char)(Points[key].Data);
            }
        }

        public void Move(Compass direction)
        {
            int x = X, y = Y;
            switch (direction)
            {
                case Compass.North:
                    y--;
                    break;
                case Compass.South:
                    y++;
                    break;
                case Compass.West:
                    x--;
                    break;
                case Compass.East:
                    x++;
                    break;
            }

            //SetTile(x, y, Tile.Open);
            X = x;
            Y = y;
        }

        //public void SetTile(Compass direction, Tile tile = Tile.Open)
        //{
        //    int x = X, y = Y;
        //    switch (direction)
        //    {
        //        case Compass.North:
        //            y--;
        //            break;
        //        case Compass.South:
        //            y++;
        //            break;
        //        case Compass.West:
        //            x--;
        //            break;
        //        case Compass.East:
        //            x++;
        //            break;
        //    }

        //    SetTile(x, y, tile);
        //}


        public void DebugPrint()
        {
            PrintGrid(Points, X, Y);
        }

        public static void PrintGrid(IDictionary<string, Point<char>> points, int droidX, int droidY)
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
                    string key = $"{x},{y}";
                    if (points.ContainsKey(key))
                    {
                        var data = (long)points[key].Data;
                        Console.Write((char)data);
                    }
                    else
                    {
                        Console.Write((char)Tile.Unknown);
                    }
                }

                Console.WriteLine();
            }
        }
    }
}
