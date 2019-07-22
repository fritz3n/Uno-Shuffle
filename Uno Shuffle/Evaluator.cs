using System;
using System.Collections.Generic;
using System.Text;

namespace Uno_Shuffle
{
    static class Evaluator
    {
        public static EvaluationResult Evaluate(Card[] cards)
        {
            int avgDist = 0;
            for(int i = 0; i < cards.Length; i++)
            {
                //reset the distance
                int dist = 1;
                while (!cards[(i + dist) % cards.Length].CanBePlaceOn(cards[i]))
                {
                    dist++;
                }

                //add the distance to the average
                avgDist += dist / cards.Length;
            }
            return new EvaluationResult()
            {
                averageDistanceBetweenLayableCards = avgDist
            };
        }

    }

    public struct EvaluationResult
    {
        public float averageDistanceBetweenLayableCards;
    }
}
