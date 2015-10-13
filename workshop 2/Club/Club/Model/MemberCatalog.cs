using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Club.Model
{
    class MemberCatalog
    {
        private Member[] members = new Member[100];
        MemberDAL membDAL = new MemberDAL();

        // Lägger till en hel katalog i medlemskatalogen.
        public void AddFullCatalog()
        {
            this.members = membDAL.GetMembers();
        }

        // Hämtar alla medlemmar från katalogen.
        public Member[] GetMembers()
        {
            return this.members;
        }

        public void CreateNewMember(string[] regArr)
        {
            Member newMemb;
            do
            {
                newMemb = new Member(regArr[0], regArr[1], 0, int.Parse(regArr[2]));
            } while (IsMemberNumberValid(newMemb) == false);

            AddMember(newMemb);
        }

        // Lägger till en medlem i katalogen
        public void AddMember(Member memberToAdd)
        {
            for (int i = 0; i < this.members.Length; i++)
            {
                if (this.members[i] == null)
                {
                    this.members[i] = memberToAdd;
                    break;
                }
            }
        }

        // Kontrollerar om ett medlemsnummer är upptaget.
        public bool IsMemberNumberValid(Member memberToAdd)
        {
            for (int i = 0; i < this.members.Length; i++)
            {
                if (this.members[i] != null)
                {
                    if (this.members[i].MemberNumber == memberToAdd.MemberNumber)
                    {
                        return false;
                    }
                }
            }
            return true;
        } 

        // Kontrollerar om ett medlemsnummer redan används.
        public bool IsMemberNumberCorrect(int number)
        {
            for (int i = 0; i < this.members.Length; i++)
            {
                if (this.members[i] != null)
                {
                    if (this.members[i].MemberNumber == number)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        // Räknar antal medlemmar.
        public int CountMembers()
        {
            int counter = 0;
            for (int i = 0; i < this.members.Length; i++)
            {
                if (this.members[i] != null)
                {
                    counter++;
                }
            }
            return counter;
        }

        // Hämtar en medlem via ett medlemsnummer.
        public Member GetMemberByMemberNumber(int number)
        {
            for (int i = 0; i < this.members.Length; i++)
            {
                if (this.members[i] != null)
                {
                    if (this.members[i].MemberNumber == number)
                    {
                        return this.members[i];
                    }
                }
            }
            return null;
        }

        // Tar bort en medlem från katalogen via ett medlemsnummer.
        public void DeleteMemberByMemberNumber(int number)
        {
            Member[] membersTemporary = new Member[100];

            for (int i = 0; i < this.members.Length; i++)
            {
                if (this.members[i] != null)
                {
                    if (this.members[i].MemberNumber == number)
                    {
                        membersTemporary = members.Where(w => w != members[i]).ToArray();
                        break;
                    }
                }
            }

            members = new Member[100];
            for (int i = 0; i < membersTemporary.Length; i++)
            {
                members[i] = membersTemporary[i];
            }
        }

        // Kontrollerar om ett personnummer är ett giltligt personnummer.
        public bool IsSocialSecurityNumberCorrect(int number)
        {
            if (number.ToString().Length != 8)
            {
                return false;
            }

            string yr = number.ToString().Substring(0,4);
            string mon = number.ToString().Substring(4,2);
            string day = number.ToString().Substring(6,2);

            try
            {
                DateTime myDate = new DateTime(int.Parse(yr), int.Parse(mon), int.Parse(day));
            }
            catch
            {
                return false;
            }

            return true;
        }

        public void SaveCatalog()
        {
            membDAL.SaveMembers(members);
        }
    }
}
