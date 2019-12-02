namespace Day22
{
    public class Region
    {
        public RegionType Type => (RegionType)(ErosionLevel % 3);
        public int X { get; private set; }
        public int Y { get; private set; }
        public int ErosionLevel { get; private set; }

        public Region(int x, int y, int erosionLevel)
        {
            X = x;
            Y = y;
            ErosionLevel = erosionLevel;
        }
    }
}