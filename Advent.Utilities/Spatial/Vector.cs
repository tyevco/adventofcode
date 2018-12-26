namespace Advent.Utilities
{
    public class Vector : IVector
    {

        public double X { get; private set; }

        public double Y { get; private set; }

        public double Z { get; private set; }

        public double[] Coords { get; }

        public Vector(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;

            Coords = new[] { X, Y, Z };
        }

        public IVector Cross(IVector other)
        {
            return new Vector((Y * other.Z) - (Z * other.Y), (X * other.Z) - (Z * other.X), (X * other.Y) - (Y * other.X));
        }

        public double Dot(IVector other)
        {
            return (X * other.X) + (Y * other.Y) + (Z * other.Z);
        }

        public IVector Minus(IVector other)
        {
            return new Vector(other.X - X, other.Y - Y, other.Z - Z);
        }
    }
}