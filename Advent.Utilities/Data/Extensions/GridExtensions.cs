using System;
using Advent.Utilities.Data.Map;

namespace Advent.Utilities.Data.Extensions
{
    public static class GridExtensions
    {
        public static void Print(this IGrid<DataPoint<char>, char> grid)
        {
            var minX = grid.MinX;
            var maxX = grid.MaxX;
            var minY = grid.MinY;
            var maxY = grid.MaxY;

            for (int y = minY; y <= maxY; y++)
            {
                for (int x = minX; x <= maxX; x++)
                {
                    if (grid.Has(x, y))
                    {
                        Console.Write(grid[x, y].Data);
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