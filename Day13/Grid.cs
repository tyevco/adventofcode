namespace Day13
{
    class Grid
    {
        Track[] Tracks;

        public int Width { get; }
        public int Height { get; }

        public Track this[int x, int y]
        {
            get
            {
                if (x >= 0 && x < Width &&
                    y >= 0 && y < Height)
                    return Tracks[GetIndex(x, y)];

                return null;
            }
            set
            {
                if (x >= 0 && x < Width &&
                    y >= 0 && y < Height)
                {
                    value.Grid = this;
                    Tracks[GetIndex(x, y)] = value;
                }
            }
        }

        private int GetIndex(int x, int y)
        {
            return x + y * Width;
        }

        public Grid(int width, int height)
        {
            Width = width;
            Height = height;
            Tracks = new Track[width * height];
        }
    }
}
