using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace AdventCalendar2019.D20
{
    [DebuggerDisplay("{X},{Y}")]
    public struct Robot : IEquatable<Robot>
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

        public bool Equals(Robot other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return X == other.X && Y == other.Y;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Robot)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }

    }
}
