using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Advent.Utilities;
using Advent.Utilities.Assembler;

namespace Day16
{
    public class InstructionParser : DataParser<(IList<Instruction>, IList<Sample>)>
    {
        Regex instructionLine = new Regex("([0-9]+) ([0-9]+) ([0-9]+) ([0-9]+)");

        Regex sampleLine = new Regex(@"(?:Before|After): ? \[([0-9]+), ([0-9]+), ([0-9]+), ([0-9]+)\]");

        protected override (IList<Instruction>, IList<Sample>) DeserializeData(IList<string> data)
        {
            IList<Instruction> instructions = new List<Instruction>();
            IList<Sample> samples = new List<Sample>();

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
                            sample.Instruction = new Instruction(int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value), int.Parse(match.Groups[3].Value), int.Parse(match.Groups[4].Value));
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

                            samples.Add(sample);
                            sample = new Sample();
                            count = 0;
                        }
                    }
                    else
                    {
                        var instructionLineMatch = instructionLine.Match(line);
                        instructions.Add(
                            new Instruction(int.Parse(instructionLineMatch.Groups[1].Value), int.Parse(instructionLineMatch.Groups[2].Value), int.Parse(instructionLineMatch.Groups[3].Value), int.Parse(instructionLineMatch.Groups[4].Value))
                            );
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return (instructions, samples);
        }
    }
}
