using System;

namespace Advent.Utilities
{
    public class ConsoleOption
    {
        public Action Handler { get; set; }

        public Func<bool> Enabled { get; set; }

        public string Text { get; set; }

        public char Command
        {
            get
            {
                return Text[0];
            }
        }

        public string ConsoleText
        {
            get
            {
                return ConsoleCodes.Option(Text);
            }
        }

        public Func<bool> Predicate { get; set; }
    }
}
