using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Club.Model;
using Club.View;

namespace Club.Model
{
    class ClubData
    {
        // Sökväg till App-basen.
        private string path = AppDomain.CurrentDomain.BaseDirectory;
        /// <summary>
        /// Lägger till en medlem.
        /// </summary>
        /// <returns>Member-objekt med ny medlem.</returns>
        public Member AddMember()
        {
            string nameInput = "";
            bool continueBool = false;
            Member myMember = new Member();// skapar en ny medlems objekt, som kommer ha all information om den nya medlemmen

            ViewMessage V = new ViewMessage();

            do// lägger till förnamn i det nya medlems objektet
            {
                V.ShowMessage("Fyll i förnamn");         // Förnamn
                try
                {
                    nameInput = Console.ReadLine();
                }

                catch
                {
                    V.ShowErrorMessage("Fel format, försök igen", true);
                    continueBool = false;
                }


                myMember.FirstName = nameInput;
                continueBool = true;

            } while (!continueBool);

            do// lägger till efternam i det nya medlems objektet
            {
                continueBool = false;

                V.ShowMessage("Fyll i efternamn");           // Efternamn
                try
                {
                    nameInput = Console.ReadLine();
                }

                catch
                {
                    V.ShowErrorMessage("Fel format, försök igen", true);
                    continueBool = false;
                }


                myMember.LastName = nameInput;
                continueBool = true;

            } while (!continueBool);

            do// lägger till personnummer i det nya medlems objektet
            {
                continueBool = false;
                int numberInput = 0;

                V.ShowMessage("Fyll i personnumer");           // personnummer
                try
                {
                    numberInput = int.Parse(Console.ReadLine());
                    continueBool = true;
                }

                catch
                {
                    V.ShowErrorMessage("Fel format, försök igen", true);
                    continueBool = false;
                }



                myMember.SocialSecurityNumber = numberInput;


            } while (!continueBool);

            myMember.CreateMemberNumber();// skapar ett medlems nummer

            return myMember;
        }                       

        /// <summary>
        /// Lägger till en ny båt.
        /// </summary>
        /// <param name="members">Array med alla registrerade medlemmar.</param>
        /// <param name="arrayPos">Position i arrayen där båten ska läggas till</param>
        /// <returns></returns>
        public Boat AddBoat(Member[] members, int arrayPos)
        {

            ViewMessage V = new ViewMessage();

            int numberInput = 0;
            int memberNumber = 0;
            bool continueBool = false;
            Boat myBoat = new Boat();// skapar ett nytt båt objekt

            do// välj ägaren till båten som ska läggas till genom att fylla medlemsnummer
            {
                V.ShowMessage("Fyll i medlemsnumret på ägaren?");         // Medlemsnummer
                try
                {
                    numberInput = Int32.Parse(Console.ReadLine());
                }

                catch
                {
                    V.ShowErrorMessage("Fel format, försök ige.", true);
                    continueBool = false;
                }


                for (int i = 0; i < arrayPos; i++)
                {
                    memberNumber = members[i].MemberNumber;

                    if (memberNumber == numberInput)
                    {
                        myBoat.OwnedBy = numberInput;
                        continueBool = true;
                        break;
                    }
                }

                if (memberNumber != numberInput)
                {
                    V.ShowErrorMessage("Kunde inte hitta medlem med det numret, försök igen", true);
                }

            } while (!continueBool);

            do// läger till längd på båt i båt objektet
            {
                continueBool = false;

                V.ShowMessage("Fyll i längden på båten");           // Längd
                try
                {
                    numberInput = Int32.Parse(Console.ReadLine());
                }

                catch
                {
                    V.ShowErrorMessage("Fel format, försök igen", true);
                    continueBool = false;
                }


                myBoat.Length = numberInput;
                continueBool = true;

            } while (!continueBool);

            do// läger till typ av båt i båt objektet
            {
                continueBool = false;

                V.ShowMessage("Vad är det för typ av båt?\n");           // Typ
                V.ShowMessage("1. Motorbåt\n");
                V.ShowMessage("2. Segelbåt\n");
                V.ShowMessage("3. Roddbåt\n");
                V.ShowMessage("4. Kanot\n");
                V.ShowMessage("5. Övrigt\n");
                try
                {
                    numberInput = int.Parse(Console.ReadLine());

                    continueBool = true;
                }

                catch
                {
                    V.ShowErrorMessage("Fel format, försök igen", true);
                    continueBool = false;
                }

                if (numberInput >= 1 && numberInput <= 5)
                {
                }

                else
                {
                    V.ShowErrorMessage("Fel format, försök igen", true);
                    continueBool = false;
                }

                myBoat.Type = numberInput;


            } while (!continueBool);

            return myBoat;
        }

