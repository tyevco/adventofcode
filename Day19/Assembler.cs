using System;

namespace Day19
{
    public class Assembler
    {
        public static class Commands
        {
            public const string SETI = "seti";
            public const string ADDI = "addi";
            public const string SETR = "setr";
            public const string ADDR = "addr";
        }

        int PointerAddress = 0;
        MemoryRegister register;

        public Assembler()
        {
            register = new MemoryRegister(6);
        }

        public void Process(AssemblerInstructions instructions)
        {
            PointerAddress = instructions.PointerAddress;

            int instructionPointer = register[PointerAddress];
            Instruction instruction = instructions[instructionPointer];

            while (instruction != null)
            {
                var nextRegister = new MemoryRegister(6);

                ApplyInstruction(instruction, nextRegister);

                PrintDebugStatement(instruction, nextRegister);
                register = nextRegister;

                instructionPointer = register[PointerAddress];
                instruction = instructions[instructionPointer];
            }
        }

        private void ApplyInstruction(Instruction instruction, MemoryRegister nextRegister)
        {

        }

        private void PrintDebugStatement(Instruction instruction, MemoryRegister nextRegister)
        {
            Console.WriteLine($"ip={register[PointerAddress]} [{register}] {instruction.Command} {instruction.Value1} {instruction.Value2} {instruction.Value3} [{nextRegister}]");
        }
    }
}
