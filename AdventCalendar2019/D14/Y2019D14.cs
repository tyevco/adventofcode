using System.IO;
using Advent.Utilities;
using Advent.Utilities.Attributes;

namespace AdventCalendar2019.D14
{
    [Exercise("Day 14: ")]
    class Y2019D14 : FileSelectionConsole, IExercise
    {
        public void Execute()
        {
            Start("D14/Data");
        }

        protected override void Execute(string file)
        {
            var lines = File.ReadAllLines(file);

            Timer.Monitor(() =>
            {

            });
        }
    }
}
