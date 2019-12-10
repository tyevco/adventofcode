using Advent.Utilities;
using Advent.Utilities.Attributes;
using Advent.Utilities.Intcode;
using System.IO;

namespace AdventCalendar2019.D11
{
    [Exercise("Day 11: ")]
    class Y2019D11 : FileSelectionConsole, IExercise
    {
        public void Execute()
        {
            Start("D11/Data");
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
