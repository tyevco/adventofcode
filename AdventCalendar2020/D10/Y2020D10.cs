using Advent.Utilities;
using Advent.Utilities.Attributes;
using System.Collections.Generic;

namespace AdventCalendar2020.D10
{
    [Exercise("Day 10: ")]
    class Y2020D10 : FileSelectionParsingConsole<IList<string>>, IExercise
    {
        public void Execute()
        {
            Start("D10/Data");
        }

        protected override IList<string> DeserializeData(IList<string> data)
        {


            return data;
        }

        protected override void Execute(IList<string> data)
        {



            AnswerPartOne($"");
            AnswerPartTwo($"");
        }
    }
}
