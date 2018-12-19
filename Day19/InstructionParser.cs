using Advent.Utilities;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day19
{
    public class InstructionParser : DataParser<AssemblerInstructions>
    {
        Regex ipLine = new Regex("#ip ([0-9]+)");
        Regex instructionLine = new Regex("([a-z]{4}) ([0-9]) ([0-9]) ([0-9])");
        protected override AssemblerInstructions DeserializeData(IList<string> data)
        {
            AssemblerInstructions instructions = new AssemblerInstructions();

            var ipLineMatch = ipLine.Match(data.First());

            instructions.PointerAddress = int.Parse(ipLineMatch.Groups[1].Value);

            for (int i = 1; i < data.Count; i++)
            {
                var instructionLineMatch = instructionLine.Match(data[i]);
                instructions.AddInstruction(
                    instructionLineMatch.Groups[1].Value,
                    int.Parse(instructionLineMatch.Groups[2].Value),
                    int.Parse(instructionLineMatch.Groups[3].Value),
                    int.Parse(instructionLineMatch.Groups[4].Value)
                    );
            }

            return instructions;
        }
    }
}
