using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackJack.model
{
    class Dealer : Player// Dealer ärver från Player
    {
        private Deck m_deck = null;
        private const int g_maxScore = 21;

        private rules.INewGameStrategy m_newGameRule;
        private rules.IHitStrategy m_hitRule;// ändrat 

        private rules.WinStrategy m_winRule;// Lag till

        private List<CardObserver> m_card;// Instans av ett list-objekt

        public Dealer(rules.RulesFactory a_rulesFactory)// regler som används 
        {
            m_newGameRule = a_rulesFactory.GetNewGameRule();
            m_hitRule = a_rulesFactory.GetHitRule();
            m_winRule = a_rulesFactory.GetWinRule();

            m_card = new List<CardObserver>();// skapar lista
        }

        public void Register(CardObserver a_card)// lag til CardObserver
        {
            m_card.Add(a_card);// läger till i listan
        }

        public void DrawCardAndShowCard(Player a_player, bool result)// lag till
        {
            Card c;
            c = m_deck.GetCard();
            c.Show(result);
            a_player.DealCard(c);

            foreach (var l in m_card)//lopar igneom listan
            {
                l.PauseBeforeShowingCard();// pusar spelet för späning 
            }
        }

        public bool NewGame(Player a_player)
        {
            if (m_deck == null || IsGameOver())
            {
                m_deck = new Deck();
                ClearHand();
                a_player.ClearHand();
                return m_newGameRule.NewGame(m_deck, this, a_player);   
            }
            return false;
        }

        public bool Hit(Player a_player)
        {
            if (m_deck != null && a_player.CalcScore() < g_maxScore && !IsGameOver())
            {
               /* Card c;
                c = m_deck.GetCard();
                c.Show(true);
                a_player.DealCard(c);*/

                DrawCardAndShowCard(a_player, true);//lag till 

                return true;
            }
            return false;
        }

        public int GetMaxScore()// lagt till 
        {
            return g_maxScore;
        }

       public bool IsDealerWinner(Player a_player)
        {
            return m_winRule.IsDealerWinner(a_player, this);
        
            /*if (a_player.CalcScore() > g_maxScore)
            {
                return true;
            }
            else if (CalcScore() > g_maxScore)
            {
                return false;
            }
            return CalcScore() >= a_player.CalcScore();*/
        }

        public bool IsGameOver()
        {
            if (m_deck != null && /*CalcScore() >= g_hitLimit*/ m_hitRule.DoHit(this) != true)
            {
                return true;
            }
            return false;
        }

        public void Stand()// funktion gör så man stanar i spelet 
        {
            ShowHand();

            while (m_hitRule.DoHit(this))
            {
                DrawCardAndShowCard(this, true);
            }
        }

   

    }
}
