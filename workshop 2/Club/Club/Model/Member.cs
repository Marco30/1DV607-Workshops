using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Club.Model
{
    class Member
    {
        private string _firstName;
        private string _lastName;
        private int _socialSecurityNumber;
        private int _memberNumber;
        private int _numberOfBoats;

        public Member()// constructor
        {
            this._firstName = "Marco";
            this._lastName = "Villegas";
            this._memberNumber = 00000;
            this._socialSecurityNumber = 000000;
            this._numberOfBoats = 0;
        }

        public string FirstName//används för att till della namn och hämta ut namn
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
            }
        }

        public string LastName//används för att till della efternamn och hämta ut efternamn
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
            }
        }

        public int NumberOfBoats//används för att till della antal båtar och hämtar ut namn
        {
            get { return _numberOfBoats; }
            set
            {
                _numberOfBoats = value;
            }
        }

        public int MemberNumber// används för att till della medlems nummer och hämta ut
        {
            get { return _memberNumber; }
            set
            {

                if (value < 0)
                {
                    throw new ArgumentException("Inte ett tillåtet nummer");
                }


                _memberNumber = value;
            }
        }

        public void CreateMemberNumber()// funktion som skapar medlems nummer
        {
            Random random = new Random();
            int randomNumber = random.Next(10000, 99999);

            this._memberNumber = randomNumber;
        }

        public int SocialSecurityNumber//används för att till della personnummer och hämta ut personnummer
        {
            get { return _socialSecurityNumber; }
            set
            {
                _socialSecurityNumber = value;
            }
        }

        public string PrintFull()// funktion som skapar en string som använda för att visa full information om en medlem
        {
            string returnString;

            returnString = String.Format("\nNamn:\t\t{0}\nEfternamn:\t{1}\nPersonnummer:\t{2}\nMedlemsnummer:\t{3}", _firstName, _lastName, _socialSecurityNumber, _memberNumber);

            return returnString;
        }

        public string PrintCompact()// funktion som skapar en string som användas för att visa enkompakt lista på namn
        {
            string returnString;

            returnString = String.Format("\nNamn:\t\t{0}\nEfternamn:\t{1}\nMedlemsnummer\t{2}\nAntal båtar:\t{3}", _firstName, _lastName, _memberNumber, _numberOfBoats);

            return returnString;
        }



    
    }
}
