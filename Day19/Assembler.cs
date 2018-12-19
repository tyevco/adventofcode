using System;

namespace Day19
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

        MemoryRegister register;
        public MemoryRegister Register => register;

        public Assembler()
        {
            register = new MemoryRegister(6, 1);
        }

        public void Process(AssemblerInstructions instructions)
        {
            MemoryRegister.PointerAddress = instructions.PointerAddress;

            register.InstructionPointer = register[MemoryRegister.PointerAddress];
            Instruction instruction = instructions[register.InstructionPointer];

            while (instruction != null)
            {
                var nextRegister = register.Clone();

                if (register.InstructionPointer == 2 && register[3] != 0)
                {
                    if (register[3] % register[5] == 0)
                    {
                        register[0] += register[5];
                    }
                    register[2] = 0;
                    register[1] = register[3];
                    register.InstructionPointer = 12;
                }
                //else
                //if (register.InstructionPointer == 12 && register[5] < register[3] - 10)
                //{
                //    register[5] = register[3] - 10;
                //}
                else
                {

                    ApplyInstruction(instruction, nextRegister);

                    PrintDebugStatement(instruction, nextRegister);
                    register = nextRegister;

                    register.InstructionPointer++;
                }

                instruction = instructions[register.InstructionPointer];
            }
        }

        private void ApplyInstruction(Instruction instruction, MemoryRegister nextRegister)
        {
            switch (instruction.Command)
            {
                case Commands.ADDR:
                    nextRegister[instruction.C] = nextRegister[instruction.A] + nextRegister[instruction.B];
                    break;

                case Commands.ADDI:
                    nextRegister[instruction.C] = nextRegister[instruction.A] + instruction.B;
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
            Console.WriteLine($"ip={register.InstructionPointer} [{register}] {instruction.Command} {instruction.A} {instruction.B} {instruction.C} [{nextRegister}]");
        }
    }
}
