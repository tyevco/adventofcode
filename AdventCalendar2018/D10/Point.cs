namespace AdventCalendar2018.D10
{
    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }

        public int dX { get; set; }

        public int dY { get; set; }

        public void Tick(int iterations = 1)
        {
            for (int i = 0; i < iterations; i++)
            {
                X += dX;
                Y += dY;
            }
        }
    }
}
