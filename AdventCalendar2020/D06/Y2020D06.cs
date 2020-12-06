using Advent.Utilities;
using Advent.Utilities.Attributes;
using System.Collections.Generic;
using System.Linq;

namespace AdventCalendar2020.D06
{
    [Exercise("Day 6: Custom Customs")]
    class Y2020D06 : FileSelectionParsingConsole<IList<CustomsGroup>>, IExercise
    {
        public void Execute()
        {
            Start("D06/Data");
        }

        protected override IList<CustomsGroup> DeserializeData(IList<string> data)
        {
            IList<CustomsGroup> customsList = new List<CustomsGroup>();

            CustomsGroup customs = new CustomsGroup();
            foreach (var line in data)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    customsList.Add(customs);
                    customs = new CustomsGroup();
                }
                else
                {
                    customs.People.Add(new PersonAnswers
                    {
                        Answers = string.Join("", line.ToCharArray().Distinct()),
                    });
                }
            }

            customsList.Add(customs);

            return customsList;
        }

        protected override void Execute(IList<CustomsGroup> data)
        {
            int totalAnyCount = 0;
            int totalAllCount = 0;
            var alphabet = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
            foreach (var group in data)
            {
                int groupAnyCount = 0;
                foreach(var c in alphabet)
                {
                    if (group.People.Any(p => p.Answers.Contains(c)))
                    {
                        groupAnyCount++;
                    }

                    if (group.People.All(p => p.Answers.Contains(c)))
                    {
                        totalAllCount++;
                    }
                }

                totalAnyCount += groupAnyCount;
            }

            AnswerPartOne(totalAnyCount);
            AnswerPartTwo(totalAllCount);
        }
    }
}
