﻿using System;
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

        public const int NumberOfCardsPerGame = 108;

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
            Card[] cards = new Card[NumberOfCardsPerGame];

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

        public void Simulate()
        {
            //TODO: Add Plus2 Accumulation -- probably will be hacky
            Card currentCard;

            currentCard = Deck.Pop();
            try
            {

                int turnPlayer = 0;
                int direction = 1;

                void AdvancePlayer(int modifier = 1)
                {
                    turnPlayer += direction * modifier;

                    // I don´t wanna do bounds checking every time i modify player! just do it here
                    turnPlayer = turnPlayer % Players;
                    if (turnPlayer < 0)
                        turnPlayer = 0;
                }

                Card? GetFittingCard()
                {
                    Card? specialOption = null;

                    foreach (var card in Hands[turnPlayer])
                    {
                        if (card.Color == Color.Nigro)
                        {
                            specialOption = card;
                            continue;
                        }

                        if (card.CanBePlaceOn(currentCard))
                            return Hands[turnPlayer].Pop(card);
                    }

                    return specialOption == null ? null : (Card?)Hands[turnPlayer].Pop(specialOption);
                }

                bool LayFittingCard()
                {
                    Card? possibleCard;

                    if ((possibleCard = GetFittingCard()) != null)
                    {
                        Card card = possibleCard.Value;
                        switch (card.Suit)
                        {
                            case Suit.Skip:
                                AdvancePlayer(2);
                                break;

                            case Suit.Reverse:
                                direction *= -1;
                                AdvancePlayer();
                                break;

                            case Suit.Plus2:

                                int cards = 2;

                                AdvancePlayer();

                                while (true)
                                {
                                    // Ceck if the player has a plus2 card and lay it onto the discard pile
                                    if (Hands[turnPlayer].Exists(c => c.Suit == Suit.Plus2))
                                    {
                                        Discard.Add(Hands[turnPlayer].Pop(Hands[turnPlayer].Where(c => c.Suit == Suit.Plus2).First()));
                                    }
                                    else // the player cant block; take cards
                                    {
                                        for (int i = 0; i < cards; i++)
                                        {
                                            Hands[turnPlayer].Add(Deck.Pop());
                                        }
                                        AdvancePlayer();
                                        break;
                                    }

                                    AdvancePlayer();
                                }
                                break;

                            case Suit.Plus4:
                                AdvancePlayer();

                                Hands[turnPlayer].Add(Deck.Pop());
                                Hands[turnPlayer].Add(Deck.Pop());
                                Hands[turnPlayer].Add(Deck.Pop());
                                Hands[turnPlayer].Add(Deck.Pop());

                                AdvancePlayer();
                                break;

                            case Suit.Pick:
                                // Find out which color is the most in the players hand and set the picking card to that
                                currentCard.Color = Hands[turnPlayer].Where(c => c.Color != Color.Nigro).GroupBy(c => c.Color).Aggregate((g1, g2) => g1.Count() > g2.Count() ? g1 : g2).First().Color;
                                break;

                            default:
                                AdvancePlayer();
                                break;
                        }

                        Discard.Add(currentCard);
                        currentCard = card;

                        return true;
                    }

                    return false;
                }


                while (Deck.Count > 0)
                {


                    if (Hands[turnPlayer].Count == 0)
                    {
                        turnPlayer = (turnPlayer + 1) % Players; //wrap around so we don't get issues when the last player has no cards left.

                        //Check if all players are out of cards
                        bool allEmpty = true;
                        foreach(List<Card> hand in Hands)
                            {
                            if (hand.Count != 0)
                            {
                                allEmpty = false;
                                break;
                            }
                        }
                        if (allEmpty) { break; }
                        continue;
                    }



                    if (LayFittingCard())
                        continue;

                    Hands[turnPlayer].Add(Deck.Pop());

                    if (!LayFittingCard())
                        AdvancePlayer();

                    if (Hands.Sum(h => h.Count) == 0)
                        break;
                }
            }
            catch (InvalidOperationException) { }
            Discard.Add(currentCard);
        }

        public Card[] GetState()
        {
            int i = 0;
            Card[] cards = new Card[NumberOfCardsPerGame];
            foreach(Card card in Discard)
            {
                cards[i] = card;
                i++;
            }
            foreach (List<Card> playerHand in Hands)
            {
                foreach(Card card in playerHand)
                {
                    cards[i] = card;
                    i++;
                }
            }

            //Add the Deck in reversed order
            int j = 1;
            while (Deck.Count != 0)
            {
                cards[NumberOfCardsPerGame - j] = Deck.Pop();
                j++;
            }

            return cards;
        }
    }
}
