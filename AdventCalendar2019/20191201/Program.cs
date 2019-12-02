using System;
using System.Collections.Generic;
using System.Linq;
using Advent.Utilities;

namespace _20191201
{
    class Program : DataParser<IList<int>>
    {
        static void Main(string[] args)
        {
            new Program().Execute();
        }

        private void Execute()
        {
            var moduleFuelReqs = this.ParseData("Part1.txt");

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
