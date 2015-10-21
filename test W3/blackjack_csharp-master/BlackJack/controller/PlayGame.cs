using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlackJack.model;
using BlackJack.view;
namespace BlackJack.controller
{
    class PlayGame : CardObserver
    {

        private view.IView v_view; // Instans av ett IViewobjekt, interface SimleView/SwedishVie
        private model.Game m_game; // Instans av ett Gameobjekt

        public PlayGame(model.Game a_game, IView a_view)// / Konstruktor som sätter inparametrarna till sina privata objekt 
        {
            v_view = a_view;
            m_game = a_game;
        }

        /*public bool Play(model.Game a_game, view.IView a_view)*/
        public bool Play()
        {
            /*a_view.DisplayWelcomeMessage();

            a_view.DisplayDealerHand(a_game.GetDealerHand(), a_game.GetDealerScore());
            a_view.DisplayPlayerHand(a_game.GetPlayerHand(), a_game.GetPlayerScore());*/
            GameRender();

            if (m_game.IsGameOver())
            {
                v_view.DisplayGameOver(m_game.IsDealerWinner());
            }

            int input = v_view.GetInput();

            //---------------------------------- lag till 

            if (v_view.NewGame(input))
            {
                m_game.NewGame();
            }

            else if (v_view.Stand(input))
            {
                m_game.Stand();
            }

            else if (v_view.Hit(input))
            {
                m_game.Hit();
            }

            return v_view.Quit(input) == false;

            //------------------------------------

            /* if (input == 'p')
             {
                 a_game.NewGame();
             }
             else if (input == 'h')
             {
                 a_game.Hit();
             }
             else if (input == 's')
             {
                 a_game.Stand();
             }

             return input != 'q';*/
        }


        public void PauseBeforeShowingCard()// funktion som pausar spelet innan nästa kort delas ut
        {
            System.Threading.Thread.Sleep(2000);
            GameRender();
        }

        public void GameRender() // Renderar ut välkommstmeddelande och spelarnas händer och score
        {

            v_view.DisplayWelcomeMessage();

            v_view.DisplayDealerHand(m_game.GetDealerHand(), m_game.GetDealerScore());
            v_view.DisplayPlayerHand(m_game.GetPlayerHand(), m_game.GetPlayerScore());
        }


    }
}
