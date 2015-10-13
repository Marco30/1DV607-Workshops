using Club.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Club.View
{
    class ViewMessage
    {
        private MemberCatalog membCatalog;
        private BoatCatalog boatCatalog;

        public ViewMessage(MemberCatalog mCata, BoatCatalog bCata)
        {
            this.membCatalog = mCata;
            this.boatCatalog = bCata;
        }

        // Funktion som skriver ut felmeddelanden i konsolen.
        public void ShowErrorMessage(string message, bool error)// funktion som visar fel Meddelande
        {
            if (error)
            {
                Console.BackgroundColor = ConsoleColor.Red;
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.DarkGreen;
            }

            Console.WriteLine(message);
            Console.ResetColor();
        }
        
        // Funktion som skriver ut meddelanden i konsolen. 
        public void ShowMessage(string message)// funktion som visar Meddelande
        {
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        // Läser in från konsolen.
        public string ReadInput()
        {
            return Console.ReadLine();
        }

        // Tömmer konsolen.
        public void ClearConsole()
        {
            Console.Clear();
        }

        // Läser in information vid registrering av medlem.
        public string[] RegisterMember()
        {
            ClearConsole();
            ShowMessage("[ REGISTRERA MEDLEM ]\n");
            ShowMessage("Fyll i förnamn:");

            string firstName;
            do
            {
                firstName = ReadInput();
                if (firstName.Length <= 0)
                {
                    ShowErrorMessage("Ange ett förnamn.", true);
                }
            } while (firstName.Length <= 0);

            ShowMessage("Fyll i efternamn:");
            string lastName;
            do
            {
                lastName = ReadInput();
                if (lastName.Length <= 0)
                {
                    ShowErrorMessage("Ange ett efternamn.", true);
                }
            } while (lastName.Length <= 0);

            ShowMessage("Fyll i personnummer (ÅÅÅÅMMDD):");
            int securityNumber;
            bool securityNumberResult = false;
            do
            {
                securityNumberResult = int.TryParse(ReadInput(), out securityNumber);
                if (!securityNumberResult || !membCatalog.IsSocialSecurityNumberCorrect(securityNumber))
                {
                    ShowErrorMessage("Ange ett korrekt personnummer.", true);
                }
            } while (securityNumberResult == false || !membCatalog.IsSocialSecurityNumberCorrect(securityNumber));

            string[] returnArr = new string[3];
            returnArr[0] = firstName;
            returnArr[1] = lastName;
            returnArr[2] = securityNumber.ToString();

            return returnArr;
        }

        // Visar lista med medlemmar.
        public void ViewMembersList()
        {
            ClearConsole();
            ShowMessage("[ MEDLEMSLISTA ]\n");

            Member[] membersList = this.membCatalog.GetMembers();
            Boat[] boatsList = this.boatCatalog.GetBoats();

            if (this.membCatalog.CountMembers() == 0)
            {
                ShowErrorMessage("Inga medlemmar registrerade.", true);
                ShowMessage("Tryck på valfri knapp för att gå vidare.");
                ReadInput();
            }
            else
            {
                ShowMessage("Välj typ av lista:\n1. Kompakt lista\n2. Fullständing lista");
                bool validNumber = false;
                int number;
                do
                {
                    validNumber = int.TryParse(ReadInput(), out number);
                    if (!(number == 1 || number == 2))
                    {
                        ShowErrorMessage("Välj ett nummer från listan.", true);
                    }
                } while (!(number == 1 || number == 2));

                ClearConsole();

                if (number == 1)
                {
                    ShowMessage("[ KOMAPKT LISTA ]");

                    for (int i = 0; i < membersList.Length; i++)
                    {
                        if (membersList[i] != null)
                        {
                            ShowMessage(membersList[i].PrintCompact());
                            ShowMessage(String.Format("Antal båtar:\t{0}", this.boatCatalog.CountBoatsByMemberNumber(membersList[i].MemberNumber).ToString()));
                        }
                    }
                }
                else
                {
                    ShowMessage("[ FULLSTÄNDIG LISTA ]");

                    for (int i = 0; i < membersList.Length; i++)
                    {
                        if (membersList[i] != null)
                        {
                            ShowMessage(membersList[i].PrintFull());

                            for (int j = 0; j < boatsList.Length; j++)
                            {
                                if (boatsList[j] != null && boatsList[j].OwnedBy == membersList[i].MemberNumber)
                                {
                                    ShowMessage(String.Format("\nBåttyp:\t\t{0}\nLängd:\t\t{1}", boatsList[j].GetBoatType(), boatsList[j].Length));
                                }
                            }
                            ShowMessage("\n");
                        }
                    }
                }

                ShowMessage("\nTryck valfri knapp för att gå vidare.");
                ReadInput();
            }
        }

        // Visar information om en medlem.
        public void ViewOneMember()
        {
            ClearConsole();

            ShowMessage("[ MEDLEMSINFORMATION ]\n");
            ListAllMembers();
            ShowMessage("\nVälj en medlem genom att skriva in ett medlemsnummer (numret inuti parentesen).");

            int number = ChooseMember();

            ClearConsole();
            ShowMessage("[ MEDLEMSINFORMATION ]");
            ShowMessage(this.membCatalog.GetMemberByMemberNumber(number).PrintFull());

            ShowMessage("\nTryck på valfri knapp för att gå vidare.");
            ReadInput();
        }

        // Visar alla medlemmar som finns registrerade.
        public void ListAllMembers()
        {
            Member[] membersList = this.membCatalog.GetMembers();
            Boat[] boatsList = this.boatCatalog.GetBoats();

            if (this.membCatalog.CountMembers() == 0)
            {
                ShowErrorMessage("Inga medlemmar registrerade.", true);
            }
            else
            {
                for (int i = 0; i < membersList.Length; i++)
                {
                    
                    if (membersList[i] != null)
                    {
                        ShowMessage(membersList[i].PrintListInfo());
                    }
                }
            }
        }


        public void ListAllMembersWithBoat()
        {
            Member[] membersList = this.membCatalog.GetMembers();
            Boat[] boatsList = this.boatCatalog.GetBoats();



            if (this.membCatalog.CountMembers() == 0)
            {
                ShowErrorMessage("Inga medlemmar registrerade.", true);
            }
            else
            {

                List<int> termsList1 = new List<int>();
                List<int> termsList2 = new List<int>();

                bool lika = false;

                for (int i = 0; i < boatsList.Length; i++)
                {
                    if (boatsList[i] != null)
                    {
                        termsList1.Add(boatsList[i].OwnedBy);
                    }
                }

                foreach (int number in termsList1)
                {
                    if (!termsList2.Contains(number))
                    {
                        termsList2.Add(number);
                    }
                }


           
                        for (int M = 0; M < membersList.Length; M++)
                        {


                            if (membersList[M] != null)
                            {


                                    foreach (int number in termsList2)
                                    {
                                        if (number == membersList[M].MemberNumber) // Will match once.
                                        {
                                            lika = true;
                                        }
                                    }

                                    if (lika == true)
                                    {
                                        ShowMessage(membersList[M].PrintListInfo());          
                                    }

                                    lika = false;

                            }

                        }
                    
                
            }
        }

        // Visar alla båtar som en medlem äger.
        public int ListAllBoatsByMember(int memberNumber, string title)
        {
            if (this.boatCatalog.CountBoatsByMemberNumber(memberNumber) == 0)
            {
                ShowErrorMessage("Inga båtar finns registrerade för vald medlem.", true);
                return 0;
            }
            else
            {
                return ListBoatsByMemberNumber(memberNumber, title);
            }
        }

        // Läser in information vid redigering av medlem.
        public string[] ViewEditMember()
        {
            ClearConsole();
            ShowMessage("[ REDIGERA MEDLEM ]\n");
            ListAllMembers();
            ShowMessage("\nVälj en medlem genom att skriva in ett medlemsnummer (numret inuti parentesen).");

            int number = ChooseMember();

            ClearConsole();

            ShowMessage(String.Format("[ REDIGERA '{0} {1}' ]", this.membCatalog.GetMemberByMemberNumber(number).FirstName, this.membCatalog.GetMemberByMemberNumber(number).LastName));
            ShowMessage(this.membCatalog.GetMemberByMemberNumber(number).PrintFullWithNumberList());
            ShowMessage("\nVälj i listan vilken egenskap som skall ändras.");

            int selectedAlternative;
            do
            {
                int.TryParse(ReadInput(), out selectedAlternative);
                if (!(selectedAlternative >= 1 && selectedAlternative <= 4))
                {
                    ShowErrorMessage("Välj en egenskap i listan.", true);
                }
            } while (!(selectedAlternative >= 1 && selectedAlternative <= 4));

            
            int inputInteger;
            bool inputIntegerResult;
            string[] returnArr = new string[3];
            returnArr[0] = number.ToString();
            returnArr[1] = selectedAlternative.ToString();
            if (selectedAlternative == 1)
            {
                ShowMessage("Ändra förnamn till: ");
                do
                {
                    returnArr[2] = ReadInput();
                    if (returnArr[2].Length <= 0)
                    {
                        ShowErrorMessage("Ange ett förnamn.", true);
                    }
                } while (returnArr[2].Length <= 0);
            }
            else if (selectedAlternative == 2)
            {
                ShowMessage("Ändra efternamn till: ");
                do
                {
                    returnArr[2] = ReadInput();
                    if (returnArr[2].Length <= 0)
                    {
                        ShowErrorMessage("Ange ett efternamn.", true);
                    }
                } while (returnArr[2].Length <= 0);
            }
            else if (selectedAlternative == 3)
            {
                ShowMessage("Ändra personnummer till (ÅÅÅÅMMDD): ");
                do
                {
                    inputIntegerResult = int.TryParse(ReadInput(), out inputInteger);
                    if (!inputIntegerResult || !membCatalog.IsSocialSecurityNumberCorrect(inputInteger))
                    {
                        ShowErrorMessage("Ange ett giltligt personnummer.", true);
                    }
                } while (!inputIntegerResult || !membCatalog.IsSocialSecurityNumberCorrect(inputInteger));
                returnArr[2] = inputInteger.ToString();
            }
            else
            {
                ShowMessage("Ändra medlemsnummer till: ");
                do
                {
                    inputIntegerResult = int.TryParse(ReadInput(), out inputInteger);
                    if (!inputIntegerResult)
                    {
                        ShowErrorMessage("Ange ett giltligt medlemsnummer.", true);
                    }
                    if (membCatalog.IsMemberNumberCorrect(inputInteger))
                    {
                        ShowErrorMessage("Medlemsnumret är upptaget.", true);
                    }
                } while (!inputIntegerResult || membCatalog.IsMemberNumberCorrect(inputInteger));
                returnArr[2] = inputInteger.ToString();
            }
            
            return returnArr;
        }

        // Läser in information vid radering av medlem.
        public int ViewDeleteMember()
        {
            ClearConsole();
            ShowMessage("[ RADERA MEDLEM ]\n");
            ListAllMembers();
            ShowMessage("\nVälj en medlem genom att skriva in ett medlemsnummer (numret inuti parentesen).");
            return ChooseMember();
        }

        // Läser in information vid val av medlem.
        public int ChooseMember()
        {
            bool validNumber = false;
            int number;
            do
            {
                validNumber = int.TryParse(ReadInput(), out number);
                if (!this.membCatalog.IsMemberNumberCorrect(number))
                {
                    ShowErrorMessage("Välj en giltlig medlem.", true);
                }
            } while (!this.membCatalog.IsMemberNumberCorrect(number));

            return number;
        }

        // Visar information vid registering av medlem.
        public int ViewAddBoatByMember()
        {
            ClearConsole();
            ShowMessage("[ REGISTRERA BÅT ]\n");
            ListAllMembers();
            ShowMessage("\nVälj en medlem genom att skriva in ett medlemsnummer (numret inuti parentesen).");

            return ChooseMember();
        }

        // Läser in information vid registering av båt.
        public int[] RegisterBoat()
        {
            ClearConsole();
            ShowMessage("[ REGISTRERA BÅT ]\n");
            ShowMessage("Fyll i längd: ");
            int securityNumber;
            bool securityNumberResult = false;
            do
            {
                securityNumberResult = int.TryParse(ReadInput(), out securityNumber);
                if (securityNumberResult == false || securityNumber <= 0)
                {
                    ShowErrorMessage("Ange en giltlig längd.", true);
                }
            } while (securityNumberResult == false || securityNumber <= 0);
            int length = securityNumber;

            ShowMessage(ViewBoatTypes());
            ShowMessage("Välj båttyp av alternativen: ");
            securityNumberResult = false;
            do
            {
                securityNumberResult = int.TryParse(ReadInput(), out securityNumber);
                if (securityNumberResult == false || !(securityNumber >= 1 && securityNumber <=5))
                {
                    ShowErrorMessage("Ange en båttyp från listan.", true);
                }
            } while (securityNumberResult == false || !(securityNumber >= 1 && securityNumber <=5));
            int type = securityNumber;

            int[] returnArr = new int[2];
            returnArr[0] = length;
            returnArr[1] = type;

            return returnArr;
        }

        // Läser in information vid radering av båt.
        public int ViewDeleteBoatByMember(string title)
        {
            ClearConsole();
            ShowMessage(String.Format("[ {0} ]\n", title));
            //ListAllMembers();
            ListAllMembersWithBoat();
            ShowMessage("\nVälj en medlem genom att skriva in ett medlemsnummer (numret inuti parentesen).");

            return ChooseMember();
        }

        // Visar lista med båtar som en medlem äger.
        public int ListBoatsByMemberNumber(int memberNumber, string title)
        {
            ClearConsole();
            ShowMessage(String.Format("[ {0} ]\n", title));
            Boat[] boatsList = this.boatCatalog.GetBoats();
            int listOfBoatsCounter = 0;
            for (int i = 0; i < boatsList.Length; i++)
            {
                if (boatsList[i] != null && boatsList[i].OwnedBy == memberNumber)
                {
                    listOfBoatsCounter++;
                    ShowMessage(String.Format("{0}. {1} {2}m", listOfBoatsCounter, GetBoatTypeString(boatsList[i].Type), boatsList[i].Length));
                    
                }
            }

            ShowMessage("\nVälj i listan vilken båt som skall redigeras.");

            bool validNumber = false;
            int number;
            do
            {
                validNumber = int.TryParse(ReadInput(), out number);
                if (!(number >= 1 && number <= listOfBoatsCounter))
                {
                    ShowErrorMessage("Välj en båt från listan.", true);
                }
            } while (!(number >= 1 && number <= listOfBoatsCounter));

            return number;
        }

        // Läser in information vid redigering av båt.
        public int[] ViewEditBoat(int memberNumber, int boatToEdit)
        {
            ClearConsole();
            ShowMessage("[ REDIGERA BÅT ]");
            ShowMessage(this.boatCatalog.GetBoatByMemberNumberAndIndex(memberNumber,boatToEdit).PrintFullWithNumberList());
            ShowMessage("\nVälj i listan vilken egenskap som skall ändras.");

            int selectedAlternative;
            do
            {
                int.TryParse(ReadInput(), out selectedAlternative);
                if (!(selectedAlternative >= 1 && selectedAlternative <= 3))
                {
                    ShowErrorMessage("Välj ett alternativ från listan.", true);
                }
            } while (!(selectedAlternative >= 1 && selectedAlternative <= 3));

            ClearConsole();
            ShowMessage("[ REDIGERA BÅT ]\n");
            int inputInteger;
            bool inputIntegerResult = false;
            int[] returnArr = new int[2];
            returnArr[0] = selectedAlternative;
            if (selectedAlternative == 1)
            {
                ListAllMembers();
                ShowMessage("\nÄndra ägare till: ");
                returnArr[1] = ChooseMember();

                inputIntegerResult = false;
            }
            else if (selectedAlternative == 2)
            {
                ShowMessage(ViewBoatTypes());
                ShowMessage("Ändra typ till: ");
                inputIntegerResult = false;
                do
                {
                    inputIntegerResult = int.TryParse(ReadInput(), out inputInteger);
                    if (!inputIntegerResult || inputInteger < 1 || inputInteger > 5)
                    {
                        ShowErrorMessage("Välj ett alternativ från listan.", true);
                    }
                } while (!inputIntegerResult || inputInteger < 1 || inputInteger > 5);
                returnArr[1] = inputInteger;
                
            }
            else
            {
                ShowMessage("Ändra längd till: ");
                do
                {
                    inputIntegerResult = int.TryParse(ReadInput(), out inputInteger);
                    if (!inputIntegerResult || inputInteger < 1)
                    {
                        ShowErrorMessage("Ange en giltlig längd.", true);
                    }
                } while (!inputIntegerResult || inputInteger < 1);
                returnArr[1] = inputInteger;
                inputIntegerResult = false;
            }

            return returnArr;
        }

        // Visar lista med båttyper.
        public string ViewBoatTypes()
        {
            return "1. Motorbåt\n2. Segelbåt\n3. Roddbåt\n4. Kanot\n5. Övrigt";
        }

        // Returnerar båttyp.
        public string GetBoatTypeString(int type)
        {
            if (type == 1)
            {
                return "Motorbåt";
            }
            else if (type == 2)
            {
                return "Segelbåt";
            }
            else if (type == 3)
            {
                return "Roddbåt";
            }
            else if (type == 4)
            {
                return "Kanot";
            }
            else
            {
                return "Övrigt";
            }
        }

        public void RegisteredMemberResponse()
        {
            ClearConsole();
            ShowMessage("[ REGISTRERA MEDLEM ]\n");
            ShowMessage("Medlem registrerad. Tryck på valfri knapp för att gå vidare.");
            ReadInput();
        }

        public void MemberNumberIsTakenResponse()
        {
            ShowErrorMessage("Medlemsnumret är upptaget.", true);
            ShowMessage("Tryck på valfri knapp för att gå vidare.");
            ReadInput();
        }

        public void MemberNumberDoesNotExistResponse()
        {
            ShowErrorMessage("Medlemsnumret existerar inte.", true);
            ShowMessage("Tryck på valfri knapp för att gå vidare.");
            ReadInput();
        }

        public void ResponseMessage(string title, string message)
        {
            ClearConsole();
            ShowMessage(String.Format("[ {0} ]\n", title));
            ShowMessage(String.Format("{0}. Tryck på valfri knapp för att gå vidare.", message));
            ReadInput();
        }
    }
}
