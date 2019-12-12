using System.Collections.Generic;
using System.Linq;

namespace Advent.Utilities
{
    public class AABoundingBox : IAABoundingBox
    {
        public IEnumerable<IVector<double>> Vertices { get; }

        public IVector<double> Start { get; private set; }

        public IVector<double> End { get; private set; }

        public AABoundingBox(params IVector<double>[] vertices)
        {
            IList<IVector<double>> verts = new List<IVector<double>>();
            foreach (var vertex in vertices)
                verts.Add(vertex);

            Vertices = verts;
            Start = verts.FirstOrDefault();
            End = verts.LastOrDefault();
        }
    }
}
