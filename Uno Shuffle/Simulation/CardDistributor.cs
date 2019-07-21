using System;
using System.Collections.Generic;
using System.Text;

namespace Uno_Shuffle.Simulation
{
    public class CardDistributor : ICardDistributor
    {
        public int CardsPerGive { get; private set; }
        public CardDistributor(int cardsPerPlayer = 7, int cardsPerGive = 1)
        {
            CardsPerPlayer = cardsPerPlayer;
            CardsPerGive = cardsPerGive;
        }

        public int CardsPerPlayer { get; }

        public Game Distribute(int players, Card[] cards = null)
        {
            //get a Stack of Cards from a Deck of cards to easily draw cards
            cards = cards ?? Game.GetRandomDeck();
            Stack<Card> deck = new Stack<Card>(cards);

            //init the "players" hands
            List<Card>[] hands = new List<Card>[players];
            for (int i = 0; i < players; i++)
            {
                hands[i] = new List<Card>();
            }

            int playersWithFullHands = 0;
            int currPlayer = 0;
            while(playersWithFullHands < players)
            {
                int cardsToBeGiven = CardsPerGive;
                while (cardsToBeGiven >= 0 && hands[currPlayer].Count != CardsPerPlayer) //maybe off by one
                {
                    hands[currPlayer].Add(deck.Pop());
                    cardsToBeGiven--;
                }
                if(hands[currPlayer].Count == CardsPerPlayer)
                {
                    playersWithFullHands++;
                }
                currPlayer = (currPlayer + 1) % players;
            }

            return new Game(players, deck, hands);
        }
    }
}
