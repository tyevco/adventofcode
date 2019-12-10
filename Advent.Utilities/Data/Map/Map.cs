using System.Collections.Generic;

namespace Advent.Utilities.Data.Map
{
    public class Map
    {
        public IList<Segment> Segments { get; set; } = new List<Segment>();
    }
}