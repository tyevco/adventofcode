using Advent.Utilities;
using Advent.Utilities.Attributes;
using System.Collections.Generic;

namespace AdventCalendar2020.D06
{
    [Exercise("Day 6: ")]
    class Y2020D06 : FileSelectionParsingConsole<IList<string>>, IExercise
    {
        public void Execute()
        {
            Start("D06/Data");
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
