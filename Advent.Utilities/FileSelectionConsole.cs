using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace Advent.Utilities
{
    public abstract class FileSelectionConsole
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool SetConsoleMode(IntPtr hConsoleHandle, int mode);
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool GetConsoleMode(IntPtr handle, out int mode);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr GetStdHandle(int handle);

        private IList<ConsoleOption> options;

        public FileSelectionConsole()
        {
            var handle = GetStdHandle(-11);
            int mode;
            GetConsoleMode(handle, out mode);
            SetConsoleMode(handle, mode | 0x4);
        }

        private int target = 0;
        private IList<string> files;

        public string SelectFileFromFolder(string folder)
        {
            files = Directory.GetFiles(folder);
            bool seekFile = true;

            options = GetOptions();

            if (files.Any())
            {
                if (files.Count == 1)
                {
                    return files.FirstOrDefault();
                }
                else
                {
                    while (seekFile)
                    {
                        Console.SetCursorPosition(0, 0);

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
                                }
                                else
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

        protected ConsoleOption CreateOption(string text, Action handler, Func<bool> enabled, Func<bool> predicate = null)
        {
            return new ConsoleOption
            {
                Text = text,
                Handler = handler,
                Predicate = predicate,
                Enabled = enabled
            };
        }

        protected void HandleOptions(ConsoleKeyInfo info)
        {
            if (options != null && options.Any())
            {
                var option = options.FirstOrDefault(o => char.ToLower(info.KeyChar) == char.ToLower(o.Command));
                if (option != null)
                {
                    if (option.Predicate == null || option.Predicate())
                        option.Handler();
                }


                foreach (var o in options)
                {
                    if (o.Predicate != null && !o.Predicate() && o.Enabled())
                    {
                        o.Handler();
                    }
                }
            }

        }

        public void Start(string folder)
        {
            while (true)
            {
                Console.CursorVisible = false;
                var file = SelectFileFromFolder(folder);
                Console.CursorVisible = true;

                if (file != null)
                {
                    Console.CursorVisible = false;
                    Execute(file);
                    Console.CursorVisible = true;
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