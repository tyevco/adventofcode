using System;
using System.Collections.Generic;

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

        private static readonly IDictionary<string, Func<MemoryRegister, int, int, int>> CommandActions = new Dictionary<string, Func<MemoryRegister, int, int, int>>()
        {
            { Commands.ADDR, (r, a, b) => r[a] + r[b] },
            { Commands.ADDI, (r, a, b) => r[a] + b },
            { Commands.MULR, (r, a, b) => r[a] * r[b] },
            { Commands.MULI, (r, a, b) => r[a] * b },
            { Commands.BANR, (r, a, b) => r[a] & r[b] },
            { Commands.BANI, (r, a, b) => r[a] & b },
            { Commands.BORR, (r, a, b) => r[a] | r[b] },
            { Commands.BORI, (r, a, b) => r[a] | b },
            { Commands.SETI, (r, a, b) => a },
            { Commands.SETR, (r, a, b) => r[a] },
            { Commands.GTIR, (r, a, b) => a > r[b] ? 1 : 0 },
            { Commands.GTRI, (r, a, b) => r[a] > b ? 1 : 0 },
            { Commands.GTRR, (r, a, b) => r[a] > r[b] ? 1 : 0 },
            { Commands.EQIR, (r, a, b) => a == r[b] ? 1 : 0 },
            { Commands.EQRI, (r, a, b) => r[a] == b ? 1 : 0 },
            { Commands.EQRR, (r, a, b) => r[a] == r[b] ? 1 : 0 }
        };

        MemoryRegister register;

        public Assembler()
        {
            register = new MemoryRegister(4);
        }

        public void Process(AssemblerInstructions instructions)
        {
            Instruction instruction = instructions[register.InstructionPointer];

            while (instruction != null)
            {
                var nextRegister = register.Clone();

                ApplyInstruction(instruction, register, nextRegister);

                PrintDebugStatement(instruction, register, nextRegister);
                register = nextRegister;

                register.InstructionPointer++;
                instruction = instructions[register.InstructionPointer];
            }
        }

        public MemoryRegister TestInstruction(Instruction instruction, MemoryRegister register)
        {
            MemoryRegister clone = register.Clone();
            ApplyInstruction(instruction, register, clone);
            return clone;
        }

        private static void ApplyInstruction(Instruction instruction, MemoryRegister register, MemoryRegister nextRegister)
        {
            nextRegister[instruction.C] = CommandActions[instruction.Command](register, instruction.A, instruction.B);
        }

        private static void PrintDebugStatement(Instruction instruction, MemoryRegister register, MemoryRegister nextRegister)
        {
            Console.WriteLine($"ip={register.InstructionPointer} [{register}] {instruction.Command} {instruction.A} {instruction.B} {instruction.C} [{nextRegister}]");
        }
    }
}
