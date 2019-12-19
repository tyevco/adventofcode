using System.Collections.Generic;

namespace Advent.Utilities.Data.Map
{
    public interface IGrid<TPoint, TData>
        where TPoint : Point<TData>
    {
        int Width { get; }

        int Height { get; }

        int MinX { get; }

        int MaxX { get; }

        int MinY { get; }

        int MaxY { get; }

        bool Has(int x, int y);

        TPoint this[int x, int y] { get; set; }
    }
}