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
            // new BasicHitStrategy();
            return new HitSoft17();
        }
        public IWinStrategy GetWinRule()
        {
            // new PlayerWinsIfEqual();
            return new DealerWinsIfEqual();
        }

        public INewGameStrategy GetNewGameRule()
        {
            // new InternationalNewGameStrategy();
            return new AmericanNewGameStrategy();
        }
    }
}
