﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackJack.model.rules// lag till
{
    interface WinStrategy
    {
        bool IsDealerWinner(model.Player a_player, model.Dealer a_dealer);
    }
}
