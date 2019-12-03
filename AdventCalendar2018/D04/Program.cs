using Advent.Utilities;

namespace Day04
{
    class Program : FileSelectionConsole
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
