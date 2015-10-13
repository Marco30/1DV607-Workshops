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
            // Skapar Views och Models.
            MenuView startMenu = new MenuView();
            MemberCatalog membCatalog = new MemberCatalog();
            BoatCatalog boatCatalog = new BoatCatalog();
            ViewMessage v = new ViewMessage(membCatalog, boatCatalog);

            // Läser in båtar och medlemmar från textfiler.
            boatCatalog.AddFullCatalog();
            membCatalog.AddFullCatalog();

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

                        string[] editArray = v.ViewEditMember();

                        if (int.Parse(editArray[1]) == 1)
                        {
                            membCatalog.GetMemberByMemberNumber(int.Parse(editArray[0])).FirstName = editArray[2];
                        }
                        else if (int.Parse(editArray[1]) == 2)
                        {
                            membCatalog.GetMemberByMemberNumber(int.Parse(editArray[0])).LastName = editArray[2];
                        }
                        else if (int.Parse(editArray[1]) == 3)
                        {
                            membCatalog.GetMemberByMemberNumber(int.Parse(editArray[0])).SocialSecurityNumber = int.Parse(editArray[2]);
                        }
                        else
                        {
                            if (!membCatalog.IsMemberNumberCorrect(int.Parse(editArray[2])))
                            {
                                membCatalog.GetMemberByMemberNumber(int.Parse(editArray[0])).MemberNumber = int.Parse(editArray[2]);
                                boatCatalog.EditBoatsMemberNumber(int.Parse(editArray[0]), int.Parse(editArray[2]));
                            }
                            else
                            {
                                v.MemberNumberIsTakenResponse();
                            }
                        }
                        break;

                    case 5: // Ta bort en medlem.

                        int memberToDelete = v.ViewDeleteMember();
                        membCatalog.DeleteMemberByMemberNumber(memberToDelete);
                        boatCatalog.DeleteBoats(memberToDelete);
                        v.ResponseMessage("RADERA MEDLEM", "Medlem raderad");
                        break;

                    case 6: // Lägger till en båt.

                        int memberToAddBoatTo = v.ViewAddBoatByMember();
                        int[] regArray = v.RegisterBoat();

                        boatCatalog.AddBoat(regArray[1], regArray[0], memberToAddBoatTo);
                        v.ResponseMessage("REGISTRERA BÅT", "Båt registrerad");

                        break;

                    case 7: // Tar bort en båt.

                        int memberToRemoveBoatTo = v.ViewDeleteBoatByMember("RADERA BÅT");
                        int boatToRemove = v.ListAllBoatsByMember(memberToRemoveBoatTo, "RADERA BÅT");
                        boatCatalog.DeleteBoat(memberToRemoveBoatTo, boatToRemove);
                        v.ResponseMessage("RADERA BÅT", "Båt raderad");
                        break;
                        

                    case 8: // Redigerar en båt.
                        int memberToEditBoatTo = v.ViewDeleteBoatByMember("REDIGERA BÅT");
                        int boatToEdit = v.ListAllBoatsByMember(memberToEditBoatTo, "REDIGERA BÅT");
                        int[] boatEditInfo = v.ViewEditBoat(memberToEditBoatTo, boatToEdit);

                        if (boatEditInfo[0] == 1 && !membCatalog.IsMemberNumberCorrect(boatEditInfo[1]))
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
