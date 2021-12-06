using Advent.Utilities;
using Advent.Utilities.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventCalendar2016.D11
{
    [Exercise("Day 11: Radioisotope Thermoelectric Generators")]
    class Y2016D11 : FileSelectionParsingConsole<IList<string>>, IExercise
    {
        public void Execute()
        {
            Start("D11/Data");
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
