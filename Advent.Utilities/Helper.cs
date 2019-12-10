using System;

namespace Advent.Utilities
{
    public static class Helper
    {
        public static int ReadIntInput(string message = "Please enter an integer")
        {
            int input;
            Console.Write($"{message}: ");
            string stdin = Console.ReadLine();

            while (string.IsNullOrWhiteSpace(stdin) || !int.TryParse(stdin, out input))
            {
                Console.Write($"{message}: ");
                stdin = Console.ReadLine();
            }

            return input;
        }

        public static T[] CreateArray<T>(int size, T defaultValue)
        {
            T[] output = new T[size];

            for (int i = 0; i < size; i++)
            {
                output[i] = defaultValue;
            }

            return output;
        }

        public static void PrintArray<T>(T[] array, int width = int.MaxValue, string delimiter = "")
        {
            if (width == int.MaxValue)
            {
                width = array.Length;
            }

            for (int i = 0; i < array.Length; i++)
            {
                Console.Write(array[i]);

                if (i > 0 && i % width == width - 1)
                {
                    Console.WriteLine();
                }
                else
                {
                    Console.Write(delimiter);
                }
            }
        }
    }
}