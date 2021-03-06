using Advent.Utilities;
using Advent.Utilities.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventCalendar2016.D14
{
    [Exercise("Day 14: One-Time Pad")]
    class Y2016D14 : FileSelectionParsingConsole<IList<string>>, IExercise
    {
        public void Execute()
        {
            Start("D14/Data");
        }

        protected override IList<string> DeserializeData(IList<string> data)
        {
            // Setup data parser.
            return data;
        }

        protected override void Execute(IList<string> data)
        {
            // perform task.
        }
    }
}
