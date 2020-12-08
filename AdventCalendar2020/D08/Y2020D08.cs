using Advent.Utilities;
using Advent.Utilities.Attributes;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AdventCalendar2020.D078
{
    [Exercise("Day 8: ")]
    class Y2020D08 : FileSelectionParsingConsole<IList<string>>, IExercise
    {
        public void Execute()
        {
            Start("D08/Data");
        }

        protected override IList<string> DeserializeData(IList<string> data)
        {
            var output = data;




            return output;
        }

        protected override void Execute(IList<string> data)
        {
            var part1 = "";
            var part2 = "";

            AnswerPartOne($"{part1}");
            AnswerPartTwo($"{part2}");
        }

    }
}
