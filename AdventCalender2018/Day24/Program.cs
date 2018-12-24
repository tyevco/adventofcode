﻿using System;
using Advent.Utilities;

namespace Day24
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
            var squads = new SquadParser().ParseData(file);

            foreach (var squad in squads)
            {
                Console.WriteLine(squad);
            }

        }
    }
}
