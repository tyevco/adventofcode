namespace Advent.Utilities
{
    public class Vector3i : IVector<int>
    {
        public int X { get; set; }

        public int Y { get; set; }

        public int Z { get; set; }

        public int[] Coords { get; }

        public Vector3i(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;

            Coords = new[] { X, Y, Z };
        }

        public IVector<int> Cross(IVector<int> other)
        {
            return new Vector3i((Y * other.Z) - (Z * other.Y), (X * other.Z) - (Z * other.X), (X * other.Y) - (Y * other.X));
        }

        public int Dot(IVector<int> other)
        {
            return (X * other.X) + (Y * other.Y) + (Z * other.Z);
        }

        public IVector<int> Minus(IVector<int> other)
        {
            return new Vector3i(other.X - X, other.Y - Y, other.Z - Z);
        }
    }
}