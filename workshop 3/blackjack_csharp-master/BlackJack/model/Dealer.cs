using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackJack.model
{
    class Dealer : Player
    {
        private Deck m_deck = null;
        private const int g_maxScore = 21;
        private rules.INewGameStrategy m_newGameRule;
        private rules.IHitStrategy m_hitRule;
        private rules.IWinStrategy m_winRule;
        private List<CardObserver> m_card;// Instans av ett list-objekt

        public Dealer(rules.RulesFactory a_rulesFactory)
        {
            m_newGameRule = a_rulesFactory.GetNewGameRule();
            m_hitRule = a_rulesFactory.GetHitRule();
            m_winRule = a_rulesFactory.GetWinRule();
            m_card = new List<CardObserver>();
        }

        // Lagt till ny CardObserver.
        public void Register(CardObserver a_card)
        {
            m_card.Add(a_card);
        }

        // Tar ett kort, sparar, visar upp det.
        public void DrawCardAndShowCard(Player a_player, bool result)
        {
            Card c = m_deck.GetCard();
            c.Show(result);
            a_player.DealCard(c);

            foreach (var l in m_card)
            {
                l.PauseBeforeShowingCard();
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
                DrawCardAndShowCard(a_player, true);
                return true;
            }
            return false;
        }

        public int GetMaxScore()
        {
            return g_maxScore;
        }

       public bool IsDealerWinner(Player a_player)
        {
            return m_winRule.IsDealerWinner(a_player, this);
        }

        public bool IsGameOver()
        {
            if (m_deck != null && m_hitRule.DoHit(this) != true)
            {
                return true;
            }
            return false;
        }

        public void Stand()
        {
            ShowHand();

            while (m_hitRule.DoHit(this))
            {
                DrawCardAndShowCard(this, true);
            }
        }
    }
}
