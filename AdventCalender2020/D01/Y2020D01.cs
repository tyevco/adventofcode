using System;
using System.Collections.Generic;
using System.Linq;
using Advent.Utilities;
using Advent.Utilities.Attributes;

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
                Console.WriteLine($"Total Sum: {data.Select(CalculateFuelReqs).Sum()}");

                int total = 0;
                foreach (var module in data)
                {
                    var fuelReq = CalculateFuelReqs(module);

                    while (fuelReq > 0)
                    {
                        total += fuelReq;

                        fuelReq = CalculateFuelReqs(fuelReq);
                    }
                }

                Console.WriteLine($"Total for fuel: {total}");
            });
        }

        private int CalculateFuelReqs(int mass)
        {
            return ((int)(mass / 3) - 2);
        }
    }
}
