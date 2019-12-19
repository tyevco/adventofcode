namespace Advent.Utilities.Data.Map
{
    public class PointData<T> : Point<T>
    {
        public PointData(int x, int y)
        : base(x, y)
        {
            this.Data = Data;
        }

        public PointData(int x, int y, T data)
        : base(x, y)
        {
            this.Data = data;
        }

        public PointData(int x, int y, int distance, T data)
         : base(x, y)
        {
            this.X = x;
            this.Y = y;
            this.Data = data;
            this.Distance = distance;
        }

        public PointData()
        {
        }

        public T Data { get; set; }

        public int Distance { get; set; }
    }
}