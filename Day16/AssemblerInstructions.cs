using System.Collections.Generic;

namespace Day16
{
    public class AssemblerInstructions
    {
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
                A = v1,
                B = v2,
                C = v3
            };

            Instructions.Add(instruction);
        }
    }
}