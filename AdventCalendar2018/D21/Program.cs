using System;
using System.Collections.Generic;
using System.Linq;
using Advent.Utilities;
using Advent.Utilities.Assembler;
using Advent.Utilities.Attributes;

namespace AdventCalendar2018.D21
{
    [Exercise("Day 21:  ")]
    class Program : FileSelectionConsole
    {
        public void Execute()
        {
            Start("D21/Data");
        }

        protected override void Execute(string file)
        {
            (var instructions, int instructionPointer) = new InstructionParser().ParseData(file);

            var assembler = new Assembler(6, instructionPointer);
            //assembler.SetRegisterValues(65536);
            HashSet<int> values = new HashSet<int>();

            assembler.AddInstructionOverride((i, r) =>
            {
                if (r.InstructionPointer == 28)
                {
                    if (values.Contains(r[4]))
                    {
                        r.InstructionPointer = 100;
                        return true;
                    }
                    else
                    {
                        values.Add(r[4]);
                    }
                }
                return false;
            });

            Console.WriteLine($"First Value is: {values.FirstOrDefault()}");
            Console.WriteLine($"Last Value is: {values.FirstOrDefault()}");

            assembler.Process(instructions);

            Console.WriteLine($"Final Values: [{assembler.Register}]");
        }
    }
}
