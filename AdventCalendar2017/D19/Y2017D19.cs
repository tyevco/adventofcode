using Advent.Utilities;
using Advent.Utilities.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventCalendar2017.D19
{
    [Exercise("Day 19: A Series of Tubes")]
    class Y2017D19 : FileSelectionParsingConsole<IList<string>>, IExercise
    {
        public void Execute()
        {
            Start("D19/Data");
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
