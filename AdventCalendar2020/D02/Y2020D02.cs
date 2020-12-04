using Advent.Utilities;
using Advent.Utilities.Attributes;
using AdventCalendar2020.D02;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventCalendar2020.D02
{
    [Exercise("Day 2: Password Philosophy")]
    class Y2020D02 : FileSelectionParsingConsole<IList<Password>>, IExercise
    {
        public void Execute()
        {
            Start("D02/Data");
        }

        protected override IList<Password> DeserializeData(IList<string> data)
        {
            // 1-3 c: cczpq
            return data.Select(x =>
            {
                var parts = x.Split(" :-".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

                return new Password
                {
                    Lower = int.Parse(parts[0]),
                    Upper = int.Parse(parts[1]),
                    Checksum = parts[2][0],
                    Value = parts[3],
                };
            }).ToList();
        }

        protected override void Execute(IList<Password> data)
        {
            int countValid = 0;
            int positionValid = 0;
            foreach (var p in data)
            {
                if (p.IsCountValid())
                    countValid++;

                if (p.IsPositionValid())
                    positionValid++;
            }

            AnswerPartOne(countValid);
            AnswerPartTwo(positionValid);
        }
    }
}
