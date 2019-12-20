using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace AdventCalendar2019.D20
{
    [DebuggerDisplay("{X},{Y}")]
    public struct RobotSet : IEquatable<RobotSet>
    {
        public Robot First { get; set; }

        public Robot Second { get; set; }

        public Robot Third { get; set; }

        public Robot Fourth { get; set; }

        public override string ToString()
        {
            return $"{First}:{Second}:{Third}:{Fourth}";
        }

        public bool Equals(RobotSet other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(First, other.First) && Equals(Second, other.Second) && Equals(Third, other.Third) && Equals(Fourth, other.Fourth);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((RobotSet)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(First, Second, Third, Fourth);
        }
    }
}
