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
        BoatDAL boatDAL = new BoatDAL();

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
        public void AddBoat(int type, int length, int ownedBy)
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
        public void DeleteBoat(int memberNumber, int boatToDelete)
        {
            int btd = 1;
            Boat[] boatsTemporary = new Boat[100];
            for (int i = 0; i < this.boats.Length; i++)
            {
                if (this.boats[i] != null && this.boats[i].OwnedBy == memberNumber)
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
        public void DeleteBoats(int memberNumber)
        {
            while (CountBoatsByMemberNumber(memberNumber) > 0)
            {
                DeleteBoat(memberNumber, 1);
            }
        }

        // Räknar antal båtar en medlem äger.
        public int CountBoatsByMemberNumber(int memberNumber)
        {
            int counter = 0;
            for (int i = 0; i < this.boats.Length; i++)
            {
                if (this.boats[i] != null && this.boats[i].OwnedBy == memberNumber)
                {
                    counter++;
                }
            }
            return counter;
        }

        public void EditBoat(int[] boatEditInfo, int memberToEditBoatTo, int boatToEdit)
        {
            Boat boatEdit = GetBoatByMemberNumberAndIndex(memberToEditBoatTo, boatToEdit);
            if (boatEditInfo[0] == 1)
            {
                boatEdit.OwnedBy = boatEditInfo[1];
            }
            else if (boatEditInfo[0] == 2)
            {
                boatEdit.Type = boatEditInfo[1];
            }
            else
            {
                boatEdit.Length = boatEditInfo[1];
            }
        }

        // Ändrar medlemsnumret på medlemmens båtar.
        public void EditBoatsMemberNumber(int memberNumber, int newMemberNumber)
        {
            for (int i = 0; i < this.boats.Length; i++)
            {
                if (this.boats[i] != null && this.boats[i].OwnedBy == memberNumber)
                {
                    this.boats[i].OwnedBy = newMemberNumber;
                }
            }
        }

        // Hämtar en båt via medlemsnummer och index.
        public Boat GetBoatByMemberNumberAndIndex(int memberNumber, int boatIndex)
        {
            int bi = 1;
            for (int i = 0; i < this.boats.Length; i++)
            {
                if (this.boats[i] != null && this.boats[i].OwnedBy == memberNumber)
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
