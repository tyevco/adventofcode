using System.Diagnostics;

namespace AdventCalendar2019.D18
{
    [DebuggerDisplay("Doors: {Obstacles} Keys: {Keys}")]
    public struct State
    {
        public int Obstacles { get; set; }
        public int Keys { get; set; }

        internal void Deconstruct(out int obstacles, out int keys)
        {
            obstacles = Obstacles;
            keys = Keys;
        }

        public override string ToString()
        {
            return $"Doors: {Obstacles} Keys: {Keys}";
        }
    }
}
