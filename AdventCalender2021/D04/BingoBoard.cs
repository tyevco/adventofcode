using Advent.Utilities.Data;
using System.Collections.Generic;
using System.Linq;

namespace AdventCalendar2021.D04
{
    public class BingoBoard
    {
        public BingoBoard(IEnumerable<int> numbers, int width, int height)
        {
            Grid = new CartesianGrid<int>(numbers, width, height);
            State = new CartesianGrid<bool>(width, height);
        }

        public CartesianGrid<int> Grid { get; }

        public CartesianGrid<bool> State { get; }

        public bool Complete { get; private set; }

        public bool Mark(int drawnNumber)
        {
            bool foundWinner = false;

            if (!Complete)
            {
                var index = Grid.IndexOf(drawnNumber);

                if (index >= 0)
                {
                    State.Set(index, true);
                }

                // check all columns
                for (int i = 0; i < State.Width; i++)
                {
                    var col = State.GetColumn(i);
                    if (col.All(x => x))
                    {
                        foundWinner = true;
                        break;
                    }
                }

                if (!foundWinner)
                {
                    // if no columns won, check all rows.
                    for (int i = 0; i < State.Height; i++)
                    {
                        var row = State.GetRow(i);
                        if (row.All(x => x))
                        {
                            foundWinner = true;
                            break;
                        }
                    }
                }

                if (foundWinner)
                {
                    Complete = true;
                }
            }

            return foundWinner;
        }

        public int[] GetUnmarked()
        {
            List<int> unmarked = new List<int>();

            for (int x = 0; x < Grid.Width; x++)
            {
                for (int y = 0; y < Grid.Height; y++)
                {
                    if (!State.Get(x, y))
                    {
                        unmarked.Add(Grid.Get(x, y));
                    }
                }
            }

            return unmarked.ToArray();
        }
    }
}