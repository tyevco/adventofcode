using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using Advent.Utilities.Attributes;

namespace Advent.Utilities
{
    public abstract class ExerciseSelectionConsole
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool SetConsoleMode(IntPtr hConsoleHandle, int mode);
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool GetConsoleMode(IntPtr handle, out int mode);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr GetStdHandle(int handle);

        private IList<ConsoleOption> options;

        public ExerciseSelectionConsole()
        {
            var handle = GetStdHandle(-11);
            int mode;
            GetConsoleMode(handle, out mode);
            SetConsoleMode(handle, mode | 0x4);
        }

        private int target = 0;
        private IList<Type> assemblyTypes;

        public Type DisplayMenu()
        {
            assemblyTypes = Assembly.GetEntryAssembly().GetTypes()
                                .Where(t => typeof(IExercise).IsAssignableFrom(t) || t.GetCustomAttribute<ExerciseAttribute>() != null).ToList();

            bool seekType = true;

            options = GetOptions();

            if (assemblyTypes.Any())
            {
                while (seekType)
                {
                    Console.Clear();

                    for (int i = 0; i < assemblyTypes.Count; i++)
                    {
                        if (i == target)
                        {
                            Console.Write("> ");
                        }
                        else
                        {
                            Console.Write("  ");
                        }

                        Console.WriteLine(assemblyTypes[i].GetCustomAttribute<ExerciseAttribute>()?.Name ?? assemblyTypes[i].Name);
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
                        seekType = false;
                    }
                    else if (info.Key == ConsoleKey.UpArrow)
                    {
                        if (target > 0)
                            target--;
                    }
                    else if (info.Key == ConsoleKey.DownArrow)
                    {
                        if (target < assemblyTypes.Count - 1)
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

                return assemblyTypes[target];
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

        public void Start()
        {
            while (true)
            {
                var type = DisplayMenu();

                if (type != null)
                {
                    var instance = Activator.CreateInstance(type);

                    if (typeof(IExercise).IsAssignableFrom(type))
                    {
                        ((IExercise)instance).Execute();
                    }
                    else
                    {
                        var executeMethod = type.GetMethod("Execute", BindingFlags.Public | BindingFlags.Instance);
                        executeMethod.Invoke(instance, new object[] { });
                    }
                }

                Console.WriteLine("Finished.");
                var info = Console.ReadKey();

                if (info.Key == ConsoleKey.Q)
                    break;

                Console.Clear();
            }
        }
    }
}