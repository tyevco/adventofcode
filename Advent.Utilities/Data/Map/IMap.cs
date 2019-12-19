using System.Collections.Generic;

namespace Advent.Utilities.Data.Map
{
    public interface IGrid<TPoint, TData>
        where TPoint : Point<TData>
    {
        IDictionary<string, TPoint> Points { get; }

        int Width { get; }

        int Height { get; }

        bool Has(int x, int y);

        TPoint this[int x, int y] { get; set; }
    }
}