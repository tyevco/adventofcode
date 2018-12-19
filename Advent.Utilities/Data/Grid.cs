namespace Advent.Utilities.Data
{
    public class Grid<T>
    {
        readonly T[] Data;

        public int Width { get; }
        public int Height { get; }

        public T this[int x, int y]
        {
            get
            {
                return Data[GetIndex(x, y)];
            }
            set
            {
                Data[GetIndex(x, y)] = value;
            }
        }

        public Grid(int width, int height)
        {
            Data = new T[width * height];
            Width = width;
            Height = height;
        }

        private int GetIndex(int x, int y)
        {
            return x + y * Width;
        }

        public bool HasEntryAt(int x, int y)
        {
            return this[x, y] != null;
        }

        public bool IsValidLocation(int x, int y)
        {
            return x >= 0 && x < Width && y >= 0 && y < Width;
        }
    }
}
