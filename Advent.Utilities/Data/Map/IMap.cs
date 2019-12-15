using System.Collections.Generic;

namespace Advent.Utilities.Data.Map
{
    public interface IMap<T>
    {
        IDictionary<string, Point> Points { get; }

        int Width { get; }

        int Height { get; }

        Point this[int x, int y] { get; }
    }
}