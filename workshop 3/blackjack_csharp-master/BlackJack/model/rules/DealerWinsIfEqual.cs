using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackJack.model.rules
{
    class DealerWinsIfEqual : IWinStrategy
    {
        public bool IsDealerWinner(Player a_player, Dealer a_dealer)
        {
            if (a_player.CalcScore() > a_dealer.GetMaxScore())
            {
                return true;
            }
            else if (a_dealer.CalcScore() > a_dealer.GetMaxScore())
            {
                return false;
            }
            return a_dealer.CalcScore() >= a_player.CalcScore();
        }
    }
}
