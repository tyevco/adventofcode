using System.Collections.Generic;

namespace Advent.Utilities
{
    public interface IShape
    {
        IEnumerable<IVector> Vertices { get; }
    }
}

