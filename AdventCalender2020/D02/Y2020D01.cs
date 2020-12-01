using Advent.Utilities;
using Advent.Utilities.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventCalendar2019.D02
{
    [Exercise("Day 2: ")]
    class Y2020D02 : FileSelectionParsingConsole<IList<int>>, IExercise
    {
        public void Execute()
        {
            Start("D02/Data");
        }

        protected override IList<int> DeserializeData(IList<string> data)
        {
            return data.Select(x => int.Parse(x)).ToList();
        }

        protected override void Execute(IList<int> data)
        {
            Timer.Monitor(() =>
            {

            });
        }
    }
}
