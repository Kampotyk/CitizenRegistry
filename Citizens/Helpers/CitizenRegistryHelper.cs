﻿using System;

namespace Citizens.Helpers
{
    public class CitizenRegistryHelper
    { 
        private const int MaxBirthsPerDay = 5000;
        private static readonly DateTime StartDate = new DateTime(1899, 12, 31);
        private static readonly Random random = new Random();
        public const int RegistryCap = 10;

        public static string GenerateVatId(DateTime birthDate, int birthNumber, Gender gender)
        {
            int dayDiff = (birthDate - StartDate).Days;
            string code = String.Format("{0:00000}", dayDiff) + String.Format("{0:0000}", birthNumber);
            int controlNum = CalculateControlNumber(code);

            return code + controlNum.ToString();
        }

        public static int CharToInt(char c)
        {
            return (int)(c - '0');
        }

        public static int CalculateControlNumber(string code)
        {
            int controlSum = -1 * CharToInt(code[0]) + 5 * CharToInt(code[1]) + 7 * CharToInt(code[2])
                            + 9 * CharToInt(code[3]) + 4 * CharToInt(code[4]) + 6 * CharToInt(code[5])
                            + 10 * CharToInt(code[6]) + 5 * CharToInt(code[7]) + 7 * CharToInt(code[8]);
            return (controlSum % 11) % 10;
        }

        public static int GetBirthNumber(Gender gender)
        {
            int evenRandomNum = 2 * random.Next(1, MaxBirthsPerDay / 2 + 1);
            int oddRandomNum = evenRandomNum != MaxBirthsPerDay ? evenRandomNum + 1 : evenRandomNum - 1;
            return gender == Gender.Male ? oddRandomNum : evenRandomNum;
        }

        public static Citizen FindCitizenById(Citizen[] citizens, int count, string id)
        {
            for (int i = 0; i < count; i++)
            {
                if (citizens[i].VatId.Equals(id))
                {
                    return citizens[i];
                }
            }
            return null;
        }
    }
}
