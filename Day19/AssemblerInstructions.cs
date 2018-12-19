using System.Collections.Generic;

namespace Day19
{
    public class AssemblerInstructions
    {
        public int PointerAddress { get; set; }

        public IList<Instruction> Instructions { get; set; } = new List<Instruction>();

        public Instruction this[int i]
        {
            get
            {
                if (i >= 0 && i < Instructions.Count)
                    return Instructions[i];
                return null;
            }
        }

        public void AddInstruction(string value, int v1, int v2, int v3)
        {
            var instruction = new Instruction
            {
                Command = value,
                Value1 = v1,
                Value2 = v2,
                Value3 = v3
            };

            Instructions.Add(instruction);
        }
    }
}