        /// <summary>
        /// Visar medlemmar
        /// </summary>
        /// <param name="array">Array med alla registrerade medlemmar.</param>
        /// <param name="pos">Position i arrayen för medlemmen som ska visas.</param>
        /// <param name="boatsArray">Array med all registrerade båtar.</param>
        /// <param name="boatsPos">Position i arrayen för medlemmens båt.</param>
        public void MembersList(Member[] array, int pos, Boat[] boatsArray, int boatsPos)        // Visar medlemmar
        {

            ViewMessage V = new ViewMessage();

            Console.Clear();
            string printString;
            bool continueBool = false;
            int input = 0;


            if (pos == 0)
            {
                V.ShowErrorMessage("Inga medlemmar är inlagda ännu. Försök igen senare", true);
            }


            else
            {

                do
                {
                    V.ShowMessage("fyll i 1 för Kompakt eller 2 för fullständig lista");         // val av lista
                    try
                    {
                        input = int.Parse(Console.ReadLine());
                    }

                    catch
                    {
                        V.ShowErrorMessage("Fel format, försök igen", true);
                        continueBool = false;
                    }

                    if (input == 1)// visar kompakt lista
                    {
                        int boatNumber = 0;
                        int memberNumber = 0;
                        int numberOfBoats = 0;

                        for (int i = 0; i < pos; i++)
                        {
                            memberNumber = array[i].MemberNumber;

                            for (int y = 0; y < boatsPos; y++)
                            {
                                boatNumber = boatsArray[y].OwnedBy;

                                if (memberNumber == boatNumber)
                                {
                                    numberOfBoats++;
                                }
                            }

                            array[i].NumberOfBoats = numberOfBoats;
                            printString = array[i].PrintCompact();

                            V.ShowMessage(printString);
                            numberOfBoats = 0;
                        }

                        continueBool = true;

                        V.ShowMessage("Tryck på valfri tangent för att fortsätta");
                        Console.ReadKey();
                    }

                    if (input == 2)// visar fullständig lista
                    {
                        int boatNumber = 0;
                        int memberNumber = 0;
                        int numberOfBoats = 0;
                        string boatInfo = "";

                        for (int i = 0; i < pos; i++)
                        {
                            memberNumber = array[i].MemberNumber;

                            for (int y = 0; y < boatsPos; y++)
                            {
                                boatNumber = boatsArray[y].OwnedBy;

                                if (memberNumber == boatNumber)
                                {
                                    boatInfo += boatsArray[y].BoatToString();

                                    numberOfBoats++;
                                }
                            }

                            array[i].NumberOfBoats = numberOfBoats;
                            printString = array[i].PrintCompact();
                            V.ShowMessage(printString);
                            V.ShowMessage(boatInfo);
                            numberOfBoats = 0;
                            boatInfo = "";
                        }

                        continueBool = true;

                        V.ShowMessage("Tryck på valfri tangent för att fortsätta");
                        Console.ReadKey();
                    }

                } while (!continueBool);


            }
        }

