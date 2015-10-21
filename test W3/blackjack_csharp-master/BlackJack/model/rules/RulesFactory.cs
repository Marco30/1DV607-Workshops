using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackJack.model.rules
{
    class RulesFactory
    {
        public IHitStrategy GetHitRule()
        {
            //return new BasicHitStrategy();
            return new HitSoft17();
        }
        public WinStrategy GetWinRule()// lag till
        {
            return new HowIsWinner();
        }

        public INewGameStrategy GetNewGameRule()
        {
            return new AmericanNewGameStrategy();
        }
    }
}
