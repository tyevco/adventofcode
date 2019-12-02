using System.Text;

namespace Day18
{
    public class Grid
    {
        public Location[] Locations { get; private set; }

        public int Width { get; }
        public int Height { get; }

        public Location this[int x, int y]
        {
            get
            {
                if (x >= 0 && x < Width &&
                    y >= 0 && y < Height)
                    return Locations[GetIndex(x, y)];

                return null;
            }
            set
            {
                if (x >= 0 && x < Width &&
                    y >= 0 && y < Height)
                {
                    Locations[GetIndex(x, y)] = value;
                }
            }
        }

        private int GetIndex(int x, int y)
        {
            return x + y * Width;
        }

        internal void AddLocation(int x, int y, LocationType type)
        {
            this[x, y] = new Location(x, y, this, type);
        }

        public Grid(int width, int height)
        {
            Width = width;
            Height = height;
            Locations = new Location[width * height];
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    sb.Append(this[x, y]);
                }

                sb.AppendLine();
            }

            return sb.ToString();
        }
    }
}
