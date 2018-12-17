namespace Day17
{
    public class Water : Material
    {
        public bool IsFlowing { get; set; } = false;

        public Water(int x, int y, Grid grid)
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
            return IsFlowing ? "|" : "~";
        }
    }
}