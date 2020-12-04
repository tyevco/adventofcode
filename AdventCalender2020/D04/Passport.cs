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

        internal void PrintState()
        {
            if (ValidBirthYear)
                System.Console.WriteLine($"BirthYear: {BirthYear} : {ValidBirthYear}");
            if (ValidIssueYear)
                System.Console.WriteLine($"IssueYear: {IssueYear} : {ValidIssueYear}");
            if (ValidExpirationYear)
                System.Console.WriteLine($"ExpirationYear: {ExpirationYear} : {ValidExpirationYear}");
            if (ValidHeight)
                System.Console.WriteLine($"Height: {Height} : {ValidHeight}");
            if (ValidHairColor)
                System.Console.WriteLine($"HairColor: {HairColor} : {ValidHairColor}");
            if (ValidEyeColor)
                System.Console.WriteLine($"EyeColor: {EyeColor} : {ValidEyeColor}");
            if (ValidPassportID)
                System.Console.WriteLine($"PassportID;: {PassportID} : {ValidPassportID}");
            Console.WriteLine();
        }

        private bool ValidBirthYear => !string.IsNullOrWhiteSpace(BirthYear) &&
                                    int.Parse(BirthYear).IsBetweenInclusive(1920, 2002);

        private bool ValidIssueYear => !string.IsNullOrWhiteSpace(IssueYear) &&
                                    int.Parse(IssueYear).IsBetweenInclusive(2010, 2020);

        private bool ValidExpirationYear => !string.IsNullOrWhiteSpace(ExpirationYear) &&
                                    int.Parse(ExpirationYear).IsBetweenInclusive(2020, 2030);

        private bool ValidHeight => !string.IsNullOrWhiteSpace(Height) && new Regex("^[0-9]+(cm|in)$").IsMatch(Height)
                    && ((Height.Contains("cm") && int.Parse(Height.Substring(0, Height.IndexOf("c"))).IsBetweenInclusive(150, 193))
                        || Height.Contains("in") && int.Parse(Height.Substring(0, Height.IndexOf("i"))).IsBetweenInclusive(59, 76));

        private bool ValidHairColor => !string.IsNullOrWhiteSpace(HairColor) && new Regex("^#[a-f0-9]{6}$").IsMatch(HairColor);

        private bool ValidEyeColor => !string.IsNullOrWhiteSpace(EyeColor) && new Regex("(amb|blu|brn|gry|grn|hzl|oth)").IsMatch(EyeColor);

        private bool ValidPassportID => !string.IsNullOrWhiteSpace(PassportID) && new Regex("^[0-9]{9}$").IsMatch(PassportID);
    }
}