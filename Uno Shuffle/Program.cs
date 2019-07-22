using System;
using System.Diagnostics;
using Uno_Shuffle.Simulation;

namespace Uno_Shuffle
{
    class Program
    {
        static void Main(string[] args)
        {

            Stopwatch sw = new Stopwatch();
            sw.Start();
            StatisticsManager manager = new StatisticsManager(100000, new CardDistributor(), 4);
            manager.GenerateDecks();

            /*foreach (var deck in manager.Decks) {
                Console.WriteLine();
                foreach (Card card in deck.Value)
                {
                    //card.WriteToConsole();
                }
            }*/
            sw.Stop();
            Console.WriteLine(sw.Elapsed);


            Console.ReadLine();
        }
    }
}
