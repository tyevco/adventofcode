﻿using System;
using System.Collections.Generic;
using System.Linq;
using Advent.Utilities.Data.Map;
using Advent.Utilities.Spatial;

namespace AdventCalendar2019.D15
{
    class Ship : IMap<ShipTile>
    {
        private const int DefaultSize = 0;

        public int X { get; set; } = 0;

        public int Y { get; set; } = 0;

        public int Width
        {
            get
            {
                var xs = Points.Select(p => p.Value.X);
                return xs.Max() - xs.Min();
            }
        }

        public int Height
        {
            get
            {
                var ys = Points.Select(p => p.Value.Y);
                return ys.Max() - ys.Min();
            }
        }

        public IDictionary<string, Point> Points { get; private set; } = new Dictionary<string, Point>();

        public Point this[int x, int y]
        {
            get
            {
                string key = $"{x},{y}";
                if (Points.ContainsKey(key))
                {
                    return Points[key];
                }
                else
                {
                    return null;
                }
            }
        }

        public Ship()
        {
            SetTile(0, 0, ShipTile.Open);
        }

        private void SetTile(int x, int y, ShipTile tile)
        {
            string key = $"{x},{y}";

            Point point;
            if (Points.ContainsKey(key))
            {
                point = Points[key];
                point.Data = (long)tile;
            }
            else
            {
                point = new Point(x, y)
                {
                    Data = (long)tile
                };
                Points.Add(key, point);
            }

            // Console.SetCursorPosition(DefaultSize + x + 1, DefaultSize + y + 1);

            //if (x == DroidX && y == DroidY)
            //{
            //    Console.Write((char)ShipTile.Open);
            //}
            //else if (Points.ContainsKey(key))
            //{
            //    var data = (long)Points[key].Data;
            //    Console.Write((char)data);
            //}
            //else
            //{
            //    Console.Write((char)ShipTile.Empty);
            //}
        }

        public ShipTile GetTile(Compass direction)
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
                return ShipTile.Unknown;
            }
            else
            {
                return (ShipTile)(Points[key].Data);
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

            SetTile(x, y, ShipTile.Open);
            X = x;
            Y = y;
        }

        public void SetTile(Compass direction, ShipTile tile = ShipTile.Wall)
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

            SetTile(x, y, tile);
        }


        public void DebugPrint()
        {
            PrintGrid(Points, X, Y);
        }

        public static void PrintGrid(IDictionary<string, Point> points, int droidX, int droidY)
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
                    if (x == 0 && y == 0)
                    {
                        Console.Write('*');
                    }
                    else if (x == droidX && y == droidY)
                    {
                        Console.Write((char)ShipTile.Droid);
                    }
                    else if (points.ContainsKey(key))
                    {
                        var data = (long)points[key].Data;
                        Console.Write((char)data);
                    }
                    else
                    {
                        Console.Write((char)ShipTile.Unknown);
                    }
                }

                Console.WriteLine();
            }
        }
    }
}
