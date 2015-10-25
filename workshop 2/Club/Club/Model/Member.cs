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

        public Member(string fName, string lName, int mNumber, int sNumber)
        {
            this._firstName = fName;
            this._lastName = lName;
            this._socialSecurityNumber = sNumber;

            if (mNumber == 0)
            {
                CreateMemberNumber();
            }
            else
            {
                this._memberNumber = mNumber;
            }
        }

        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
            }
        }

        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
            }
        }

        public int MemberNumber
        {
            get { return _memberNumber; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Inte ett tillåtet nummer.");
                }
                else
                {
                    _memberNumber = value;
                }
            }
        }

        public int SocialSecurityNumber
        {
            get { return _socialSecurityNumber; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Inte ett tillåtet nummer.");
                }
                else
                {
                    _socialSecurityNumber = value;
                }
            }
        }

        // Skapar ett slumpat medlemsnummer.
        public void CreateMemberNumber()
        {
            Random random = new Random();
            int randomNumber = random.Next(10000, 99999);

            this._memberNumber = randomNumber;
        }
    }
}
