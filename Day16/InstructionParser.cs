using Advent.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day16
{
    public class InstructionParser : DataParser<AssemblerInstructions>
    {
        Regex instructionLine = new Regex("([0-9]+) ([0-9]+) ([0-9]+) ([0-9]+)");

        Regex sampleLine = new Regex(@"(?:Before|After): ? \[([0-9]+), ([0-9]+), ([0-9]+), ([0-9]+)\]");

        protected override AssemblerInstructions DeserializeData(IList<string> data)
        {
            AssemblerInstructions instructions = new AssemblerInstructions();

            bool secondHalf = false;
            int newLineCount = 0;
            int count = 0;
            Sample sample = new Sample();
            try
            {
                foreach (var line in data)
                {
                    if (string.IsNullOrWhiteSpace(line))
                    {
                        newLineCount++;
                        continue;
                    }
                    else if (newLineCount >= 3)
                    {
                        secondHalf = true;
                    }
                    else
                    {
                        newLineCount = 0;
                    }

                    if (!secondHalf)
                    {
                        if (count == 0)
                        {
                            var match = sampleLine.Match(line);

                            sample.Before = new MemoryRegister(4,
                                int.Parse(match.Groups[1].Value),
                                int.Parse(match.Groups[2].Value),
                                int.Parse(match.Groups[3].Value),
                                int.Parse(match.Groups[4].Value));

                            count++;
                        }
                        else if (count == 1)
                        {
                            var match = instructionLine.Match(line);
                            sample.Instruction = new Instruction
                            {
                                Command = int.Parse(match.Groups[1].Value),
                                A = int.Parse(match.Groups[2].Value),
                                B = int.Parse(match.Groups[3].Value),
                                C = int.Parse(match.Groups[4].Value)
                            };
                            count++;
                        }
                        else
                        {
                            var match = sampleLine.Match(line);

                            sample.After = new MemoryRegister(4,
                                int.Parse(match.Groups[1].Value),
                                int.Parse(match.Groups[2].Value),
                                int.Parse(match.Groups[3].Value),
                                int.Parse(match.Groups[4].Value));

                            instructions.AddSample(sample);
                            sample = new Sample();
                            count = 0;
                        }
                    }
                    else
                    {
                        var instructionLineMatch = instructionLine.Match(line);
                        instructions.AddInstruction(
                             int.Parse(instructionLineMatch.Groups[1].Value),
                            int.Parse(instructionLineMatch.Groups[2].Value),
                            int.Parse(instructionLineMatch.Groups[3].Value),
                            int.Parse(instructionLineMatch.Groups[4].Value)
                            );
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return instructions;
        }
    }
}
