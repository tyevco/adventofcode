namespace Day17
{
    public class Spring : Material
    {
        public override MaterialType Type => MaterialType.Spring;

        public Spring(int x, int y, Grid grid)
        : base(grid)
        {
            X = x;
            Y = y;
        }

        public override void Propagate()
        {
            Material below = Grid[X, Y + 1];
            if (below == null)
            {
                below = new Water(X, Y + 1, Grid);

                Grid[X, Y + 1] = below;
                below.Propagate();
            }
        }

        public override string ToString()
        {
            return "+";
        }
    }
}