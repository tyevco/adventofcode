using System;
using System.Collections.Generic;
using System.Linq;
using Advent.Utilities;
using Advent.Utilities.Attributes;

namespace AdventCalendar2021.D01
{
    [Exercise("Day 1: Sonar Sweep")]
    class Y2021D01 : FileSelectionParsingConsole<IList<int>>, IExercise
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
            Timer.Monitor(() =>
            {
                var deltas = Enumerable.Range(1, data.Count - 1)
                                .Select(i => data[i - 1] < data[i])
                                .Where(v => v)
                                .Count();

                Console.WriteLine($"Total increases: {deltas}");

                var groupId = 0;

                var groups = Enumerable.Range(2, data.Count - 2)
                            .Select(i => (id: groupId++, depth: data[i] + data[i - 1] + data[i - 2]))
                            .ToList();
                    
                var groupDeltas = Enumerable.Range(1, groups.Count - 1)
                                    .Select(i => groups[i - 1].depth < groups[i].depth)
                                    .Where(v => v)
                                    .Count();

                Console.WriteLine($"Group increases: {groupDeltas}");
            });
        }
    }
}
