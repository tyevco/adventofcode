using System;
using Advent.Utilities;

namespace Day21
{
    class Program : SelectableConsole
    {
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                new Program().Start(args[0]);
            }
        }

        protected override void Execute(string file)
        {
            
        }
    }
}
