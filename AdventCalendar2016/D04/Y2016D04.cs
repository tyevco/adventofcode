using Advent.Utilities;
using Advent.Utilities.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventCalendar2016.D04
{
    [Exercise("Day 4: Security Through Obscurity")]
    class Y2016D04 : FileSelectionParsingConsole<IList<string>>, IExercise
    {
        public void Execute()
        {
            Start("D04/Data");
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
