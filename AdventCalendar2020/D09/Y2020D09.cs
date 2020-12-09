using Advent.Utilities;
using Advent.Utilities.Attributes;
using System.Collections.Generic;
using System.Linq;

namespace AdventCalendar2020.D09
{
    [Exercise("Day 9: Encoding Error")]
    class Y2020D09 : FileSelectionParsingConsole<IList<long>>, IExercise
    {
        public void Execute()
        {
            Start("D09/Data");
        }

        protected override IList<long> DeserializeData(IList<string> data)
        {
            return data.Select(x => long.Parse(x)).ToList();
        }

        protected override void Execute(IList<long> data)
        {
            Queue<long> preamble = new Queue<long>();
            int count = 25;
            long partOne = 0;

            foreach (var curr in data)
            {
                if (preamble.Count < count)
                {
                    preamble.Enqueue(curr);
                }
                else
                {
                    var totals = preamble.SelectMany(x => preamble.Where(y => x != y).Select(y => x + y)).ToList();

                    if (totals.Contains(curr))
                    {
                        preamble.Enqueue(curr);
                        preamble.Dequeue();
                    }
                    else
                    {
                        partOne = AnswerPartOne(curr);
                        break;
                    }
                }
            }

            Queue<long> sums = new Queue<long>();
            foreach (var curr in data)
            {
                sums.Enqueue(curr);

                while (sums.Sum() > partOne)
                {
                    sums.Dequeue();
                }

                if (sums.Sum() == partOne)
                {
                    AnswerPartTwo(sums.Min() + sums.Max());
                    break;
                }
            }
        }
    }
}
