using System;
using System.Collections.Generic;
using System.Linq;
using Advent.Utilities.Attributes;

namespace AdventCalendar2019.D04
{
    [Exercise("Day 4: Secure Container")]
    class Y2019D04
    {
        public void Execute()
        {
            //ExecuteEach(111112, 112233, 123456, 111122, 112222, 122222, 111222, 123444, 113455);
            Execute(206938, 679128);
        }

        protected void ExecuteEach(params int[] values)
        {
            foreach (var value in values)
            {
                Console.Write($"{value}: ");
                if (MatchPattern(value))
                {
                    Console.WriteLine($"true");
                }
                else
                {
                    Console.WriteLine($"false");
                }
            }

        }

        protected void Execute(int lower, int? upper = null)
        {
            Console.WriteLine($"Analyzing between {lower} and {upper ?? lower}");

            int matches = 0;
            for (int i = lower; i <= (upper ?? lower); i++)
            {
                if (MatchPattern(i))
                {
                    matches++;
                }
            }

            Console.WriteLine($"There were {matches} matches.");
        }

        private bool MatchPattern(int input)
        {
            var digits = input.ToString().ToCharArray().Select(x => int.Parse(x.ToString())).ToArray();

            IDictionary<int, int> adjCount = new Dictionary<int, int>();

            AddDigit(adjCount, digits[0]);

            for (int i = 1; i < digits.Length; i++)
            {
                if (digits[i - 1] > digits[i])
                {
                    return false;
                }

                AddDigit(adjCount, digits[i]);
            }

            return adjCount.Any(x => x.Value == 2);
        }

        private void AddDigit(IDictionary<int, int> adjCount, int digit)
        {
            if (adjCount.ContainsKey(digit))
            {
                adjCount[digit]++;
            }
            else
            {
                adjCount.Add(digit, 1);
            }
        }
    }
}
