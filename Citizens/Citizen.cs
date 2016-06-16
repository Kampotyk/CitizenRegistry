using System;
using Citizens.Helpers;

namespace Citizens
{
    public class Citizen : ICitizen
    {
        private string firstName;
        private string lastName;
        private Gender gender;
        private DateTime birthDate;
        private string vatId;

        public string FirstName
        {
            get { return firstName; }
        }

        public string LastName
        {
            get { return lastName; }
        }

        public Gender Gender
        {
            get { return gender; }
        }

        public DateTime BirthDate
        {
            get { return birthDate; }
        }

        public string VatId
        {
            get
            {
                return vatId;
            }
            set
            {
                vatId = value;
            }
        }

        public Citizen(string firstName, string lastName, DateTime birthDate, Gender gender)
        {
            this.firstName = CitizenHelper.ToTitleCase(firstName);
            this.lastName = CitizenHelper.ToTitleCase(lastName);

            if (CitizenHelper.IsGenderValid(gender))
            {
                this.gender = gender;
            }
            else
            {
                throw new ArgumentOutOfRangeException(gender.ToString());
            }

            if (CitizenHelper.IsPastDate(birthDate))
            {
                this.birthDate = CitizenHelper.GetDateOnly(birthDate);
            }
            else
            {
                throw new ArgumentException(birthDate.ToString());
            }
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }


}
