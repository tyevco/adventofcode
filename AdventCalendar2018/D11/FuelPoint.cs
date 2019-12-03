namespace Day11
{
    public class FuelPoint
    {
        public int X { get; set; }
        public int Y { get; set; }

        public int TotalPower { get; set; }
        public int Size { get; set; }

        public string ToString()
        {
            return $"{X},{Y},{Size} : {TotalPower}";
        }
    }
}
