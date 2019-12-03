using System.Collections.Generic;
using System.Text.RegularExpressions;
using Advent.Utilities;

namespace Day23
{
    public class NanoBotGroupParser : DataParser<IList<NanoBot>>
    {
        Regex botRegex = new Regex(@"pos=<(-?[0-9]+),(-?[0-9]+),(-?[0-9]+)>, r=([0-9]+)");
        protected override IList<NanoBot> DeserializeData(IList<string> data)
        {
            IList<NanoBot> bots = new List<NanoBot>();

            foreach (var line in data)
            {
                var match = botRegex.Match(line);

                NanoBot bot = new NanoBot(
                    int.Parse(match.Groups[1].Value),
                    int.Parse(match.Groups[2].Value),
                    int.Parse(match.Groups[3].Value),
                    int.Parse(match.Groups[4].Value)
                    );

                bots.Add(bot);
            }

            return bots;
        }
    }
}
