using System;
using System.Collections.Generic;
using System.Linq;
using Advent.Utilities;
using Advent.Utilities.Attributes;
using Advent.Utilities.Intcode;

namespace AdventCalendar2019.D02
{
    [Exercise("Day 2: 1202 Program Alarm")]
    class Y2019D02 : DataParser<string>, IExercise
    {
        public void Execute()
        {
            var processor = new IntcodeProcessor();

            Timer.Monitor(() =>
            {
                var intcodeInput = ParseData("D02/02P1.txt");
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
            });
        }

        protected override string DeserializeData(IList<string> data)
        {
            return data.FirstOrDefault();
        }
    }
}
