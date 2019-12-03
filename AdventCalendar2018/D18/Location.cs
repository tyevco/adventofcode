namespace Day18
{
    public class Location
    {
        public Location(int x, int y, Grid grid, LocationType type)
        {
            this.X = x;
            this.Y = y;
            this.Grid = grid;
            this.Type = type;
        }

        public Location N => Grid[X, Y - 1];
        public Location S => Grid[X, Y + 1];
        public Location E => Grid[X + 1, Y];
        public Location W => Grid[X - 1, Y];
        public Location NE => Grid[X + 1, Y - 1];
        public Location SE => Grid[X + 1, Y + 1];
        public Location NW => Grid[X - 1, Y - 1];
        public Location SW => Grid[X - 1, Y + 1];

        public int X { get; }
        public int Y { get; }
        internal Grid Grid { get; }
        public LocationType Type { get; set; }

        public int CountOfNeighborType(LocationType type)
        {
            int count = 0;

            if (N?.Type == type) count++;
            if (E?.Type == type) count++;
            if (S?.Type == type) count++;
            if (W?.Type == type) count++;
            if (NE?.Type == type) count++;
            if (NW?.Type == type) count++;
            if (SE?.Type == type) count++;
            if (SW?.Type == type) count++;

            return count;
        }

        public override string ToString()
        {
            switch (Type)
            {
                case LocationType.Lumberyard:
                    return "#";
                case LocationType.Tree:
                    return "|";
                default:
                case LocationType.Open:
                    return ".";
            }
        }
    }

    public enum LocationType
    {
        Tree,
        Open,
        Lumberyard
    }
}