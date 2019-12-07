using Advent.Utilities;
using Advent.Utilities.Attributes;
using Advent.Utilities.Intcode;
using System.IO;

namespace AdventCalendar2019.D05
{
    [Exercise("Day 5: Sunny with a Chance of Asteroids")]
    class Y2019D05 : FileSelectionConsole, IExercise
    {
        public void Execute()
        {
            Start("D05/Data");
        }

        protected override void Execute(string file)
        {
            var intcodeData = File.ReadAllText(file);

            Timer.Monitor(() =>
            {
                IntcodeProcessor Processor = new IntcodeProcessor(intcodeData);
                var output = Processor.Process();
            });
        }
    }
}
