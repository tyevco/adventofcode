using Advent.Utilities;
using Advent.Utilities.Attributes;
using System.Collections.Generic;
using System.Linq;

namespace AdventCalendar2020.D07
{
    [Exercise("Day 7: ")]
    class Y2020D07 : FileSelectionParsingConsole<IList<string>>, IExercise
    {
        public void Execute()
        {
            Start("D07/Data");
        }

        protected override IList<string> DeserializeData(IList<string> data)
        {
            return data;
        }

        protected override void Execute(IList<string> data)
        {
            AnswerPartOne(string.Empty);
            AnswerPartTwo(string.Empty);
        }
    }
}
