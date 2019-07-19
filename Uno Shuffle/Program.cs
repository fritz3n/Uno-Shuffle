using System;

namespace Uno_Shuffle
{
    class Program
    {
        static void Main(string[] args)
        {
            var cards = Simulator.GetDeck();

            foreach(Card card in cards)
            {
                Console.WriteLine(card);
            }

            cards = Simulator.GetRandomDeck();

            foreach (Card card in cards)
            {
                Console.WriteLine(card);
            }

            Console.ReadLine();
        }
    }
}
