using System.Collections.Generic;

namespace Advent.Utilities.Data.Map
{
    public interface IGrid<T>
    {
        IDictionary<string, Point<T>> Points { get; }

        int Width { get; }

        int Height { get; }

        bool Has(int x, int y);

        Point<T> this[int x, int y] { get; set; }
    }
}