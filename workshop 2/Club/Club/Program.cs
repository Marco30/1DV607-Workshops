using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Club.View;
using Club.Controller;
namespace Club
{
    class Program
    {
        static void Main(string[] args)
        {
            // Kör igång applikationen.
            MenuChoice menu = new MenuChoice();
            menu.GetMenuChoice();
        }
    }
}