        /// <summary>
        /// Kontrollerar så att numret som matas in är en registrerad medlem.
        /// </summary>
        /// <returns>Ett korrekt nummer från listan med registrerade medlemmar.</returns>
        public int GetMemberNumber()//kontrollerar att nummert som mattats in är en medlem som finns i listan 
        {
            ViewMessage V = new ViewMessage();
            int numberInput = 0;



            V.ShowMessage("fyll i Numret som representerar medlemmen i denordning\n som medlemmar visas i medlemslistan!!!");
            V.ShowMessage("(fyll i 0 för att avbryta)\n");

            do
            {
                try
                {
                    numberInput = int.Parse(Console.ReadLine());
                }

                catch
                {
                    V.ShowErrorMessage("Hittade inte medlem, försök igen", true);
                }

                if (numberInput == 0)
                {
                    break;
                }

                else if (numberInput > 0 && numberInput <= 100)
                {
                    numberInput = numberInput - 1;
                    return numberInput;
                }

                else
                {
                    V.ShowErrorMessage("Hittade inte medlem, försök igen", true);
                }
            } while (numberInput != 0);

            return -1;
        }

        /// <summary>
        /// Kontrollerar så att numret som matas in är en registrerad båt.
        /// </summary>
        /// <param name="array">Array med alla registrerade medlemmar.</param>
        /// <param name="pos">Position i arrayen för medlemmen som ska visas.</param>
        /// <param name="boatsArray">Array med all registrerade båtar.</param>
        /// <param name="boatsPos">Position i arrayen för medlemmens båt.</param>
        /// <returns></returns>
        public int GetBoatNumber(Member[] array, int pos, Boat[] boatArray, int boatPos)// kontrollerar att medlemsnummeret som fylts i är en medlem som finns i listan 
        {
            ViewMessage V = new ViewMessage();

            int numberInput = 0;



            V.ShowMessage("Sök båt via medlemsnummer\n");
            V.ShowMessage("(0 för att avbryta)\n");

            do
            {
                try
                {
                    numberInput = int.Parse(Console.ReadLine());
                }

                catch
                {
                    V.ShowErrorMessage("Hittade inte båten, försök igen.", true);
                }

                if (numberInput == 0)
                {
                    break;
                }

                else if (numberInput > 0 && numberInput <= 999999999)
                {
                    int memberNumber = 0;
                    int matches = 0;
                    int[] positions = new int[50];
                    int x = 0;

                    for (int i = 0; i < boatPos; i++)
                    {
                        memberNumber = boatArray[i].OwnedBy;

                        if (numberInput == memberNumber)
                        {
                            matches++;
                            positions[x] = i;
                            x++;
                        }
                    }

                    if (matches == 0)
                    {
                        V.ShowErrorMessage("Hittade inte båt, försök igen.", true);
                        V.ShowMessage("Tryck på valfri tangent för att fortsätta");
                        Console.ReadKey();
                        return -1;
                    }

                    else if (matches == 1)
                    {
                        numberInput = positions[0]; // eller?
                    }

                    else if (matches > 1)
                    {
                        int positionOfBoat = 0;
                        int boatType = 0;
                        int boatLength = 0;
                        string type = "";

                        V.ShowMessage("Vilken båt letar du efter? \n");
                        for (int i = 0; i < matches; i++)
                        {
                            positionOfBoat = positions[i];
                            boatType = boatArray[positionOfBoat].Type;
                            boatLength = boatArray[positionOfBoat].Length;

                            if (boatType == 1)
                            {
                                type = "Motorbåt";
                            }

                            if (boatType == 2)
                            {
                                type = "Segelbåt";
                            }

                            if (boatType == 3)
                            {
                                type = "Roddbåt";
                            }

                            if (boatType == 4)
                            {
                                type = "Kanot";
                            }

                            if (boatType == 5)
                            {
                                type = "Övrigt";
                            }

                            //Console.WriteLine("Båt nummer {0}", i + 1);
                            int T = i + 1;
                            V.ShowMessage("Båt nummer " + T);
                            V.ShowMessage("Båttyp:\t" + type);
                            V.ShowMessage("Båtlängd:\t" + boatLength + "\n");
                        }

                        try
                        {
                            numberInput = int.Parse(Console.ReadLine());
                        }

                        catch
                        {
                            V.ShowErrorMessage("Hittade inte båten, försök igen.", true);
                        }

                        if (numberInput < 0 || numberInput > matches)
                        {
                            V.ShowErrorMessage("Fel båt, försök igen.", true);
                            return -1;
                        }

                        else
                        {
                            numberInput = numberInput - 1;
                            return numberInput;
                        }
                    }



                    return numberInput;
                }

                else
                {
                    V.ShowErrorMessage("Hittade inte båt, försök igen.", true);
                }
            } while (numberInput != 0);

            return -1;
        }               // Få numret

