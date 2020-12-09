using Advent.Utilities;
using Advent.Utilities.Attributes;
using System.Collections.Generic;

namespace AdventCalendar2020.D09
{
    [Exercise("Day 9: ")]
    class Y2020D09 : FileSelectionParsingConsole<IList<string>>, IExercise
    {
        public void Execute()
        {
            Start("D09/Data");
        }

        protected override IList<string> DeserializeData(IList<string> data)
        {
            return data;
        }

        protected override void Execute(IList<string> data)
        {
            AnswerPartOne(data);

            AnswerPartTwo(data);
        }

    }
}
