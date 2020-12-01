using Advent.Utilities;
using Advent.Utilities.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventCalendar2019.D01
{
    [Exercise("Day 1: ")]
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
            Timer.Monitor(() =>
            {

                for (int i = 0; i < data.Count; i++)
                {
                    for (int j = 0; j < data.Count; j++)
                    {
                        if (i == j)
                            continue;

                        for (int k = 0; k < data.Count; k++)
                        {
                            if (j == k || i == k)
                                continue;

                            if (data[i] + data[j] + data[k] == 2020)
                            {
                                Console.WriteLine($"{data[i]} * {data[j]} * {data[k]} = {data[i] * data[j] * data[k]}");
                                break;
                            }
                        }

                        if (data[i] + data[j] == 2020)
                        {
                            Console.WriteLine($"{data[i]} * {data[j]} = {data[i] * data[j]}");
                            break;
                        }
                    }
                }
            });
        }

        private int CalculateFuelReqs(int mass)
        {
            return ((int)(mass / 3) - 2);
        }
    }
}
