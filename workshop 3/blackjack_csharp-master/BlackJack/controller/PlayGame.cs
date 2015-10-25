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
        private view.IView v_view;
        private model.Game m_game;

        public PlayGame(model.Game a_game, IView a_view)// / Konstruktor som sätter inparametrarna till sina privata objekt 
        {
            v_view = a_view;
            m_game = a_game;
        }

        public bool Play()
        {
            GameRender();

            if (m_game.IsGameOver())
            {
                v_view.DisplayGameOver(m_game.IsDealerWinner());
            }

            int input = v_view.GetInput();

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
        }

        // Fryser spelet i 2 sekunder innan varje kort delas ut.
        public void PauseBeforeShowingCard()
        {
            System.Threading.Thread.Sleep(2000);
            GameRender();
        }

        // Renderar ut ett välkomstmeddelande. Även dealern och spelarens händer och resultat.
        public void GameRender()
        {

            v_view.DisplayWelcomeMessage();

            v_view.DisplayDealerHand(m_game.GetDealerHand(), m_game.GetDealerScore());
            v_view.DisplayPlayerHand(m_game.GetPlayerHand(), m_game.GetPlayerScore());
        }


    }
}
