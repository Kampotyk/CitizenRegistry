using System;
using System.Collections.Generic;
using Citizens.Helpers;
using Humanizer;

namespace Citizens
{
    public class CitizenRegistry : ICitizenRegistry
    {
        Citizen[] citizens;
        DateTime lastRegistrationTime;

        public CitizenRegistry()
        {
            citizens = new Citizen[0];
            lastRegistrationTime = DateTime.MinValue;
        }

        public void Register(ICitizen citizen)
        {
            if (citizen.VatId == null || Array.Find(citizens, person => person.VatId == citizen.VatId) == null)
            {
                if (String.IsNullOrWhiteSpace(citizen.VatId))
                {
                    int birthNumber;
                    string id;

                    do
                    {
                        birthNumber = CitizenRegistryHelper.GetBirthNumber(citizen.Gender);
                        id = CitizenRegistryHelper.GenerateVatId(citizen.BirthDate, birthNumber, citizen.Gender);
                    } while (Array.Find(citizens, person => person.VatId == id) != null);
                    citizen.VatId = id;
                }
                Array.Resize(ref citizens, citizens.Length + 1);
                citizens[citizens.Length - 1] = (citizen as Citizen).Clone() as Citizen;
                lastRegistrationTime = SystemDateTime.Now();
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public ICitizen this[string id]
        {
            get
            {
                if (id == null)
                {
                    throw new ArgumentNullException();
                }
                return Array.Find(citizens, citizen => citizen.VatId == id);
            }
        }

        public string Stats()
        {
            int genderCount = Enum.GetNames(typeof(Gender)).Length;
            int[] stats = new int[genderCount];

            foreach (var citizen in citizens)
            {
                stats[(int)citizen.Gender]++;
            }
            string maleCount = "man".ToQuantity(stats[(int)Gender.Male]);
            string femaleCount = "woman".ToQuantity(stats[(int)Gender.Female]);
            if (stats[(int)Gender.Male] == 0 && stats[(int)Gender.Female] == 0)
            {
                return String.Format("{0} and {1}", maleCount, femaleCount);
            }
            else
            {
                var hoursPassed = (lastRegistrationTime - SystemDateTime.Now()).TotalHours;
                string lastRegHuman = DateTime.UtcNow.AddHours(hoursPassed).Humanize();
                return String.Format("{0} and {1}. Last registration was {2}", maleCount, femaleCount, lastRegHuman);
            }

        }
    }
}
