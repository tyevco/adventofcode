using System;

namespace Day16
{
    public class Assembler
    {
        public static class Commands
        {
            public const string ADDR = "addr";
            public const string ADDI = "addi";

            public const string MULR = "mulr";
            public const string MULI = "muli";

            public const string BANR = "banr";
            public const string BANI = "bani";

            public const string BORR = "borr";
            public const string BORI = "bori";

            public const string SETI = "seti";
            public const string SETR = "setr";

            public const string GTIR = "gtir";
            public const string GTRI = "gtri";
            public const string GTRR = "gtrr";

            public const string EQIR = "eqir";
            public const string EQRI = "eqri";
            public const string EQRR = "eqrr";
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
            switch (instruction.Command)
            {
                case Commands.ADDR:
                    nextRegister[instruction.C] = register[instruction.A] + register[instruction.B];
                    break;

                case Commands.ADDI:
                    nextRegister[instruction.C] = register[instruction.A] + instruction.B;
                    break;

                case Commands.MULR:
                    nextRegister[instruction.C] = register[instruction.A] * register[instruction.B];
                    break;
                case Commands.MULI:
                    nextRegister[instruction.C] = register[instruction.A] * instruction.B;
                    break;

                case Commands.BANR:
                    nextRegister[instruction.C] = register[instruction.A] & register[instruction.B];
                    break;
                case Commands.BANI:
                    nextRegister[instruction.C] = register[instruction.A] & instruction.B;
                    break;

                case Commands.BORR:
                    nextRegister[instruction.C] = register[instruction.A] | register[instruction.B];

                    break;
                case Commands.BORI:
                    nextRegister[instruction.C] = register[instruction.A] | instruction.B;
                    break;

                case Commands.SETR:
                    nextRegister[instruction.C] = register[instruction.A];
                    break;

                case Commands.SETI:
                    nextRegister[instruction.C] = instruction.A;
                    break;

                case Commands.GTIR:
                    nextRegister[instruction.C] = instruction.A > register[instruction.B] ? 1 : 0;
                    break;

                case Commands.GTRI:
                    nextRegister[instruction.C] = register[instruction.A] > instruction.B ? 1 : 0;
                    break;

                case Commands.GTRR:
                    nextRegister[instruction.C] = register[instruction.A] > register[instruction.B] ? 1 : 0;
                    break;

                case Commands.EQIR:
                    nextRegister[instruction.C] = instruction.A == register[instruction.B] ? 1 : 0;
                    break;

                case Commands.EQRI:
                    nextRegister[instruction.C] = register[instruction.A] == instruction.B ? 1 : 0;
                    break;

                case Commands.EQRR:
                    nextRegister[instruction.C] = register[instruction.A] == register[instruction.B] ? 1 : 0;
                    break;
            }
        }

        private void PrintDebugStatement(Instruction instruction, MemoryRegister nextRegister)
        {
            Console.WriteLine($"ip={register[PointerAddress]} [{register}] {instruction.Command} {instruction.A} {instruction.B} {instruction.C} [{nextRegister}]");
        }
    }
}
