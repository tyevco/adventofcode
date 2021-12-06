using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent.Utilities.Data
{
    public class CartesianGrid<T>
    {
        public CartesianGrid(IEnumerable<T> items, int width, int height)
        {
            Items = new List<T>(width * height);
            int i = 0;
            foreach (var item in items)
            {
                Items.Add(item);
                i++;

                if (i >= Items.Capacity)
                    break;
            }

            Width = width;
            Height = height;
        }

        public CartesianGrid(int width, int height)
        {
            Items = new List<T>(width * height);
            for (int i = 0; i < Items.Capacity; i++)
            {
                Items.Add(default);
            }

            Width = width;
            Height = height;
        }

        private List<T> Items { get; }

        public int Width { get; }

        public int Height { get; }

        public T Get(int i)
        {
            return Items[i];
        }

        public T Get(int x, int y)
        {
            if (x >= 0 && x < Width && y >= 0 && y < Height)
                return Items[(x + (y * Height))];

            return default;
        }

        public int IndexOf(T item)
        {
            return Items.IndexOf(item);
        }

        public void Set(int i, T item)
        {
            Items[i] = item;
        }

        public void Set(int x, int y, T item)
        {
            if (x >= 0 && x < Width && y >= 0 && y < Height)
                Items[(x + (y * Height))] = item;
        }

        public T[] GetRow(int y)
        {
            var row = new T[Height];
            int i = 0;
            for (int x = 0; x < Width; x++)
            {
                row[i++] = Items[(x + (y * Height))];
            }

            return row;
        }

        public T[] GetColumn(int x)
        {
            var col = new T[Height];
            int i = 0;
            for (int y = 0; y < Height; y++)
            {
                col[i++] = Items[(x + (y * Height))];
            }

            return col;
        }
    }
}
