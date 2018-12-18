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

        private IList<ConsoleOption> options;

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

            options = GetOptions();

            if (files.Any())
            {
                while (seekFile)
                {
                    Console.Clear();

                    for (int i = 0; i < files.Count; i++)
                    {
                        if (i == target)
                        {
                            Console.Write("> ");
                        }
                        else
                        {
                            Console.Write("  ");
                        }

                        Console.WriteLine(files[i]);
                    }

                    if (options != null && options.Any())
                    {
                        Console.WriteLine();

                        foreach (var option in options)
                        {
                            if (option.Enabled())
                            {
                                Console.Write("[x] ");
                            } else
                            {
                                Console.Write("[ ] ");
                            }

                            Console.WriteLine(option.ConsoleText);
                        }
                    }

                    Console.WriteLine();
                    Console.WriteLine("Press Q to quit...");

                    var info = Console.ReadKey();

                    if (info.Key == ConsoleKey.Enter)
                    {
                        seekFile = false;
                    }
                    else if (info.Key == ConsoleKey.UpArrow)
                    {
                        if (target > 0)
                            target--;
                    }
                    else if (info.Key == ConsoleKey.DownArrow)
                    {
                        if (target < files.Count - 1)
                            target++;
                    }
                    else if (info.Key == ConsoleKey.Q)
                    {
                        Environment.Exit(0);
                    }
                    else
                    {
                        HandleOptions(info);
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

        protected virtual IList<ConsoleOption> GetOptions()
        {
            return null;
        }

        protected virtual void HandleOptions(ConsoleKeyInfo info)
        {
            if (options != null && options.Any())
            {
                var option = options.FirstOrDefault(o => info.KeyChar == o.Command);
                if (option != null)
                {
                    option.Handler();
                }
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