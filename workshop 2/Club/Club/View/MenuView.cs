using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Club.Model;
using Club.Controller;

namespace Club.View
{
    class MenuView
    {


        public void ViewMenu()      //fukntion som skriver utt meny
        {
           
            Console.WriteLine("--------------------------------------");
            Console.WriteLine("-             Båtklubb               -");
            Console.WriteLine("--------------------------------------\n");
            Console.WriteLine("1. Registrera en medlem \n");
            Console.WriteLine("2. Medlemslista\n");
            Console.WriteLine("3. Information om en enskild medlem\n");
            Console.WriteLine("4. Redigera en medlem \n");
            Console.WriteLine("5. Ta bort en medlem \n");
            Console.WriteLine("6. Registrera en båt \n");
            Console.WriteLine("7. Ta bort en båt \n");
            Console.WriteLine("8. Redigera en båt \n");
            Console.WriteLine("0. Avsluta och sparar alla ändringar som gjorts\n");
            Console.WriteLine("-------------------------------------\n");
            Console.WriteLine("\nFyll i menyval 0-8:");
        }


    }
}
