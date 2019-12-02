using System;
using System.Collections.Generic;
using System.Linq;
using Advent.Utilities;
using Advent.Utilities.Intcode;

namespace Advent.Y2019.D02
{
    class Program : DataParser<string>
    {
        static void Main(string[] args)
        {
            new Program().Execute();
        }

        private void Execute()
        {
            var processor = new IntcodeProcessor();

            var intcodeInput = ParseData("02P1.txt");
            for (int noun = 0; noun <= 99; noun++)
            {
                for (int verb = 0; verb <= 99; verb++)
                {
                    var test = intcodeInput.Replace("{noun}", noun.ToString()).Replace("{verb}", verb.ToString());
                    var output = processor.Process(test);
                    if (output[0] == 19690720)
                    {
                        Console.WriteLine($"Found the answer at {noun} and {verb}: {100 * noun + verb}");
                    }
                }
            }
        }

        protected override string DeserializeData(IList<string> data)
        {
            return data.FirstOrDefault();
        }
    }
}