        //  Returnerar resultat av båtar som den inlästa medlemmen äger. Returneras i en array där [0] är medlemsnumret och [1] är båtnumret i medlemmens "båt-array".
        /// <summary>
        /// Läser in ett medlemsnummer.
        /// </summary>
        /// <param name="array"></param>
        /// <param name="pos"></param>
        /// <param name="boatArray"></param>
        /// <param name="boatPos"></param>
        /// <returns></returns>
        public int[] GetBoatNumberAndMemberNumber(Member[] array, int pos, Boat[] boatArray, int boatPos) 
        {
            ViewMessage V = new ViewMessage();

            int numberInput = 0;
            int memberNumberForBoat = -1;
            int[] returnArray = new int[2];
            returnArray[0] = -1;
            returnArray[1] = -1;

            V.ShowMessage("Sök båt via medlemsnummer\n");
            V.ShowMessage("(0 för att avbryta)\n");

            do
            {
                try
                {
                    numberInput = int.Parse(Console.ReadLine());
                    memberNumberForBoat = numberInput;
                }

                catch
                {
                    V.ShowErrorMessage("Hittade inte båten, försök igen.", true);
                }

                if (numberInput == 0)
                {
                    break;
                }

                else if (numberInput > 0 && numberInput <= 999999999)
                {
                    int memberNumber = 0;
                    int matches = 0;
                    int[] positions = new int[50];
                    int x = 0;

                    for (int i = 0; i < boatPos; i++)
                    {
                        memberNumber = boatArray[i].OwnedBy;

                        if (numberInput == memberNumber)
                        {
                            matches++;
                            positions[x] = i;
                            x++;
                        }
                    }

                    if (matches == 0)
                    {
                        V.ShowErrorMessage("Hittade inte båt, försök igen.", true);
                        V.ShowMessage("Tryck på valfri tangent för att fortsätta");
                        Console.ReadKey();
                        return returnArray;
                    }

                    else if (matches == 1)
                    {
                        returnArray[0] = numberInput;
                        returnArray[1] = 0;
                    }

                    else if (matches > 1)
                    {
                        int positionOfBoat = 0;
                        int boatType = 0;
                        int boatLength = 0;
                        string type = "";

                        V.ShowMessage("Vilken båt letar du efter? \n");
                        for (int i = 0; i < matches; i++)
                        {
                            positionOfBoat = positions[i];
                            boatType = boatArray[positionOfBoat].Type;
                            boatLength = boatArray[positionOfBoat].Length;

                            if (boatType == 1)
                            {
                                type = "Motorbåt";
                            }

                            if (boatType == 2)
                            {
                                type = "Segelbåt";
                            }

                            if (boatType == 3)
                            {
                                type = "Roddbåt";
                            }

                            if (boatType == 4)
                            {
                                type = "Kanot";
                            }

                            if (boatType == 5)
                            {
                                type = "Övrigt";
                            }

                            int T = i + 1;
                            V.ShowMessage("Båt nummer " + T);
                            V.ShowMessage("Båttyp:\t" + type);
                            V.ShowMessage("Båtlängd:\t" + boatLength + "\n");
                        }

                        try
                        {
                            numberInput = int.Parse(Console.ReadLine());
                        }

                        catch
                        {
                            V.ShowErrorMessage("Hittade inte båten, försök igen.", true);
                        }

                        if (numberInput < 0 || numberInput > matches)
                        {
                            V.ShowErrorMessage("Fel båt, försök igen.", true);
                            return returnArray;
                        }

                        else
                        {
                            numberInput = numberInput - 1;
                            returnArray[0] = memberNumberForBoat;
                            returnArray[1] = numberInput;
                            return returnArray;
                        }
                    }
                    return returnArray;
                }

                else
                {
                    V.ShowErrorMessage("Hittade inte båt, försök igen.", true);
                }
            } while (numberInput != 0);

            return returnArray;
        }   

