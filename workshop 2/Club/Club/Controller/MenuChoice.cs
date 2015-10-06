using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Club.Model;
using Club.Controller;
using Club.View;
namespace Club.Controller
{
    class MenuChoice
    {
        public void GetMenuChoice()
        {
            // Skapa View och Controller.
            ViewMessage V = new ViewMessage();
            ClubData Handler = new ClubData();
            MenuView menu = new MenuView();

            // Deklarerar arrayer för medlemmar och båtar (100 platser var).
            Member[] members = new Member[100];
            Boat[] boats = new Boat[100];

            // Läser in båtar och medlemmar från textfiler.
            boats = Handler.readBoats(boats);
            members = Handler.readMembers(members);

            // Räknar ihop antal medlemmar och båtar.
            int arrayPosition = members.Count(s => s != null);
            int boatsPosition = boats.Count(z => z != null);

            // Deklaration av variabel som kommer innehålla valet man gjort i menyn.
            int choice = 12;

            do
            {
                // Kör funktionen som visar menyn.
                menu.ViewMenu(); 

                try
                {
                    // Läser in menyval.
                    choice = int.Parse(Console.ReadLine());

                    // Kontrollerar att man gjort ett val i menyn som är mellan 0-8, om man gjort det så kommer inga felmeddelanden att visas.
                    if (choice < 0 && choice > 8)
                    {
                        V.ShowErrorMessage("FEL! Ange ett nummer mellan 0 och 8", true);
                    }
                }
                catch
                {
                    V.ShowErrorMessage("FEL! Ange ett nummer mellan 0 och 8", true);
                }

                switch (choice)
                {
                    case 0:
                        continue;

                    case 1: // Skapar ny medlem.

                        members[arrayPosition] = Handler.AddMember();// kallar på funktionen som lägger till medlem     
                        arrayPosition++;
                        break;

                    case 2: // Presenterar lista med medlemmar.

                        Handler.MembersList(members, arrayPosition, boats, boatsPosition);// kallar på funktionen som listar medlemar 
                        break;

                    case 3: // Presenterar detaljerad information om en medlem.

                        // Presenterar felmeddelande om inga medlemmar finns registrerade.
                        if (arrayPosition == 0)
                        {
                            V.ShowErrorMessage("Inga medlemmar finns ännu", true);
                            break;
                        }

                        // Hämtar medlemmens position i arrayen.
                        V.ShowErrorMessage("Vilken medlem vill du ha information om?", false);
                        int whatMember = Handler.GetMemberNumber();

                        // Om en korrekt medlem är vald, presenteras info om medlemmen.
                        if (whatMember > 0 && (whatMember <= arrayPosition))
                        {
                            Handler.GetMemberData(members, whatMember);
                        }
                        break;

                    case 4: // Ändrar en medlem.

                        // Hämtar medlemmens position i arrayen.
                        V.ShowErrorMessage("Vilken medlem vill du redigera?", false);
                        int whichMember = Handler.GetMemberNumber();

                        // Om en korrekt medlem är vald, presenteras info om medlemmen.
                        if (whichMember > 0 && (whichMember <= arrayPosition))
                        {
                            Handler.EditMemberData(members, whichMember);
                        }
                        break;

                    case 5: // Ta bort en medlem.

                        // Hämtar medlemmens position i arrayen.
                        V.ShowErrorMessage("Vilken medlem vill du radera?", false);
                        int whoMember = Handler.GetMemberNumber();

                        // Om en korrekt medlem är vald, raderas medlemmen.
                        if (whoMember > 0 && (whoMember <= arrayPosition))
                        {
                            members = Handler.DeleteMember(members, whoMember);
                            arrayPosition--;
                        }
                        break;

                    case 6: // Lägger till en båt.

                        boats[boatsPosition] = Handler.AddBoat(members, arrayPosition);
                        boatsPosition++;
                        break;

                    case 7: // Tar bort en båt.

                        V.ShowErrorMessage("Vilken båt vill du radera?", false);

                        int[] memberAndBoatDelete = Handler.GetBoatNumberAndMemberNumber(members, arrayPosition, boats, boatsPosition);        // Hittar rätt nummer

                        if (memberAndBoatDelete[0] != -1)
                        {
                            int whatOwner = memberAndBoatDelete[0];
                            int whatBoatOfOwner = memberAndBoatDelete[1];
                            int whatBoatOfOwnerInArray = Handler.readBoatsPosByMember(members, whatOwner, boats, whatBoatOfOwner);

                            // Om en korrekt båt är vald, raderas båten.
                            if (whatBoatOfOwnerInArray > 0 && (whatBoatOfOwnerInArray <= boatsPosition))
                            {
                                boats = Handler.DeleteBoat(boats, whatBoatOfOwnerInArray);
                                boatsPosition--;
                            }
                            
                        }
                        break;
                        

                    case 8: // Redigerar en båt.
                        V.ShowErrorMessage("Vilken båt vill du redigera?", false);
                        int[] memberAndBoat = Handler.GetBoatNumberAndMemberNumber(members, arrayPosition, boats, boatsPosition);
                        int memberNumber = memberAndBoat[0];
                        int whichBoat = memberAndBoat[1];

                        // Om en korrekt båt är vald, tillåts användaren ändra båten.
                        if (whichBoat >= 0 && (whichBoat <= arrayPosition))
                        {
                            int boatPos = Handler.readBoatsPosByMember(members, memberNumber, boats, whichBoat);
                            Handler.EditBoatData(boats, boatPos);
                        }
                        break;
                }
            } while (choice != 0);
               
            // Sparar förändringar.
            Handler.saveMembers(members, arrayPosition);
            Handler.saveBoats(boats, boatsPosition);
        }
    }
}
