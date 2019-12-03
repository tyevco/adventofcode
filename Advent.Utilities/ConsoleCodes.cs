namespace Advent.Utilities
{
    public static class ConsoleCodes
    {
        public const string UNDERLINE = "\x1B[4m";
        public const string RESET = "\x1B[0m";

        public static string Option(string output)
        {
            return UNDERLINE + output[0] + RESET + output.Substring(1, output.Length - 1);
        }

        public static string Colorize(string message, int color, int originalColor = 255)
        {
            return $"{COLOR(color)}{message}{COLOR(originalColor)}";
        }

        public static string COLOR(int color)
        {
            return "\x1b[38;5;" + color.ToString() + "m";
        }
    }
}
