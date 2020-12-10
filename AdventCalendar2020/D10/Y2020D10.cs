using Advent.Utilities;
using Advent.Utilities.Attributes;
using System.Collections.Generic;
using System.Linq;

namespace AdventCalendar2020.D10
{
    [Exercise("Day 10: ")]
    class Y2020D10 : FileSelectionParsingConsole<IList<long>>, IExercise
    {
        public void Execute()
        {
            Start("D10/Data");
        }

        protected override IList<long> DeserializeData(IList<string> data)
        {
            return data.Select(x => long.Parse(x)).ToList();
        }

        private int Diff1 = 0;
        private int Diff2 = 0;
        private int Diff3 = 0;

        protected override void Execute(IList<long> data)
        {
            Diff1 = 0;
            Diff2 = 0;
            Diff3 = 0;

            var highestJoltage = data.Max() + 3;

            var copy = data.Select(x => x).ToList();

            copy.Add(highestJoltage);
            PickAdapter(0, copy);

            System.Console.WriteLine(Diff1);
            System.Console.WriteLine(Diff2);
            System.Console.WriteLine(Diff3);

            AnswerPartOne(Diff1 * Diff3);
            data.Add(0);
            data.Add(highestJoltage);
            AnswerPartTwo(CountWays(data.OrderBy(x => x).ToList()));
        }

        private void PickAdapter(long adapter, List<long> data)
        {
            if (data.Any())
            {
                var selected = data.Where(x => x > adapter && x <= adapter + 3).Min();
                data.Remove(selected);

                System.Console.WriteLine($"{adapter} -> {selected} [{selected - adapter}]");

                switch (selected - adapter)
                {
                    case 1:
                        Diff1++;
                        break;
                    case 2:
                        Diff2++;
                        break;
                    case 3:
                        Diff3++;
                        break;

                    default:
                        System.Console.WriteLine($"difference was too large {selected - adapter}");
                        break;
                }

                PickAdapter(selected, data);
            }
        }

        private long CountWays(List<long> data)
        {
            var visited = new Dictionary<int, long>();
            visited[data.Count - 1] = 1;

            for (var i = data.Count - 2; i >= 0; i--)
            {
                long curr = 0;
                for (var connected = i + 1; connected < data.Count && data[connected] - data[i] <= 3; connected++)
                {
                    curr += visited[connected];
                }
                visited[i] = curr;
            }


            return visited[0];
        }
    }
}
