using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Advent.Utilities;

namespace AdventCalendar2018.D24
{
    public class SquadParser : DataParser<IList<Squad>>
    {
        Regex squadRegex = new Regex(@"([0-9]+) units each with ([0-9]+) hit points(?: \((?:(weak|immune) to ([a-z, ]+))(?:; (weak|immune) to ([a-z, ]+))?\))? with an attack that does ([0-9]+) ([a-z]+) damage at initiative ([0-9]+)");

        protected override IList<Squad> DeserializeData(IList<string> data)
        {
            bool immuneSystem = true;
            IList<Squad> squads = new List<Squad>();

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
                    squad.Id = squads.Count(s => s.Team == squad.Team) + 1;
                    squads.Add(squad);
                }
            }

            return squads;
        }

        private Squad ParseMatch(Match regex)
        {
            var squad = new Squad();

            squad.Units = int.Parse(regex.Groups[1].Value);
            squad.Health = int.Parse(regex.Groups[2].Value);

            if (regex.Groups[3].Success)
            {
                var immuneOrWeak = regex.Groups[3].Value;
                var dmgTypes = regex.Groups[4].Value.Split(", ");
                SetupDamageTypesForSquad(squad, immuneOrWeak, dmgTypes);
            }

            if (regex.Groups[5].Success)
            {
                var immuneOrWeak = regex.Groups[5].Value;
                var dmgTypes = regex.Groups[6].Value.Split(", ");
                SetupDamageTypesForSquad(squad, immuneOrWeak, dmgTypes);
            }

            squad.AttackPower = int.Parse(regex.Groups[7].Value);
            squad.AttackType = Enum.Parse<DamageType>(regex.Groups[8].Value);
            squad.Initiative = int.Parse(regex.Groups[9].Value);

            return squad;
        }

        private void SetupDamageTypesForSquad(Squad squad, string immuneOrWeak, string[] dmgTypes)
        {
            if (immuneOrWeak.Equals("immune"))
            {
                foreach (var dmgType in dmgTypes)
                {
                    squad.AddImmunity(Enum.Parse<DamageType>(dmgType));
                }
            }
            else
            {
                foreach (var dmgType in dmgTypes)
                {
                    squad.AddWeakness(Enum.Parse<DamageType>(dmgType));
                }
            }
        }
    }
}
