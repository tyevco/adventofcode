﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Advent.Utilities.Assembler
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

            public static IDictionary<string, int> CommandList { get; } = new Dictionary<string, int>()
            {
                { GTRI, 0 },
                { BANI, 1 },
                { EQRR, 2 },
                { GTIR, 3 },
                { EQIR, 4 },
                { BORI, 5 },
                { SETI, 6 },
                { SETR, 7 },
                { ADDR, 8 },
                { BORR, 9 },
                { MULI, 10 },
                { BANR, 11 },
                { ADDI, 12 },
                { EQRI, 13 },
                { MULR, 14 },
                { GTRR, 15 }
            };
        }

        private static readonly IDictionary<int, Func<MemoryRegister, int, int, int>> CommandActions = new Dictionary<int, Func<MemoryRegister, int, int, int>>()
        {
            { Commands.CommandList[Commands.ADDR], (r, a, b) => r[a] + r[b] },
            { Commands.CommandList[Commands.ADDI], (r, a, b) => r[a] + b },
            { Commands.CommandList[Commands.MULR], (r, a, b) => r[a] * r[b] },
            { Commands.CommandList[Commands.MULI], (r, a, b) => r[a] * b },
            { Commands.CommandList[Commands.BANR], (r, a, b) => r[a] & r[b] },
            { Commands.CommandList[Commands.BANI], (r, a, b) => r[a] & b },
            { Commands.CommandList[Commands.BORR], (r, a, b) => r[a] | r[b] },
            { Commands.CommandList[Commands.BORI], (r, a, b) => r[a] | b },
            { Commands.CommandList[Commands.SETI], (r, a, b) => a },
            { Commands.CommandList[Commands.SETR], (r, a, b) => r[a] },
            { Commands.CommandList[Commands.GTIR], (r, a, b) => a > r[b] ? 1 : 0 },
            { Commands.CommandList[Commands.GTRI], (r, a, b) => r[a] > b ? 1 : 0 },
            { Commands.CommandList[Commands.GTRR], (r, a, b) => r[a] > r[b] ? 1 : 0 },
            { Commands.CommandList[Commands.EQIR], (r, a, b) => a == r[b] ? 1 : 0 },
            { Commands.CommandList[Commands.EQRI], (r, a, b) => r[a] == b ? 1 : 0 },
            { Commands.CommandList[Commands.EQRR], (r, a, b) => r[a] == r[b] ? 1 : 0 }
        };

        public MemoryRegister Register { get; private set; }

        private IList<Func<Instruction, MemoryRegister, bool>> InstructionOverrides = new List<Func<Instruction, MemoryRegister, bool>>();

        public Assembler(int registerSize = 4, int instructionPointer = -1)
        {
            Register = new MemoryRegister(registerSize);
            Register.InstructionPointer = instructionPointer;
        }

        public void Process(IList<Instruction> instructions)
        {
            Instruction instruction = instructions[Register.InstructionPointer];

            while (instruction != null)
            {
                bool isInstructionOverriden = false;
                if (InstructionOverrides.Any())
                {
                    foreach (var instructionOverride in InstructionOverrides)
                    {
                        if (instructionOverride(instruction, Register))
                        {
                            isInstructionOverriden = true;
                            break;
                        }
                    }
                }

                if (!isInstructionOverriden)
                {
                    var nextRegister = Register.Clone();

                    ApplyInstruction(instruction, Register, nextRegister);

                    PrintDebugStatement(instruction, Register, nextRegister);
                    Register = nextRegister;

                    Register.InstructionPointer++;
                }

                instruction = instructions[Register.InstructionPointer];
            }
        }

        public void AddInstructionOverride(Func<Instruction, MemoryRegister, bool> overrideFunc)
        {
            InstructionOverrides.Add(overrideFunc);
        }

        public static MemoryRegister TestInstruction(Instruction instruction, MemoryRegister register)
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
