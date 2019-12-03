namespace AdventCalendar2018.D17
{
    public abstract class Material
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Grid Grid { get; private set; }

        public Material Left => Grid[X - 1, Y];
        public Material Right => Grid[X + 1, Y];
        public Material Below => Grid[X, Y + 1];
        public Material Above => Grid[X, Y - 1];
        public Material DiagonalLeft => Grid[X - 1, Y + 1];
        public Material DiagonalRight => Grid[X + 1, Y + 1];

        public Material(Grid grid)
        {
            this.Grid = grid;
        }

        public abstract void Propagate();

        public abstract MaterialType Type { get; }
    }
}