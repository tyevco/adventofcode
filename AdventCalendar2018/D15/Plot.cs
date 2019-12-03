namespace Day15
{
    public class Plot
    {
        public PlotType Type { get; }

        public int X { get; }
        public int Y { get; }

        public Plot(int x, int y, PlotType type)
        {
            this.X = x;
            this.Y = y;
            this.Type = type;
        }

        public override string ToString()
        {
            return Type == PlotType.Open ? "   " : "###";
        }
    }
}