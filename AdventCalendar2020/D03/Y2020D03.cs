using Advent.Utilities;
using Advent.Utilities.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventCalendar2020.D03
{
    [Exercise("Day 3: Toboggan Trajectory")]
    class Y2020D03 : FileSelectionParsingConsole<IList<string>>, IExercise
    {
        public void Execute()
        {
            Start("D03/Data");
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
