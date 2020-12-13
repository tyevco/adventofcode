using Advent.Utilities;
using Advent.Utilities.Attributes;
using System.Collections.Generic;

namespace AdventCalendar2020.D13
{
    [Exercise("Day 13: ")]
    class Y2020D13 : FileSelectionParsingConsole<IList<string>>, IExercise
    {
        public void Execute()
        {
            Start("D13/Data");
        }

        protected override IList<string> DeserializeData(IList<string> data)
        {
            return data;
        }

        protected override void Execute(IList<string> data)
        {
            PartOne(data);
            PartTwo(data);
        }

        protected void PartOne(IList<string> data)
        {


            AnswerPartOne("");
        }


        protected void PartTwo(IList<string> data)
        {


            AnswerPartTwo("");
        }
    }
}
