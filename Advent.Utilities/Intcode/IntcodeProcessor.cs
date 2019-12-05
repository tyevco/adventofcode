using System;
using System.Linq;

namespace Advent.Utilities.Intcode
{
    public class IntcodeProcessor
    {
        public bool DisplayDebugOutput { get; set; } = true;

        public int Pointer { get; private set; }

        private static int MaxParameterCount { get; } = 3;

        public int[] Process(string intcodeInput)
        {
            int[] intcode = intcodeInput?.Split(",").Select(x => int.Parse(x)).ToArray();
            bool continueRun = true;

            Pointer = 0;

            for (int i = Pointer; i < intcode.Length && continueRun; i = Pointer)
            {
                var instruction = intcode[i].ToString().PadLeft(MaxParameterCount + 2, '0');

                int param1, param2;
                ParameterMode[] modes = new ParameterMode[MaxParameterCount];
                for (int m = 0; m < MaxParameterCount; m++)
                {
                    Enum.TryParse(instruction[m].ToString(), out modes[MaxParameterCount - m - 1]);
                }

                if (Enum.TryParse(instruction.Substring(MaxParameterCount), out OpCode curr))
                {
                    OutputDebugStatement(intcode, curr, i, modes, instruction);

                    switch (curr)
                    {
                        case OpCode.Add:
                            param1 = ReadValue(intcode, modes[0], i + 1);
                            param2 = ReadValue(intcode, modes[1], i + 2);

                            intcode = StoreValue(intcode, modes[2], i + 3, param1 + param2);

                            Pointer += 4;

                            break;
                        case OpCode.Multiply:
                            param1 = ReadValue(intcode, modes[0], i + 1);
                            param2 = ReadValue(intcode, modes[1], i + 2);

                            intcode = StoreValue(intcode, modes[2], i + 3, param1 * param2);

                            Pointer += 4;

                            break;
                        case OpCode.Input:

                            Console.Write("Please enter an integer: ");
                            string stdin = Console.ReadLine();
                            int input = 0;

                            while (string.IsNullOrWhiteSpace(stdin) || !int.TryParse(stdin, out input))
                            {
                                Console.Write("Please enter an integer: ");
                                stdin = Console.ReadLine();
                            }

                            intcode = StoreValue(intcode, modes[0], i + 1, input);

                            Pointer += 2;

                            break;
                        case OpCode.Output:
                            param1 = ReadValue(intcode, modes[0], i + 1);

                            Console.WriteLine(param1);

                            Pointer += 2;

                            break;
                        case OpCode.JumpIfTrue:
                            param1 = ReadValue(intcode, modes[0], i + 1);
                            param2 = ReadValue(intcode, modes[1], i + 2);

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
                            param1 = ReadValue(intcode, modes[0], i + 1);
                            param2 = ReadValue(intcode, modes[1], i + 2);

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
                            param1 = ReadValue(intcode, modes[0], i + 1);
                            param2 = ReadValue(intcode, modes[1], i + 2);

                            intcode = StoreValue(intcode, modes[2], i + 3, param1 < param2 ? 1 : 0);

                            Pointer += 4;

                            break;
                        case OpCode.Equals:
                            param1 = ReadValue(intcode, modes[0], i + 1);
                            param2 = ReadValue(intcode, modes[1], i + 2);

                            intcode = StoreValue(intcode, modes[2], i + 3, param1 == param2 ? 1 : 0);

                            Pointer += 4;

                            break;
                        case OpCode.Exit:
                            continueRun = false;

                            Pointer++;

                            break;
                        default:
                            Console.WriteLine($"Invalid opcode detected at pos {i}: {intcode[i]}");
                            continueRun = false;
                            break;
                    }
                }
                else
                {
                    Console.WriteLine($"Invalid opcode detected at pos {i}: {intcode[i]}");
                }
            }

            return intcode;
        }

        private int ReadValue(int[] intcode, ParameterMode mode, int addr)
        {
            if (mode == ParameterMode.Position)
            {
                return intcode[intcode[addr]];
            }
            else
            {
                return intcode[addr];
            }
        }

        private int[] StoreValue(int[] intcode, ParameterMode mode, int addr, int value)
        {
            if (mode == ParameterMode.Position)
            {
                intcode[intcode[addr]] = value;
            }
            else
            {
                throw new NotImplementedException();
            }

            return intcode;
        }

        private void OutputDebugStatement(int[] intcode, OpCode opCode, int codePos, ParameterMode[] modes, string instruction)
        {
            if (DisplayDebugOutput)
            {
                switch (opCode)
                {
                    case OpCode.Add:
                        Console.WriteLine($"[{instruction}] {opCode.GetPaddedName()}: {(modes[2] == ParameterMode.Position ? "*" : " ")}{intcode[codePos + 1]} + {(modes[1] == ParameterMode.Position ? "*" : " ")}{intcode[codePos + 2]} => {(modes[0] == ParameterMode.Position ? "*" : " ")}{intcode[codePos + 3]}");
                        break;
                    case OpCode.Multiply:
                        Console.WriteLine($"[{instruction}] {opCode.GetPaddedName()}: {(modes[2] == ParameterMode.Position ? "*" : " ")}{intcode[codePos + 1]} * {(modes[1] == ParameterMode.Position ? "*" : " ")}{intcode[codePos + 2]} => {(modes[0] == ParameterMode.Position ? "*" : " ")}{intcode[codePos + 3]}");
                        break;
                    case OpCode.Input:
                        Console.WriteLine($"[{instruction}] {opCode.GetPaddedName()}: [STDIN] => {(modes[2] == ParameterMode.Position ? "*" : " ")}{intcode[codePos + 1]}");
                        break;
                    case OpCode.Output:
                        Console.WriteLine($"[{instruction}] {opCode.GetPaddedName()}: {(modes[2] == ParameterMode.Position ? "*" : " ")}{intcode[codePos + 1]} => [STDOUT]");
                        break;
                    case OpCode.LessThan:
                        Console.WriteLine($"[{instruction}] {opCode.GetPaddedName()}: {(modes[2] == ParameterMode.Position ? "*" : " ")}{intcode[codePos + 1]} < {(modes[1] == ParameterMode.Position ? "*" : " ")}{intcode[codePos + 2]} => {(modes[0] == ParameterMode.Position ? "*" : " ")}{intcode[codePos + 3]}");
                        break;
                    case OpCode.Equals:
                        Console.WriteLine($"[{instruction}] {opCode.GetPaddedName()}: {(modes[2] == ParameterMode.Position ? "*" : " ")}{intcode[codePos + 1]} == {(modes[1] == ParameterMode.Position ? "*" : " ")}{intcode[codePos + 2]} => {(modes[0] == ParameterMode.Position ? "*" : " ")}{intcode[codePos + 3]}");
                        break;
                    case OpCode.JumpIfTrue:
                        Console.WriteLine($"[{instruction}] {opCode.GetPaddedName()}: {(modes[2] == ParameterMode.Position ? "*" : " ")}{intcode[codePos + 1]} != 0 => PTR[{(modes[1] == ParameterMode.Position ? "*" : " ")}{intcode[codePos + 2]}]");
                        break;
                    case OpCode.JumpIfFalse:
                        Console.WriteLine($"[{instruction}] {opCode.GetPaddedName()}: {(modes[2] == ParameterMode.Position ? "*" : " ")}{intcode[codePos + 1]} == 0 => PTR[{(modes[1] == ParameterMode.Position ? "*" : " ")}{intcode[codePos + 2]}]");
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
