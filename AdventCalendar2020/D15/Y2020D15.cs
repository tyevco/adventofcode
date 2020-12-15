using Advent.Utilities;
using Advent.Utilities.Attributes;
using System.Collections.Generic;
using System.Linq;

namespace AdventCalendar2020.D15
{
    [Exercise("Day 15: Rambunctious Recitation")]
    class Y2020D15 : FileSelectionParsingConsole<IEnumerable<IList<int>>>, IExercise
    {
        public void Execute()
        {
            Start("D15/Data");
        }

        protected override IEnumerable<IList<int>> DeserializeData(IList<string> data)
        {
            return data.Select(line => line.Split(",").Select(x => int.Parse(x)).ToList());
        }

        protected override void Execute(IEnumerable<IList<int>> set)
        {
            foreach (var data in set)
            {
                AnswerPartOne(GetLastSpoken(data, 2020));
                AnswerPartTwo(GetLastSpoken(data, 30000000));
            }
        }

        private int GetLastSpoken(IList<int> data, int iterations)
        {
            IDictionary<int, (int a, int b)> spokenMap = new Dictionary<int, (int a, int b)>();

            int lastSpoken = 0;

            for (int i = 0; i < iterations; i++)
            {
                int speak = 0;

                if (i < data.Count)
                {
                    speak = data[i];
                }
                else if (spokenMap.TryGetValue(lastSpoken, out (int a, int b) value))
                {
                    if (value.a >= 0)
                    {
                        var spokeAt = value.a - value.b;

                        speak = spokeAt;
                    }
                }

                if (spokenMap.TryGetValue(speak, out (int a, int b) speakValue))
                {
                    if (speakValue.a >= 0)
                    {
                        spokenMap[speak] = (i, speakValue.a);
                    }
                    else
                    {
                        spokenMap[speak] = (i, speakValue.b);
                    }
                }
                else
                {
                    spokenMap[speak] = (-1, i);
                }

                lastSpoken = speak;
            }

            return lastSpoken;
        }
    }
}
