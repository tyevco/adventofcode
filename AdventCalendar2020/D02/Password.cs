using Advent.Utilities.Extensions;
using System.Linq;

namespace AdventCalendar2020.D02
{
    public class Password
    {
        public string Value { get; set; }

        public int Lower { get; set; }

        public int Upper { get; set; }

        public char Checksum { get; set; }

        public bool IsCountValid()
        {
            return Value.Count(c => c.Equals(Checksum)).IsBetweenInclusive(Lower, Upper);
        }

        public bool IsPositionValid()
        {
            return Checksum.Equals(Value[Lower - 1]) ^ Checksum.Equals(Value[Upper - 1]);
        }
    }
}
