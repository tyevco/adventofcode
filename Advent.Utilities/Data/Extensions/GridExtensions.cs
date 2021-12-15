using System;
using System.Collections.Generic;
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

        public static void Perform<T>(this CartesianGrid<T> grid, Action<(int x, int y, T item)> action)
        {
            foreach (var entry in grid)
            {
                action(entry);
            }
        }

        public static void PerformIf<T>(this CartesianGrid<T> grid, Predicate<(int x, int y, T item)> predicate, Action<(int x, int y, T item)> action)
        {
            foreach (var entry in grid)
            {
                if (predicate(entry))
                {
                    action(entry);
                }
            }
        }

        public static void Print<T>(this CartesianGrid<T> grid)
        {
            var minX = 0;
            var maxX = grid.Width;
            var minY = 0;
            var maxY = grid.Height;

            for (int y = minY; y < maxY; y++)
            {
                for (int x = minX; x < maxX; x++)
                {
                    var d = grid.Get(x, y);
                    if (!Equals(d, default))
                    {
                        Console.Write(d);
                    }
                    else
                    {
                        Console.Write('.');
                    }
                }

                Console.WriteLine();
            }

            Console.WriteLine();
        }

        public static (int x, int y, T item)[] GetAdjascent<T>(this CartesianGrid<T> grid, int x, int y, bool includeDiagonals = false)
        {
            List<(int x, int y, T item)> adj = new List<(int x, int y, T item)>();
            // check if on the left edge
            if (x > 0)
            {
                adj.Add((x - 1, y, grid.Get(x - 1, y)));
            }

            // check if on the right edge
            if (x < grid.Width - 1)
            {
                adj.Add((x + 1, y, grid.Get(x + 1, y)));
            }

            // check if on the top edge
            if (y > 0)
            {
                adj.Add((x, y - 1, grid.Get(x, y - 1)));
            }

            // check if on the bottom edge
            if (y < grid.Height - 1)
            {
                adj.Add((x, y + 1, grid.Get(x, y + 1)));
            }

            if (includeDiagonals)
            {
                // check lower left
                if (x > 0 && y < grid.Height - 1)
                {
                    adj.Add((x - 1, y + 1, grid.Get(x - 1, y + 1)));
                }

                // check upper left
                if (x > 0 && y > 0)
                {
                    adj.Add((x - 1, y - 1, grid.Get(x - 1, y - 1)));
                }

                // check lower right
                if (x < grid.Width - 1 && y < grid.Height - 1)
                {
                    adj.Add((x + 1, y + 1, grid.Get(x + 1, y + 1)));
                }

                // check upper right
                if (x < grid.Width - 1 && y > 0)
                {
                    adj.Add((x + 1, y - 1, grid.Get(x + 1, y - 1)));
                }
            }

            return adj.ToArray();
        }
    }
}