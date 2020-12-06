using Advent.Utilities;
using Advent.Utilities.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventCalendar2020.D05
{
    [Exercise("Day 5: Binary Boarding")]
    class Y2020D05 : FileSelectionParsingConsole<IList<string>>, IExercise
    {
        public void Execute()
        {
            Start("D05/Data");
        }

        protected override IList<string> DeserializeData(IList<string> data)
        {
            return data;
        }

        protected override void Execute(IList<string> data)
        {
            IList<int> seatIds = new List<int>();
            foreach (var assignment in data)
            {
                var binary = string.Join("", assignment.Select(d => d.Equals('B') || d.Equals('R') ? "1" : "0"));
                int seatId = Convert.ToInt32(binary, 2);

                Debug.WriteLine($"{assignment}: {binary} - {seatId}");

                seatIds.Add(seatId);
            }

            AnswerPartOne(seatIds.Max());

            var leftovers = Enumerable.Range(7, 1011).Where(x => !seatIds.Contains(x) && (seatIds.Contains(x - 1) && seatIds.Contains(x + 1)));

            if (leftovers.Count() == 1)
            {
                var mySeat = leftovers.FirstOrDefault();
                var mybinary = Convert.ToString(mySeat, 2).PadLeft(10, '0');

                char[] myAssignment = new char[10];

                for (int i = 0; i < myAssignment.Length; i++)
                {
                    if (i < 7)
                    {
                        myAssignment[i] = mybinary[i] == '1' ? 'B' : 'F';
                    }
                    else
                    {
                        myAssignment[i] = mybinary[i] == '1' ? 'R' : 'L';
                    }
                }

                Debug.WriteLine("\nMy seat:");
                Debug.WriteLine($"{string.Join("", myAssignment)}: {mybinary} - {mySeat}");

                AnswerPartTwo(mySeat);
            }
        }
    }
}
