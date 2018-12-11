using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventCalendar.Day9
{
    public class MarblePlayer
    {
        public IList<Marble> Marbles { get; set; } = new List<Marble>();

        public long Score { get { return Marbles.Sum(x => x.Score); } }

        public int Id { get; internal set; }
    }
}
