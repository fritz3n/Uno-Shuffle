using System;
using Uno_Shuffle.Simulation;

namespace Uno_Shuffle
{
    class Program
    {
        static void Main(string[] args)
        {
            var cards = Game.GetDeck();

            foreach(Card card in cards)
            {
                Console.WriteLine(card);
            }

            cards = Game.GetRandomDeck();

            foreach (Card card in cards)
            {
                Console.WriteLine(card);
            }

            Console.WriteLine("----");

            Game game = new OneCardDistributor().Distribute(4);

            game.Simulate();

            foreach (Card card in game.Discard)
            {
                Console.WriteLine(card);
            }
            Console.ReadLine();
        }
    }
}
