using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventCalendar2020.D08
{
    public class GameMan
    {
        public GameMan(IList<string> programData)
        {
            this.ProgramData = programData;
            this.Reset();
        }

        private IList<string> ProgramData { get; set; }

        private long Accumulator { get; set; }

        public bool Running { get; private set; }

        public int Pointer { get; private set; }

        public long RelativeBase { get; private set; }


        public void Reset()
        {
            Accumulator = 0;
            Pointer = 0;
            Running = false;
        }

        Regex InstructionParser = new Regex("([a-z]{3}) ([+-][0-9]+)");

        public long Process()
        {
            Running = true;
            HashSet<int> instructions = new HashSet<int>();
            for (int i = Pointer; i < ProgramData.Count && Running; i = Pointer)
            {
                var instruction = ProgramData[i];

                if (instructions.Contains(i))
                {
                    break;
                }
                else
                {
                    instructions.Add(i);

                    var match = InstructionParser.Match(instruction);

                    var command = match.Groups[1].Value;
                    var value = int.Parse(match.Groups[2].Value);

                    if (Enum.TryParse(command, true, out OpCode curr))
                    {
                        switch (curr)
                        {
                            case OpCode.Acc:
                                Accumulator += value;
                                Pointer += 1;
                                break;

                            case OpCode.Jmp:
                                Pointer += value;
                                break;

                            case OpCode.Nop:
                                Pointer += 1;
                                break;

                            default:
                                Console.WriteLine($"Invalid opcode detected at pos {i}: {ProgramData[i]}");
                                Running = false;
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Invalid opcode detected at pos {i}: {ProgramData[i]}");
                    }
                }
            }

            return Accumulator;
        }

        public long ProcessTwo()
        {
            Running = true;

            var original = ProgramData.Select(x => x).ToList();
            HashSet<int> instructions = new HashSet<int>();
            HashSet<int> flippedCommands = new HashSet<int>();
            bool executing = true;

            while (executing)
            {
                bool looped = false;
                bool flipped = false;

                Accumulator = 0;
                Pointer = 0;
                instructions.Clear();

                for (int i = Pointer; i < ProgramData.Count && Running; i = Pointer)
                {
                    var instruction = ProgramData[i];
                    //Console.WriteLine(instruction);

                    if (instructions.Contains(i))
                    {
                        Console.WriteLine($"Looped on line {i} : {Accumulator}");
                        looped = true;
                        break;
                    }
                    else
                    {
                        instructions.Add(i);

                        var match = InstructionParser.Match(instruction);

                        var command = match.Groups[1].Value;
                        var value = int.Parse(match.Groups[2].Value);

                        if (Enum.TryParse(command, true, out OpCode curr))
                        {
                            if (!flipped && (curr == OpCode.Jmp || curr == OpCode.Nop) && !flippedCommands.Contains(i))
                            {
                                var now = curr == OpCode.Jmp ? OpCode.Nop : OpCode.Jmp;
                                flippedCommands.Add(i);
                                Console.WriteLine($"Flipping line {i} from {curr} to {now}");
                                curr = now;
                                flipped = true;
                            }

                            switch (curr)
                            {
                                case OpCode.Acc:
                                    Accumulator += value;
                                    Pointer += 1;
                                    break;

                                case OpCode.Jmp:
                                    Pointer += value;
                                    break;

                                case OpCode.Nop:
                                    Pointer += 1;
                                    break;

                                default:
                                    Console.WriteLine($"Invalid opcode detected at pos {i}: {ProgramData[i]}");
                                    Running = false;
                                    break;
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Invalid opcode detected at pos {i}: {ProgramData[i]}");
                        }
                    }
                }

                if (looped)
                {
                }
                else
                {
                    executing = false;
                }
            }
            return Accumulator;
        }
    }
}
