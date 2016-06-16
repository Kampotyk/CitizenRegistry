using System;
using System.Collections.Generic;
using Citizens.Helpers;
using Humanizer;

namespace Citizens
{
    public class CitizenRegistry : ICitizenRegistry
    {
        private Citizen[] citizens;
        private DateTime lastRegistrationTime;
        private int citizenCount;

        public CitizenRegistry()
        {
            citizens = new Citizen[CitizenRegistryHelper.RegistryCap];
            lastRegistrationTime = DateTime.MinValue;
            citizenCount = 0;
        }

        public void Register(ICitizen citizen)
        {
            if (citizen.VatId == null || CitizenRegistryHelper.FindCitizenById(citizens, citizenCount, citizen.VatId) == null)
            {
                if (String.IsNullOrWhiteSpace(citizen.VatId))
                {
                    int birthNumber;
                    string id;

                    do
                    {
                        birthNumber = CitizenRegistryHelper.GetBirthNumber(citizen.Gender);
                        id = CitizenRegistryHelper.GenerateVatId(citizen.BirthDate, birthNumber, citizen.Gender);
                    } while (CitizenRegistryHelper.FindCitizenById(citizens, citizenCount, id) != null);
                    citizen.VatId = id;
                }
                if (citizenCount == citizens.Length)
                {
                    Array.Resize(ref citizens, citizens.Length * 2);
                }
                citizens[citizenCount] = citizen.Clone() as Citizen;
                citizenCount++;
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
                    throw new ArgumentNullException("id");
                }
                else
                {
                    return CitizenRegistryHelper.FindCitizenById(citizens, citizenCount, id);
                }
            }
        }

        public string Stats()
        {
            int maleCount = 0;
            int femaleCount = 0;

            for (int i = 0; i < citizenCount; i++)
            {
                if (citizens[i].Gender == Gender.Male)
                {
                    maleCount++;
                }
                else
                {
                    femaleCount++;
                }
            }

            var maleCountHuman = "man".ToQuantity(maleCount);
            var femaleCountHuman = "woman".ToQuantity(femaleCount);

            if (maleCount == 0 && femaleCount == 0)
            {
                return String.Format("{0} and {1}", maleCountHuman, femaleCountHuman);
            }
            else
            {
                var lastRegHuman = lastRegistrationTime.Humanize(true, SystemDateTime.Now());
                return String.Format("{0} and {1}. Last registration was {2}", maleCountHuman, femaleCountHuman, lastRegHuman);
            }

        }
    }
}
