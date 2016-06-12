using Citizens.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            this.firstName = CitizenHelper.toTitleCase(firstName);
            this.lastName = CitizenHelper.toTitleCase(lastName);

            if (CitizenHelper.isGenderValid(gender))
            {
                this.gender = gender;
            }
            else
            {
                throw new ArgumentOutOfRangeException();
            }

            if (CitizenHelper.IsPastDate(birthDate))
            {
                this.birthDate = CitizenHelper.GetDateOnly(birthDate);
            }
            else
            {
                throw new ArgumentException();
            }
        }
    }


}
