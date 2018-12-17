namespace Day15
{
    public class Map
    {
        public Plot[] Plots { get; internal set; }


        public Map(int width, int height)
        {
            Plots = new Plot[width * height];
        }
    }
}