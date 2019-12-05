using System;
using System.Linq;

namespace Advent.Utilities.Intcode
{
    public enum OpCode : int
    {
        Add = 1,
        Multiply = 2,
        Input = 3,
        Output = 4,
        JumpIfTrue = 5,
        JumpIfFalse = 6,
        LessThan = 7,
        Equals = 8,
        Exit = 99
    }

    public static class OpCodeExtensions
    {
        public static int MaxOpCodeNameLength { get; } = Enum.GetNames(typeof(OpCode)).Select(x => x.Length).Max();

        public static string GetPaddedName(this OpCode code, char padchar = ' ')
        {
            return Enum.GetName(typeof(OpCode), code).PadLeft(MaxOpCodeNameLength, padchar);
        }
    }
}
