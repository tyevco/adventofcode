using Advent.Utilities;
using Advent.Utilities.Attributes;
using System.Collections.Generic;
using System.Linq;

namespace AdventCalendar2020.D04
{
    [Exercise("Day 4: Passport Processing")]
    class Y2020D04 : FileSelectionParsingConsole<IList<Passport>>, IExercise
    {
        public void Execute()
        {
            Start("D04/Data");
        }

        protected override IList<Passport> DeserializeData(IList<string> data)
        {
            IList<Passport> passports = new List<Passport>();

            Passport passport = new Passport();
            foreach (var line in data)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    passports.Add(passport);
                    passport = new Passport();
                }
                else
                {
                    var entries = line.Split(" ", System.StringSplitOptions.RemoveEmptyEntries).Select(x => x.Split(":"));

                    foreach (var entry in entries)
                    {
                        switch (entry[0])
                        {
                            case "byr":
                                passport.BirthYear = entry[1].Trim();
                                break;

                            case "iyr":
                                passport.IssueYear = entry[1].Trim();
                                break;

                            case "eyr":
                                passport.ExpirationYear = entry[1].Trim();
                                break;

                            case "hgt":
                                passport.Height = entry[1].Trim();
                                break;

                            case "hcl":
                                passport.HairColor = entry[1].Trim();
                                break;

                            case "ecl":
                                passport.EyeColor = entry[1].Trim();
                                break;

                            case "pid":
                                passport.PassportID = entry[1].Trim();
                                break;

                            case "cid":
                                passport.CountryID = entry[1].Trim();
                                break;

                            default:
                                break;
                        }
                    }
                }
            }

            if (passport.Valid())
                passports.Add(passport);

            return passports;
        }

        protected override void Execute(IList<Passport> data)
        {
            AnswerPartOne(data.Count(c => c.NotBlank()));
            AnswerPartTwo(data.Count(c => c.Valid()));
        }
    }
}
