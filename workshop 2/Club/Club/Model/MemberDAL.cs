using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Club.Model
{
    class MemberDAL
    {
        private string path = AppDomain.CurrentDomain.BaseDirectory;

        // Hämtar alla medlemmar från en textfil.
        public Member[] GetMembers()
        {
            Member[] allMembers = new Member[100];
            string membersPath = path + "/../../Members.txt";
            string[] lines = System.IO.File.ReadAllLines(membersPath);
            int count = 1;

            string firstName = "";
            string lastName = "";
            int membNumber = 0;
            int secNumber = 0;

            foreach (string line in lines)// foreach som löper igenom tex filen och skapar ett objekt som tilldelas värden från textfilen
            {
                if (count == 1)
                {
                    firstName = line;
                }

                if (count == 2)
                {
                    lastName = line;
                }

                if (count == 3)
                {
                    membNumber = Int32.Parse(line);
                }

                if (count == 4)
                {
                    count = 0;
                    secNumber = Int32.Parse(line);
                    Member newMember = new Member(firstName, lastName, membNumber, secNumber);
                    allMembers = AddMemberToArray(allMembers, newMember);
                }
                count++;
            }

            return allMembers;
        }

        // Sparar alla medlemmar till en textfil.
        public void SaveMembers(Member[] members)
        {
            Member[] membersList = members;
            int i = 0;
            string[] lines = new string[400];

            foreach (Member memb in membersList)
            {
                if (memb != null)
                {
                    lines[i] = memb.FirstName;
                    i++;
                    lines[i] = memb.LastName;
                    i++;
                    lines[i] = memb.MemberNumber.ToString();
                    i++;
                    lines[i] = memb.SocialSecurityNumber.ToString();
                    i++;
                }
            }

            lines = lines.Where(h => !string.IsNullOrEmpty(h)).ToArray();

            string membersPath = path + "/../../Members.txt";
            System.IO.File.WriteAllLines(membersPath, lines);
        }

        // Lägger till en medlem i katalogen vid hämtning av medlemmar från en textfil.
        public Member[] AddMemberToArray(Member[] membersArray, Member memberToAdd)
        {
            for (int i = 0; i < membersArray.Length; i++)
            {
                if (membersArray[i] == null)
                {
                    membersArray[i] = memberToAdd;
                    return membersArray;
                }
            }
            return membersArray;
        }
    }
}
