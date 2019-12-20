using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace AdventCalendar2019.D20
{
    [DebuggerDisplay("Doors: {Obstacles} Keys: {Keys}")]
    public struct State : IEquatable<State>
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

        public bool Equals([AllowNull] State other)
        {
            return other.Obstacles == Obstacles
                    && other.Keys == Keys;
        }
    }
}
