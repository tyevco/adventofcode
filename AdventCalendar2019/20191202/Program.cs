using System;
using System.Collections.Generic;
using System.Linq;
using Advent.Utilities;
using Advent.Utilities.Intcode;

namespace Advent.Y2019.D02
{
    class Program : DataParser<string>
    {
        static void Main(string[] args)
        {
            new Program().Execute();
        }

        private bool DisplayOutput { get; } = false;

        private void Execute()
        {
            var intcodeInput = ParseData("02P1.txt");
            for (int noun = 0; noun <= 99; noun++)
            {
                for (int verb = 0; verb <= 99; verb++)
                {
                    var test = intcodeInput.Replace("{noun}", noun.ToString()).Replace("{verb}", verb.ToString());
                    var output = ProcessIntcode(test);
                    if (output[0] == 19690720)
                    {
                        Console.WriteLine($"Found the answer at {noun} and {verb}: {100 * noun + verb}");
                    }
                }
            }
        }

        private int[] ProcessIntcode(string intcodeInput)
        {
            int[] intcode = intcodeInput?.Split(",").Select(x => int.Parse(x)).ToArray();
            bool continueRun = true;

            for (int i = 0; i < intcode.Length && continueRun; i += 4)
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
                            break;
                        case OpCode.Multiply:
                            if (DisplayOutput)
                                Console.WriteLine($"[ 2] Mult: &{intcode[i + 1]} + &{intcode[i + 2]} => &{intcode[i + 3]}");
                            addr1 = intcode[i + 1];
                            addr2 = intcode[i + 2];
                            addr3 = intcode[i + 3];

                            intcode[addr3] = intcode[addr1] * intcode[addr2];
                            break;
                        case OpCode.Exit:
                            if (DisplayOutput)
                                Console.WriteLine("[99] Exit.");
                            continueRun = false;
                            break;
                    }
                }
                else
                {
                    Console.WriteLine($"Invalid opcode detected at pos {i}: {intcode[i]}");
                }
            }

            Console.WriteLine(string.Join(",", intcode));

            return intcode;
        }

        protected override string DeserializeData(IList<string> data)
        {
            return data.FirstOrDefault();
        }
    }
}
