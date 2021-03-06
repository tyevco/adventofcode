using Advent.Utilities.Extensions;
using System;
using System.Text.RegularExpressions;

namespace AdventCalendar2020.D04
{
    internal class Passport
    {
        public string BirthYear { get; internal set; }
        public string IssueYear { get; internal set; }
        public string ExpirationYear { get; internal set; }
        public string HairColor { get; internal set; }
        public string Height { get; set; }
        public string EyeColor { get; internal set; }
        public string PassportID { get; internal set; }
        public string CountryID { get; internal set; }

        public bool NotBlank()
        {
            return !string.IsNullOrEmpty(BirthYear)
                    && !string.IsNullOrEmpty(IssueYear)
                    && !string.IsNullOrEmpty(ExpirationYear)
                    && !string.IsNullOrEmpty(Height)
                    && !string.IsNullOrEmpty(HairColor)
                    && !string.IsNullOrEmpty(EyeColor)
                    && !string.IsNullOrEmpty(PassportID);
            //&& !string.IsNullOrWhiteSpace(CountryID);
        }

        public bool Valid()
        {
            return ValidBirthYear
                    && ValidIssueYear
                    && ValidExpirationYear
                    && ValidHeight
                    && ValidHairColor
                    && ValidEyeColor
                    && ValidPassportID;
            //&& !string.IsNullOrWhiteSpace(CountryID);
        }

        private bool ValidBirthYear => !string.IsNullOrWhiteSpace(BirthYear) &&
                                    int.Parse(BirthYear).IsBetweenInclusive(1920, 2002);

        private bool ValidIssueYear => !string.IsNullOrWhiteSpace(IssueYear) &&
                                    int.Parse(IssueYear).IsBetweenInclusive(2010, 2020);

        private bool ValidExpirationYear => !string.IsNullOrWhiteSpace(ExpirationYear) &&
                                    int.Parse(ExpirationYear).IsBetweenInclusive(2020, 2030);

        // language=regex
        private bool ValidHeight => !string.IsNullOrWhiteSpace(Height) && Height.IsMatch("^[0-9]+(cm|in)$")
                    && ((Height.Contains("cm") && int.Parse(Height.Substring(0, Height.IndexOf("c"))).IsBetweenInclusive(150, 193))
                        || Height.Contains("in") && int.Parse(Height.Substring(0, Height.IndexOf("i"))).IsBetweenInclusive(59, 76));

        // language=regex
        private bool ValidHairColor => !string.IsNullOrWhiteSpace(HairColor) && HairColor.IsMatch("^#[a-f0-9]{6}$");

        // language=regex
        private bool ValidEyeColor => !string.IsNullOrWhiteSpace(EyeColor) && EyeColor.IsMatch("(amb|blu|brn|gry|grn|hzl|oth)");

        // language=regex
        private bool ValidPassportID => !string.IsNullOrWhiteSpace(PassportID) && PassportID.IsMatch("^[0-9]{9}$");
    }
}