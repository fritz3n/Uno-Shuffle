using System.Collections.Generic;

namespace Uno_Shuffle
{
    public interface ICardDistributor
    {
        Game Distribute(int players, Card[] cards = null);
    }
}