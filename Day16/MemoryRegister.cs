using System.Text;

namespace Day16
{
    public class MemoryRegister
    {
        int[] register;

        public int InstructionPointer { get; set; }

        public MemoryRegister(int size, params int[] registerValues)
        {
            register = new int[size];
            for (int i = 0; i < registerValues.Length; i++)
            {
                if (i < register.Length)
                {
                    register[i] = registerValues[i];
                }
                else
                {
                    break;
                }
            }
        }

        public int this[int i]
        {
            get
            {
                return register[i];
            }
            set
            {
                register[i] = value;
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < register.Length; i++)
            {
                sb.Append(register[i]);

                if (i < register.Length - 1)
                {
                    sb.Append(", ");
                }
            }

            return sb.ToString();
        }

        public MemoryRegister Clone()
        {
            var clone = new MemoryRegister(register.Length)
            {
                register = (int[])register.Clone(),
                InstructionPointer = InstructionPointer
            };

            return clone;
        }

        public bool Equals(MemoryRegister other)
        {
            bool match = true;

            if (register.Length == other.register.Length)
            {
                for (int i = 0; i < register.Length; i++)
                {
                    if (register[i] != other.register[i])
                    {
                        match = false;
                        break;
                    }
                }
            }
            else
            {
                match = false;
            }

            return match;
        }
    }
}
