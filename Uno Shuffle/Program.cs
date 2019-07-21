using System;
using Uno_Shuffle.Simulation;

namespace Uno_Shuffle
{
    class Program
    {
        static void Main(string[] args)
        {
            
            StatisticsManager manager = new StatisticsManager(1, new CardDistributor(), 4);
            manager.GenerateDecks();

            foreach (var deck in manager.Decks) {
                Console.WriteLine();
                foreach (Card card in deck.Value)
                {
                    card.WriteToConsole();
                }
            }


            var cards = Game.GetRandomDeck();

            foreach (Card card in cards)
            {
                Console.WriteLine(card);
            }

            Console.WriteLine("----");

            Game game = new CardDistributor().Distribute(4);

            game.Simulate();

            foreach (Card card in game.Discard)
            {
                Console.WriteLine(card);
            }
            Console.ReadLine();
        }
    }
}
