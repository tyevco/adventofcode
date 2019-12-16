using System.Collections.Generic;

namespace AdventCalendar2019.D14
{
    class Reaction
    {
        public IDictionary<string, int> Ingredients { get; } = new Dictionary<string, int>();

        public int Yields { get; set; }
    }
}
