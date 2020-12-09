using Advent.Utilities;
using Advent.Utilities.Attributes;
using System.Collections.Generic;
using System.Linq;

namespace AdventCalendar2020.D09
{
    [Exercise("Day 9: Encoding Error")]
    class Y2020D09 : FileSelectionParsingConsole<IList<string>>, IExercise
    {
        public void Execute()
        {
            Start("D09/Data");
        }

        protected override IList<string> DeserializeData(IList<string> data)
        {


            return data;
        }

        protected override void Execute(IList<string> data)
        {
            Queue<long> preamble = new Queue<long>();
            int count = 25;
            long partOne = 0;

            foreach (var item in data)
            {
                var curr = long.Parse(item);
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
            foreach (var item in data)
            {
                var curr = long.Parse(item);

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
