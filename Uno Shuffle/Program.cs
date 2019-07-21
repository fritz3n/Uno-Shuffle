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


            Console.ReadLine();
        }
    }
}
