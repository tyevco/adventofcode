using Advent.Utilities;
using Advent.Utilities.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventCalendar2020.D06
{
    [Exercise("Day 6: Custom Customs")]
    class Y2020D06 : FileSelectionParsingConsole<IList<IList<int>>>, IExercise
    {
        public void Execute()
        {
            Start("D06/Data");
        }

        protected override IList<IList<int>> DeserializeData(IList<string> data)
        {
            IList<IList<int>> customsList = new List<IList<int>>();

            IList<int> customs = new List<int>();
            foreach (var line in data)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    customsList.Add(customs);
                    customs = new List<int>();
                }
                else
                {
                    customs.Add(line.Select(x => char.ToLowerInvariant(x) - 'a').Aggregate(0, (x, y) => x |= (int)Math.Pow(2, y)));
                }
            }

            customsList.Add(customs);

            return customsList;
        }

        protected override void Execute(IList<IList<int>> data)
        {
            int totalAnyCount = 0;
            int totalAllCount = 0;

            foreach (var group in data)
            {
                totalAnyCount += Convert.ToString(group.Aggregate(0, (x, y) => x |= y), 2).Count(x => x == '1');
                totalAllCount += Convert.ToString(group.Aggregate(1073741823, (x, y) => x &= y), 2).Count(x => x == '1');
            }

            AnswerPartOne(totalAnyCount);
            AnswerPartTwo(totalAllCount);
        }
    }
}
