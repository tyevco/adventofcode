using Advent.Utilities;
using Advent.Utilities.Attributes;
using System.Collections.Generic;
using System.Linq;

namespace AdventCalendar2020.D03
{
    [Exercise("Day 3: Toboggan Trajectory")]
    class Y2020D03 : FileSelectionParsingConsole<int[][]>, IExercise
    {
        public void Execute()
        {
            Start("D03/Data");
        }

        protected override int[][] DeserializeData(IList<string> data)
        {
            return data.Select(x => x.Select(i => i == '.' ? 0 : 1).ToArray()).ToArray();
        }

        protected override void Execute(int[][] data)
        {
            {
                int x = 0, y = 0;
                int width = data[0].Length;

                int trees = 0;
                while (y < data.Length)
                {
                    x = (x + 3) % width;
                    y = y + 1;
                    if (y >= data.Length)
                        break;

                    trees += data[y][x];
                }

                AnswerPartOne(trees);
            }

            {
                var xSlopes = new int[] { 1, 3, 5, 7, 1 };
                var ySlopes = new int[] { 1, 1, 1, 1, 2 };
                List<long> mult = new List<long>(); ;

                for (int i = 0; i < xSlopes.Length; i++)
                {
                    int x = 0, y = 0;
                    int width = data[0].Length;

                    int trees = 0;
                    while (y < data.Length)
                    {
                        x = (x + xSlopes[i]) % width;
                        y = y + ySlopes[i];
                        if (y >= data.Length)
                            break;

                        trees += data[y][x];
                    }

                    System.Console.WriteLine($"{trees}");

                    mult.Add(trees);
                }

                AnswerPartTwo(mult.Aggregate((a, b) => a * b));
            }
        }
    }
}