        public void GetMemberData(Member[] array, int pos)// Visar specifik information om en medlem  
        {

            ViewMessage V = new ViewMessage();

            string printString;

            printString = array[pos].PrintFull();

            V.ShowMessage(printString);

            V.ShowMessage("Tryck på valfri tangent för att fortsätta");
            Console.ReadKey();

        }

        public void EditMemberData(Member[] array, int pos)
        {
            ViewMessage V = new ViewMessage();

            int input = 10;
            int number;
            string nameInput;
            string name;

            do
            {
                string printIt = array[pos].PrintFull();
                Console.Clear();
                V.ShowMessage(printIt + "\n");

                V.ShowMessage("Vilken egenskap vill du ändra? \n");
                V.ShowMessage("0. Avsluta\n1. Förnamn\n2. Efternamn\n3. Personnummer\n4. Medlemsnummer");

                try
                {
                    input = int.Parse(Console.ReadLine());
                }

                catch
                {
                    V.ShowErrorMessage("Inte ett tal mellan 0-4, försök igen.", true);
                }

                if (input < 0 && input > 4)
                {
                    V.ShowErrorMessage("Inte ett tal mellan 0-4, försök igen.", true);
                }

                else if (input == 1)
                {
                    name = array[pos].FirstName;

                    V.ShowMessage("Vad vill du byta " + name + " till?");

                    nameInput = Console.ReadLine();

                    array[pos].FirstName = nameInput;
                }

                else if (input == 2)
                {
                    name = array[pos].LastName;

                    V.ShowMessage("Vad vill du byta " + name + " till?");

                    nameInput = Console.ReadLine();

                    array[pos].LastName = nameInput;
                }

                else if (input == 3)
                {
                    bool continueBool = false;
                    number = array[pos].SocialSecurityNumber;

                    do
                    {
                        continueBool = false;
                        int numberInput = 0;

                        V.ShowMessage("Vad vill du byta " + number + " till?");

                        try
                        {
                            numberInput = int.Parse(Console.ReadLine());
                            continueBool = true;
                        }

                        catch
                        {
                            V.ShowErrorMessage("Fel format, försök igen.", true);
                            continueBool = false;
                        }

                        if (numberInput < 0)
                        {
                            V.ShowErrorMessage("Måste vara större än 0, försök igen.", true);
                            continueBool = false;
                        }



                        array[pos].SocialSecurityNumber = numberInput;


                    } while (!continueBool);
                }

                else if (input == 4)
                {
                    bool continueBool = false;
                    number = array[pos].MemberNumber;

                    do
                    {
                        continueBool = false;
                        int numberInput = 0;

                        V.ShowMessage("Vad vill du byta " + number + " till?");           // Medlemsnummer

                        try
                        {
                            numberInput = int.Parse(Console.ReadLine());
                            continueBool = true;
                        }

                        catch
                        {
                            V.ShowErrorMessage("Fel format, försök igen.", true);
                            continueBool = false;
                        }

                        if (numberInput < 0)
                        {
                            V.ShowErrorMessage("Måste vara större än 0, försök igen.", true);
                            continueBool = false;
                        }

                        array[pos].MemberNumber = numberInput;


                    } while (!continueBool);
                }

            } while (input != 0);
        }       // redigerar medlem

