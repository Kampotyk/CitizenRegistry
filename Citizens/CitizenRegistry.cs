using System;
using System.Collections.Generic;
using Citizens.Helpers;

namespace Citizens
{
    public class CitizenRegistry : ICitizenRegistry
    {
        Citizen[] citizens;

        public CitizenRegistry()
        {
            citizens = new Citizen[0];
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
            throw new NotImplementedException();
        }
    }
}
