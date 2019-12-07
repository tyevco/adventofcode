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

        private int[] Program { get; set; }

        public bool Running { get; private set; }

        public int Pointer { get; private set; }

        public delegate bool OnOutput(int output);

        public event OnOutput Output;

        private static int MaxParameterCount { get; } = 3;

        public IList<int> Arguments { get; private set; } = new List<int>();

        private int ArgPos { get; set; } = 0;

        public bool Halted { get; private set; }

        public void Reset()
        {
            this.Program = ProgramData?.Split(",").Select(x => int.Parse(x)).ToArray();
            this.Pointer = 0;
            this.ArgPos = 0;
            this.Arguments = new List<int>();
            Running = false;
            Halted = false;
        }

        public int[] Process()
        {
            Running = true;

            for (int i = Pointer; i < Program.Length && Running; i = Pointer)
            {
                var instruction = Program[i].ToString().PadLeft(MaxParameterCount + 2, '0');

                int param1, param2;
                ParameterMode[] modes = new ParameterMode[MaxParameterCount];
                for (int m = 0; m < MaxParameterCount; m++)
                {
                    Enum.TryParse(instruction[m].ToString(), out modes[MaxParameterCount - m - 1]);
                }

                if (Enum.TryParse(instruction.Substring(MaxParameterCount), out OpCode curr))
                {
                    OutputDebugStatement(Program, curr, i, modes, instruction);

                    switch (curr)
                    {
                        case OpCode.Add:
                            param1 = ReadValue(Program, modes[0], i + 1);
                            param2 = ReadValue(Program, modes[1], i + 2);

                            Program = StoreValue(Program, modes[2], i + 3, param1 + param2);

                            Pointer += 4;

                            break;
                        case OpCode.Multiply:
                            param1 = ReadValue(Program, modes[0], i + 1);
                            param2 = ReadValue(Program, modes[1], i + 2);

                            Program = StoreValue(Program, modes[2], i + 3, param1 * param2);

                            Pointer += 4;

                            break;
                        case OpCode.Input:
                            int input = 0;

                            if (Arguments?.Count > 0)
                            {
                                input = Arguments[ArgPos++];

                                Console.WriteLine($"Using input argument {ArgPos}: {input}");
                            }
                            else
                            {
                                Console.Write("Please enter an integer: ");
                                string stdin = Console.ReadLine();

                                while (string.IsNullOrWhiteSpace(stdin) || !int.TryParse(stdin, out input))
                                {
                                    Console.Write("Please enter an integer: ");
                                    stdin = Console.ReadLine();
                                }
                            }

                            Program = StoreValue(Program, modes[0], i + 1, input);

                            Pointer += 2;

                            break;
                        case OpCode.Output:
                            param1 = ReadValue(Program, modes[0], i + 1);

                            Console.WriteLine(param1);
                            bool cont = Output?.Invoke(param1) ?? true;

                            if (!cont)
                            {
                                Running = false;
                            }

                            Pointer += 2;

                            break;
                        case OpCode.JumpIfTrue:
                            param1 = ReadValue(Program, modes[0], i + 1);
                            param2 = ReadValue(Program, modes[1], i + 2);

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
                            param1 = ReadValue(Program, modes[0], i + 1);
                            param2 = ReadValue(Program, modes[1], i + 2);

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
                            param1 = ReadValue(Program, modes[0], i + 1);
                            param2 = ReadValue(Program, modes[1], i + 2);

                            Program = StoreValue(Program, modes[2], i + 3, param1 < param2 ? 1 : 0);

                            Pointer += 4;

                            break;
                        case OpCode.Equals:
                            param1 = ReadValue(Program, modes[0], i + 1);
                            param2 = ReadValue(Program, modes[1], i + 2);

                            Program = StoreValue(Program, modes[2], i + 3, param1 == param2 ? 1 : 0);

                            Pointer += 4;

                            break;
                        case OpCode.Exit:
                            Halted = true;
                            Running = false;

                            Pointer++;

                            break;
                        default:
                            Console.WriteLine($"Invalid opcode detected at pos {i}: {Program[i]}");
                            Halted = true;
                            Running = false;
                            break;
                    }
                }
                else
                {
                    Console.WriteLine($"Invalid opcode detected at pos {i}: {Program[i]}");
                }
            }

            return Program;
        }

        private static int ReadValue(int[] program, ParameterMode mode, int addr)
        {
            if (mode == ParameterMode.Position)
            {
                return program[program[addr]];
            }
            else
            {
                return program[addr];
            }
        }

        private static int[] StoreValue(int[] program, ParameterMode mode, int addr, int value)
        {
            if (mode == ParameterMode.Position)
            {
                program[program[addr]] = value;
            }
            else
            {
                throw new NotImplementedException();
            }

            return program;
        }

        private static void OutputDebugStatement(int[] program, OpCode opCode, int codePos, ParameterMode[] modes, string instruction)
        {
            if (Debug.EnableDebugOutput)
            {
                switch (opCode)
                {
                    case OpCode.Add:
                        Console.WriteLine($"[{instruction}] {opCode.GetPaddedName()}: {(modes[2] == ParameterMode.Position ? "*" : " ")}{program[codePos + 1]} + {(modes[1] == ParameterMode.Position ? "*" : " ")}{program[codePos + 2]} => {(modes[0] == ParameterMode.Position ? "*" : " ")}{program[codePos + 3]}");
                        break;
                    case OpCode.Multiply:
                        Console.WriteLine($"[{instruction}] {opCode.GetPaddedName()}: {(modes[2] == ParameterMode.Position ? "*" : " ")}{program[codePos + 1]} * {(modes[1] == ParameterMode.Position ? "*" : " ")}{program[codePos + 2]} => {(modes[0] == ParameterMode.Position ? "*" : " ")}{program[codePos + 3]}");
                        break;
                    case OpCode.Input:
                        Console.WriteLine($"[{instruction}] {opCode.GetPaddedName()}: [STDIN] => {(modes[2] == ParameterMode.Position ? "*" : " ")}{program[codePos + 1]}");
                        break;
                    case OpCode.Output:
                        Console.WriteLine($"[{instruction}] {opCode.GetPaddedName()}: {(modes[2] == ParameterMode.Position ? "*" : " ")}{program[codePos + 1]} => [STDOUT]");
                        break;
                    case OpCode.LessThan:
                        Console.WriteLine($"[{instruction}] {opCode.GetPaddedName()}: {(modes[2] == ParameterMode.Position ? "*" : " ")}{program[codePos + 1]} < {(modes[1] == ParameterMode.Position ? "*" : " ")}{program[codePos + 2]} => {(modes[0] == ParameterMode.Position ? "*" : " ")}{program[codePos + 3]}");
                        break;
                    case OpCode.Equals:
                        Console.WriteLine($"[{instruction}] {opCode.GetPaddedName()}: {(modes[2] == ParameterMode.Position ? "*" : " ")}{program[codePos + 1]} == {(modes[1] == ParameterMode.Position ? "*" : " ")}{program[codePos + 2]} => {(modes[0] == ParameterMode.Position ? "*" : " ")}{program[codePos + 3]}");
                        break;
                    case OpCode.JumpIfTrue:
                        Console.WriteLine($"[{instruction}] {opCode.GetPaddedName()}: {(modes[2] == ParameterMode.Position ? "*" : " ")}{program[codePos + 1]} != 0 => PTR[{(modes[1] == ParameterMode.Position ? "*" : " ")}{program[codePos + 2]}]");
                        break;
                    case OpCode.JumpIfFalse:
                        Console.WriteLine($"[{instruction}] {opCode.GetPaddedName()}: {(modes[2] == ParameterMode.Position ? "*" : " ")}{program[codePos + 1]} == 0 => PTR[{(modes[1] == ParameterMode.Position ? "*" : " ")}{program[codePos + 2]}]");
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
    }
}