        public void EditBoatData(Boat[] array, int pos)
        {
            ViewMessage V = new ViewMessage();
            int input = 10;
            int length = 0;
            int type = 0;
            int owner = 0;

            do
            {
                string printIt = array[pos].BoatToStringFull();
                Console.Clear();
                V.ShowMessage(printIt + "\n");

                V.ShowMessage("Vilken egenskap vill du ändra? \n");
                V.ShowMessage("0. Avsluta\n1. Båttyp\n2. Längd\n3. Ägare\n");

                try
                {
                    input = int.Parse(Console.ReadLine());
                }
                catch
                {
                    V.ShowErrorMessage("Inte ett tal mellan 0-3, försök igen.", true);
                }

                if (input < 0 && input > 3)
                {
                    V.ShowErrorMessage("Inte ett tal mellan 0-3, försök igen.", true);
                }
                else if (input == 1)
                {
                    type = array[pos].Type;

                    V.ShowMessage("1. Motorbåt\n");
                    V.ShowMessage("2. Segelbåt\n");
                    V.ShowMessage("3. Roddbåt\n");
                    V.ShowMessage("4. Kanot\n");
                    V.ShowMessage("5. Övrigt\n");

                    V.ShowMessage("Vad vill du byta " + type + " till?");

                    try
                    {
                        input = int.Parse(Console.ReadLine());
                    }
                    catch
                    {
                        V.ShowErrorMessage("Inte ett tal mellan 1-5, försök igen.", true);
                    }

                    if (input < 1 && input > 5)
                    {
                        V.ShowErrorMessage("Inte ett tal mellan 1-5, försök igen.", true);
                    }
                    else if (input >= 1 && input <= 5)
                    {
                        array[pos].Type = input;
                    }
                }
                else if (input == 2)
                {
                    length = array[pos].Length;
                    bool continueBool = false;

                    do
                    {
                        continueBool = false;
                        int numberInput = 0;

                        V.ShowMessage("Vad vill du byta " + length + " till?");
                        try
                        {
                            numberInput = int.Parse(Console.ReadLine());
                            continueBool = true;
                        }
                        catch
                        {
                            V.ShowErrorMessage("Fel format, försök igen.", true);
                            continueBool = false;
                        }

                        array[pos].Length = numberInput;

                    } while (!continueBool);
                }
                else if (input == 3)
                {
                    owner = array[pos].OwnedBy;
                    bool continueBool2 = false;

                    do
                    {
                        continueBool2 = false;
                        int numberInput = 0;

                        V.ShowMessage("Vad vill du byta " + owner + " till?");
                        try
                        {
                            numberInput = int.Parse(Console.ReadLine());
                            continueBool2 = true;
                        }
                        catch
                        {
                            V.ShowErrorMessage("Fel format, försök igen.", true);
                            continueBool2 = false;
                        }

                        array[pos].OwnedBy = numberInput;

                    } while (!continueBool2);
                }
            } while (input != 0);
        }       // redigerar båt

        public Boat[] DeleteBoat(Boat[] boats, int pos)
        {
            var newList = boats.ToList();
            newList.RemoveAt(pos);
            Boat[] newArray = new Boat[100];
            newArray = newList.ToArray();

            return newArray;
        }           // Ta bort båt

        public Member[] DeleteMember(Member[] array, int pos)       // Ta bort medlem
        {

            var newList = array.ToList();
            newList.RemoveAt(pos);
            Member[] newArray = new Member[100];
            newArray = newList.ToArray();

            return newArray;
        }

        public Member[] readMembers(Member[] members)
        {
            string membersPath = path + "/../../Members.txt";
            string[] lines = System.IO.File.ReadAllLines(membersPath);
            int count = 1;
            int i = 0;

            foreach (string line in lines)// foreach som löper igenom tex filen och skapar ett objekt som tilldelas värden från textfilen
            {
                if (count == 1)
                {
                    members[i] = new Member();
                    members[i].FirstName = line;
                }

                if (count == 2)
                {
                    members[i].LastName = line;
                }

                if (count == 3)
                {
                    members[i].MemberNumber = Int32.Parse(line);
                }

                if (count == 4)
                {
                    count = 0;
                    members[i].SocialSecurityNumber = Int32.Parse(line);
                    i++;
                }
                count++;
            }



            return members;
        }   // Läser in medlemmar från textfil

        public Boat[] readBoats(Boat[] boats)
        {
            string boatPath = path + "/../../Boats.txt";
            string[] lines = System.IO.File.ReadAllLines(boatPath);
            int count = 1;
            int i = 0;

            foreach (string line in lines)// foreach som löper igenom tex filen och skapar ett objekt som tilldelas värden från textfilen
            {
                if (count == 1)
                {
                    boats[i] = new Boat();
                    boats[i].OwnedBy = Int32.Parse(line);
                }

                if (count == 2)
                {
                    boats[i].Length = Int32.Parse(line);
                }

                if (count == 3)
                {
                    count = 0;
                    boats[i].Type = Int32.Parse(line);
                    i++;
                }
                count++;
            }
            return boats;
        }       // Läser in båtar från textfil

