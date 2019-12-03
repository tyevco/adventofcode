using System;
using System.IO;
using Advent.Utilities;
using Advent.Utilities.Attributes;

namespace AdventCalendar2015
{
    [Exercise]
    class Y2015D01 : SelectableConsole
    {
        public void Execute()
        {
            this.Start("D01");
        }

        protected override void Execute(string file)
        {
            var lines = File.ReadAllLines(file);

            foreach (var line in lines)
            {
                var sections = line.Split("=>");

                var floor = CalculateFloor(sections[0]);
                if (sections.Length == 2)
                {
                    int expected = int.Parse(sections[1]);
                    Console.WriteLine($"Santa arrived on floor {floor}, expected {expected} : {floor == expected}");
                }
                else
                {
                    Console.WriteLine($"Santa arrived on floor {floor}");
                }
            }

        }

        private int CalculateFloor(string input)
        {
            bool firstBasement = false;
            int floor = 0;
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == ')')
                {
                    floor--;
                }
                else if (input[i] == '(')
                {
                    floor++;
                }

                if (floor == -1 && !firstBasement)
                {
                    firstBasement = true;
                    Console.WriteLine($"Entered basement at {i + 1}");
                }
            }

            return floor;
        }
    }
}
