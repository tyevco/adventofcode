using Advent.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Day24
{
    public class SquadParser : DataParser<string>
    {
        Regex squadRegex = new Regex(@"([0-9]+) units each with ([0-9]+) hit points \(\) with an attack that does ([0-9]+) /type/ damage at initiatve ([0-9]+)");
               
        protected override string DeserializeData(IList<string> data)
        {
            bool immuneSystem = true;
            foreach (var line in data)
            {
                if (line.StartsWith("Immune System:"))
                {
                    immuneSystem = true;
                }
                else if (line.StartsWith("Infection:"))
                {
                    immuneSystem = false;
                }
                else if (squadRegex.IsMatch(line))
                {
                    var squad = ParseMatch(squadRegex.Match(line));
                    squad.Team = immuneSystem ? Team.ImmuneSystem : Team.Infection;
                }
            }
            throw new NotImplementedException();
        }

        private Squad ParseMatch(Match regex)
        {
            var squad = new Squad();

            return squad;
        }
    }
}
