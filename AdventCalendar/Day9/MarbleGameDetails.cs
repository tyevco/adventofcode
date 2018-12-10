﻿using System.Collections.Generic;

namespace AdventCalendar.Day9
{
    public class MarbleGameDetails
    {
        public long HighScore { get; set; }

        public IList<MarblePlayer> Players { get; set; }
        public Marble LastMarble { get; internal set; }
        public long Rounds { get; internal set; }
    }
}