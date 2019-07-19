using System.Collections.Generic;

namespace Uno_Shuffle
{
    interface ICardDistributor
    {
        Game Distribute(int players, Card[] cards = null);
    }
}