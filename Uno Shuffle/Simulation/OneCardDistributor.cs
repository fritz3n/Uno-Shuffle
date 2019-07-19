using System;
using System.Collections.Generic;
using System.Text;

namespace Uno_Shuffle.Simulation
{
    class OneCardDistributor : ICardDistributor
    {
        public OneCardDistributor(int cardsPerPlayer = 7)
        {
            CardsPerPlayer = cardsPerPlayer;
        }

        public int CardsPerPlayer { get; }

        public Game Distribute(int players, Card[] cards = null)
        {
            cards = cards ?? Game.GetRandomDeck();

            Stack<Card> deck = new Stack<Card>(cards);

            List<Card>[] hands = new List<Card>[players];

            for (int i = 0; i < players; i++)
            {
                hands[i] = new List<Card>();
            }

            for(int card = 0; card < CardsPerPlayer; card++)
            {
                for(int player = 0; player < players; player++)
                {
                    hands[player].Add(deck.Pop());
                }
            }

            return new Game(players, deck, hands);
        }
    }
}
