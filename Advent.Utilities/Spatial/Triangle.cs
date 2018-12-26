using System.Collections.Generic;

namespace Advent.Utilities
{
    public class Triangle : ITriangle
    {
        public IVector Normal { get; private set; }

        public IVector A { get; private set; }

        public IVector B { get; private set; }

        public IVector C { get; private set; }

        public IEnumerable<IVector> Vertices { get; }

        public Triangle(Vector a, Vector b, Vector c)
        {
            A = a;
            B = b;
            C = c;

            Vertices = new List<IVector> { A, B, C };
        }
    }
}
