using System.Collections.Generic;
using System.Linq;

namespace AdventCalendar.Day2
{
    public class InventoryManagementSystem
    {
        public static InventoryResult Scan(string id)
        {
            IDictionary<char, int> counts = new Dictionary<char, int>();

            for (int i = 0; i < id.Length; i++)
            {
                if (counts.ContainsKey(id[i]))
                {
                    counts[id[i]]++;
                }
                else
                {
                    counts.Add(id[i], 1);
                }
            }

            return new InventoryResult()
            {
                HasTwo = counts.Any(x => x.Value == 2),
                HasThree = counts.Any(x => x.Value == 3)
            };
        }

        public static int Compare(string first, string second)
        {
            int differences = 0;
            for (int i = 0; i < first.Length; i++)
            {
                if (!first[i].Equals(second[i]))
                {
                    differences++;
                }
            }

            return differences;
        }


        public static int IndexOf(string first, string second)
        {
            for (int i = 0; i < first.Length; i++)
            {
                if (!first[i].Equals(second[i]))
                {
                    return i;
                }
            }

            return -1;
        }
    }
}