using Advent.Utilities.Data.Map;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventCalendar2019.D15
{
    class Ship
    {
        private const int DefaultSize = 0;

        public int DroidX { get; set; } = 0;

        public int DroidY { get; set; } = 0;

        public IDictionary<string, Point> Points { get; private set; } = new Dictionary<string, Point>();

        public void SetTile(int x, int y, ShipTile tile)
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


        public void DebugPrint()
        {
            PrintGrid(Points, DroidX, DroidY);
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
                    if (x == droidX && y == droidY)
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

        internal ShipTile GetTile(int x, int y)
        {
            string key = $"{x},{y}";
            if (!Points.ContainsKey(key))
            {
                return ShipTile.Unknown;
            }
            else
            {
                return (ShipTile)(Points[key].Data);
            }
        }
    }
}
