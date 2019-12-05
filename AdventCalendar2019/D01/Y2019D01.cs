using System;
using System.Collections.Generic;
using System.Linq;
using Advent.Utilities;
using Advent.Utilities.Attributes;

namespace AdventCalendar2019.D01
{
    [Exercise("Day 1: The Tyranny of the Rocket Equation")]
    class Y2019D01 : DataParser<IList<int>>, IExercise
    {
        public void Execute()
        {
            Timer.Monitor(() =>
            {
                var moduleFuelReqs = this.ParseData("D01/Part1.txt");

                Console.WriteLine($"Total Sum: {moduleFuelReqs.Select(CalculateFuelReqs).Sum()}");

                int total = 0;
                foreach (var module in moduleFuelReqs)
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

        protected override IList<int> DeserializeData(IList<string> data)
        {
            return data.Select(x => int.Parse(x)).ToList();
        }

        private int CalculateFuelReqs(int mass)
        {
            return ((int)(mass / 3) - 2);
        }
    }
}
