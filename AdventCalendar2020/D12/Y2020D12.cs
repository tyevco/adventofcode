using Advent.Utilities;
using Advent.Utilities.Attributes;
using System.Collections.Generic;
using System.Linq;

namespace AdventCalendar2020.D12
{
    [Exercise("Day 12: ")]
    class Y2020D12 : FileSelectionParsingConsole<IList<string>>, IExercise
    {
        public void Execute()
        {
            Start("D12/Data");
        }

        protected override IList<string> DeserializeData(IList<string> data)
        {

            return data;
        }


        protected override void Execute(IList<string> data)
        {

            AnswerPartOne("");
            AnswerPartTwo("");
        }
    }
}
