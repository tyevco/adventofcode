using System;
using Advent.Utilities;

namespace Day19
{
    class Program : SelectableConsole
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
            var instructions = new InstructionParser().ParseData(file);

            var assembler = new Assembler();

            assembler.Process(instructions);
        }
    }
}
