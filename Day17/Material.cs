namespace Day17
{
    public abstract class Material
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Grid Grid { get; private set; }

        public Material(Grid grid)
        {
            this.Grid = grid;
        }

        public abstract void Propagate();

    }
}