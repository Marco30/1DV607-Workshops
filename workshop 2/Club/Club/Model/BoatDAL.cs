using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Club.Model
{
    class BoatDAL
    {
        private string path = AppDomain.CurrentDomain.BaseDirectory;

        // Hämtar alla båtar från en textfil.
        public Boat[] GetBoats()
        {
            Boat[] allBoats = new Boat[100];
            string boatsPath = path + "/../../Boats.txt";
            string[] lines = System.IO.File.ReadAllLines(boatsPath);
            int count = 1;

            int type = 0;
            int length = 0;
            int ownedBy = 0;

            foreach (string line in lines)
            {
                if (count == 1)
                {
                    ownedBy = Int32.Parse(line);
                }

                if (count == 2)
                {
                    length = Int32.Parse(line);
                }

                if (count == 3)
                {
                    count = 0;
                    type = Int32.Parse(line);
                    Boat newBoat = new Boat(type, length, ownedBy);
                    allBoats = AddBoatToArray(allBoats, newBoat);
                }

                count++;
            }
            return allBoats;
        }

        // Sparar alla båtar till en textfil.
        public void SaveBoats(Boat[] boats)
        {
            Boat[] boatslist = boats;
            int i = 0;
            string[] lines = new string[400];

            foreach (Boat bo in boatslist)
            {
                if (bo != null)
                {
                    lines[i] = bo.OwnedBy.ToString();
                    i++;
                    lines[i] = bo.Length.ToString();
                    i++;
                    lines[i] = bo.Type.ToString();
                    i++;
                }
            }

            lines = lines.Where(h => !string.IsNullOrEmpty(h)).ToArray();

            string boatsPath = path + "/../../Boats.txt";
            System.IO.File.WriteAllLines(boatsPath, lines);
        }

        // Lägger till en båt i katalogen vid hämtning av båtar från en textfil.
        public Boat[] AddBoatToArray(Boat[] boatsArray, Boat boatToAdd)
        {
            for (int i = 0; i < boatsArray.Length; i++)
            {
                if (boatsArray[i] == null)
                {
                    boatsArray[i] = boatToAdd;
                    return boatsArray;
                }
            }
            return boatsArray;
        }
    }
}
