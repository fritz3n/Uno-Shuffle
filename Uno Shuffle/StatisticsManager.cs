using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Uno_Shuffle
{
    public class StatisticsManager
    {
        //The number of iterations each algorithm will be evaluated
        public int Interations { get; }
        public ICardDistributor Distributor { get; }
        //The number of players to be simulated
        public int Players { get; }
        public ConcurrentDictionary<int,Card[]> Decks { get; }

        public StatisticsManager(int interations, ICardDistributor distributor, int players = 7)
        {
            Interations = interations;
            Distributor = distributor;
            Players = players;
        }

        public void GenerateDecks()
        {
            Parallel.For(0, this.Interations, (i) => 
            {
                this.Decks.TryAdd(i, this.Distributor.Distribute(this.Players).Simulate());
            });
        }


    }
}
