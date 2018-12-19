using System;
using System.Collections.Generic;

namespace Day16
{
    public class AssemblerInstructions
    {
        public IList<Sample> Samples { get; set; } = new List<Sample>();
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

        public void AddInstruction(int value, int a, int b, int c)
        {
            var instruction = new Instruction
            {
                Command = value,
                A = a,
                B = b,
                C = c
            };

            Instructions.Add(instruction);
        }

        public void AddSample(Sample sample)
        {
            Samples.Add(sample);
        }
    }
}