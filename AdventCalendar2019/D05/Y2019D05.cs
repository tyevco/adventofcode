using System.Collections.Generic;
using System.IO;
using Advent.Utilities;
using Advent.Utilities.Attributes;
using Advent.Utilities.Intcode;

namespace AdventCalendar2019.D05
{
    [Exercise("Day 5: Sunny with a Chance of Asteroids")]
    class Y2019D05 : FileSelectionConsole, IExercise
    {
        private IntcodeProcessor Processor { get; } = new IntcodeProcessor();

        public void Execute()
        {
            Start("D05/Data");
        }

        protected override void Execute(string file)
        {
            var intcodeData = File.ReadAllText(file);

            Timer.Monitor(() =>
            {
                var output = Processor.Process(intcodeData);
            });
        }

        protected override IList<ConsoleOption> GetOptions()
        {
            return new List<ConsoleOption>
            {
                new ConsoleOption
                {
                    Text = "Enable Debug Output",
                    Enabled = () => Debug.EnableDebugOutput,
                    Handler = () => Debug.EnableDebugOutput = !Debug.EnableDebugOutput,
                }
            };
        }
    }
}
