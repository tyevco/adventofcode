using System.Collections.Generic;
using System.Linq;

namespace Advent.Utilities
{
    public class AABoundingBox : IAABoundingBox
    {
        public IEnumerable<IVector> Vertices { get; }

        public IVector Start { get; private set; }

        public IVector End { get; private set; }

        public AABoundingBox(params IVector[] vertices)
        {
            IList<IVector> verts = new List<IVector>();
            foreach (var vertex in vertices)
                verts.Add(vertex);

            Vertices = verts;
            Start = verts.FirstOrDefault();
            End = verts.LastOrDefault();
        }
    }
}
