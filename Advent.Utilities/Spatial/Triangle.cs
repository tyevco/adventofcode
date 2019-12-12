using System.Collections.Generic;

namespace Advent.Utilities
{
    public class Triangle : ITriangle
    {
        public IVector<double> Normal { get; private set; }

        public IVector<double> A { get; private set; }

        public IVector<double> B { get; private set; }

        public IVector<double> C { get; private set; }

        public IEnumerable<IVector<double>> Vertices { get; }

        public Triangle(Vector3d a, Vector3d b, Vector3d c)
        {
            A = a;
            B = b;
            C = c;

            Normal = b.Minus(a).Cross(c.Minus(a));

            Vertices = new List<IVector<double>> { A, B, C };
        }
    }
}
