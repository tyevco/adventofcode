using Advent.Utilities;
using Advent.Utilities.Assembler;
using System;

namespace Day19
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
