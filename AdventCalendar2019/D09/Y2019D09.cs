using Advent.Utilities;
using Advent.Utilities.Attributes;
using Advent.Utilities.Intcode;
using System.IO;

namespace AdventCalendar2019.D09
{
    [Exercise("Day 9: Sensor Boost")]
    class Y2019D09 : FileSelectionConsole, IExercise
    {
        public void Execute()
        {
            Start("D09/Data");
        }

        protected override void Execute(string file)
        {
            var intcodeData = File.ReadAllText(file);

            Timer.Monitor(() =>
            {
                IntcodeProcessor Processor = new IntcodeProcessor(intcodeData);
                var output = Processor.Process();

                // output.Print();
            });
        }
    }
}
