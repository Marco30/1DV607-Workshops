using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackJack
{
    class Program
    {
        static void Main(string[] args)
        {
            
            model.Game g = new model.Game();// // ändrat til CardObserver
            view.IView v = new view.SimpleView(); // new view.SwedishView();
            controller.PlayGame ctrl = new controller.PlayGame(g, v); // ändrat 

            g.SetPlayerObserver(ctrl);

            while (ctrl.Play()) ;// ändrat 

            /*controller.PlayGame ctrl = new controller.PlayGame();
            while (ctrl.Play(g, v));*/
        }
    }
}
