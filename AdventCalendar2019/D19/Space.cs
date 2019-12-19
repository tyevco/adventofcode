using Advent.Utilities.Data.Map;
using Advent.Utilities.Spatial;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventCalendar2019.D19
{
    class Space : Grid<PointData<long>, long>
    {
        private const int DefaultSize = 0;

        public Space()
        {
            SetTile(0, 0, '.');
        }

        public void SetTile(int x, int y, long tile)
        {
            string key = $"{x},{y}";

            PointData<long> point;
            if (Points.ContainsKey(key))
            {
                point = Points[key];
                point.Data = tile;
            }
            else
            {
                point = new PointData<long>(x, y)
                {
                    Data = tile
                };
                Points.Add(key, point);
            }

            // Console.SetCursorPosition(DefaultSize + x + 1, DefaultSize + y + 1);

            //if (x == DroidX && y == DroidY)
            //{
            //    Console.Write((long)Tile.Open);
            //}
            //else if (Points.ContainsKey(key))
            //{
            //    var data = (long)Points[key].Data;
            //    Console.Write((long)data);
            //}
            //else
            //{
            //    Console.Write((long)Tile.Empty);
            //}
        }

        public long GetTile(int x, int y)
        {
            string key = $"{x},{y}";

            if (!Points.ContainsKey(key))
            {
                return '?';
            }
            else
            {
                return (long)(Points[key].Data);
            }
        }

        public void DebugPrint()
        {
            PrintGrid(Points, X, Y);
        }

        public static void PrintGrid(IDictionary<string, PointData<long>> points, int droidX, int droidY)
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
                        var data = (long)points[key].Data;
                        Console.Write((long)data);
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
