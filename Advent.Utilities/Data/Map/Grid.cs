using System.Collections.Generic;
using System.Linq;

namespace Advent.Utilities.Data.Map
{
    public class Grid<TPoint, TData> : IGrid<TPoint, TData>
        where TPoint : Point<TData>
    {
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
