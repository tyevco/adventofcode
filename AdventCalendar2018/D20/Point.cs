namespace AdventCalendar2018.D20
{
    public class Point
    {
        public Point(int x, int y, int distance)
        {
            X = x;
            Y = y;
            this.Distance = distance;
        }

        public int X { get; set; }
        public int Y { get; set; }
        public int Distance { get; set; }

        public override string ToString()
        {
            return $"({X},{Y}):{Distance}";
        }
    }
}