        // Läser in vart en vald båt ligger i "båt-arrayen".
        /// <summary>
        /// 
        /// </summary>
        /// <param name="members">Array med Medlem-objekt.</param>
        /// <param name="memberNumber">MedlemsIDet.</param>
        /// <param name="boats">Array med Båt-objekt.</param>
        /// <param name="boatPos">Numret av vald båt i arrayen.</param>
        /// <returns></returns>
        public int readBoatsPosByMember(Member[] array, int memberNumber, Boat[] boats, int boatPos)
        {
            string boatPath = path + "/../../Boats.txt";
            string[] lines = System.IO.File.ReadAllLines(boatPath);
            int countLineCount = 1;
            int countMembersBoats = 0;
            int countBoatsPosInArray = 0;
            int pos = 0;

            int countMembPos = 0;
            foreach (Member memb in array)
            {
                if (memb.MemberNumber == memberNumber)
                {
                    pos = countMembPos;
                    break;
                }
                countMembPos++;
            }

            foreach (string line in lines)// foreach som löper igenom tex filen och skapar ett objekt som tilldelas värden från textfilen
            {
                if (Int32.Parse(line) == array[pos].MemberNumber && countLineCount == 1)
                {
                    if (countMembersBoats == boatPos)
                    {
                        return countBoatsPosInArray;
                    }

                    countMembersBoats++;
                }

                if (countLineCount == 1)
                {
                    countBoatsPosInArray++;
                }

                if (countLineCount == 3)
                {
                    countLineCount = 0;
                }

                countLineCount++;


                
            }
            return countBoatsPosInArray;
        }

        /// <summary>
        /// Spara medlemmar.
        /// </summary>
        /// <param name="members">Array med Medlem-objekt.</param>
        /// <param name="numberOfMembers">Antal medlemmar.</param>
        public void saveMembers(Member[] members, int numberOfMembers)
        {
            int count = 1;
            int i = 0;
            int x = 0;
            string[] lines = new string[400];

            numberOfMembers = numberOfMembers * 4;              // vi får fram de rader vi behöver genom att ta antal medlemmar gånger antal rader med information

            for (int y = 0; y < numberOfMembers; y++)
            {
                {
                    if (count == 1)
                    {
                        lines[x] = members[i].FirstName;
                    }

                    if (count == 2)
                    {
                        lines[x] = members[i].LastName;
                    }

                    if (count == 3)
                    {
                        lines[x] = members[i].MemberNumber.ToString();
                    }

                    if (count == 4)
                    {
                        count = 0;
                        lines[x] = members[i].SocialSecurityNumber.ToString();
                        i++;
                    }
                    count++;
                    x++;
                }
            }

            lines = lines.Where(h => !string.IsNullOrEmpty(h)).ToArray();

       
            string membersPath = path + "/../../Members.txt";
            System.IO.File.WriteAllLines(membersPath, lines);

        }       // Sparar medlemmar till textfil

        /// <summary>
        /// Sparar båtar.
        /// </summary>
        /// <param name="boats">Array med Båt-objekt.</param>
        /// <param name="numberOfBoats">Antal båtar.</param>
        public void saveBoats(Boat[] boats, int numberOfBoats)
        {
            int count = 1;
            int i = 0;
            int x = 0;
            string[] lines = new string[400];

            numberOfBoats = numberOfBoats * 3;  // vi får fram de rader vi behöver genom att ta antal medlems båtar gånger antal rader med information

            for (int y = 0; y < numberOfBoats; y++)
            {
                {
                    if (count == 1)
                    {
                        lines[x] = boats[i].OwnedBy.ToString();
                    }

                    if (count == 2)
                    {
                        lines[x] = boats[i].Length.ToString();
                    }

                    if (count == 3)
                    {
                        count = 0;
                        lines[x] = boats[i].Type.ToString();
                        i++;
                    }
                    count++;
                    x++;
                }
            }

            lines = lines.Where(h => !string.IsNullOrEmpty(h)).ToArray();

            string boatsPath = path + "/../../Boats.txt";
            System.IO.File.WriteAllLines(boatsPath, lines);

        }       // Sparar båtar till textfil


    }
}
