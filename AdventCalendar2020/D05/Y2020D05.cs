using Advent.Utilities;
using Advent.Utilities.Attributes;
using System.Collections.Generic;
using System.Linq;

namespace AdventCalendar2020.D05
{
    [Exercise("Day 5: ")]
    class Y2020D05M : FileSelectionParsingConsole<IList<string>>, IExercise
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
            IList<long> seats = new List<long>();
            for (int row = 1; row < 127; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    seats.Add((long)((row * 8) + col));
                }
            }

            IList<long> seatIds = new List<long>();
            foreach (var assignment in data)
            {
                int rowMinIn = 0;
                int rowMaxEx = 128;
                int colMinIn = 0;
                int colMaxEx = 8;
                int row = -1;
                int col = -1;
                foreach (var d in assignment.Substring(0, 7))
                {
                    switch (d)
                    {
                        case 'F':
                            rowMaxEx = ((rowMaxEx - rowMinIn) / 2) + rowMinIn;
                            break;
                        case 'B':
                            rowMinIn = ((rowMaxEx - rowMinIn) / 2) + rowMinIn;
                            break;
                    }
                }

                if (rowMaxEx - 1 == rowMinIn)
                    row = rowMinIn;

                foreach (var d in assignment.Substring(7, 3))
                {
                    switch (d)
                    {
                        case 'L':
                            colMaxEx = ((colMaxEx - colMinIn) / 2) + colMinIn;
                            break;
                        case 'R':
                            colMinIn = ((colMaxEx - colMinIn) / 2) + colMinIn;
                            break;
                    }
                }

                if (colMaxEx - 1 == colMinIn)
                    col = colMinIn;

                seatIds.Add((long)((row * 8) + col));
            }
            AnswerPartOne(seatIds.Max());

            var leftovers = seats.Where(x => !seatIds.Contains(x));
            var mine = leftovers.Where(x => !leftovers.Contains(x - 1) && !leftovers.Contains(x + 1));

            if (mine.Count() == 1)
            {
                AnswerPartTwo(mine.FirstOrDefault());
            }
        }
    }
}
