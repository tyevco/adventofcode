namespace Day17
{
    public class Clay : Material
    {
        public Clay(int x, int y, Grid grid)
            : base(grid)
        {
            X = x;
            Y = y;
        }

        public override void Propagate()
        {
        }

        public override string ToString()
        {
            return "#";
        }
    }
}