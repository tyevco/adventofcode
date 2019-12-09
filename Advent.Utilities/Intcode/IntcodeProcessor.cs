using System;
using System.Collections.Generic;
using System.Linq;

namespace Advent.Utilities.Intcode
{
    public class IntcodeProcessor
    {
        public IntcodeProcessor(string programData)
        {
            this.ProgramData = programData;
            this.Reset();
        }

        private string ProgramData { get; set; }

        private MemoryRegister Register { get; set; }

        public bool Running { get; private set; }

        public long Pointer { get; private set; }

        public long RelativeBase { get; private set; }

        public delegate bool OnOutput(long output);

        public event OnOutput Output;

        private static int MaxParameterCount { get; } = 3;

        public IList<long> Arguments { get; private set; } = new List<long>();

        private int ArgPos { get; set; } = 0;

        public bool Halted { get; private set; }

        public void Reset()
        {
            this.Register = new MemoryRegister(ProgramData?.Split(",").Select(x => long.Parse(x)).ToArray());
            this.Pointer = 0;
            this.ArgPos = 0;
            this.Arguments = new List<long>();
            Running = false;
            Halted = false;
        }

        public MemoryRegister Process()
        {
            Running = true;

            for (long i = Pointer; i < Register.Length && Running; i = Pointer)
            {
                var instruction = Register[i].ToString().PadLeft(MaxParameterCount + 2, '0');

                long param1, param2;
                ParameterMode[] modes = new ParameterMode[MaxParameterCount];
                for (int m = 0; m < MaxParameterCount; m++)
                {
                    Enum.TryParse(instruction[m].ToString(), out modes[MaxParameterCount - m - 1]);
                }

                if (Enum.TryParse(instruction.Substring(MaxParameterCount), out OpCode curr))
                {
                    OutputDebugStatement(curr, i, modes, instruction);

                    switch (curr)
                    {
                        case OpCode.Add:
                            param1 = ReadValue(modes[0], i + 1);
                            param2 = ReadValue(modes[1], i + 2);

                            StoreValue(modes[2], i + 3, param1 + param2);

                            Pointer += 4;

                            break;
                        case OpCode.Multiply:
                            param1 = ReadValue(modes[0], i + 1);
                            param2 = ReadValue(modes[1], i + 2);

                            StoreValue(modes[2], i + 3, param1 * param2);

                            Pointer += 4;

                            break;
                        case OpCode.Input:
                            long input = 0;

                            if (Arguments?.Count > 0)
                            {
                                input = Arguments[ArgPos++];

                                Console.WriteLine($"Using input argument {ArgPos}: {input}");
                            }
                            else
                            {
                                Console.Write("Please enter an integer: ");
                                string stdin = Console.ReadLine();

                                while (string.IsNullOrWhiteSpace(stdin) || !long.TryParse(stdin, out input))
                                {
                                    Console.Write("Please enter an integer: ");
                                    stdin = Console.ReadLine();
                                }
                            }

                            StoreValue(modes[0], i + 1, input);

                            Pointer += 2;

                            break;
                        case OpCode.Output:
                            param1 = ReadValue(modes[0], i + 1);

                            Console.WriteLine(param1);
                            bool cont = Output?.Invoke(param1) ?? true;

                            if (!cont)
                            {
                                Running = false;
                            }

                            Pointer += 2;

                            break;
                        case OpCode.JumpIfTrue:
                            param1 = ReadValue(modes[0], i + 1);
                            param2 = ReadValue(modes[1], i + 2);

                            if (param1 != 0)
                            {
                                Pointer = param2;
                            }
                            else
                            {
                                Pointer += 3;
                            }

                            break;
                        case OpCode.JumpIfFalse:
                            param1 = ReadValue(modes[0], i + 1);
                            param2 = ReadValue(modes[1], i + 2);

                            if (param1 == 0)
                            {

                                Pointer = param2;
                            }
                            else
                            {
                                Pointer += 3;
                            }

                            break;
                        case OpCode.LessThan:
                            param1 = ReadValue(modes[0], i + 1);
                            param2 = ReadValue(modes[1], i + 2);

                            StoreValue(modes[2], i + 3, param1 < param2 ? 1 : 0);

                            Pointer += 4;

                            break;
                        case OpCode.Equals:
                            param1 = ReadValue(modes[0], i + 1);
                            param2 = ReadValue(modes[1], i + 2);

                            StoreValue(modes[2], i + 3, param1 == param2 ? 1 : 0);

                            Pointer += 4;

                            break;
                        case OpCode.RelativeBaseAdjust:
                            param1 = ReadValue(modes[0], i + 1);

                            RelativeBase += param1;

                            Pointer += 2;

                            break;
                        case OpCode.Exit:
                            Halted = true;
                            Running = false;

                            Pointer++;

                            break;
                        default:
                            Console.WriteLine($"Invalid opcode detected at pos {i}: {Register[i]}");
                            Halted = true;
                            Running = false;
                            break;
                    }
                }
                else
                {
                    Console.WriteLine($"Invalid opcode detected at pos {i}: {Register[i]}");
                }
            }

            return Register;
        }

