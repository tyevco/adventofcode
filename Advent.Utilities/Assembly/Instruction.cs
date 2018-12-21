namespace Advent.Utilities.Assembler
{
    public class Instruction
    {
        public int Command { get; internal set; }

        public int A { get; internal set; }
        public int B { get; internal set; }
        public int C { get; internal set; }

        public Instruction(int Command, int A, int B, int C)
        {
            this.Command = Command;
            this.A = A;
            this.B = B;
            this.C = C;
        }

        public Instruction Clone()
        {
            return new Instruction(Command, A, B, C);
        }

        public override string ToString()
        {
            return $"{Command} {A} {B} {C}";
        }
    }
}