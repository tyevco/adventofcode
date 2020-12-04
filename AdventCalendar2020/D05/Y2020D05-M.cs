using Advent.Utilities;
using Advent.Utilities.Attributes;
using System.Collections.Generic;
using System.Linq;

namespace AdventCalendar2020.D05
{
    [Exercise("Day 5: ")]
    class Y2020D05M : FileSelectionParsingConsole<IList<string>>, IExercise
    {
        public void Execute()
        {
            Start("D05/Data");
        }

        protected override IList<string> DeserializeData(IList<string> data)
        {
            return data;
        }

        protected override void Execute(IList<string> data)
        {
            AnswerPartOne(data.Any());
            AnswerPartTwo(data.Any());
        }
    }
}
