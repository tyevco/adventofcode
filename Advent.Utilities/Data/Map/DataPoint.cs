namespace Advent.Utilities.Data.Map
{
    public class DataPoint<T> : Point<T>
    {
        public DataPoint(int x, int y)
        : base(x, y)
        {
            this.Data = Data;
        }

        public DataPoint(int x, int y, T data)
        : base(x, y)
        {
            this.Data = data;
        }

        public DataPoint(int x, int y, int distance, T data)
         : base(x, y)
        {
            this.X = x;
            this.Y = y;
            this.Data = data;
            this.Distance = distance;
        }

        public DataPoint()
        {
        }

        public int Distance { get; set; }
    }
}