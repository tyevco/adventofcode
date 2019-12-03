using System.Text;

namespace AdventCalendar2018.D05
{
    public class PolymerReactionManager
    {
        public static string React(string input)
        {
            return new PolymerReactionManager().handleReaction(input);
        }

        public static string Purge(string input, char polymer)
        {
            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 0; i < input.Length; i++)
            {
                char curr = input[i];

                if (curr.Equals(char.ToLower(polymer)) || curr.Equals(char.ToUpper(polymer)))
                {
                    continue;
                }
                else
                {
                    stringBuilder.Append(curr);
                }
            }

            return stringBuilder.ToString();
        }

        private string handleReaction(string input)
        {
            bool itemRemoved = false;
            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 0; i < input.Length; i++)
            {
                if (i < input.Length - 1)
                {
                    char curr = input[i];
                    char next = input[i + 1];

                    if (!curr.Equals(next) && (curr.Equals(char.ToLower(next)) || curr.Equals(char.ToUpper(next))))
                    {
                        i++;
                        itemRemoved = true;
                    }
                    else
                    {
                        stringBuilder.Append(curr);
                    }
                }
                else
                {
                    stringBuilder.Append(input[i]);
                }
            }

            return itemRemoved ? handleReaction(stringBuilder.ToString()) : stringBuilder.ToString();
        }
    }
}
