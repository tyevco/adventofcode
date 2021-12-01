using System.Collections.Generic;
using Advent.Utilities;

namespace AdventCalendar2021
{
    class Program : ExerciseSelectionConsole
    {
        public static void Main(string[] args)
        {
            new Program().Start();
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
