using Advent.Utilities;
using Advent.Utilities.Attributes;
using System.Collections.Generic;

namespace AdventCalendar2020.D15
{
    [Exercise("Day 15: ")]
    class Y2020D15 : FileSelectionParsingConsole<IList<string>>, IExercise
    {
        public void Execute()
        {
            Start("D15/Data");
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

            AnswerPartOne(data);
        }


        protected void PartTwo(IList<string> data)
        {

            AnswerPartTwo(data);
        }
    }
}
