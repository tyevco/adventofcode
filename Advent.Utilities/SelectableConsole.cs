using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace Advent.Utilities
{
    public abstract class SelectableConsole
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool SetConsoleMode(IntPtr hConsoleHandle, int mode);
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool GetConsoleMode(IntPtr handle, out int mode);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr GetStdHandle(int handle);



        public SelectableConsole()
        {
            var handle = GetStdHandle(-11);
            int mode;
            GetConsoleMode(handle, out mode);
            SetConsoleMode(handle, mode | 0x4);
        }

        private IList<string> files;
        public string SelectFileFromFolder(string folder)
        {
            int target = 0;
            files = Directory.GetFiles(folder);
            bool seekFile = true;

            if (files.Any())
            {
                while (seekFile)
                {
                    Console.Clear();

                    for (int i = 0; i < files.Count; i++)
                    {
                        if (i == target)
                        {
                            Console.Write("[x] ");
                        }
                        else
                        {
                            Console.Write("[ ] ");
                        }

                        Console.WriteLine(files[i]);
                    }

                    bool keyOkay = false;

                    while (!keyOkay)
                    {
                        var info = Console.ReadKey();

                        if (info.Key == ConsoleKey.Enter)
                        {
                            seekFile = false;
                            keyOkay = true;
                        }
                        else if (info.Key == ConsoleKey.UpArrow)
                        {
                            if (target > 0)
                                target--;
                            keyOkay = true;
                        }
                        else if (info.Key == ConsoleKey.DownArrow)
                        {
                            if (target < files.Count - 1)
                                target++;
                            keyOkay = true;
                        }
                        else if (info.Key == ConsoleKey.Q)
                        {
                            Environment.Exit(0);
                            keyOkay = true;
                        }
                    }
                }

                Console.Clear();

                return files[target];
            }
            else
            {
                Console.WriteLine("No files were found in the specified directory...");
                return null;
            }
        }

        public void Start(string folder)
        {
            while (true)
            {
                var file = SelectFileFromFolder(folder);

                if (file != null)
                {
                    Execute(file);
                }

                Console.WriteLine("Finished.");
                var info = Console.ReadKey();

                if (info.Key == ConsoleKey.Q)
                    break;

                Console.Clear();
            }
        }

        protected abstract void Execute(string file);
    }
}