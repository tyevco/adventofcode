using System.Text;

namespace Day16
{
    public class MemoryRegister
    {
        int[] register;
        public MemoryRegister(int size)
        {
            register = new int[size];
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
    }
}
