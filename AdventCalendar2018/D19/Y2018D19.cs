using System;
using Advent.Utilities;
using Advent.Utilities.Assembler;
using Advent.Utilities.Attributes;

namespace AdventCalendar2018.D19
{
    [Exercise("Day 19: Go With The Flow")]
    class Program : FileSelectionConsole, IExercise
    {
        public void Execute()
        {
            Start("D19/Data");
        }

        protected override void Execute(string file)
        {
            (var instructions, int instructionPointer) = new InstructionParser().ParseData(file);

            var assembler = new Assembler(6, instructionPointer);

            assembler.AddInstructionOverride((i, r) =>
            {
                if (r.InstructionPointer == 2 && r[3] != 0)
                {
                    if (r[3] % r[5] == 0)
                    {
                        r[0] += r[5];
                    }
                    r[2] = 0;
                    r[1] = r[3];
                    r.InstructionPointer = 12;

                    return true;
                }

                return false;
            });

            assembler.Process(instructions);

            Console.WriteLine($"Final Values: [{assembler.Register}]");
        }
    }
}
