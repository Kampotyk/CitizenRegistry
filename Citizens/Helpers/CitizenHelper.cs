using System;

namespace Citizens.Helpers
{
    public class CitizenHelper
    {
        public static bool isGenderValid(Gender gender)
        {
            return gender == Gender.Male || gender == Gender.Female;
        }

        public static string toTitleCase(string input)
        {
            if (String.IsNullOrEmpty(input))
            {
                throw new ArgumentException();
            }
            else
            {
                var firstLetter = input.Substring(0, 1).ToUpper();
                return input.Length == 1 ? firstLetter : firstLetter + input.Substring(1).ToLower();
            }
        }

        public static bool IsPastDate(DateTime date)
        {
            return date.Date <= SystemDateTime.Now();
        }

        public static DateTime GetDateOnly(DateTime date)
        {
            return date.Date;
        }
    }
}
