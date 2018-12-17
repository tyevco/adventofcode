namespace Day17
{
    public class Spring : Material
    {
        public Spring(int x, int y, Grid grid)
        : base(grid)
        {
            X = x;
            Y = y;
        }

        public override void Propagate()
        {
            throw new System.NotImplementedException();
        }

        public override string ToString()
        {
            return "+";
        }
    }
}