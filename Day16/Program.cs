using Advent.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Day16
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

            Console.WriteLine("Press 1 to run compare, 2 to run evaluation.");

            var partInfo = Console.ReadKey();

            Console.Clear();

            if (partInfo.Key == ConsoleKey.D1)
            {
                RunCompare(instructions);
            }
            else if (partInfo.Key == ConsoleKey.D2)
            {
                RunEvaluation(instructions);
            }
        }

        private void RunCompare(AssemblerInstructions instructions)
        {
            Console.WriteLine("Running compare...");
            Assembler assembler = new Assembler();
            int totalMatchCount = 0;

            IDictionary<int, IList<(string, bool)>> OpResults = new Dictionary<int, IList<(string, bool)>>();

            foreach (var sample in instructions.Samples)
            {
                int sampleMatchCount = 0;

                var instruction = sample.Instruction.Clone();
                IList<(string, bool)> opCodes;
                if (OpResults.ContainsKey(sample.Instruction.Command))
                {
                    opCodes = OpResults[sample.Instruction.Command];
                }
                else
                {
                    opCodes = new List<(string, bool)>();
                    OpResults.Add(sample.Instruction.Command, opCodes);
                }

                foreach (var command in Assembler.Commands.CommandList)
                {
                    instruction.Command = command.Value;
                    var after = assembler.TestInstruction(instruction, sample.Before);
                    if (after.Equals(sample.After))
                    {
                        sampleMatchCount++;
                        opCodes.Add((command.Key, true));
                    }
                    else
                    {
                        opCodes.Add((command.Key, false));
                    }
                }

                if (sampleMatchCount >= 3)
                {
                    totalMatchCount++;
                }
            }

            foreach (var opResult in OpResults)
            {
                var r = opResult.Value.GroupBy(x => x.Item1).Where(x => x.All(y => y.Item2));
                foreach (var a in r)
                {
                    Console.WriteLine($"{opResult.Key} :: {a.Key}");
                }
            }

            Console.WriteLine($"{totalMatchCount} samples matched 3 or more operations.");
        }

        private void RunEvaluation(AssemblerInstructions instructions)
        {
            Console.WriteLine("Running evaluation...");

            Assembler assembler = new Assembler();
            assembler.Process(instructions);

            Console.WriteLine($"Final Register: {assembler.Register}");
        }
    }
}