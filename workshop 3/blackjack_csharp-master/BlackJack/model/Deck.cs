using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackJack.model
{
    class Deck
    {
        List<Card> m_cards;// skapar en lista som ska ha korten som ska användas i spelet  

        public Deck()// Lägger till alla kort som ska anävndas i spelet, färg och värde, sen blandar den korten
        {
            m_cards = new List<Card>();

            for (int colorIx = 0; colorIx < (int)Card.Color.Count; colorIx++)
            {
                for (int valueIx = 0; valueIx < (int)Card.Value.Count; valueIx++)
                {
                    Card c = new Card((Card.Color)colorIx, (Card.Value)valueIx);
                    AddCard(c);
                }
            }

            Shuffle();
        }

        public Card GetCard()
        {
            Card c = m_cards.First();
            m_cards.RemoveAt(0);
            return c;
        }

        public void AddCard(Card a_c)// Lägger till ett kort till som ska anändas i spelet
        {
            m_cards.Add(a_c);
        }

        public IEnumerable<Card> GetCards()// Kapslar in medhjälp av IEnumerables kortleken 
        {
            return m_cards.Cast<Card>();
        }

        private void Shuffle() // Blandar korten i en slumpmässig ordning
        {
            Random rnd = new Random();

            for (int i = 0; i < 1017; i++)
            {
                int index = rnd.Next() % m_cards.Count;
                Card c = m_cards.ElementAt(index);
                m_cards.RemoveAt(index);
                m_cards.Add(c);
            }
        }
    }
}
