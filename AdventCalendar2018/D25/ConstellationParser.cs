using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Advent.Utilities;

namespace AdventCalendar2018.D25
{
    public class ConstellationParser : DataParser<(IList<Star>, int)>
    {
        Regex regex = new Regex("(-?[0-9]+),(-?[0-9]+),(-?[0-9]+),(-?[0-9]+)");

        protected override (IList<Star>, int) DeserializeData(IList<string> data)
        {
            int expected = 0;
            IList<Star> stars = new List<Star>();

            var expectedLine = data.LastOrDefault();
            if (!regex.IsMatch(expectedLine))
            {
                expected = int.Parse(expectedLine.Split(":")[1]);
            }

            foreach (var line in data)
            {
                if (regex.IsMatch(line))
                {
                    var match = regex.Match(line);

                    var star = new Star(
                        int.Parse(match.Groups[1].Value),
                        int.Parse(match.Groups[2].Value),
                        int.Parse(match.Groups[3].Value),
                        int.Parse(match.Groups[4].Value)
                        );

                    stars.Add(star);
                }
            }

            return (stars, expected);
        }
    }
}
