using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Advent.Utilities;
using Advent.Utilities.Assembler;

namespace Day21
{
    public class InstructionParser : DataParser<(IList<Instruction>, int)>
    {
        Regex ipLine = new Regex("#ip ([0-9]+)");
        Regex instructionLine = new Regex("([a-z]{4}) ([0-9]+) ([0-9]+) ([0-9]+)");
        protected override (IList<Instruction>, int) DeserializeData(IList<string> data)
        {
            IList<Instruction> instructions = new List<Instruction>();

            var ipLineMatch = ipLine.Match(data.First());

            int instructionPointer = int.Parse(ipLineMatch.Groups[1].Value);

            for (int i = 1; i < data.Count; i++)
            {
                var instructionLineMatch = instructionLine.Match(data[i]);
                instructions.Add(new Instruction(
                    Assembler.Commands.CommandList[instructionLineMatch.Groups[1].Value],
                    int.Parse(instructionLineMatch.Groups[2].Value),
                    int.Parse(instructionLineMatch.Groups[3].Value),
                    int.Parse(instructionLineMatch.Groups[4].Value)
                    ));
            }

            return (instructions, instructionPointer);
        }
    }
}
