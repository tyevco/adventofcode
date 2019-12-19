using System.Diagnostics;

namespace AdventCalendar2019.D18
{
    [DebuggerDisplay("{X},{Y}")]
    public struct Robot
    {
        public int X { get; set; }
        public int Y { get; set; }

        public override string ToString()
        {
            return $"{X},{Y}";
        }

        internal void Deconstruct(out int x, out int y)
        {
            x = X;
            y = Y;
        }
    }
}
