namespace Day16
{
    public class Instruction
    {
        public int Command { get; internal set; }

        public int A { get; internal set; }
        public int B { get; internal set; }
        public int C { get; internal set; }

        public Instruction Clone()
        {
            return new Instruction
            {
                Command = Command,
                A = A,
                B = B,
                C = C
            };
        }

        public override string ToString()
        {
            return $"{Command} {A} {B} {C}";
        }
    }
}