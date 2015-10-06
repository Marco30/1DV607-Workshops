using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Club.View
{
    class ViewMessage
    {

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

        public void ShowMessage(string message)// funktion som visar Meddelande
        {
      
                Console.BackgroundColor = ConsoleColor.DarkGreen;
            

            Console.WriteLine(message);
            Console.ResetColor();
        }

    }
}
