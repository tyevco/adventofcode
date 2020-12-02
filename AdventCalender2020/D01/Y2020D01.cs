using Advent.Utilities;
using Advent.Utilities.Attributes;
using System.Collections.Generic;
using System.Linq;

namespace AdventCalendar2019.D01
{
    [Exercise("Day 1: Report Repair")]
    class Y2020D01 : FileSelectionParsingConsole<IList<int>>, IExercise
    {
        public void Execute()
        {
            Start("D01/Data");
        }

        protected override IList<int> DeserializeData(IList<string> data)
        {
            return data.Select(x => int.Parse(x)).ToList();
        }

        protected override void Execute(IList<int> data)
        {
            var smallest = data.Min();
            var dataSet = data.Where(x => x < 2020 - smallest).ToList();

            for (int i = 0; i < dataSet.Count; i++)
            {
                for (int j = 0; j < dataSet.Count; j++)
                {
                    if (i == j)
                        continue;

                    for (int k = 0; k < dataSet.Count; k++)
                    {
                        if (j == k || i == k)
                            continue;

                        if (dataSet[i] + dataSet[j] + dataSet[k] == 2020)
                        {
                            AnswerPartTwo($"{dataSet[i]} * {dataSet[j]} * {dataSet[k]} = {dataSet[i] * dataSet[j] * dataSet[k]}");
                            break;
                        }
                    }

                    if (dataSet[i] + dataSet[j] == 2020)
                    {
                        AnswerPartOne($"{dataSet[i]} * {dataSet[j]} = {dataSet[i] * dataSet[j]}");
                        break;
                    }
                }
            }
        }
    }
}
