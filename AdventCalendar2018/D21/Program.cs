using Advent.Utilities;
using Advent.Utilities.Assembler;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Day21
{
    class Program : FileSelectionConsole
    {
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                new Program().Start(args[0]);
            }
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
