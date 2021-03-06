namespace Advent.Utilities
{
    public class Vector3d : IVector<double>
    {
        public double X { get; private set; }

        public double Y { get; private set; }

        public double Z { get; private set; }

        public double[] Coords { get; }

        public Vector3d(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;

            Coords = new[] { X, Y, Z };
        }

        public IVector<double> Cross(IVector<double> other)
        {
            return new Vector3d((Y * other.Z) - (Z * other.Y), (X * other.Z) - (Z * other.X), (X * other.Y) - (Y * other.X));
        }

        public double Dot(IVector<double> other)
        {
            return (X * other.X) + (Y * other.Y) + (Z * other.Z);
        }

        public IVector<double> Minus(IVector<double> other)
        {
            return new Vector3d(other.X - X, other.Y - Y, other.Z - Z);
        }
    }
}