        private long ReadValue(ParameterMode mode, long addr)
        {
            if (mode == ParameterMode.Position)
            {
                return Register[Register[addr]];
            }
            else if (mode == ParameterMode.Immediate)
            {
                return Register[addr];
            }
            else if (mode == ParameterMode.Relative)
            {
                return Register[RelativeBase + addr];
            }

            throw new NotImplementedException($"Invalid Parameter Mode specified. {mode}");
        }

        private void StoreValue(ParameterMode mode, long addr, long value)
        {
            if (mode == ParameterMode.Position)
            {
                Register[Register[addr]] = value;
            }
            else if (mode == ParameterMode.Immediate)
            {
                throw new NotImplementedException("Immediate position not supported for store value.");
            }
            else if (mode == ParameterMode.Relative)
            {
                Register[RelativeBase + addr] = value;
            }
        }

        private void OutputDebugStatement(OpCode opCode, long codePos, ParameterMode[] modes, string instruction)
        {
            if (Debug.EnableDebugOutput)
            {
                switch (opCode)
                {
                    case OpCode.Add:
                        Console.WriteLine($"[{instruction}] {opCode.GetPaddedName()}: {GetAddressDisplay(modes[0], codePos + 1)} + {GetAddressDisplay(modes[1], codePos + 2)} => {GetAddressDisplay(modes[2], codePos + 3)}");
                        break;
                    case OpCode.Multiply:
                        Console.WriteLine($"[{instruction}] {opCode.GetPaddedName()}: {GetAddressDisplay(modes[0], codePos + 1)} * {GetAddressDisplay(modes[1], codePos + 2)} => {GetAddressDisplay(modes[2], codePos + 3)}");
                        break;
                    case OpCode.Input:
                        Console.WriteLine($"[{instruction}] {opCode.GetPaddedName()}: [STDIN] => {GetAddressDisplay(modes[0], codePos + 1)}");
                        break;
                    case OpCode.Output:
                        Console.WriteLine($"[{instruction}] {opCode.GetPaddedName()}: {GetAddressDisplay(modes[0], codePos + 1)} => [STDOUT]");
                        break;
                    case OpCode.LessThan:
                        Console.WriteLine($"[{instruction}] {opCode.GetPaddedName()}: {GetAddressDisplay(modes[0], codePos + 1)} < {GetAddressDisplay(modes[1], codePos + 2)} => {GetAddressDisplay(modes[2], codePos + 3)}");
                        break;
                    case OpCode.Equals:
                        Console.WriteLine($"[{instruction}] {opCode.GetPaddedName()}: {GetAddressDisplay(modes[0], codePos + 1)} == {GetAddressDisplay(modes[1], codePos + 2)} => {GetAddressDisplay(modes[2], codePos + 3)}");
                        break;
                    case OpCode.JumpIfTrue:
                        Console.WriteLine($"[{instruction}] {opCode.GetPaddedName()}: {GetAddressDisplay(modes[0], codePos + 1)} != 0 => PTR[{GetAddressDisplay(modes[1], codePos + 2)}]");
                        break;
                    case OpCode.JumpIfFalse:
                        Console.WriteLine($"[{instruction}] {opCode.GetPaddedName()}: {GetAddressDisplay(modes[0], codePos + 1)} == 0 => PTR[{GetAddressDisplay(modes[1], codePos + 2)}]");
                        break;
                    case OpCode.RelativeBaseAdjust:
                        Console.WriteLine($"[{instruction}] {opCode.GetPaddedName()}: {GetAddressDisplay(modes[0], codePos + 1)} -> RMP");
                        break;
                    case OpCode.Exit:
                        Console.WriteLine($"[{instruction}] {opCode.GetPaddedName()}.");
                        break;
                    default:
                        Console.WriteLine($"[{instruction}] Invalid OpCode.");
                        break;
                }
            }
        }

        private string GetAddressDisplay(ParameterMode mode, long addr)
        {
            if (mode == ParameterMode.Immediate)
            {
                return $" {Register[addr]}";
            }
            else if (mode == ParameterMode.Position)
            {
                return $"*{Register[Register[addr]]}";
            }
            else
            {
                return $"~{Register[RelativeBase + Register[addr]]}";
            }
        }
    }
}