using System;

namespace Advent.Utilities.Intcode
{
    public class MemoryRegister
    {
        private long[] Register;

        public int Length => Register.Length;

        public MemoryRegister(long[] initialValues)
        {
            this.Register = initialValues;
        }

        public long this[long index]
        {
            get
            {
                ValidateRegisterCapacity((int)index);

                return Register[index];
            }
            set
            {
                ValidateRegisterCapacity((int)index);

                Register[index] = value;
            }
        }

        public long this[int index]
        {
            get
            {
                ValidateRegisterCapacity(index);

                return Register[index];
            }
            set
            {
                ValidateRegisterCapacity(index);

                Register[index] = value;
            }
        }

        private void ValidateRegisterCapacity(int index)
        {
            if (index < 0)
            {
                throw new NotSupportedException("Negative index not supported.");
            }

            if (index + 1 > Register.Length)
            {
                Array.Resize(ref Register, index + 1);
            }
        }

        public void Print()
        {
            Helper.PrintArray(Register, delimiter: ", ");
        }
    }
}
