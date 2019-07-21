using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Uno_Shuffle
{
    public class Game
    {
        public Stack<Card> Deck { get; private set; }
        public List<Card> Discard { get; private set; }
        public List<Card>[] Hands { get; private set; }

        public int Players { get; private set; }

        public Game(int players, Stack<Card> deck, List<Card>[] hands)
        {
            Players = players;
            Deck = deck;
            Hands = hands;

            Discard = new List<Card>();
        }

        static readonly Random random = new Random();

        public static Card[] GetDeck()
        {// Creates an ordered deck to the specifications of https://exceptionnotfound.net/modeling-practice-uno-in-c-sharp-part-one-rules-assumptions-cards/
            Card[] cards = new Card[108];

            for (int color = 1; color < 5; color++)
            {
                for (int suit = 0; suit < 25; suit++)
                {
                    // Per specification, we want N0 to be the least common N-Card
                    int curSuit = 12 - (suit % 13);

                    cards[(color - 1) * 25 + suit] = new Card((Suit)curSuit, (Color)color);
                }
            }

            // add special cards

            for (int i = 0; i < 8; i++)
            {
                cards[100 + i] = new Card(13 + (Suit)(i / 4), Color.Nigro);
            }

            return cards;
        }

        public static Card[] GetRandomDeck()
        {
            List<Card> sorted = GetDeck().ToList();

            Card[] shuffled = new Card[sorted.Count];

            for (int i = 0; i < shuffled.Length; i++)
            {
                int index = random.Next(sorted.Count);

                shuffled[i] = sorted[index];

                sorted.RemoveAt(index);
            }

            return shuffled;
        }

        public Card[] Simulate()
        {
            throw new NotImplementedException();
        }
    }
}
