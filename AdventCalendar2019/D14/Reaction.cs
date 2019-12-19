using System.Collections.Generic;

namespace AdventCalendar2019.D14
{
    class Reaction
    {
        public IDictionary<string, long> Ingredients { get; } = new Dictionary<string, long>();

        public long Yields { get; set; }
    }
}
