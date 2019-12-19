using System;
using System.Collections.Generic;
using System.Linq;

namespace Advent.Utilities.Data.Map
{
    public class Grid<TPoint, TData> : IGrid<TPoint, TData>
        where TPoint : Point<TData>
    {
        private const int DefaultSize = 0;

        public IDictionary<string, TPoint> Points { get; protected set; } = new Dictionary<string, TPoint>();

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

        public TPoint Current => this[X, Y];

        public int MinX => Points.Any() ? Math.Min(-DefaultSize, Points.Select(p => p.Value.X).Min()) : -DefaultSize;

        public int MaxX => Points.Any() ? Math.Max(DefaultSize, Points.Select(p => p.Value.X).Max()) : DefaultSize;

        public int MinY => Points.Any() ? Math.Min(-DefaultSize, Points.Select(p => p.Value.Y).Min()) : -DefaultSize;

        public int MaxY => Points.Any() ? Math.Max(DefaultSize, Points.Select(p => p.Value.Y).Max()) : DefaultSize;

        public bool Has(int x, int y)
        {
            string key = $"{x},{y}";

            return Points.ContainsKey(key);
        }

        public TPoint this[int x, int y]
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
            set
            {
                string key = $"{x},{y}";

                Points[key] = value;
            }
        }
    }
}
