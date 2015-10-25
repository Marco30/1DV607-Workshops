using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Club.Model
{
    class BoatCatalog
    {
        private Boat[] boats = new Boat[100];
        private MemberCatalog membC;
        private BoatDAL boatDAL;
        

        public BoatCatalog(MemberCatalog mc) 
        {
            this.membC = mc;
            this.boatDAL = new BoatDAL(this.membC);
        }

        // Lägger till en hel katalog i båtkatalogen.
        public void AddFullCatalog()
        {
            this.boats = boatDAL.GetBoats();
        }

        // Hämtar alla båtar från katalogen.
        public Boat[] GetBoats()
        {
            return this.boats;
        }

        // Lägger till en båt i katalogen.
        public void AddBoat(int type, int length, Member ownedBy)
        {
            Boat boatToAdd = new Boat(type, length, ownedBy);

            for (int i = 0; i < this.boats.Length; i++)
            {
                if (this.boats[i] == null)
                {
                    this.boats[i] = boatToAdd;
                    break;
                }
            }
        }

        // Tar bort en båt från katalogen.
        public void DeleteBoat(Member m, int boatToDelete)
        {
            int btd = 1;
            Boat[] boatsTemporary = new Boat[100];
            for (int i = 0; i < this.boats.Length; i++)
            {
                if (this.boats[i] != null && this.boats[i].OwnedBy == m)
                {
                    if (btd == boatToDelete)
                    {
                        boatsTemporary = this.boats.Where(w => w != this.boats[i]).ToArray();
                        break;
                    }
                    btd++;
                }
            }

            this.boats = new Boat[100];
            for (int i = 0; i < boatsTemporary.Length; i++)
            {
                this.boats[i] = boatsTemporary[i];
            }
        }

        // Tar bort alla båtar av en medlem från katalogen.
        public void DeleteBoats(Member m)
        {
            while (CountBoatsByMember(m) > 0)
            {
                DeleteBoat(m, 1);
            }
        }

        // Räknar antal båtar en medlem äger.
        public int CountBoatsByMember(Member m)
        {
            int counter = 0;
            for (int i = 0; i < this.boats.Length; i++)
            {
                if (this.boats[i] != null && this.boats[i].OwnedBy == m)
                {
                    counter++;
                }
            }
            return counter;
        }

        // Ändrar en båt.
        public void EditBoat(object[] boatEditInfo, Member memberToEditBoatTo, int boatToEdit)
        {
            Boat boatEdit = GetBoatByMemberAndIndex(memberToEditBoatTo, boatToEdit);
            if ((int)boatEditInfo[0] == 1)
            {
                boatEdit.OwnedBy = membC.GetMemberByMemberNumber((int)boatEditInfo[1]);
            }
            else if ((int)boatEditInfo[0] == 2)
            {
                boatEdit.Type = (int)boatEditInfo[1];
            }
            else
            {
                boatEdit.Length = (int)boatEditInfo[1];
            }
        }

        // Ändrar medlemsnumret på medlemmens båtar.
        public void EditBoatsMemberNumber(Member m, int newMemberNumber)
        {
            for (int i = 0; i < this.boats.Length; i++)
            {
                if (this.boats[i] != null && this.boats[i].OwnedBy == m)
                {
                    this.boats[i].OwnedBy.MemberNumber = newMemberNumber;
                }
            }
        }

        // Hämtar en båt via medlemsnummer och index.
        public Boat GetBoatByMemberAndIndex(Member m, int boatIndex)
        {
            int bi = 1;
            for (int i = 0; i < this.boats.Length; i++)
            {
                if (this.boats[i] != null && this.boats[i].OwnedBy == m)
                {
                    if (bi == boatIndex)
                    {
                        return this.boats[i];
                    }
                    bi++;
                }
            }
            return null;
        }

        public void SaveCatalog()
        {
            boatDAL.SaveBoats(boats);
        }
    }
}
