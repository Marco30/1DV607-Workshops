using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackJack.model.rules
{
    class HitSoft17 : IHitStrategy//ny regel
    {
        private const int g_hitLimit = 17;

        public bool DoHit(model.Player a_dealer)
        {
            return a_dealer.HitSoftCalcScore() < g_hitLimit;
        }
    }
}
