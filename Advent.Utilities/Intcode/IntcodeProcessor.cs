using System;
using System.Linq;

namespace Advent.Utilities.Intcode
{
    public class IntcodeProcessor
    {
        public bool DisplayOutput { get; set; } = false;

        public int Pointer { get; private set; }

        public int[] Process(string intcodeInput)
        {
            int[] intcode = intcodeInput?.Split(",").Select(x => int.Parse(x)).ToArray();
            bool continueRun = true;

            Pointer = 0;

            for (int i = Pointer; i < intcode.Length && continueRun; i = Pointer)
            {
                int addr1, addr2, addr3;

                if (Enum.TryParse(intcode[i].ToString(), out OpCode curr))
                {
                    switch (curr)
                    {
                        case OpCode.Add:
                            if (DisplayOutput)
                                Console.WriteLine($"[ 1]  Add: &{intcode[i + 1]} + &{intcode[i + 2]} => &{intcode[i + 3]}");
                            addr1 = intcode[i + 1];
                            addr2 = intcode[i + 2];
                            addr3 = intcode[i + 3];

                            intcode[addr3] = intcode[addr1] + intcode[addr2];

                            Pointer += 4;

                            break;
                        case OpCode.Multiply:
                            if (DisplayOutput)
                                Console.WriteLine($"[ 2] Mult: &{intcode[i + 1]} + &{intcode[i + 2]} => &{intcode[i + 3]}");
                            addr1 = intcode[i + 1];
                            addr2 = intcode[i + 2];
                            addr3 = intcode[i + 3];

                            intcode[addr3] = intcode[addr1] * intcode[addr2];

                            Pointer += 4;

                            break;
                        case OpCode.Exit:
                            if (DisplayOutput)
                                Console.WriteLine("[99] Exit.");
                            continueRun = false;

                            Pointer++;

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
    }
}
