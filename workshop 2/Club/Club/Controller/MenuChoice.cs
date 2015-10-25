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
            // Skapar Views och Models. Läser in från textfiler.
            MenuView startMenu = new MenuView();
            MemberCatalog membCatalog = new MemberCatalog();
            membCatalog.AddFullCatalog();
            BoatCatalog boatCatalog = new BoatCatalog(membCatalog);
            boatCatalog.AddFullCatalog();
            ViewMessage v = new ViewMessage(membCatalog, boatCatalog);

            // Deklaration av variabel som kommer innehålla valet man gjort i menyn.
            int? choice = null;

            do
            {
                // Kör funktionen som visar menyn.
                startMenu.ViewMenu();

                int inputChoice;
                bool inputResult = int.TryParse(v.ReadInput(), out inputChoice);
                
                if (inputResult)
                {
                    choice = inputChoice;
                }

                // Väljer case utifrån användarens input (0-8).
                switch (choice)
                {
                    case 0:
                        continue;

                    case 1: // Skapar ny medlem.

                        string[] regArr = v.RegisterMember();
                        membCatalog.CreateNewMember(regArr);
                        v.RegisteredMemberResponse();
                        break;

                    case 2: // Presenterar lista med medlemmar.

                        v.ViewMembersList();
                        break;

                    case 3: // Presenterar detaljerad information om en medlem.

                        v.ViewOneMember();
                        break;

                    case 4: // Ändrar en medlem.

                        object[] editArray = v.ViewEditMember();

                        if ((int)editArray[1] == 1)
                        {
                            membCatalog.GetMemberByMemberNumber((int)editArray[0]).FirstName = editArray[2].ToString();
                        }
                        else if ((int)editArray[1] == 2)
                        {
                            membCatalog.GetMemberByMemberNumber((int)editArray[0]).LastName = editArray[2].ToString();
                        }
                        else if ((int)editArray[1] == 3)
                        {
                            membCatalog.GetMemberByMemberNumber((int)editArray[0]).SocialSecurityNumber = (int)editArray[2];
                        }
                        else
                        {
                            if (!membCatalog.IsMemberNumbCorrect((int)editArray[2]))
                            {
                                membCatalog.GetMemberByMemberNumber((int)editArray[0]).MemberNumber = (int)editArray[2];
                                boatCatalog.EditBoatsMemberNumber(membCatalog.GetMemberByMemberNumber((int)editArray[0]), (int)editArray[2]);
                            }
                            else
                            {
                                v.MemberNumberIsTakenResponse();
                            }
                        }
                        break;

                    case 5: // Ta bort en medlem.

                        Member memberToDelete = membCatalog.GetMemberByMemberNumber(v.ViewDeleteMember());
                        membCatalog.DeleteMember(memberToDelete);
                        boatCatalog.DeleteBoats(memberToDelete);
                        v.ResponseMessage("RADERA MEDLEM", "Medlem raderad");
                        break;

                    case 6: // Lägger till en båt.

                        Member memb = membCatalog.GetMemberByMemberNumber(v.ViewAddBoatByMember());
                        int[] regArray = v.RegisterBoat();
                        boatCatalog.AddBoat(regArray[1], regArray[0], memb);
                        v.ResponseMessage("REGISTRERA BÅT", "Båt registrerad");

                        break;

                    case 7: // Tar bort en båt.

                        Member memberToRemoveBoatTo = membCatalog.GetMemberByMemberNumber(v.ViewDeleteBoatByMember("RADERA BÅT"));
                        int boatToRemove = v.ListAllBoatsByMember(memberToRemoveBoatTo, "RADERA BÅT");
                        boatCatalog.DeleteBoat(memberToRemoveBoatTo, boatToRemove);
                        v.ResponseMessage("RADERA BÅT", "Båt raderad");
                        break;
                        

                    case 8: // Redigerar en båt.
                        // Hämta memb att redigera ifrån
                        Member memberToEditBoatTo = membCatalog.GetMemberByMemberNumber(v.ViewDeleteBoatByMember("REDIGERA BÅT"));
                        // Lista båtindex.
                        int boatToEdit = v.ListAllBoatsByMember(memberToEditBoatTo, "REDIGERA BÅT");

                        object[] boatEditInfo = v.ViewEditBoat(memberToEditBoatTo, boatToEdit);

                        if ((int)boatEditInfo[0] == 1 && !membCatalog.IsMemberNumbCorrect((int)boatEditInfo[1]))
                        {
                            v.MemberNumberDoesNotExistResponse();
                        }
                        else
                        {
                            boatCatalog.EditBoat(boatEditInfo, memberToEditBoatTo, boatToEdit);
                            v.ResponseMessage("REDIGERA BÅT", "Båt redigerad");
                        }
                        break;
                }

                v.ClearConsole();
            } while (choice != 0);
               
            // Sparar förändringar.
            membCatalog.SaveCatalog();
            boatCatalog.SaveCatalog();
        }
    }
}
